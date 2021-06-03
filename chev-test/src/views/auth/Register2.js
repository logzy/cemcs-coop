import Loader from '../../components/ui/loader/loader.vue'
import axios from 'axios'
export default {   
    components: {
        Loader
    },
    data() {
    return {
        loader: false,
        checked: false,
        options: {
          format: 'YYYY-MM-DD',
          useCurrent: false,
          showClear: true,
          showClose: true,
        },
      nameState:null,
      state:null,
      selectedLoan: "",
      show: true,
      notify: 0,
      savings: {
        minimumSavingsAmount: 0
      },
      form: {
        fname: "",
        mname: "",
        lname: "",
        title: null,
        sex: null,
        marital: null,        
        DoB: new Date(),
        empDate : new Date(),
        RetDate: new Date(),
        email: "",
        Personalemail:"",
        location: null,
        workPhone: "",
        mobileNo: "",
        address1: "",
        address2: "",
        state: null,
        country: null,
        createdBy: null,
        LastModifiedBy: "",    
        EmployeeNo: "",
        MemberType: "",
        minSaving: "",
      },
      login: {        
        userName:"",
        userType:0,
        userTypeCategory:0
      },
      maritals: [{ text: "Select Status", value: null }, "Single", "Married"],
      gender: [{ text: "Select Gender", value: null }, "Female", "Male"],
      members: [
        { text: "Regular", value: 1, disabled: true },
        { text: "Retiree", value: 2, disabled: true },
        { text: "Expatriate", value: 3, disabled: true }
      ],
      locations: [
        { text: "Select Location", value: null },
        { text: "Lagos", value: "Lagos" },
        { text: "Escravos", value: "Escravos" },
      ],
      states: [
        { text: "Select State", value: null },
        { text: "Lagos", value: 1 },
        { text: "FCT", value: 2 },
        { text: "Ondo", value: 3 },
        { text: "Imo", value: 4 }
      ],
      countrys: [
        { text: "Select Country", value: null },
        { text: "Nigeria", value: "Nigeria" },
        { text: "Ghana", value: "Ghana" },
        { text: "USA", value: "USA" }
      ],
    };
  },
  mounted () {
        document.title = "Register | Chevron CEMCS Corporate"
    },
  async created() {
    await this.loginDetails();
    await this.initMinSavings();
  },
  methods: {

    numberFormat(value) {
        this.points = Number(value.replace(/\D/g, ''))
        return value =='0.00' ? '' : this.points.toLocaleString();
      },
    
    async loginDetails() {        
     await axios
        .get(`${process.env.VUE_APP_API_URL}/Members/loginId`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.login = response.data;
        })
        .catch((error) => {
          this.$bvToast.toast(error.message ,{
                title: "Warning",
                variant: "warning",
                solid: true,
                autoHideDelay: 5000
            })
        });
    },

    makeToast(variant = null) {
      this.notify++;
      this.$bvToast.toast(`Member Added`, {
        title: "Successful",
        variant: variant,
        solid: true,
        autoHideDelay: 5000
      });
    },     
    checkFormValidity() {
      const valid = this.$refs.form.checkValidity();
      if ((parseFloat(this.form.minSaving.replace(/,/g, ''))) < this.savings.minimumSavingsAmount){
        return;
      }
      // this.nameState = valid;
      return valid;
    },
    onReset(event) {
      event.preventDefault();

      this.nameState = null;
      this.form.fname = "";
      this.form.mname = "";
      this.form.lname = "";
      this.form.title = "";
      this.form.sex = null;
      this.form.marital = null;
      this.form.DoB = "";
      this.form.email = "";
      this.form.workPhone = "";
      this.form.mobileNo = "";
      this.form.address1 = "";
      this.form.address2 = "";
      this.form.state = "";
      this.form.country = "";
      this.form.EmployeeNo = "";
      this.form.MemberType = null;

      this.show = false;
      this.$nextTick(() => {
        this.show = true;
      });
    },
    async onSubmit(event) {
      event.preventDefault();
      // Exit when the form isn't valid
      this.loader = true     
      if (!this.checkFormValidity()) {
        return;
      }

      let RetDate = null
      if (this.login.userTypeCategory == 2){
        RetDate = this.form.RetDate
      }

      let rawData = {
        EmployeeNumber: this.login.userName,
        MemberType: parseInt(this.login.userTypeCategory),
        EmploymentDate: this.form.empDate,
        RetirementDate : RetDate,
        Location: this.form.location,
        SavingsAmount: parseInt(this.form.minSaving.replace(/,/g, '')),
        Person: {
          FirstName: this.form.fname,
          LastName: this.form.lname,
          MiddleName: this.form.mname,
          Sex: this.form.sex,
          MaritalStatus: this.form.marital,
          DateOfBirth: this.form.DoB,
          Email: this.login.email,
          Personalemail:this.form.Personalemail,
          WorkPhone: this.form.workPhone,
          MobileNumber: this.form.mobileNo,
          Address1: this.form.address1,
          Address2: this.form.address2,
          StateId: this.form.state,
          Country: this.form.country
        }
      };
      rawData = JSON.stringify(rawData);
      await axios
        .post(`${process.env.VUE_APP_API_URL}/Members/Register`, rawData, {
          headers: {
            "Content-Type": "application/json;charset=utf-8",
             Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        })
        .then((response) => {
            this.loader = true     
          this.rawData = response.data;
          this.makeToast(`success`);
          if (this.form.MemberType != 2){       
            window.history.length > this.$router.
            push(`/payment`);
          }
           this.$router.push(`/`);
        })
        .catch((error) => {
          this.loader = false
          this.$bvToast.toast(error.response.data ,{
                title: "Warning",
                variant: "warning",
                solid: true,
                autoHideDelay: 5000
            })
        });
        await axios
        .post(`${process.env.VUE_APP_API_URL}/Members/CreatePending`, rawData, {
          headers: {
            "Content-Type": "application/json;charset=utf-8",
             Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        })
        .then(() =>{  this.makeToast(`success`);    })


    },
    async initMinSavings() {        
     await axios
        .get(`${process.env.VUE_APP_API_URL}/MinSavings/${this.login.userTypeCategory}`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          }
        })
        .then(response => {
          this.savings = response.data;
        })
        .catch(error => {
         this.$bvToast.toast(error.response ,{
                title: "Warning",
                variant: "warning",
                solid: true,
                autoHideDelay: 5000
            })
        });
    },
    
  },
};
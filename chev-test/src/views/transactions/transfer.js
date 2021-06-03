import axios from 'axios';
import Loader from '../../components/ui/loader/loader.vue'
import Status from '../../components/ui/state/state.vue'
import createNumberMask from 'text-mask-addons/dist/createNumberMask';
  const currencyMask = createNumberMask({
    prefix: '',
    allowDecimal: true,
    decimalLimit :2,
    includeThousandsSeparator: true,
    allowNegative: false,
  });
export default {
  components: {
    Loader,
    Status
},
  data() {
    return {
      modeUnselected:"mode-unselected",
      modeSelected:"mode-selected",
      modeActive: "mode-active",
      modeDefault: "mode-default",
      state: 'failed',
      status: false,
      message: '',
      loader: false,
      show: true,
      mask: currencyMask,
      notify: 0,
      options: {
          format: 'YYYY-MM-DD',
          useCurrent: false,
          showClear: true,
          showClose: true,
        },
      user: {},
      balance:{},
      employeeNumber: '',
      employeeName: '',
      name: '',
      amount:'',
      sourceAccount: 1,
      destAccount: 2,
      effectiveDate: new Date(),
      accountTypes: [
        { text: "---Select Account Type---", value: null, disabled: true},
        { value: 1, text: "Savings", },
        { value: 2, text: "Special Deposit" }
      ],
      currentMode: 1
    };
  },
  async mounted() {
    await this.initUser();
  },
  computed: {
    memberBalance() {
      return this.$store.state.balance
    }
  },
  created() {
    this.$store.dispatch('memberBalance');
  },
  methods: {
   async getGuarantorInfo(){
     this.loader = true
   let guarantor = {            
      EmployeeNumber: this.employeeNumber
    };    
await axios
  .post(`${process.env.VUE_APP_API_URL}/Members/EmpNumber`, guarantor, {
    headers: {
      "Content-Type": "application/json;charset=utf-8",
      Authorization: `Bearer ${localStorage.getItem('token')}`
    }
  })
  .then(response => {
    this.loader = false
    this.employeeName= response.data.data.person.firstName + ' ' + response.data.data.person.lastName
  })
  .catch(error => {
    this.loader = false
    this.$bvToast.toast(error.message, {
          title: "Error",
          variant: "danger",
          solid: true,
          autoHideDelay: 5000
      });
  });
},
    toggleMode(mode){
    if(mode == 1){
     this.currentMode = 1
    }
    else if(mode == 2){
      this.currentMode = 2
    }
    },
    closeModal(){
      this.status = false
  },

    numberFormat(value) {
        this.points = Number(value.replace(/\D/g, ''))
        return value == '0.00' ? '' : this.points.toLocaleString();
      },
      
    makeToast(variant = null) {
      this.notify++;
      this.$bvToast.toast(`Transfered`, {
        title: "Successful",
        variant: variant,
        solid: true,
        autoHideDelay: 5000
      });
    },

    onReset(event) {
      event.preventDefault()
      // Reset our form values
      this.form.amount = ''
      this.form.account = null
      // Trick to reset/clear native browser form validation state
      this.show = false
      this.$nextTick(() => {
        this.show = true
      })
    },

    clearForm(){
     this.sourceAccount = null
     this.destAccount = null
     this.effectiveDate =  new Date()
    this.amount = ''
    },

    async onSubmit(event) {
      event.preventDefault()
         this.loader = true;
      let rawData = {
        MemberId: this.user.data.id,
        SourceSavingsType: this.sourceAccount,
        DestinationSavingsType: this.destAccount,
        EffectiveDate: this.effectiveDate,
        Amount:parseInt(this.amount.replace(/,/g, '')),
      };
      rawData = JSON.stringify(rawData);
      await axios.post(
          `${process.env.VUE_APP_API_URL}/SavingDepositTransactions/Transfer`,
          rawData,
          {
            headers: {
              "Content-Type": "application/json;charset=utf-8",
              Authorization: `Bearer ${localStorage.getItem('token')}`
            }
          }
        )
        .then((response) => {          
          if(response.data.success == true){
            this.message = 'The transfer operation was successful'
            this.loader = false;
            this.state = 'success';
            this.status = true;
            this.clearForm();
         let memberType = localStorage.getItem('userType')
         this.rawData = response.data;
         this.makeToast(`success`);
         if (memberType != 2) {
             this.$router.push(`/payment}`);
         }
         }
         else{
          this.message = response.data.errors[0].errorMessage
          this.loader = false;
          this.status = true;
          this.state = 'failed';
          this.clearForm();
         }
        })
        .catch(error => {
          this.clearForm();
          this.message = error.message
          this.loader = false;
          this.status = true;
          this.state = 'failed';
          this.$bvToast.toast(error.message, {
                title: "Error",
                variant: "danger",
                solid: true,
                autoHideDelay: 5000
            });
        });
    },

    async initUser() {
     const user= await axios.get(`${process.env.VUE_APP_API_URL}/Members/Usertype`, {
          headers: {
            'Content-Type': 'application/json;charset=utf-8',
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        })
          this.user = user.data;

    },
  },
};
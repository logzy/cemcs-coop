import Loader from '../../../components/ui/loader/loader.vue'
import Status from '../../../components/ui/state/state.vue'
// import axios from 'axios';
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
        buildR:0.155,
        contentR:0.65,
        generatorR:0.215,
        cashR:0.65,
        rentR:0.215,
        jewelriesR:3.5,
        lapR:4,
        liabilityR:0.1,
        totalR:null,
      user: {},
      employeeNumber: '',
      selected:'',
      effectiveDate: new Date(),
      bankcode:null,
      location:"",
      banks:[],
      name:{data:""},
      accountNumber:"",
      amount:'',
    };
  },
  methods: {
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
  
      this.account = ""
      this.location = ""
      this.effectiveDate= new Date()
      this.bankcode= null
      this.amount= ""
      this.accountNumber= ""
      this.account= 2
     },
     closeModal(){
      this.status = false
  },

    async onSubmit(event) {

      event.preventDefault()
       this.loader = true;
      // let rawData = {
      //    MemberId: this.user.data.id,
      //    DebitAccountType: this.account,
      //    MethodOfCollectionId: 1,
      //    CollectionLocation: this.location,
      //    EffectiveDate : this.effectiveDate,
      //    BankCode : this.bankcode,
      //    Amount : parseInt(this.amount.replace(/,/g, '')),
      //    AccountNumber : this.accountNumber,
      //    AccountName :this.name.data,
      // };
      // rawData = JSON.stringify(rawData);
      // await axios
      //   .post(
      //     `${process.env.VUE_APP_API_URL}/SavingDepositTransactions/Withdrawal`,
      //     rawData,
      //     {
      //       headers: {
      //         'Content-Type': 'application/json;charset=utf-8',
      //         Authorization: `Bearer ${localStorage.getItem('token')}`
      //       },
      //     }
      //   )
      //   .then((response) => {
          
      //     if(response.data.success == true){
            this.message = 'The Insurance was successful'
            this.loader = false;
            this.state = 'success';
            this.status = true;
            this.clearForm();
      //    let memberType = localStorage.getItem('userType')
      //    this.rawData = response.data;
      //    this.makeToast(`success`);
      //    if (memberType != 2) {
      //        this.$router.push(`/payment}`);
      //    }
      //    }
      //    else{
      //     this.clearForm();
      //     this.message = response.data.errors[0].errorMessage
      //     this.loader = false;
      //     this.status = true;
      //     this.state = 'failed';
      //    }
      //   })
      //   .catch(error => {
      //     this.clearForm();
      //     this.message = error.message
      //     this.loader = false;
      //     this.status = true;
      //     this.state = 'failed';
      //     this.$bvToast.toast(error.message, {
      //           title: "Error",
      //           variant: "danger",
      //           solid: true,
      //           autoHideDelay: 5000
      //       });
      //   });
    },    
  },
};
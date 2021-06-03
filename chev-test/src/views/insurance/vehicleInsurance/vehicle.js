import Loader from '../../../components/ui/loader/loader.vue'
import Status from '../../../components/ui/state/state.vue'
import axios from 'axios';
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
      user: {},
      employeeNumber: '',
      selected:'',
      effectiveDate: new Date(),
      bankcode:null,
      location:"",
      banks:[],
      name:{data:""},
      accountNumber:"",
      rate:'',
      valueOfVehicle:'',
      Subscription:null,
      purposeOfUse:null,
      vehicleType:null,
      paymentMode:null,
      tenor:null,
      proofOfOwner:null,
      proofOfOwer:null,
      tenors: [
        { text: "---Select Tenor---", value: null, disabled: true },
        { value: 1, text: "Q1" },
        { value: 2, text: "Q2" },
        { value: 2, text: "Q3" },
        { value: 2, text: "Q4" }

      ],
      proofOfOwnerShip: [
        { text: "---Select Proof---", value: null, disabled: true },
        { value: 1, text: "VIN " },
        { value: 2, text: "Model Number " }
      ],
      Subscriptions: [
        { text: "---Select Subscription Type---", value: null, disabled: true },
        { value: 1, text: "Third Party " },
        { value: 2, text: "Comprehension " }
      ],
      purposeOfUses: [
        { text: "---Select Purpose of Use---", value: null, disabled: true },
        { value: 1, text: "Commercial " },
        { value: 2, text: "Private " }
      ],
      vehicles: [
        { text: "---Select Type of Vehicle---", value: null, disabled: true },
        { value: 1, text: "Bus " },
        { value: 1, text: "Saloon " },
        { value: 1, text: "SUV " },
        { value: 2, text: "Truck " }
      ],
      paymentModes: [
        { text: "---Select Payment Mode---", value: null, disabled: true },
        { value: 1, text: "Cash " },
        { value: 1, text: "Cheque " },
        { value: 1, text: "Special Deposit " },
        { value: 2, text: "Transfer " }
      ],
    };
  },
  async mounted() {
    await this.getAllBanks();
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
    async getAllBanks() {        
        await axios
           .get(`${process.env.VUE_APP_API_URL}/banks/All`,{
             headers: {
               "Content-Type": "application/json;charset=utf-8"
             }
           })
           .then(response => {
             this.banks = response.data;
           })
           .catch(error => {
             this.$bvToast.toast(error, {
                   title: "Error",
                   variant: "danger",
                   solid: true,
                   autoHideDelay: 5000
               });
           });
       },
       async verifyAcc() {    
        this.loader = true; 
        let verifyData = {            
          destbankcode: this.bankcode,
          recipientaccount: this.accountNumber,
        }; 
        verifyData = JSON.stringify(verifyData);           
            await axios
      .post(`${process.env.VUE_APP_API_URL}/flutterwave/account/name`,verifyData, {
        headers: {
          "Content-Type": "application/json;charset=utf-8"
        }
      })
      .then(response => {
        this.loader = false;
        this.name = response.data;          
      })   
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
  
      this.rate = ""
      this.valueOfVehicle = ""
      this.tenor= null
      this.proofOfOwnerShip= null
      this.purposeOfUse= null
      this.Subscription= null
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
            this.message = 'The Vehicle Insurance was successful'
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
import axios from "axios";
import moment from 'moment'
import Loader from '../../components/ui/loader/loader.vue'
import Status from '../../components/ui/state/state.vue'
import FileUpload from '../../components/fileUpload.vue'


export default {
  name: "Home",
  components: {
    Loader,
    Status,
    FileUpload
},
  data() {
    return {
      // amountToword: parseFloat(this.loanAmount.replace(/,/g, '')) | NumbersToWords,
      file: '',
      currentFile: undefined,
      showPreview: false,
      imagePreview: '',
      progress: 0,
      show: true,
      state: 'failed',
      status: false,
      message: '',
      loader: false,
      checked: false,
      dismissSecs: 5,
      dismissCountDown: 0,
      showDismissibleAlert: false,
      selectedLoan: null,
      selectedRadio:"",
      errors: "",
      result : {
        errors:{}
      },
      errorMsg:"",
      
      id:0,
      Loanid:"",
      LoanId:"",
      MemberId: "",
      InterestRate:"",
        name:{data:""},
      guarant: {data:0},
      loanAmount:"",
      amountDesire:"",
      amountExpected: "",
      expectedLumpSum:null,
      minMonthlyRepayPeriod:"",
      garantorEmpNo : "",
      garantorName : "",
      accountName : "",      
      beneficiary : "",
      effectMonth: "",
      effectYear: moment.utc(new Date).format("YYYY"),     
      effectiveMonth: "",
      effectiveYear: moment.utc(new Date).format("YYYY"),
      form: {
        accountNumber:"",
        bankcode:"",
      },
      grant: 
        {
          guarantorNumber :{},
          guarantorName : {},
          guarantorEmail : {},      
       },
       Info: {
          EmployeeNumber:"",
          person:  {
          firstName : [],
          email : [],      
       }
       },
       grantData:[],
       LoanGuarantors:[],
      mType : "",
      items: [],
      details: [],
      mLoan:[],
      loan:{
        loanType: ""
      },
      banks:[],
      lumpSums: [
         { text: "---Select Lump Sum---", value: null },
        { value: 1, text: "Annual Rent Subsidy" },
        { value: 2, text: "Security Allowance" },
        { value: 3, text: "Generator Maintenance/ Diesel Allowance" },
        { value: 4, text: "Annual Productivity Bonus"}
      ], 
      Months: [
        { name: "January", value: 0 },
        { name: "February", value: 1 },
        { name: "March", value: 2 },
        { value: 3, name: "April" },
        { value: 4, name: "May" },
        { value: 5, name: "June"},
        { value: 6, name: "July" },
        { value: 7, name: "August" },
        { value: 8, name: "September" },
        { value: 9, name: "October"},
        { value: 10, name: "November" },
        { value: 11, name: "December"}
      ],
      guarantorArray: [],
      pastGuarantors:[],
      togglePastGuarantors: false,
      pastGuarantorCount:0
    };
    
  },

  async created() {
    this.$store.dispatch('memberDetails');
    await this.initialize();
    await this.getAllBanks();
    this.effectiveDate();
    await this.getPastGuarantors()
  },
  computed: {
  memberLogin() {
    return this.$store.state.member
  }
},

  methods: {
    closeModal(){
      this.status = false
  },
    
    FileUpload() {
      this.file = this.$refs.file.files[0];
      this.progress = 0;
      let reader  = new FileReader();
      reader.addEventListener("load", function () {
        this.showPreview = true;
        this.imagePreview = reader.result;
      }.bind(this), false);
      if( this.file ){
        if ( /\.(jpe?g|png|gif)$/i.test( this.file.name ) ) {
          /*Check for the required format(s)          */
          reader.readAsDataURL( this.file );
          console.log('fileName',JSON.stringify(this.file.name))
          event => { this.progress = Math.round((100 * event.loaded) / event.total)   }
        }
      }else {
        this.message = 'File format not supported'
        this.loader = false;
        this.state = 'warning';
        this.status = true;
      }
    },

  async selectGuarantor(gNo){
      let guarantor = {            
        EmployeeNumber: gNo
      }; 

      this.loader = true;
      
 await axios
    .post(`${process.env.VUE_APP_API_URL}/Members/EmpNumber`, guarantor, {
      headers: {
        "Content-Type": "application/json;charset=utf-8",
        Authorization: `Bearer ${localStorage.getItem('token')}`
      }
    })
    .then(response => {
      this.loader = false
     
      if(response.data.data != null) {

      if(this.pastGuarantorCount == 0 ){
        this.pastGuarantorCount = 1
        this.guarantorArray[0].guarantorNumber = gNo
      this.guarantorArray[0].guarantorName = response.data.data.person.firstName + ' ' + response.data.data.person.lastName
      this.guarantorArray[0].guarantorEmail = response.data.data.person.email
      }
      else if(this.pastGuarantorCount == 1){
        this.pastGuarantorCount = 2
        this.guarantorArray[1].guarantorNumber = gNo
        this.guarantorArray[1].guarantorName = response.data.data.person.firstName + ' ' + response.data.data.person.lastName
        this.guarantorArray[1].guarantorEmail = response.data.data.person.email
      }
      else if(this.pastGuarantorCount == 2){
        this.guarantorArray[2].guarantorNumber = gNo
        this.guarantorArray[2].guarantorName = response.data.data.person.firstName + ' ' + response.data.data.person.lastName
        this.guarantorArray[2].guarantorEmail = response.data.data.person.email
      }
      else { return false;}
      }
      else{
        this.$bvToast.toast("Invalid Guarantor", {
          title: "Error",
          variant: "danger",
          solid: true,
          autoHideDelay: 5000
      });
      }
     

    })
    .catch(error => {
      this.$bvToast.toast(error.message, {
            title: "Error",
            variant: "danger",
            solid: true,
            autoHideDelay: 5000
        });
    });
    },
    toggleGuarantors(){
      this.togglePastGuarantors ? this.togglePastGuarantors = false : this.togglePastGuarantors = true
    },
    async getPastGuarantors() {        
      await axios
          .get(`${process.env.VUE_APP_API_URL}/Loans/guarantor`,{
            headers: {
              "Content-Type": "application/json;charset=utf-8",
              Authorization: `Bearer ${localStorage.getItem('token')}`
            }
          })
          .then(response => {
            this.pastGuarantors = response.data.data
           console.log("g>>>",response.data)
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
    effectiveDate () {
      const current = new Date();
      const currentDate = current.getDate();      
      if (currentDate < 15) {
        return this.effectiveMonth = current.getMonth();
      }
      else {
       return  this.effectiveMonth = current.getMonth() +1;
      }
    },

    LumpSumDate(value) {
      const current = new Date();
      const currentYear = current.getFullYear();    
      if (value == 1) {
        if ( current.getMonth() > 0) {
          this.effectYear = currentYear+1;
      }else { this.effectYear = currentYear; }
        this.effectMonth = 0;
      }
      else if (value == 2) {
        this.effectMonth = 3;
        if ( current.getMonth() > 3) {
          this.effectYear = currentYear+1;
      }else { this.effectYear = currentYear; }
      }
      else if (value == 3) {
        this.effectMonth = 8;
        if ( current.getMonth() > 8) {
          this.effectYear = currentYear+1;
      }else { this.effectYear = currentYear; }
      }
      else if (value == 4) {
        this.effectMonth = 10;
        if ( current.getMonth() > 8) {
          this.effectYear = currentYear+1;
          console.log(this.effectYear)
      }else { this.effectYear = currentYear; }
      }else { this.effectMonth = "";}
    },
    
    numberFormat(value) {
        this.points = Number(value.replace(/\D/g, ''))
        return value == '0.00' ? '' : this.points.toLocaleString();
      },
      errorToast(toaster,variant = null, msg,append = false) {
      this.notify++;
      this.$bvToast.toast(msg, {
        title: 'Input Error',
        variant: variant,
        toaster: toaster,
        solid: true,
        autoHideDelay: 5000,
        appendToast: append
      });
    },    

    AmountValidation() {      
      let amount = parseFloat(this.loanAmount.replace(/,/g, ''))
      let expectedAmount = parseFloat(this.amountExpected.replace(/,/g, ''))
      let mAmountDesire = expectedAmount * 0.75

      if (this.mType == 3 || this.mType == 2) {
        if(amount < this.minLoanAmount && this.minLoanAmount != 0){
              this.showDismissibleAlert = !this.showDismissibleAlert;
              return this.errors = `Loan Amount must be Minimum of ₦ ${Number(this.minLoanAmount).toLocaleString()} only`
        }
        if(this.maxLoanAmount != 0 && amount > this.maxLoanAmount){
              this.showDismissibleAlert = !this.showDismissibleAlert;
              return this.errors = `Loan Amount must be Maximum of ₦ ${Number(this.maxLoanAmount).toLocaleString()} only`
        }
      } 
      if (this.mType == 1) {
        if (amount > mAmountDesire) {
          this.showDismissibleAlert = !this.showDismissibleAlert;
          return this.errors = `Enter a value less than or equal to 75% (${Number(mAmountDesire).toLocaleString()}) of Amount Expected ₦ ${Number(expectedAmount).toLocaleString()}`
        }  
      }
      return this.errors = "";
    },

    RepaymentValidation() {      
      // this.dismissCountDown = this.dismissSecs
      if (this.mType == 3 || this.mType == 2) {
        if (this.MinRepayPeriod != 0 && this.minMonthlyRepayPeriod < this.MinRepayPeriod) {
        this.showDismissibleAlert = !this.showDismissibleAlert;
          return this.errors = `Minimum Repayment Period supplied is not valid, enter a value greater than 
          ${this.MinRepayPeriod}, less than or equal to ${this.MaxRepayPeriod}` 
        }
        if (this.MinRepayPeriod != 0 && this.minMonthlyRepayPeriod > this.MaxRepayPeriod) {
        this.showDismissibleAlert = !this.showDismissibleAlert;
          return this.errors = `Maximum Repayment Period supplied is not valid, enter a value greater than
          ${this.MinRepayPeriod}, less than or equal to ${this.MaxRepayPeriod}`
        }
      }

    },

    async initialize() {        
     await axios
        .get(`${process.env.VUE_APP_API_URL}/LoanCongfig/Member/Type`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.items = response.data;
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
    
    //.............................................Start................................
    async getGuarantor() {
      this.guarantorArray = []
      let guarantor = {            
            MemberId: this.memberLogin.id,
            LoanId: this.details.loanId,
            LoanAmount: parseInt(this.loanAmount.replace(/,/g, ''))
          }; 
          guarantor = JSON.stringify(guarantor);           
            
     await axios.post(`${process.env.VUE_APP_API_URL}/LoanConfig/Guarantors/count`, guarantor, {
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.guarant = response.data;
          for (let index = 0; index < response.data.data; index++) {
            this.guarantorArray.push({
              id: index,
              guarantorNumber : 0,
              guarantorName : "",
              guarantorEmail : "", 
          });
          }
          console.log('guarant>>>>>>', response.data.data)
          console.log('guarantArray>>>>>>', this.guarantorArray)
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

      async getGuarantorInfo(gNo, index) {
        //  console.log("Gotten Number", gNo)
      let guarantor = {            
            EmployeeNumber: gNo
          };    
     await axios
        .post(`${process.env.VUE_APP_API_URL}/Members/EmpNumber`, guarantor, {
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.Info = response.data.data;
          console.log('gInfo>>>>>>>>>>>>', response.data.data)
          this.guarantorArray[index].guarantorName = response.data.data.person.firstName + ' ' + response.data.data.person.lastName
          this.guarantorArray[index].guarantorEmail = response.data.data.person.email

        })
        .catch(error => {
          this.$bvToast.toast(error.message, {
                title: "Error",
                variant: "danger",
                solid: true,
                autoHideDelay: 5000
            });
        });
    },



    //..........................................End Guarantor Info.............................
    async verifyAcc() {
          this.loader = true;
          let verifyData = {            
            destbankcode: this.form.bankcode,
            recipientaccount: this.form.accountNumber,
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

    async getConfig(selectedLoan) {        
      await axios
          .get(`${process.env.VUE_APP_API_URL}/loantype/amount/${selectedLoan}`,{
            headers: {
              "Content-Type": "application/json;charset=utf-8",
              Authorization: `Bearer ${localStorage.getItem('token')}`
            }
          })
          .then(response => {
            if(response.data.errorMessage != null){
              this.message = response.data.errorMessage
              this.loader = false;
              this.status = true;
              this.state = 'failed';
            }else {
              console.log(response.data)
              this.details = response.data;
              this.mType = response.data.loan.loanType;
              this.mLoanCode = response.data.loan.loanCode;
              this.minLoanAmount = response.data.minLoanAmount;
              this.maxLoanAmount = response.data.maxLoanAmount;
              this.MinRepayPeriod = response.data.minMonthlyRepayPeriod;
              this.MaxRepayPeriod = response.data.maxMonthlyRepayPeriod
            }
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

    async getLoanType(Loanid) {        
      await axios
          .get(`${process.env.VUE_APP_API_URL}/loans/${Loanid}`,{
            headers: {
              "Content-Type": "application/json;charset=utf-8",
              Authorization: `Bearer ${localStorage.getItem('token')}`
            }
          })
          .then(response => {
            this.mLoan = response.data;
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

    // Submit FormData
    async onSubmit(event) {
      event.preventDefault()
      let formData = new FormData();
      // this.file = this.$refs.file.files[0];
      console.log("ImageFile",JSON.stringify(this.file));

      if (this.file == "") {
        this.message = 'File upload cannot be empty'
        this.loader = false;
        this.state = 'warning';
        this.status = true;
      }
      // formData.append('FormFile', this.file, this.file.name);

      if (this.AmountValidation()) {
        return this.errors;
      }
      if (this.RepaymentValidation()) {
        return this.errors;
      }

      let Month = this.effectiveMonth
      let Year = parseInt(this.effectiveYear)
      let repay = parseInt(this.minMonthlyRepayPeriod)      
      if (this.mType === 1) {
        Month = this.effectMonth
        Year = parseInt(this.effectYear)
        repay = 0
      }

      // let rawData = {
        // LoanId = this.details.loanId,
        // MemberId: this.memberLogin.id,
        // InterestRate: this.details.intrestRate,
        // LoanAmount: parseInt(this.loanAmount.replace(/,/g, '')),
        // RepaymntPeriod: repay,
        // EffectiveMonth: Month,
        // EffectiveYear: Year,
        // BankCode: this.form.bankcode,
        // MethodOfCollection: 2,
        // AccountNumber: this.form.accountNumber,
        // AccountName: this.name.data,
        // LoanGuarantors: this.guarantorArray,
        // FormFile: formData

      formData.append('LoanId', this.details.loanId);
      formData.append('MemberId', this.memberLogin.id);
      formData.append('InterestRate', this.details.intrestRate);
      formData.append('LoanAmount', parseInt(this.loanAmount.replace(/,/g, '')));
      formData.append('RepaymntPeriod', repay);
      formData.append('EffectiveMonth', Month);
      formData.append('EffectiveYear', Year);
      formData.append('BankCode', this.form.bankcode);
      formData.append('MethodOfCollection', 2);
      formData.append('AccountNumber', this.form.accountNumber);
      formData.append('AccountName', this.name.data);
      formData.append('LoanGuarantors', this.guarantorArray);
      formData.append('FormFile', this.$refs.file.files[0]);
      // }
      // rawData = JSON.stringify(rawData);
      /* Add the form data we need to submit */
      // formData.append('FormFile', this.file);
      // formData.append('rawData',rawData)
      await axios
        .post(
          `${process.env.VUE_APP_API_URL}/Loans/submit`,
          formData,
          {
            headers: {
              'Content-Type': 'multipart/form-data',
              Authorization: `Bearer ${localStorage.getItem('token')}`
            }
          }
        )
        .then((response) => {
                    
          if (response.data.data === null && response.data.success == true){
            this.showDismissibleAlert = !this.showDismissibleAlert;
            this.result = response.data;
            this.message = 'Loan Application was successful'
            this.loader = false;
            this.state = 'success';
            this.status = true;
            this.clearForm(); 
          }else {
            this.result = response.data;
            this.$emit("setParamResp", this.result);
            localStorage.setItem("LoanPlan",JSON.stringify(this.result));
            this.$router.push('/repayment_plan');
          }
        })
        .catch(error => {
          if (error.response.status == 401) {
            // auto logout if 401 response returned from api
            localStorage.removeItem('token')
            this.$router.push('/')
        }
            this.message = error.message
            this.loader = false;
            this.status = true;
            this.state = 'failed';
            this.$bvToast.toast(error, {
                title: "Error",
                variant: "danger",
                solid: true,
                autoHideDelay: 5000
            });
          });
    },

    onReset(event) {
        event.preventDefault()
        this.selectedLoan = null;
        this.details.loanId = "";
        this.details.memberTypeId = "";
        this.loanAmount = "";
        this.minMonthlyRepayPeriod = "";
        this.form.bankcode = "";
        this.form.accountNumber = "";
        this.name.data = "";
        this.grantData = null,
        this.errors = ""

        this.show = false
      this.$nextTick(() => {
        this.show = true
      })
    },

  },

}
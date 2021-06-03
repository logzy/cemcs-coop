<template>
  <div>      
    <div class="mt-2">
    <div class="header_background">Apply for a loan</div>
    </div>
    <div>
    <div class="row">
        <div class="col-md-12">
        <div>
            <div v-if="this.errors != ''">
            <!-- <b-alert show variant="danger" fade dismissible >
                {{this.errors}}
            </b-alert> -->
            <b-alert
                variant="danger"
                dismissible
                fade
                :show="showDismissibleAlert"
                @dismissed="showDismissibleAlert=false"
            >
                {{this.errors}}
            </b-alert>
            </div>
            <b-form class="form">
            <b-form-group
            class="pt-1 form-label"
            label-cols="4"
            label-cols-lg="4"
            label-size="sm"
            label="Loan Type"
            label-for="sm"
            >
            <b-form-select
                id="loantype"
                v-model="selectedLoan"
                @change="getConfig(selectedLoan)"
                required
            >                                               
                <b-form-select-option :value="null" disabled>
                    -- Select LoanType -- 
                </b-form-select-option>
                <b-form-select-option 
                v-for="item in items" 
                :value="item.loanId"
                :key="item.loan.id">
                    {{item.loan.name}} 
                </b-form-select-option>
            </b-form-select >                      
            <br/>
            </b-form-group>
            <div v-if="mType === 1">
                <b-row class="my-1 form-row mb-3 ">
                <b-col sm="4">
                <label class="pt-1 form-label" :for="lumpSum"
                    >Expected Lump Sum Type<code>*</code></label
                >
                </b-col>
                <b-col sm="8">
                <b-form-select
                    v-model="expectedLumpSum"
                    :options="lumpSums"
                    @change="LumpSumDate(expectedLumpSum)"
                ></b-form-select>
                </b-col>
                </b-row> 
                
                <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                    <label
                    class="pt-1 form-label"
                    :for="DateExpected"
                    >Date Expected MM/YY <code>*</code></label
                    >
                </b-col>
                <b-col sm="4">
                    <b-form-select
                    v-model="effectMonth" disabled
                    :options="Months"
                    value-field="value"
                    text-field="name"
                ></b-form-select></b-col>{{this.effectMonth}}
                    <b-col sm="3"><b-form-input
                    :id="`DateExpected`"
                    v-model="effectYear" disabled
                    type="text"
                    ></b-form-input>
                </b-col>
                </b-row>
                <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                    <label
                    class="pt-1 form-label"
                    :for="AmountExpected"
                    >Amount Expected <code>*</code></label
                    >
                </b-col>
                <b-col sm="8">
                    <b-form-input
                    :id="`AmountExpected`"
                    v-model="amountExpected"
                    :formatter="numberFormat"
                    type="text"
                    ></b-form-input>
                    <span v-if="amountExpected != ''"><code>
    {{parseFloat(this.amountExpected.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
                </b-col>
                </b-row>
                <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                    <label
                    class="pt-1 form-label"
                    :for="AmountDesire"
                    >Amount Desired <code>*</code></label
                    >
                </b-col>
                <b-col sm="8">
                    <b-form-input
                    :id="`AmountDesire`"
                    type="text"
                    v-model.trim="loanAmount"                                                                                
                    :formatter="numberFormat"
                    ></b-form-input>
                    <span v-if="loanAmount != ''"><code>
    {{parseFloat(this.loanAmount.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
                </b-col>
                </b-row>
            </div>
            <div v-if="mType  >= 2">
                <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                    <label
                    class="pt-1 form-label"
                    >Loan Amount <code>*</code></label
                    >
                </b-col>                                    
                <b-col sm="8">
                    <b-form-input
                    :id="`Amount`"
                    v-model.lazy.trim="loanAmount"
                    :max="maxLoanAmount" :min="minLoanAmount"
                    @blur="AmountValidation"
                    @change="getGuarantor"
                    type="text"                                                                               
                    :formatter="numberFormat"
                    ></b-form-input>
                    <span v-if="loanAmount != ''"><code>
    {{parseFloat(this.loanAmount.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
                </b-col>
                
                </b-row>
                <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                    <label
                    class="pt-1 form-label"
                    :for="DateExpected"
                    >Effective Date MM/YY <code>*</code></label
                    >
                </b-col>
                <b-col sm="3">
                    <b-form-select
                    v-model="this.effectiveMonth" disabled
                    :options="Months"
                    value-field="value"
                    text-field="name"
                ></b-form-select></b-col>{{this.effectiveMonth}}
                    <b-col sm="3"><b-form-input
                    :id="`DateExpected`"
                    v-model="effectiveYear" disabled
                    type="text"
                    ></b-form-input>
                </b-col>
                </b-row>
                
                <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                    <label
                    class="pt-1 form-label"
                    >Repayment Period (months)
                    <code>*</code></label
                    > 
                </b-col>                                                                       
                <b-col sm="8">
                    <b-form-input
                    :id="`payment period`"
                    v-model.lazy="minMonthlyRepayPeriod"
                    :max="this.MaxRepayPeriod" :min="this.MinRepayPeriod"
                    type="number"
                    @blur="RepaymentValidation" 
                    ></b-form-input>
                </b-col>
                </b-row>

                <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                    <label class="pt-1 form-label" :for="Rate"
                    >Rate (%) <code>*</code></label
                    >
                </b-col>
                <b-col sm="8">
                    <b-form-input
                    disabled
                    :id="`Rate`"
                    v-model="details.intrestRate"
                    type="text"
                    ></b-form-input>
                </b-col>
                </b-row>
            </div>

            <div v-if="(mType === 2 && loanAmount.length > 9) || (selectedLoan === 4) || (selectedLoan === 1)" >              
             
              
              <span v-if="guarant.data !== 0">
                 <div class="header_2">Guarantors</div>
                <div class="content" id='#gt' v-for="n in guarant.data" :key="n">          
                  <b-row class="my-1 form-row mb-3">
                  <b-col sm="4">
                      <label
                      class="pt-1 form-label"                                       
                      >Employee Number<code>*</code></label
                      >
                  </b-col>
                  <!-- {{ this.guarantorNumber }} -->
                  <b-col sm="8">
                      <b-form-input
                      :id="`guarantorNumber${n}`"
                      v-model.lazy.trim="grant.guarantorNumber[n]"
                      @blur="getGuarantorInfo(grant.guarantorNumber[n])"
                      type="number"
                      ></b-form-input>
                  </b-col>
                  </b-row>
                  <b-row class="my-1 form-row mb-3">
                  <b-col sm="4">
                      <label
                      class="pt-1 form-label"
                      >Name <code>*</code></label>
                  </b-col>
                  <b-col sm="8">
                      <b-form-input
                      v-bind:id="`name-${n}`"
                      v-model="grant.guarantorName[n]"
                      type="text"
                      ></b-form-input>
                  </b-col>
                  </b-row>
                  <b-row class="my-1 form-row mb-3">
                  <b-col sm="4">
                      <label
                      class="pt-1 form-label"
                      >Email Address  <code>*</code></label>
                  </b-col>
                  <b-col sm="8">
                      <b-form-input
                      v-bind:id="`name-${n}`"
                      v-model="grant.guarantorEmail[n]"
                      type="text"
                      @blur="addGrant(grant.guarantorNumber[n],grant.guarantorName[n],grant.guarantorEmail[n])"
                      ></b-form-input>
                  </b-col>
                  </b-row>
                </div>
              </span>
              <!-- <span v-if="guarant.data === 0">
                <div>
                  <strong> Not Enough Years of Experience for Loan the Request</strong><br/>
                </div>
              </span> -->
            </div>
            <div class="header_2">Method Of Collection</div>
            <b-row class="my-1 form-row mb-3 ">
                <b-col sm="4">
                <label class="pt-1 form-label" 
                    >Bank <code>*</code></label
                >
                </b-col>
                <b-col sm="8">
                <b-form-select
                    v-model="form.bankcode"
                    :options="banks"
                ></b-form-select>
                </b-col>
            </b-row>
            <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                <label class="pt-1 form-label" 
                    >Account Number <code>*</code></label
                >
                </b-col>
                <b-col sm="8">
                <b-form-input
                    id="`Account Number`"
                    v-model="form.accountNumber"
                    :max="10"
                    @blur="verifyAcc"
                    type="number"
                    :state="accNum"
                    aria-describedby="input-live-help input-live-feedback"
                    trim
                ></b-form-input>
                <b-form-invalid-feedback id="input-live-feedback">
                  Account Number must be 10 digits
                </b-form-invalid-feedback>
                </b-col>
            </b-row>
            <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                <label
                    class="pt-1 form-label"                                      
                    >Beneficiary Name <code>*</code></label
                >
                </b-col>
                <b-col sm="8">
                <b-form-input
                    disabled
                    id="`Beneficiary`"
                    v-model="name.data"
                    type="text"
                ></b-form-input>
                </b-col>
            </b-row>
            </b-form>
            <!-- {{this.grantData}} -->
            <div class="form-buttons">
            <b-button class="form-btn" @click="reset">Reset</b-button>
            <b-button class="form-btn" @click="saveLoan">Submit</b-button>
            </div>
        </div>
        </div>
    </div>
    </div>
  </div>
</template>
<script>
import axios from "axios";
import moment from 'moment'


export default {
  el:'#gt',
  name: "Home",
  components: {
  },
  data() {
    return {
      // amountToword: parseFloat(this.loanAmount.replace(/,/g, '')) | NumbersToWords,
      checked: false,
      dismissSecs: 5,
      dismissCountDown: 0,
      showDismissibleAlert: false,
      selectedLoan: "",
      errors: "",
      id:0,
      Loanid:"",
      LoanId:"",
      MemberId:"",
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
          firstName : {},
          email : {},      
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
      banks: [
        { value: "214", text: "First City Monument Bank" },
        { value: "215", text: "Unity Bank" },
        { value: "221", text: "Stanbic IBTC Bank" },
        { value: "232", text: "Sterling Bank" },
        { value: "301", text: "JAIZ Bank" },
        { value: "032", text: "Union Bank" },
        { value: "044", text: "Access Bank" },
        { value: "063", text: "Diamond Bank" },
        { value: "076", text: "Skye Bank" },
        { value: "082", text: "Keystone Bank" },
        { value: "058", text: "GTBank Plc" },
        { value: "050", text: "Ecobank Plc" },
        { value: "068", text: "Standard Chartered Bank" },
        { value: "070", text: "Fidelity Bank" }
      ],
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
       
    };
  },
  async created() {
    await this.initialize();
    this.effectiveDate();
    axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('token')}`
    this.$store.dispatch('getAllMembers')
  },
  computed: {
      accNum() {
        return this.form.accountNumber.length == 10 ? true : false
      },
      getAllmembers () {
          return this.$store.state.Allmember
      },
      memNumbers () {
          return this.$store.state.memberNum
      }
    },
  methods: {

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
      
      this.showDismissibleAlert = !this.showDismissibleAlert;
      let amount = parseFloat(this.loanAmount.replace(/,/g, ''))
      let expectedAmount = parseFloat(this.amountExpected.replace(/,/g, ''))
      let desireAmount = parseFloat(this.amountDesire.replace(/,/g, ''))
      let mAmountDesire = expectedAmount * 0.75

      if (this.mType == 3 || this.mType == 2) {
        if(amount < this.minLoanAmount && this.minLoanAmount != 0){
              return this.errors = `Loan Amount must be Minimum of ₦ ${this.minLoanAmount | this.numberFormat} only`
        }
        if(this.maxLoanAmount != 0 && amount > this.maxLoanAmount){
              return this.errors = `Loan Amount must be Maximum of ₦ ${this.maxLoanAmount | this.numberFormat} only`
        }
      } 
      if (this.mType == 1) {
        if (desireAmount > mAmountDesire) {
          return this.errors = `Sorry, Maximum loan value is 75% of your Amount Expected  ${mAmountDesire}`
        }  
      }
      return this.errors = "";
    },

    RepaymentValidation() {
      
      // this.dismissCountDown = this.dismissSecs
      this.showDismissibleAlert = !this.showDismissibleAlert;
      if (this.mType == 3 || this.mType == 2) {
        if (this.MinRepayPeriod != 0 && this.minMonthlyRepayPeriod < this.MinRepayPeriod) {
          return this.errors = `Value Less than Minimum Repayment Period for ${this.details.loan.name} Loan` 
        }
        if (this.MinRepayPeriod != 0 && this.minMonthlyRepayPeriod > this.MaxRepayPeriod) {
          return this.errors = `Value more than Maximum Repayment Period for ${this.details.loan.name} Loan`
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
    
    //.............................................Start................................
    async getGuarantor() {
      let guarantor = {            
            MemberId: parseInt(localStorage.getItem('memberId')),
            LoanId: this.details.loanId,
            LoanAmount: parseInt(this.loanAmount.replace(/,/g, ''))
          }; 
          guarantor = JSON.stringify(guarantor);           
              await axios      
     await axios
        .post(`${process.env.VUE_APP_API_URL}/LoanConfig/Guarantors/count`, guarantor, {
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.guarant = response.data;
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

    addGrant(gNo,gName,gMail){
      let index = null
      console.log(this.grantData.length)
      for (let i = 0; i < this.grantData.length; i++) {
              console.log("Lenght is " + this.grantData.length)
              console.log("gData is " + JSON.stringify(this.grantData[i]))
              console.log("Gno is " + gNo)
        if (this.grantData[i].EmployeeNumber === gNo) {
          console.log("Index is ", index)
          console.log("i is ",i)
          index = i
          break
        }
      }

      console.log("Last index is ",index)
      // this.grantData.splice(index, 1)

      this.grantData.push({
        EmployeeNumber : gNo,
        GuarantorName: gName,
        GuarantorEmail: gMail
      });
    },


       async getGuarantorInfo(gNo) {
      let guarantor = {            
            EmployeeNumber: gNo
          }; 
          guarantor = JSON.stringify(guarantor);           
              await axios      
     await axios
        .post(`${process.env.VUE_APP_API_URL}/Members/EmpNumber`, guarantor, {
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.Info = response.data.data;
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
          this.name = response.data;          
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

    async getConfig(selectedLoan) {        
      await axios
          .get(`${process.env.VUE_APP_API_URL}/loantype/amount/${selectedLoan}`,{
            headers: {
              "Content-Type": "application/json;charset=utf-8",
              Authorization: `Bearer ${localStorage.getItem('token')}`
            }
          })
          .then(response => {
            this.details = response.data;
            this.mType = response.data.loan.loanType;
            this.mLoanCode = response.data.loan.loanCode;
            this.minLoanAmount = response.data.minLoanAmount;
            this.maxLoanAmount = response.data.maxLoanAmount;
            this.MinRepayPeriod = response.data.minMonthlyRepayPeriod;
            this.MaxRepayPeriod = response.data.maxMonthlyRepayPeriod
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

    async saveLoan() {

      let Month = this.effectiveMonth
      let Year = parseInt(this.effectiveYear)
      let repay = parseInt(this.minMonthlyRepayPeriod)      
      if (this.mType === 1) {
        Month = this.effectMonth
        Year = parseInt(this.effectYear)
        repay = 0
      }

      let rawData = {
        LoanId : this.details.loanId,
        MemberId: this.details.memberTypeId,
        InterestRate: this.details.intrestRate,
        LoanAmount: parseInt(this.loanAmount.replace(/,/g, '')),
        RepaymntPeriod: repay,
        EffectiveMonth: Month,
        EffectiveYear: Year,
        BankCode: this.form.bankcode,
        MethodOfCollection: 2,
        AccountNumber: this.form.accountNumber,
        AccountName: this.name.data,
        LoanGuarantors: this.grantData
      }
      rawData = JSON.stringify(rawData);
      await axios
        .post(
          `${process.env.VUE_APP_API_URL}/Loans/apply`,
          rawData,
          {
            headers: {
              'Content-Type': 'application/json;charset=utf-8',
              Authorization: `Bearer ${localStorage.getItem('token')}`
            },
          }
        )
        .then((response) => {
          this.errors = response.message      
          this.result = response.data;
          this.$emit("setParamResp", this.result);
          localStorage.setItem("LoanPlan",JSON.stringify(this.result));
          this.$router.push('/repayment_plan');
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

      reset() {
        this.selectedLoan = "";
        this.details.loanId = "";
        this.details.memberTypeId = "";
        this.loanAmount = "";
        this.minMonthlyRepayPeriod = "";
        this.form.bankcode = "";
        this.form.accountNumber = "";
        this.name.data = "";
        this.grantData = null
    },

  },

};
</script>

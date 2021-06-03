<template>
    <div>        
        <div v-if="loanType == 1">
            <div class="content-header">Target Loan Planner</div>
        </div>
        <div v-else>
            <span v-if="memberLogin.memberType === 1">
            <div class="content-header">Regular Member Loan Planner</div>
            </span>
            <span v-if="memberLogin.memberType === 2">
            <div class="content-header">Retiree Member Loan Planner</div>
            </span>
            <span v-if="memberLogin.memberType === 3">
            <div class="content-header">Expatriate Member Loan Planner</div>
            </span>            
        </div>
        <div class="form-flex-col">
                    <label class="login-label">Loan Type</label>
                    <b-form-select class="app-text-field w-input"
                        v-model="selectedLoanType" @change="getConfig(selectedLoanType)"
                        required>
                        <b-form-select-option :value="null" disabled>
                            -- Select LoanType -- 
                        </b-form-select-option>
                        <b-form-select-option 
                        v-for="item in items" 
                        :value="item.loanId"
                        :key="item.loan.id">
                            {{item.loan.name}} 
                        </b-form-select-option>
                    </b-form-select>
                    </div>                 
                <span v-if="loanType == 1">
                    <div class="form-flex">  
                    <div class="form-flex-col">
                        <label class="login-label">Lump Sum Type</label>
                        <b-form-select class="app-text-field w-input"
                            v-model="selectedLumpSum" @change="getLumpSumPeriod"
                            :options="lumpSums" required>
                        </b-form-select>
                    </div>
                    <div class="form-flex-col">
                        <label class="login-label">Loan Period  (Months)</label>
                        <input type="text" v-model="loanPeriod1" @change="planner"
                        class="app-text-field w-input" disabled />
                    </div>                   
                    <div class="form-flex-col">
                        <label class="login-label">Amount Expected</label>
                        <input type="text" v-model="amountExpected" v-mask="mask"
                        class="app-text-field w-input" required placeholder="Type Here" />
                        <span v-if="amountExpected != ''"><p style="color:red;font-size:12px;" >
                        {{parseFloat(this.amountExpected.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only</p></span>
                    </div>
                     <div class="form-flex-col">
                    <label class="login-label">Loan Amount</label>
                    <input type="text" v-model="loanAmount" @blur="planner" v-mask="mask"
                    class="app-text-field w-input" required placeholder="Type Here" />
                    <span v-if="loanAmount != ''"><p style="color:red;font-size:12px;" >
                  {{parseFloat(this.loanAmount.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only</p></span>
                </div>                    
                    <div class="form-flex-col">
                        <label class="login-label">Interest Rate pa:</label>
                        <input type="text" v-model="loanData.interestRate" class="app-text-field w-input" disabled />
                    </div>
                    <div class="form-flex-col">
                        <label class="login-label">Monthly Interest</label>
                        <input type="text" v-model="loanData.interest" class="app-text-field w-input" disabled />
                    </div>
                    <div class="form-flex-col">
                        <label class="login-label">Total Interest Over Tenor</label>
                        <input type="text" v-model="loanData.totalMonthly" class="app-text-field w-input" disabled />
                    </div>
                    </div>
                </span>
                <span v-else>
                    <div class="form-flex">  
                    
                <div class="form-flex-col">
                    <label class="login-label">Loan Amount</label>
                    <input type="text" v-model="loanAmount" @blur="planner" v-mask="mask"
                    class="app-text-field w-input" required placeholder="Type Here" />
                    <span v-if="loanAmount != ''"><p style="color:red;font-size:12px;" >
                  {{parseFloat(this.loanAmount.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only</p></span>
                </div>
                <div class="form-flex-col">
                    <label class="login-label">Loan Periods (Max of 60 Months)</label>
                    <input type="text" v-model="loanPeriod" @blur="planner"
                    class="app-text-field w-input" max="60"
                     required placeholder="Type Here" />
                    <!-- <span v-if="loanPeriod != ''"><p style="color:red;font-size:12px;" >
                  {{loanPeriod | NumbersToWords | capitalize}} Months</p></span> -->
                  <div v-if="loanPeriod.length !== 2 && loanPeriod > 60">
                  <p style="color:red;font-size:12px;" >Loan Period must not be more than 60 Months</p>
                </div>                  
                </div>
                
                <div class="form-flex-col">
                    <label class="login-label">Interest Rate pa:</label>
                    <input type="text" v-model="loanData.interestRate" v-mask="mask" disabled class="app-text-field w-input" placeholder="Type Here" />
                </div>
                <div class="form-flex-col">
                    <label class="login-label">Available For Loan</label>
                    <input type="text" v-model="loanData.availableForLoan" v-mask="mask" class="app-text-field w-input" disabled />
                </div>
                <div class="form-flex-col">
                    <label class="login-label">30% Savings Required</label>
                    <input type="text" v-model="loanData.requiredSavings" v-mask="mask" class="app-text-field w-input" disabled />
                </div>
                <div class="form-flex-col">
                    <label class="login-label">1/3 Repayment Period</label>
                    <input type="text" v-model="loanData.oneThirdRepaymentPeriod" v-mask="mask" class="app-text-field w-input" disabled />
                </div>
            <!-- <div class="col-md-8">
                <input type="number" class="app-text-field w-input" required placeholder="Type Here" />
            </div> -->
                <div class="form-flex-col">
                    <label class="login-label">Min Savings Per Month(3%) on Loan</label>
                    <input type="text" v-model="loanData.minimumSavings" v-mask="mask" class="app-text-field w-input" disabled />
                </div>
                <div class="form-flex-col">
                    <label class="login-label">Actual Monthly Savings on Loan</label>
                    <input type="text" v-model="loanData.actualMonthlySavings" v-mask="mask" class="app-text-field w-input" disabled />
                </div>
                <div class="form-flex-col">
                    <label class="login-label">Principla Repayment Per Month</label>
                    <input type="text" v-model="loanData.principal" v-mask="mask" class="app-text-field w-input" disabled />
                </div>
                <div class="form-flex-col">
                    <label class="login-label">Monthly Interest</label>
                    <input type="text" v-model="loanData.interest" v-mask="mask" class="app-text-field w-input" disabled />
                </div></div>
                </span>
                <div style="margin-top:50px;" class="button-group">
          <button type="submit" style="background:#c00;display:inline-block;margin-right:30px" 
          class="app-form-button" @click="reset">Reset</button>
        
    </div>
    </div>
</template>
<script>
import axios from "axios";
// import Loader from '../../components/ui/loader/loader.vue'
// import Status from '../../components/ui/state/state.vue'
import createNumberMask from 'text-mask-addons/dist/createNumberMask';
    let currencyMask = createNumberMask({
    prefix: '',
    allowDecimal: true,
    decimalLimit :2,
    includeThousandsSeparator: true,
    allowNegative: false,
    });
export default {
    // components: {
    // Loader,
    // Status
    // },
    data() {
        return {
            mask: currencyMask,
            state: 'failed',
            status: false,
            message: '',
            loader: false,
            show: true,
            loanData:{},
            items:{},
            loanAmount:'',
            loanPeriod:'',
            loanPeriod1:'',
            selectedLoanType:null,
            loanType:0,
            amountExpected:'',
            selectedLumpSum:null,
            lumpSums: [
                { text: "---Select Lump Sum---", value: null, disabled: true},
                { value: 1, text: "Annual Rent Subsidy" },
                { value: 2, text: "Security Allowance" },
                { value: 3, text: "Generator Maintenance/ Diesel Allowance" },
                { value: 4, text: "Annual Productivity Bonus"}
            ], 
            memberType:0,
            // interestRate:'',
            // availableLoan:'',
            // savingsRequired:'',
            // thirdRepayment:'',
            // minSavings:'',
            // actualSavings:'',
            // principal:'',
            // monthlyInterest:'',
            // totalRepayment:''
        };
    },
    watch:{
        loanAmount(){
            if (this.loanAmount == '' && this.loanPeriod =='') {
                this.clearForm();
                this.loanData.interestRate = '';
            }
        }
    },
    computed: {
  memberLogin() {
    return this.$store.state.member
  }
},
    async created (){        
        await this.initialize();
        this.$store.dispatch('memberDetails');
    },
    methods: {
        planner() {                
            let Body = {}
            if ( !this.amountExpected) {
                Body = {
                    loanId: this.selectedLoanType,
                    loanAmount: parseInt(this.loanAmount.replace(/,/g, '')),
                    repaymentPeriod: parseInt(this.loanPeriod),
                }
            }else {
                Body = {
                    loanId: this.selectedLoanType,
                    loanAmount: parseInt(this.loanAmount.replace(/,/g, '')),
                    ExpectedAmount: parseInt(this.amountExpected),
                    repaymentPeriod: parseInt(this.loanPeriod1),
                }                
                this.loanPeriod = this.loanPeriod1
            }                     
            Body = JSON.stringify(Body); 
            if (this.loanAmount != '' && this.loanPeriod != '' && this.loanPeriod <= 60){
                axios.post(`${process.env.VUE_APP_API_URL}/LoanConfig/planner`,Body, {
                headers: {"Content-Type": "application/json;charset=utf-8",
                Authorization: `Bearer ${localStorage.getItem('token')}`
                }
                }).then((response) => {
                    if(response.data.success == true){
                        this.loanData = response.data.data;
                    }
                }).catch(error => {
                    this.message = error.message
                });
            }
        },
        getLumpSumPeriod() {
            if (this.selectedLumpSum === 1) {
                return this.loanPeriod1 = 10
            }else if (this.selectedLumpSum === 2) {
                return this.loanPeriod1 = 1
            }else if (this.selectedLumpSum === 3) {
                return this.loanPeriod1 = 6
            }else if (this.selectedLumpSum === 4) {
                return this.loanPeriod1 = 8
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
    async getConfig(selectedLoanType) {
        this.reset()       
      await axios
          .get(`${process.env.VUE_APP_API_URL}/loantype/amount/${selectedLoanType}`,{
            headers: {
              "Content-Type": "application/json;charset=utf-8",
              Authorization: `Bearer ${localStorage.getItem('token')}`
            }
          })
          .then(response => {
            this.loanType = response.data.loan.loanType;
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
        this.loanAmount=''
        this.loanPeriod=''
        this.loanType=0
        this.amountExpected=''
        this.selectedLumpSum=null
        this.loanData.interestRate = ''
        this.loanData.interest=''
        this.loanData.actualMonthlySavings=''
        this.loanData.availableForLoan = ''
        this.loanData.requiredSavings = ''
        this.loanData.totalMonthly =''
        this.loanData.oneThirdRepaymentPeriod = ''
        this.loanData.principal = ''
        this.loanData.minimumSavings=''

    }
        
    }  
};
</script>
<style></style>

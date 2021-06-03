<template>
    <div>
        <div class="content-header">Target Loan Planner</div>
        <div class="form-flex">
            <!-- <div v-show="loader">
                <Loader/>
            </div>
            <Status :state="state" :closeModal = "closeModal" :message = "message" :resetState="resetState" v-if="status"/>           -->
            
                <div class="form-flex-col">
                    <label class="login-label">Lump Sum Type</label>
                    <b-form-select class="app-text-field w-input"
                        v-model="selectedLumpSum" @change="getLumpSumPeriod"
                        :options="lumpSums" required>
                    </b-form-select>
                </div>
                <div class="form-flex-col">
                    <label class="login-label">Amount Expected</label>
                    <input type="text" v-model="amountExpected" @blur="planner" v-mask="mask"
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
                    <label class="login-label">Loan Period  (Months)</label>
                    <input type="text" v-model="loanPeriod" @blur="planner"
                    class="app-text-field w-input" disabled />
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
            <!-- <div class="col-md-8">
                <input type="number" class="app-text-field w-input" placeholder="Type Here" />
            </div> -->
                        
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
            selectedLumpSum:null,
            mask: currencyMask,
            loanId:0,
            loanData:{},
            loanAmount:'',
            amountExpected:'',
            loanPeriod:'',
            lumpSums: [
                { text: "---Select Lump Sum---", value: null, disabled: true},
                { value: 1, text: "Annual Rent Subsidy" },
                { value: 2, text: "Security Allowance" },
                { value: 3, text: "Generator Maintenance/ Diesel Allowance" },
                { value: 4, text: "Annual Productivity Bonus"}
            ], 
        };
    },
    methods: {
        planner() {
            let Body = {
                loanId: 1,
                loanAmount: parseInt(this.loanAmount),
                ExpectedAmount: parseInt(this.amountExpected),
                repaymentPeriod: parseInt(this.loanPeriod),
            }            
            Body = JSON.stringify(Body); 
            if (this.loanPeriod != ''){
                axios.post(`${process.env.VUE_APP_API_URL}/LoanConfig/planner`,Body, {
                headers: {"Content-Type": "application/json;charset=utf-8",
                Authorization: `Bearer ${localStorage.getItem('token')}`
                }
                }).then((response) => {
                    if(response.data.success == true){
                        this.loanData = response.data.data;
                    }
                }).catch(error => {
                    this.clearForm();
                    this.message = error.message
                });
            }
        },

        getLumpSumPeriod() {
            if (this.selectedLumpSum === 1) {
                return this.loanPeriod = 10
            }else if (this.selectedLumpSum === 2) {
                return this.loanPeriod = 1
            }else if (this.selectedLumpSum === 3) {
                return this.loanPeriod = 6
            }else if (this.selectedLumpSum === 4) {
                return this.loanPeriod = 8
            }

        }
        
    }   
};
</script>
<style></style>
<template>
  <div class="dashboard">
    <div class="background-design"></div>
    <!-- <Header></Header> -->
    <Menu></Menu>
    <div class="container">
      <!-- <NavBar></NavBar> -->
      <div class="row">
        <div class="col-md-12">
          <div class="main-dashboard">
            <div class="dashboard-greeting">
              <div class="overview-text">
                <font-awesome-icon icon="exchange-alt" />
                <div class="header-title">Configuration</div>
              </div>
              <div class="date">
                <font-awesome-icon icon="clock" />
                <div class="date-item ml-2">{{ currentDateTime() | humanize }}</div>
              </div>
            </div>
            <div class="line"></div>
            <div class="row">
            <div class="col-md-8">
              <div class="overview-board">
                  <div class="account-overview">
                    <div class="account-overview-content">
                      <div class="mt-2">
                  <div class="header_background">Loan Configuration</div>
                </div>
                <div>
                  <div style="position:relative; overflow-y:auto; height:400px">
                  <b-form
                    ref="form"
                    @submit="onSubmit"
                    @reset="onReset"
                    v-if="show"
                  >
                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="Loan"
                      label-for="sm"
                      invalid-feedback="Loan is required"
                      :state="nameState"
                    >
                      <b-form-select
                        id="loan"
                        v-model="form.loan"
                        :state="nameState"
                        required
                      >                     
                        <b-form-select-option :value="null" disabled>
                            -- Select Loan -- 
                          </b-form-select-option>
                          <b-form-select-option 
                          v-for="item in items.data" 
                          :value="item.id"
                          :key="item.id">
                            {{item.name}} 
                        </b-form-select-option>
                      </b-form-select >                      
                      <br/>
                      </b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="MEMBER"
                      label-for="input-sm"
                    >
                      <b-form-select
                        id="member"
                        v-model="form.member"
                        :options="members"
                      ></b-form-select
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="MIN AMOUNT"
                      label-for="input-sm"
                      invalid-feedback=" Amount is required"
                      :state="nameState"
                    >
                      <b-form-input
                        type="text"
                        id="min-amount"
                        v-model="form.nAmount"
                        :formatter="numberFormat"
                        :state="nameState"
                        required
                      ></b-form-input> {{form.nAmount | formatMoney}}
                      <br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="MAX AMOUNT"
                      label-for="input-sm"
                      invalid-feedback="Amount is required"
                      :state="nameState"
                    >
                      <b-form-input
                        id="max-amount"
                        v-model="form.mAmount"
                        :formatter="numberFormat"
                        :state="nameState"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="REPAY PERIOD"
                      label-for="input-sm"
                      :state="nameState"
                    >
                      <b-form-input
                      type="number"
                        id="repay"
                        v-model="form.repay"
                        :state="nameState"
                        :formatter="numberFormat"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      id="input-group-3"
                      label="MIN REPAY PERIOD"
                      label-for="input-3"
                      label-cols-lg="2"
                      label-size="sm"
                      invalid-feedback="Gendr is required"
                      :state="nameState"
                    >
                      <b-form-input
                      type="number"
                        id="repay-min"
                        v-model="form.nRepay"
                        :state="nameState"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      id="input-group-3"
                      label="MAX REPAY PERIOD"
                      label-for="input-3"
                      label-cols-lg="2"
                      label-size="sm"
                      invalid-feedback="Status is required"
                      :state="nameState"
                    >
                      <b-form-input
                      type="number"
                        id="repay-max"
                        v-model="form.mRepay"
                        :state="nameState"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="INTEREST RATE"
                      invalid-feedback="Interest is required"
                      :state="nameState"
                    >
                      <b-form-input
                      type="number"
                        id="interest"
                        v-model="form.Interest"
                        :state="nameState"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="MONTHLY SAVING AMOUNT"
                      label-for="input-sm"
                      invalid-feedback="Saving Amount is required"
                      :state="nameState"
                    >
                      <b-form-input
                      type="number"
                        id="m-savaing"
                        v-model="form.monthlySaving"
                        :state="nameState"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="LUMPSUM SAVING AMOUNT"
                      label-for="input-sm"
                      :state="nameState"
                    >
                      <b-form-input
                      type="number"
                        id="lum-saving"
                        v-model="form.lumpsumSaving"
                        :state="nameState"
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="EXISING LOANFEE"
                      label-for="input-sm"
                      invalid-feedback="Loanfee No is required"
                      :state="nameState"
                    >
                      <b-form-input
                        type="number"
                        id="loanfee"
                        v-model="form.exisitLoanfee"
                        :state="nameState"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="WAITING PERIOD"
                      label-for="input-sm"
                      invalid-feedback="Waiting period is required"
                      :state="nameState"
                    >
                      <b-form-input
                      type="number"
                        id="waiting"
                        v-model="form.WaitingPeriod"
                        :state="nameState"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="PERIOD BEFORE OFFSET"
                      label-for="input-sm"
                      :state="nameState"
                    >
                      <b-form-input
                      type="number"
                        id="offset"
                        v-model="form.offset"
                        :state="nameState"
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="ALLOW PARTIAL OFFSET"
                      label-for="input-sm"
                      :state="nameState"
                    >
                      <b-form-radio-group
                      v-model="form.partialOffset"
                      :options="partialOffsets"
                      class="mb-3"
                      value-field="item"
                      text-field="name"
                      disabled-field="notEnabled"
                    ></b-form-radio-group><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="ADMIN CHARGE TYPE"
                      label-for="input-sm"
                      invalid-feedback="State is required"
                      :state="nameState"
                    >
                      <b-form-select
                        id="name-input"
                        v-model="form.chargeType"
                        :options="chargeTypes"
                        :state="nameState"
                        required
                      ></b-form-select
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="ADMIN CHARGE AMOUNT"
                      label-for="input-sm"
                      invalid-feedback="Admin charge is required"
                      :state="nameState"
                    >
                      <b-form-input
                        id="chargeA"
                        v-model="form.chargeAmount"
                        :state="nameState"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    
                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="ALLOW CONCURRENT"
                      label-for="input-sm"
                      :state="nameState"
                    >
                      <b-form-radio-group
                      v-model="form.concurrent"
                      :options="concurrents"
                      class="mb-3"
                      value-field="item"
                      text-field="name"
                      disabled-field="notEnabled"
                    ></b-form-radio-group><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      id="qualifying"
                      label="QUALIFYING PERIOD"
                      label-for="input-3"
                      label-cols-lg="2"
                      label-size="sm"
                      invalid-feedback="Qualifying period is required"
                      :state="nameState"
                    >
                      <b-form-input
                        id="input-3"
                        v-model="form.qualifyingPeriod"
                        type= "number"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>
                    <div class="row justify-content-md-center">
                    <b-row>
                      <b-button type="submit" class="btn" variant="primary">Submit</b-button>                    
                      <b-button  class="btn" variant="light">Cancel</b-button>
                    </b-row>
                    </div>
                  </b-form>
                  </div>
                </div>
              </div>
              </div>
              </div>            
            </div>
            <div v-if="this.userType == 2">
            <div class="col-md-4">
                <div class="dashboard-right-side-bar">
                <div class="header_2">Profile</div>
                    
                    <RightSidebar></RightSidebar>
                </div>
              </div>
            </div>
            </div>
          </div>
        </div>
      </div>
      <Footer></Footer>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src

// import Header from "../../components/layout/headers/headerDashboard.vue";
// import NavBar from "../../components/layout/headers/dashboardNav.vue";
import Menu from "../../components/layout/headers/menus.vue";
import RightSidebar from "../../components/layout/sidebar/profile-sidebar.vue"
import Footer from "../../components/layout/footer/footer.vue";

import axios from "axios";

export default {
  name: "Home",
  el: '#vue',
  components: {
    // Header,
    // NavBar,
    Menu,
    RightSidebar,
    Footer
  },
  data() {
    return {
      checked: false,
      selectedLoan: "",
      show: true,
      nameState: null,
      Person:[],
      points: 0,
      items: [
        { text: "Select Loan", value: null },
      ],
      form: {
        loan: null,
        member: null,
        nAmount: 0,
        mAmount: 0,
        repay: 0,
        nRepay: 0,
        mRepay: 0,
        Interest: 0,
        monthlySaving: 0,
        lumpsumSaving: 0,
        exisitLoanfee: 0,
        WaitingPeriod: 0,
        offset: 0,
        partialOffsets: null,
        chargeType: null,
        chargeAmount: 0,
        concurrent: null,
        qualifyingPeriod: 0,
        IsATargetLoan: ""
      },
      partialOffsets: [
                { item: true, name: 'Yes' },
                { item: false, name: 'No'}
      ],
      

      members: [
        { text: "Select Member", value: null },
        { text: "Regular", value: 1 },
        { text: "Retiree", value: 2 },
        { text: "Expatriate", value: 3 }
      ],
      chargeTypes: [
        { text: "Select Amin Charge Type", value: null },
        { text: "One Month Interest off Current Loan", value: 1 },
        { text: "Flat Amount", value: 2 },
        { text: "% of Loan Amount", value: 3 },
        { text: "None", value: 4 }
      ],
      concurrents: [
                { item: true, name: 'Yes' },
                { item: false, name: 'No'}  ],
    };
  },
  async created() {
    await this.initialize();
  },
  methods: {

    numberFormat(value) {
        this.points = Number(value.replace(/\D/g, ''))
        return value == '0.00' ? '' : this.points.toLocaleString();
      },
     makeToast(variant = null) {
        this.notify++
        this.$bvToast.toast(`Loan Added`, {
          title: 'Successful',
          variant: variant,
          solid: true,
          autoHideDelay: 5000,
        })
      },
    currentDateTime() {
      const current = new Date();
      const date = current.toDateString(); //+'-'+(current.getMonth()+1)+'-'+current.getDate();
      const time = current.getHours() + ":" + current.getMinutes(); // + ":" //+ current.getSeconds();
      const dateTime = date + " " + time;

      return dateTime;
    },
    checkFormValidity() {
      const valid = this.$refs.form.checkValidity();
      this.nameState = valid;
      return valid;
    },
    onReset(event) {
      event.preventDefault();

      

      this.show = false;
      this.$nextTick(() => {
        this.show = true;
      });
    },

    async initialize() {        
     await axios
        .get(`${process.env.VUE_APP_API_URL}/Loans/All`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8"
          }
        })
        .then(response => {
          this.items = response.data;
        })
        .catch(error => {
          error.alert("Error");
        });
    }, 
   async onSubmit (event) {
      event.preventDefault();
      // Exit when the form isn't valid
      // if (!this.checkFormValidity()) {
      //   return;
      // }
      let res = {
                MinLoanAmount : parseInt(this.form.nAmount),
                MaxLoanAmount: parseInt(this.form.mAmount),
                MonthlyRepayPeriod : parseInt(this.form.repay),
                MinMonthlyRepayPeriod : parseInt(this.form.nRepay),
                MaxMonthlyRepayPeriod : parseInt(this.form.mRepay),
                IntrestRate : parseInt(this.form.Interest),
                LumpSumSavingsAmount: parseInt(this.form.lumpsumSaving),
                IsATargetLoan : false,
                MonthlySavingsAmount : parseInt(this.form.monthlySaving),
                ExistingLoanFeeAmount : parseInt(this.form.exisitLoanfee),
                WaitingPeriod : parseInt(this.form.WaitingPeriod),
                PeriodBeforeOffset: parseInt(this.form.offset),
                AllowPartialOffset : this.form.partialOffset,
                AdminChargeType : this.form.chargeType,
                AdminChargeAmount: parseInt(this.form.chargeAmount),
                LoanId : this.form.loan,
                MemberTypeId : this.form.member,
                AllowConcurent: this.form.concurrent,
                ConcurrentQualifyingPeriods : parseInt(this.form.qualifyingPeriod)
      }
      res = JSON.stringify(res);
      await axios
        .post(`${process.env.VUE_APP_API_URL}/LoanConfig`, res, {
          headers: {
            "Content-Type": "application/json;charset=utf-8"
          }
        })
        .then(response => {
          this.rawData = response.data;
          this.makeToast(`success`);
        })
        .catch(error => {
          alert(error);
        });



        
    }
  }
};
</script>

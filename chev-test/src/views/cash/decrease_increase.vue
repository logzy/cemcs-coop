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
                <div class="header-title">Savings</div>
              </div>
              <div class="date">
                <font-awesome-icon icon="clock" />
                <div class="date-item ml-2">{{new Date().toLocaleString() | humanize}}</div>
              </div>
            </div>
            <div class="line"></div>
            <div class="row">              
              <div class="col-md-8">
                <div class="overview-board">
                  <div class="account-overview">
                    <div class="account-overview-content">
                      <div class="mt-2">
                <!-- <b-alert show variant="success">Success Alert</b-alert> -->
                        <div class="header_background">Increase and Decrease</div>
                      </div>
                    <div style="position:relative; overflow-y:auto; height:400px">
                           <strong> 
                               <b-form-group
                                label-cols="2"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Name"
                                label-for="input-sm"
                              >

                              {{user.data.person.lastName+", " +user.data.person.firstName+" " 
                              +user.data.person.middleName}}
                                <!-- <b-form-input
                                  id="name-input"
                                  v-model="user.data.person.firstName"
                                  required
                                ></b-form-input >-->
                                </b-form-group>
                               
                               <b-form-group
                                label-cols="2"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Employee Number"
                                label-for="input-sm"
                              >
                                <b-form-input
                                  id="number-input"
                                  v-model="user.data.employeeNumber"
                                  disabled
                                  required
                                ></b-form-input>
                              </b-form-group>
                              
                               <b-form-group
                                label-cols="2"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Effective Date"
                                label-for="example-datepicker"
                              >
                                <b-form-datepicker
                                  id="example-datepicker"
                                  v-model="effectiveDate"
                                  required
                                ></b-form-datepicker
                                ></b-form-group>

                                <b-form-group
                                label-cols="2"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Increase/Decrease Savings"
                                label-for="input-sm"
                                >
                                <b-form-select
                                    id="input-3"
                                    v-model="inc_dec"
                                    :options="inc_decs"
                                ></b-form-select
                                ></b-form-group>

                              <b-form-group
                                label-cols="2"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Account"
                                label-for="input-sm"
                                >
                                <b-form-select
                                    id="input-3"
                                    v-model="account"
                                    :options="accountTypes"
                                ></b-form-select
                                ></b-form-group>
                                 <b-form-group
                                label-cols="2"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Amount"
                                label-for="input-sm"
                              >
                                <b-form-input
                                  id="input-4"
                                  v-model="amount"
                                  :formatter="numberFormat"
                                ></b-form-input>
                                <span v-if="amount != ''"><code>
    {{parseFloat(this.amount.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
                                </b-form-group>
                           </strong>

                    
                      <div class="row justify-content-md-center">
                        <b-button type="submit" @click="onSubmit" variant="primary">Submit</b-button>
                      </div> 
                      </div>                
                    </div>
                  </div>
                </div>
              </div>
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
  components: {
    // Header,
    // NavBar,
    Menu,
    RightSidebar,
    Footer
  },
  data() {
    return {
      show: true,
      notify:0,
      user: {},
        employeeNumber: "",
        name: "",
        effectiveDate: "",
        account: null,
        amount:"",
        inc_dec: null,
      accountTypes: [
        { text: "---Select Account Type---", value: null, disabled: true },
        { value: 1, text: "Savings " },
        { value: 2, text: "Special Deposit " }
      ],
      inc_decs: [
        { text: "---Select Saving Type---", value: null, disabled: true },
        { value: 1, text: "Savings Increase" },
        { value: 2, text: "Savings Decrease" }
      ],

    };
  },
  async created() {
      await this.initUser();
  },
  methods: {

    numberFormat(value) {
        this.points = Number(value.replace(/\D/g, ''))
        return value == '0.00' ? '' : this.points.toLocaleString();
      },

    makeToast(variant = null) {
      this.notify++;
      this.$bvToast.toast(`Savings Added`, {
        title: "Successful",
        variant: variant,
        solid: true,
        autoHideDelay: 5000
      });
    },

     async onSubmit() {
      let rawData = {

        TransactionDate: this.effectiveDate,
        MemberId: this.user.data.id,
        DepositAmount: parseInt(this.amount.replace(/,/g, '')),
        SavingsType: this.account,        
        TransactionTypeId: this.inc_dec
        }
      rawData = JSON.stringify(rawData);
      await axios
        .post(`${process.env.VUE_APP_API_URL}/SavingDepositTransactions`, rawData, {
          headers: {
            "Content-Type": "application/json;charset=utf-8"
          },
        })
        .then((response) => {
          this.rawData = response.data;
          this.makeToast(`success`);
          if (this.form.MemberType != 2){       
            window.history.length > this.$router.
            push(`/payment/${this.form.fname}&${this.form.lname}&
              ${this.form.email}&${this.form.mobileNo}`);
          }
        })
        .catch(error => {
          this.$bvToast.toast(error.Message, {
                title: "Error",
                variant: "danger",
                solid: true,
                autoHideDelay: 5000
            });
        });
     },

    async initUser() {        
     await axios
        .get(`${process.env.VUE_APP_API_URL}/Members/Usertype`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.user = response.data;
        })
        .catch(error => {
          error.alert("Error");
        });
      } 
  }
};
</script>

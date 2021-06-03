<template>
  <div class="dashboard">
    <div class="background-design"></div>
    <Menu></Menu>
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <div class="main-dashboard">
            <div class="dashboard-greeting">
              <div class="overview-text">
                <font-awesome-icon icon="exchange-alt" />
                <div class="header-title">CASH</div>
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
                        <div class="header_background">CASH ADDITION</div>
                      </div>
                      <div
                        style="position:relative; overflow-y:auto; height:400px"
                      >
                        <strong>
                          <b-form-group
                            label-cols="2"
                            label-cols-lg="3"
                            label-size="sm"
                            label="Name"
                            label-for="input-sm"
                          >
                            {{
                              user.data.person.lastName +
                                ', ' +
                                user.data.person.firstName +
                                ' ' +
                                user.data.person.middleName
                            }}
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
                          <!-- <b-row class="my-1 form-row mb-3">
                          <b-col sm="3">
                    <b-form-select
                    v-model="effectiveMonth" disabled
                    :options="Months"
                    value-field="value"
                    text-field="name"
                ></b-form-select></b-col>
                    <b-col sm="3"><b-form-input
                    :id="`DateExpected`"
                    v-model="effectiveYear" disabled
                    type="text"
                    ></b-form-input>
                </b-col></b-row> -->
                            <date-picker class='input-group date down' :state="effectiveDate"
                                 v-model="effectiveDate" :config="options"></date-picker>
                          </b-form-group>

                          <b-form-group
                            label-cols="2"
                            label-cols-lg="3"
                            label-size="sm"
                            label="Account Type"
                            label-for="input-sm"
                          ><b-form-select
                              id="input-3"
                              v-model="account"
                              :options="accountTypes"
                            ></b-form-select
                          ></b-form-group>

                          <b-form-group
                            label-cols="2"
                            label-cols-lg="3"
                            label-size="sm"
                            label="Amount Desired"
                            label-for="input-sm"
                          ><b-form-input
                              id="input-4"
                              v-model.trim="amount"
                              :formatter="numberFormat"
                            ></b-form-input >
                          <span v-if="amount != ''"><code>
    {{parseFloat(this.amount.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
                          </b-form-group>
                        </strong>

                        <div class="row justify-content-md-center">
                          <b-button
                            type="submit"
                            @click="onSubmit"
                            variant="primary"
                            >Submit</b-button
                          >
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

import Menu from '../../components/layout/headers/menus.vue';
import RightSidebar from '../../components/layout/sidebar/profile-sidebar.vue';
import Footer from '../../components/layout/footer/footer.vue';

import moment from 'moment'
import axios from 'axios';
export default {
  name: 'Home',
  components: {
    Menu,
    RightSidebar,
    Footer,
  },
  data() {
    return {
      show: true,
      notify: 0,
      options: {
          format: 'YYYY-MM-DD',
          useCurrent: false,
          showClear: true,
          showClose: true,
        },
      user: {},
      employeeNumber: '',
      name: '',
      account: null,
      amount: '',
      effectiveDate: new Date(),
      effectiveMonth: "",
      effectiveYear: moment(new Date().toLocaleString()).format("YYYY"),
      accountTypes: [
        { text: "---Select Account Type---", value: null, disabled: true },
        { value: 1, text: "Savings " },
        { value: 2, text: "Special Deposit " }
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
    await this.initUser();
    this.effectDate();
  },
  methods: {


    effectDate () {
      const current = new Date();
      const currentDate = current.getDate();
      if (currentDate < 15) {
        return this.effectiveMonth = current.getMonth();
      }
      else {
       return  this.effectiveMonth = current.getMonth() +1;
      }
      
      
    },

    numberFormat(value) {
        this.points = Number(value.replace(/\D/g, ''))
        return value == '0.00' ? '' : this.points.toLocaleString();
      },
      
    makeToast(variant = null) {
      this.notify++;
      this.$bvToast.toast(`Cash Addition Added`, {
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
        TransactionTypeId: 3,
      };
      rawData = JSON.stringify(rawData);
      await axios
        .post(
          `${process.env.VUE_APP_API_URL}/SavingDepositTransactions`,
          rawData,
          {
            headers: {
              'Content-Type': 'application/json;charset=utf-8'
            },
          }
        )
        .then((response) => {
          this.rawData = response.data;
          this.makeToast(`success`);
          if (this.form.MemberType != 2) {
            window.history.length >
              this.$router.push(`/payment}`);
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

    async initUser() {
      await axios
        .get(`${process.env.VUE_APP_API_URL}/Members/Usertype`, {
          headers: {
            'Content-Type': 'application/json;charset=utf-8',
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        })
        .then((response) => {
          this.user = response.data;
        })
    },
  },
};
</script>

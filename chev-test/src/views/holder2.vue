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
                <font-awesome-icon icon="chart-bar" />
                <div class="header-title">Overview</div>
              </div>
              <div class="date">
                <font-awesome-icon icon="clock" />
                <div class="date-item ml-2">{{new Date().utc() | humanize}}</div>
              </div>
            </div>
            <div class="line"></div>
            <div class="row">
              <div class="col-md-8">
                <div class="overview-board">
                  <div class="account-overview">
                    <div class="account-overview-content">
                      <div class="mt-2">
                        <div class="header_2">Personal Balance</div>
                      </div>
                      <div>
                        <div class="row">
                          <div class="col-md-6">
                            <div class="saving-balance">
                              <div class="saving-img">
                                <img
                                  class="save-money-image"
                                  src="../../assets/images/icons/save-money.png"
                                  alt="save money"
                                />
                              </div>
                              <div class="saving-text">
                                <p class="saving-balance-title">Savings</p>
                                <p class="saving-balance-amount">N0:00</p>
                                <p class="saving-balance-balance">Balance</p>
                              </div>
                            </div>
                          </div>
                          <div class="col-md-6">
                            <div class="loan-balance">
                              <div class="saving-img">
                                <img
                                  class="save-money-image"
                                  src="../../assets/images/icons/loan-money.png"
                                  alt="save money"
                                />
                              </div>
                              <div class="saving-text">
                                <p class="loan-balance-title">Loans</p>
                                <p class="loan-balance-amount">N0:00</p>
                                <p class="loan-balance-balance">Balance</p>
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="account-history">
                    <div class="account-history-content">
                      <div class="account-history-header">
                        <div class="header_2">Transaction History</div>
                      </div>
                      <div class="trans-history">
                        <div
                          v-for="(trans, index) in transactionHistory"
                          :key="index"
                          class="trans-history-items"
                        >
                          <div>
                            <div class="trans-amount">
                              <!-- &#8358; {{ trans.amount }} -->
                            </div>
                          </div>
                          <div class="trans-details">
                            <div class="trans-type">{{ trans.type }}</div>
                            <div class="trans-date">{{ trans.date }}</div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>              
              <div class="col-md-4">
                <div v-if="this.userType == 2">
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

import Menu from "../../components/layout/headers/menus.vue"
import RightSidebar from "../../components/layout/sidebar/profile-sidebar.vue";
import Footer from "../../components/layout/footer/footer.vue";
import axios from 'axios';


export default {
  name: "Home",
  components: {
    Menu,
    RightSidebar,
    Footer
  },
  data() {
    return {
    userType: localStorage.getItem('userType'),
      transactionHistory: [
        { amount: "0", type: "Loan", date: `20-12-2020 9:00pm` },
        { amount: "0", type: "withdraw", date: "20-12-2020 9:00pm" },
        { amount: "0", type: "Transfer", date: "20-12-2020 9:00pm" },
        { amount: "0", type: "Saving", date: "20-12-2020 9:00pm" }
      ]
    };
  },
 
  methods: {

    async initUser() {
      await axios
        .get(`${process.env.VUE_APP_API_URL}`, {
          headers: {
            'Content-Type': 'application/json;charset=utf-8',
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        })
        .then((response) => {
          this.user = response.data;
        })
        .catch((error) => {
          error.alert('Error');
        });
    },
  }
};
</script>

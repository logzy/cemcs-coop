<template>
  <div class="dashboard">
    <div class="background-design"></div>
    <Header></Header>
    <div class="container">
      <NavBar></NavBar>
      <div class="row">
        <div class="col-md-12">
          <div class="main-dashboard">
            <div class="dashboard-greeting">
              <div class="overview-text">
                <font-awesome-icon icon="exchange-alt" />
                <div class="header-title">Transfer</div>
              </div>
              <div class="date">
                <font-awesome-icon icon="clock" />
                <div class="date-item ml-2">19 December, 2020</div>
              </div>
            </div>
            <div class="line"></div>
            <div class="row">
              <div class="col-md-8">
                <div class="overview-board">
                  <div class="account-overview">
                    <div class="account-overview-content">
                      <div class="mt-2">
                        <div class="header_background">
                          Transfer To Own Account
                        </div>
                      </div>
                      <div>
                        <div class="row">
                          <div class="col-md-12">
                            <div>
                              <b-form class="form">
                                <b-row class="my-1 form-row mb-3 ">
                                  <b-col sm="4">
                                    <label class="pt-1 form-label" :for="select"
                                      >From <code>*</code></label
                                    >
                                  </b-col>
                                  <b-col sm="8">
                                    <b-form-select
                                      v-model="selected"
                                      :options="accountTypes"
                                    ></b-form-select>
                                  </b-col>
                                </b-row>
                                <b-row class="my-1 form-row mb-3">
                                  <b-col sm="4">
                                    <label class="pt-1 form-label" :for="Amount"
                                      >Amount <code>*</code></label
                                    >
                                  </b-col>
                                  <b-col sm="8">
                                    <b-form-input
                                      :id="`Amount`"
                                      type="number"
                                    ></b-form-input>
                                  </b-col>
                                </b-row>
                                <b-row class="my-1 form-row mb-3 ">
                                  <b-col sm="4">
                                    <label class="pt-1 form-label" :for="select"
                                      >To <code>*</code></label
                                    >
                                  </b-col>
                                  <b-col sm="8">
                                    <b-form-select
                                      v-model="selected"
                                      :options="accountTypes"
                                    ></b-form-select>
                                  </b-col>
                                </b-row>
                              </b-form>
                              <div class="form-buttons">
                                <b-button class="form-btn">Reset</b-button>
                                <b-button class="form-btn">Submit</b-button>
                              </div>
                            </div>
                          </div>
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

import Header from "../../components/layout/headers/headerDashboard.vue";
import NavBar from "../../components/layout/headers/dashboardNav.vue";
import RightSidebar from "../../components/layout/sidebar/profile-sidebar.vue";
import Footer from "../../components/layout/footer/footer.vue";
export default {
  name: "Home",
  components: {
    Header,
    NavBar,
    RightSidebar,
    Footer
  },
  data() {
    return {
      transactionHistory: [
        { amount: "2,000", type: "Loan", date: "20-12-2020 9:00pm" },
        { amount: "4,500", type: "withdraw", date: "20-12-2020 9:00pm" },
        { amount: "10,000", type: "Transfer", date: "20-12-2020 9:00pm" },
        { amount: "21,000", type: "Saving", date: "20-12-2020 9:00pm" }
      ],
      accountTypes: [
        { value: 1, text: "Special Deposit " },
        { value: 2, text: "Fixed Deposit " }
      ]
    };
  }
};
</script>

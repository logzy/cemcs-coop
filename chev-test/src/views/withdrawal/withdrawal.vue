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
                <div class="header-title">Withdrawal</div>
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
                          Withdraw From Special Deposit or Fixed Deposit Account
                        </div>
                      </div>
                      <div>
                        <div class="row">
                          <div class="col-md-12">
                            <div>
                              <b-form class="form">
                                <!-- <b-row class="my-1 form-row mb-3 ">
                                                                <b-col sm="4">
                                                                    <label class="pt-1 form-label" :for="select">Account <code>*</code></label>
                                                                </b-col>
                                                                <b-col sm="8">
                                                                    <b-form-select v-model="selected" :options="accountTypes"></b-form-select>
                                                                     
                                                                </b-col>
                                                            </b-row> -->
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

                                <div class="header_2">Method Of Collection</div>
                                <b-row class="my-1 form-row mb-3 ">
                                  <b-col sm="4">
                                    <label class="pt-1 form-label" :for="select"
                                      >Bank <code>*</code></label
                                    >
                                  </b-col>
                                  <b-col sm="8">
                                    <b-form-select
                                      v-model="selected"
                                      :options="banks"
                                    ></b-form-select>
                                  </b-col>
                                </b-row>
                                <b-row class="my-1 form-row mb-3">
                                  <b-col sm="4">
                                    <label class="pt-1 form-label" :for="Amount"
                                      >Account Number <code>*</code></label
                                    >
                                  </b-col>
                                  <b-col sm="8">
                                    <b-form-input
                                      :id="`Account Number`"
                                      type="number"
                                    ></b-form-input>
                                  </b-col>
                                </b-row>
                                <b-row class="my-1 form-row mb-3">
                                  <b-col sm="4">
                                    <label
                                      class="pt-1 form-label"
                                      :for="Beneficiary"
                                      >Beneficiary Name <code>*</code></label
                                    >
                                  </b-col>
                                  <b-col sm="8">
                                    <b-form-input
                                      disabled
                                      :id="`Beneficiary`"
                                      type="text"
                                    ></b-form-input>
                                  </b-col>
                                </b-row>

                                <b-row class="my-1 form-row mb-3">
                                  <b-col sm="4">
                                    <label
                                      class="pt-1 form-label"
                                      :for="Description"
                                      >Description <code>*</code></label
                                    >
                                  </b-col>
                                  <b-col sm="8">
                                    <b-form-input
                                      :id="`Description`"
                                      type="text"
                                    ></b-form-input>
                                  </b-col>
                                </b-row>
                                <b-row class="my-1 form-row mb-3">
                                  <b-col sm="4">
                                    <label
                                      class="pt-1 form-label"
                                      :for="Description"
                                      >Save Beneficiary
                                    </label>
                                  </b-col>
                                  <b-col sm="8">
                                    <b-form-checkbox
                                      v-model="checked"
                                      name="check-button"
                                      switch
                                    >
                                    </b-form-checkbox>
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
      checked: false,
      accountTypes: [
        { value: 1, text: "Special Deposit " },
        { value: 2, text: "Fixed Deposit " }
      ],
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
      ]
    };
  }
};
</script>

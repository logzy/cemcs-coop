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
                <div class="header-title">Loan</div>
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
                        <div class="header_background">Loan Setup</div>
                      </div>
                      <div>
                        <div class="row">
                          <div class="col-md-12">
                            <div>
                              <b-form class="form">
                                <b-row class="my-1 form-row mb-3 ">
                                  <b-col sm="4">
                                    <label class="pt-1 form-label" :for="concurrent"
                                      >Concurrent Loan<code>*</code></label
                                    >
                                  </b-col>
                                  <b-col sm="8">
                                    <b-form-input
                                      v-model="concurrent"
                                    ></b-form-input>
                                  </b-col>
                                </b-row>
                                <div>
                                  <b-row class="my-1 form-row mb-3 ">
                                    <b-col sm="4">
                                      <label
                                        class="pt-1 form-label"
                                        :for="primary"
                                        >Primary Loan
                                        <code>*</code></label
                                      >
                                    </b-col>
                                    <b-col sm="8">
                                      <b-form-input
                                        v-model="primary"
                                        type="text"
                                      ></b-form-input>
                                    </b-col>
                                  </b-row>
                                  <b-row class="my-1 form-row mb-3">
                                    <b-col sm="4">
                                      <label
                                        class="pt-1 form-label"
                                        >Loans </label
                                      >
                                    </b-col>
                                    <b-col sm="8">
                                      <b-form-input
                                        id="loans"
                                        v-model="loans"
                                        rows="3"
                                        max-rows="6"
                                      ></b-form-input>
                                    </b-col>
                                  </b-row>
                                  <div class="row justify-content-md-center">
                                    <b-button type="submit" variant="primary">ADD</b-button>
                                  </div><br/>
                                </div>
                                <div>
                                    <template>
                                        <div>
                                            <b-table responsive :items="items"></b-table>
                                        </div>
                                    </template>
                                </div><div class="line"></div><br/>                                
                              <div class="row justify-content-md-center">
                                <b-col lg="2" class="pb-2"><b-button type="submit" variant="primary">Submit</b-button></b-col>
                                <b-col lg="2" class="pb-2"><b-button variant="outline-primary">Cancel</b-button></b-col>
                              </div>
                              </b-form>
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

// import Header from "../../components/layout/headers/headerDashboard.vue";
// import NavBar from "../../components/layout/headers/dashboardNav.vue";
import Menu from "../../components/layout/headers/menus.vue";
import RightSidebar from "../../components/layout/sidebar/profile-sidebar.vue";
import Footer from "../../components/layout/footer/footer.vue";
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
      checked: false,
      selectedLoan: "",
      loanType: null,
      items: [
          {
            SN : "1",
            Primary: 'LY',
            Concurrent: 'SCHOOL'
          },
          {
            SN : "2",
            Primary: 'LY',
            Concurrent: 'CAR'       
          },
          {
           SN : "3",
            Primary: 'LY',
            Concurrent: 'HOUSE'
          }
        ]
      
    };
  }
};
</script>

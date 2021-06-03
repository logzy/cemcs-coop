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
                <div class="header-title">LOAN</div>
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
                        <div class="header_background">Create New Loan</div>
                      </div>
                      <div>
                        <div class="row">
                          <div class="col-md-12">
                            <div>
                              <b-form
                                  ref="form"
                                  @submit="onSubmit"
                                  @reset="onReset"
                                >
                                <b-row class="my-1 form-row mb-3 ">
                                  <b-col sm="4">
                                    <label class="pt-1 form-label" :for="code"
                                      >Loan Code <code>*</code></label
                                    >
                                  </b-col>
                                  <b-col sm="8">
                                    <b-form-input
                                      v-model="LoanCode"
                                    ></b-form-input>
                                  </b-col>
                                </b-row>
                                <div>
                                  <b-row class="my-1 form-row mb-3 ">
                                    <b-col sm="4">
                                      <label
                                        class="pt-1 form-label"
                                        :for="Name"
                                        >Loan Name
                                        <code>*</code></label
                                      >
                                    </b-col>
                                    <b-col sm="8">
                                      <b-form-input
                                        v-model="name"
                                        type="text"
                                      ></b-form-input>
                                    </b-col>
                                  </b-row>
                                  <b-row class="my-1 form-row mb-3">
                                    <b-col sm="4">
                                      <label
                                        class="pt-1 form-label"
                                        >Date Description </label
                                      >
                                    </b-col>
                                    <b-col sm="8">
                                      <b-form-textarea
                                        id="description"
                                        v-model="description"
                                        placeholder="Enter description..."
                                        rows="3"
                                        max-rows="6"
                                      ></b-form-textarea>
                                    </b-col>
                                  </b-row>
                                  <b-row class="my-1 form-row mb-3">
                                    <b-col sm="4">
                                      <label
                                        class="pt-1 form-label"
                                        :for="LoanType"
                                        >Loan Type<code>*</code></label
                                      >
                                    </b-col>
                                    <b-col sm="8">
                                      <b-form-select
                                      v-model="loanType"
                                      :options="loanTypes"
                                    ></b-form-select>
                                    </b-col>
                                  </b-row>
                                </div>
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
      checked: false,
      selectedLoan: "",

      loanType: null,
      LoanCode: "",
      Name: "",
      description: "",

      loanTypes: [
        {value: null, text: "Select Loan Type"},
        { value: 1, text: "Target special loan" },
        { value: 2, text: "Executive loan" },
        { value: 3, text: "Regular loan" }
        
        
      ],
      exepctedLumpSumTypes: [
        {value: null, text: "Select"},
        { value: "1", text: "Lump type 1" },
        { value: "2", text: "Lump type 2" },
        { value: "3", text: "Lump type 3" }
      ]
    };
  },

  methods: {

    makeToast(variant = null) {
        this.notify++
        this.$bvToast.toast(`Loan Added`, {
          title: 'Successful',
          variant: variant,
          solid: true,
          autoHideDelay: 5000,
        })
      },

    onReset(event) {
      event.preventDefault();

      this.LoanCode = "",
      this.name = "",
      this.description = "",
      this.loanType = null,

      this.$nextTick(() => {
        this.show = true;
      });
    },
    
    async onSubmit(event) {
      event.preventDefault();
      // // Exit when the form isn't valid
      // if (!this.checkFormValidity()) {
      //   return;
      // }
      let data = {
        LoanCode : this.LoanCode,
        Name : this.name,
        Description : this.description,
        Active: true,
        LoanType : this.loanType
      }      
      data = JSON.stringify(data);
      await axios
        .post(`${process.env.VUE_APP_API_URL}/Loans`, data, {
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

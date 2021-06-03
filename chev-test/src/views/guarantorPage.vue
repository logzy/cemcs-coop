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
                <div class="header-title">Request</div>
              </div>
              <div class="date">
                <font-awesome-icon icon="clock" />
                <div class="date-item ml-2">{{new Date().utc | humanize}}</div>
              </div>
            </div>
            <div class="line"></div>
            <div class="row">
              <div class="col-md-12">
                <div class="overview-board">
                  <div class="account-overview">
                    <div class="account-overview-content">
                        <div class="profile-image">
                        <p class="profile-name"><strong>
                        <h6> CEMCS Member with the details below has requested that you be His/Her Guarantor for the Loan Amount specifield</h6>
                            </strong>
                        </p><br/>
                               <h3> <code>Requested Loan Amount : {{ LoanAppDetails.loanAmount | numberFormat}} {{LoanAppDetails.loanAmount | NumbersToWords | capitalize}} Naira Only</code></h3>
                        </div>
                        <div class="row justify-content-md-center">
                        <div class=col-md-8>
                                <b-table-simple striped hover>
                                    <b-tr>
                                        <b-th >
                                            Member's Employee Number : {{LoanAppDetails.member.employeeNumber}}
                                        </b-th>
                                    </b-tr>
                                    <b-tr>
                                        <b-th>
                                            Member Full-Name : {{LoanAppDetails.member.person.firstName}}
                                        </b-th>
                                    </b-tr> 
                                    <b-tr>
                                        <b-th >
                                            Member Extention/Mobile : {{LoanAppDetails.member.person.workPhone}}
                                        </b-th>
                                    </b-tr>
                                    <b-tr>
                                        <b-th>
                                           Member Email Address : {{LoanAppDetails.member.person.email}}
                                        </b-th>
                                    </b-tr>                                                 
                                </b-table-simple>
                                <div>
                                  <b-alert
                                    :show="dismissCountDown"
                                    dismissible
                                    :variant="dyn"
                                    @dismissed="dismissCountDown=0"
                                    @dismiss-count-down="countDownChanged">
                                    {{this.errors}}
                                  </b-alert>
                                  <b-form>
                                  <b-form-textarea
                                    id="textarea"
                                    v-model="comment"
                                    placeholder="Type in comment..."
                                    rows="3"
                                    max-rows="6"
                                  ></b-form-textarea>
                                  </b-form>
                                </div><br/>
                                <div class="form-buttons">
                          <b-button
                            type="submit"
                            @click="reject"
                            variant="primary"
                            >Reject</b-button
                          >
                          <b-button
                            type="submit"
                            @click="approve"
                            variant="primary"
                            >Approve</b-button
                          >
                                </div>                         
                            </div>
                            </div>
                            <!-- <div class="profile-image">
                                <span><a href="http://localhost:8080">Click here to accept this request</a></span>
                            </div> -->
                    </div>
                  </div>
                </div>
              </div>
              <!-- <div class="col-md-4">
                <div class="dashboard-right-side-bar">
                  <div class="header_2">Profile</div>
                  <RightSidebar></RightSidebar>
                </div>
              </div> -->
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
import Menu from "../components/layout/headers/menus.vue";
import Footer from "../components/layout/footer/footer.vue";

import axios from "axios";



export default {
  name: "Home",
  components: {
    Menu,
    Footer,
  },
  data () {
    return {
      dismissSecs: 5,
      dismissCountDown: 0,
      dyn:"success",
      comment: null,      
      LoanApplicationId: "",
      errors: "",
      msg:"",
      code:"",
      amount:"",
      loanAmount: "",
      employeeNumber:"",
      firstName: "",
      lastName: "",
      middleName: "",
      email:"",
      workPhone: "",
      LoanAppDetails:{}      
      // member:{
      //     person: {        
      //     firstName: "",
      //     lastName: "",
      //     middleName: "",
      //     email:"",
      //     workPhone: ""
      //   },
      // },
      
    }
  },
  computed: {
    // LoanAppDetails() {
    //     return this.$store.state.AppLoanId
    // }
  },
  async created () {
    this.code = atob(this.$route.query.code);    
    this.guarantorNo = this.code.split(':')[1];
    this.LoanId = this.code.split(':')[0];
    this.memberId = this.code.split(':')[2];    
    

    await this.loanAppId();
    // axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('token')}`
    // this.$store.dispatch('getLoanApplication',this.LoanId)

    this.amount = this.LoanAppDetails.loanAmount;
  },
  methods: {

        countDownChanged(dismissCountDown) {
        this.dismissCountDown = dismissCountDown
      },

      async reject() {

        if (this.comment == null) {
          this.dyn = "danger"                   
          this.dismissCountDown = this.dismissSecs
          return this.errors = " Please Kindly leave a comment for Rejection" 
        }

          let rawData = {
          LoanApplicationId : parseInt(this.LoanId),
          EmployeeNumber: this.guarantorNo.toString(),
          Comments: this.comment,
          Status: 2
        }
        rawData = JSON.stringify(rawData);
        await axios
          .post(
            `${process.env.VUE_APP_API_URL}/Guarantors/approve`,
            rawData,
            {
              headers: {
                'Content-Type': 'application/json;charset=utf-8'
              },
            }
          )
          .then((response) => {
            this.dyn = "success"
            this.errors = response.data.data
          this.dismissCountDown = this.dismissSecs
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

    async approve() {
      let rawData = {
        LoanApplicationId : parseInt(this.LoanId),
        EmployeeNumber: this.guarantorNo.toString(),
        Comments: this.comment,
        Status: 1
      }
      rawData = JSON.stringify(rawData);
      await axios
        .post(
          `${process.env.VUE_APP_API_URL}/Guarantors/approve`,
          rawData,
          {
            headers: {
              'Content-Type': 'application/json;charset=utf-8'
            },
          }
        )
        .then((response) => {
          this.dyn="success"
          this.errors = response.data.data
          this.dismissCountDown = this.dismissSecs
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

    async loanAppId() {        
     await axios
        .get(`${process.env.VUE_APP_API_URL}/Loans/${this.LoanId}`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.LoanAppDetails = response.data.data;
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

  }
};
</script>

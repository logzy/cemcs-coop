<template>
  <div class="dashboard">
    <div class="background-design"></div>
    <!-- <Header></Header> -->
    <!-- <Menu></Menu> -->
    <header class="sticky-top headernav">
			<nav class="navbar navbar-expand-lg navbar-light container">
				<a class="navbar-brand" href="#"><img src="../../assets/images/chevron-cemcs.svg"/></a>
				<button
					class="navbar-toggler"
					type="button"
					data-toggle="collapse"
					data-target="#navbarSupportedContent"
					aria-controls="navbarSupportedContent"
					aria-expanded="false"
					aria-label="Toggle navigation"
				>
					<span class="navbar-toggler-icon"></span>
				</button>

				<div class="collapse navbar-collapse" id="navbarSupportedContent">
					<ul class="navbar-nav ml-auto ">
						<li class="nav-item ">
							<a class="nav-link" href="#">HOME</a>
						</li>
						<li class="nav-item ">
							<a class="nav-link" href="#">ABOUT US</a>
						</li>
						<li class="nav-item ">
							<a class="nav-link" href="#">CONTACT US</a>
						</li>
						<li class="nav-item ">
							<a class="nav-link" href="#">NEWS</a>
						</li>
						<li class="nav-item ">
							<a class="nav-link" href="/login">LOGIN</a>
						</li>
						<li class="nav-item register_btn">
							<a class="nav-link" href="/new-account">Register Now</a>
						</li>
					</ul>
				</div>
			</nav>
		</header>
    <div class="container">
      <!-- <NavBar></NavBar> -->
      <div class="row">

        <div class="col-md-12">
          <div class="main-dashboard">
            <div class="dashboard-greeting">
              <div class="overview-text">
                <font-awesome-icon icon="exchange-alt" />
                <div class="header-title">Registration</div>
              </div>
              <div class="date">
                <font-awesome-icon icon="clock" />
                <div class="date-item ml-2">{{new Date().toLocaleString() | humanize}}</div>
              </div>
            </div>
            <div class="line"></div>
            <div class="row">              
              <div class="col-md-12">
                <!-- <div class="row justify-content-md-center"> -->
                <div class="overview-board">
                  <div class="account-overview">
                    <div class="account-overview-content">
                      <div class="mt-2">
                        <div class="header_background">Form Details</div>
                      </div>
                      <div class="row">
                        <div class="col-md-8 mx-auto col-xs-12" style="position: relative;">
                          <div>
                            <b-form
                              ref="form"
                              @submit="onSubmit"
                              @reset="onReset"
                              v-if="show"
                            >
                            <strong>
                            <b-form-group
                                label-cols="4"
                                id="input-group-3"
                                label="Member Type"
                                label-for="input-sm"
                                label-cols-lg="3"
                                label-size="sm"
                              >
                                <b-form-select
                                  id="input-3"
                                  v-model="login.userTypeCategory"
                                  :options="members"
                                  disabled
                                ></b-form-select
                                ></b-form-group>
                                <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Employee Number"
                                label-for="input-sm"
                              >
                                <b-form-input
                                  id="name-input"
                                  v-model="login.userName"
                                  disabled
                                  required
                                ></b-form-input>
                                </b-form-group>

                                <b-form-group
                                label-cols="4"
                                id="input-group-3"
                                label="Location"
                                label-for="input-sm"
                                label-cols-lg="3"
                                label-size="sm"
                                invalid-feedback="Location is required"
                                :state="nameState"
                              >
                                <b-form-select
                                  id="input-3"
                                  :state="nameState"
                                  v-model="form.location"
                                  :options="locations"
                                  required
                                ></b-form-select
                                ></b-form-group>

                                <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Date Of Employment"
                                label-for="example-datepicker"
                                invalid-feedback="Date of Employment is required"
                                :state="nameState"
                              >
                            
                                <!-- <b-form-datepicker
                                  id="example-datepicker"
                                  v-model="form.DoB"
                                  :state="nameState"
                                  required
                                ></b-form-datepicker> -->

                                <date-picker class='input-group date down' :state="nameState"
                                 v-model="form.empDate" :config="options"></date-picker>
                                </b-form-group> {{this.form.empDate}}


                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label-for="input-sm"> 
                                <span v-if="savings.minimumSavingsAmount !==''">
                                <code>Minimum Savings of ₦{{savings.minimumSavingsAmount | numberFormat}}
                                   ({{savings.minimumSavingsAmount | NumbersToWords | capitalize}}) Naira Only
                                </code></span>
                                </b-form-group>
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Monthly Deduction"
                                label-for="input-sm"
                                invalid-feedback="Monthly Deduction is required"
                                :state="mDeduct"
                              >
                                <b-form-input
                                  id="input-1"
                                  v-model="form.minSaving"
                                  :formatter="numberFormat"
                                  :state="mDeduct"
                                  required
                                ></b-form-input>
                                </b-form-group>
                                <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label-for="input-sm"> 
                                 <span v-if="form.minSaving!= ''"><code>
    {{parseFloat(this.form.minSaving.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
                                </b-form-group>
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                id="input-group-3"
                                label="First Name"
                                label-for="input-sm"
                                invalid-feedback="First Name is required"
                                :state="fname"
                              >
                                <b-form-input
                                  id="name-input"
                                  v-model="form.fname"
                                  :state="fname"
                                  required
                                ></b-form-input
                                ></b-form-group>
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Middle Name"
                                label-for="input-sm"
                                :state="nameState"
                              >
                                <b-form-input
                                  id="name-input"
                                  v-model="form.mname"
                                  :state="nameState"
                                ></b-form-input
                                ></b-form-group>
                              <b-form-group
                                label-cols="4"
                                id="input-group-3"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Last Name"
                                label-for="input-sm"
                                invalid-feedback=" Last Name is required"
                                :state="nameState"
                              >
                                <b-form-input
                                  id="name-input"
                                  v-model="form.lname"
                                  :state="nameState"
                                  required
                                ></b-form-input
                                ></b-form-group>
                              <b-form-group
                                label-cols="4"
                                id="input-group-3"
                                label="Gender"
                                label-for="input-sm"
                                label-cols-lg="3"
                                label-size="sm"
                                invalid-feedback="Gender is required"
                                :state="nameState"
                              >
                                <b-form-select
                                  id="input-3"
                                  v-model="form.sex"
                                  :options="gender"
                                  :state="nameState"
                                  required
                                ></b-form-select
                                ></b-form-group>
                              <b-form-group
                                label-cols="4"
                                id="input-group-3"
                                label="Marital Status"
                                label-for="input-sm"
                                label-cols-lg="3"
                                label-size="sm"
                                invalid-feedback="Status is required"
                                :state="nameState"
                              >
                                <b-form-select
                                  id="input-3"
                                  v-model="form.marital"
                                :state="nameState"
                                  :options="maritals"
                                  required
                                ></b-form-select
                                ></b-form-group>
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Date Of Birth"
                                label-for="example-datepicker"
                                :state="nameState"
                              >
                                <!-- <b-form-datepicker
                                  id="example-datepicker"
                                  v-model="form.DoB"
                                  :state="nameState"
                                  required
                                ></b-form-datepicker > -->
                                <date-picker class='input-group date down'
                                :state="nameState"
                                 v-model="form.DoB"  :config="options"></date-picker>
                                </b-form-group>
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Work Email address"
                                label-for="input-sm"
                                invalid-feedback="Email Address is required"
                                :state="nameState">
                                <span class="input-group-addon" id="basic-addon1"><span class="fa fa-envelope"></span></span>
                                <b-form-input
                                  id="name-input"
                                  v-model="login.email"
                                  type="email" disabled
                                  :state="nameState"
                                  required
                                ></b-form-input
                                ></b-form-group>
                                <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Personal Email address"
                                label-for="input-sm"
                              >
                                <b-form-input
                                  id="name-input"
                                  v-model="form.Personalemail"
                                  type="Email"
                                ></b-form-input
                                ></b-form-group>
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Phone Extention"
                                label-for="input-sm"
                                :state="nameState"
                              >
                                <b-form-input
                                  id="name-input"
                                  v-model="form.workPhone"
                                  :state="nameState"
                                ></b-form-input
                                ></b-form-group>

                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Mobile Number"
                                label-for="input-sm"
                              >
                                <b-form-input
                                  id="name-input"
                                  v-model="form.mobileNo"
                                ></b-form-input
                                ></b-form-group>
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Address 1"
                                label-for="input-sm"
                                invalid-feedback="Address is required"
                                :state="nameState"
                              >
                                <b-form-input
                                  id="name-input"
                                  v-model="form.address1"
                                  :state="nameState"
                                  required
                                ></b-form-input
                                ></b-form-group>
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Address 2"
                                label-for="input-sm"
                              >
                                <b-form-input
                                  id="name-input"
                                  v-model="form.address2"
                                ></b-form-input
                                >
                                </b-form-group>
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="State"
                                label-for="input-sm"
                                invalid-feedback="State is required"
                                :state="nameState"
                              >
                                <b-form-select
                                  id="name-input"
                                  v-model="form.state"
                                  :options="states"
                                  :state="nameState"
                                  required
                                ></b-form-select
                                ></b-form-group>
                              <b-form-group
                                label-cols="3"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Country"
                                label-for="input-sm"
                                invalid-feedback="Country is required"
                                :state="state"
                              >
                                <b-form-select
                                  id="name-input"
                                  v-model="form.country"
                                  :options="countrys"
                                  :state="state"
                                ></b-form-select
                                ></b-form-group>
                            </strong>
                              <div class="row justify-content-md-center">
                                <b-button type="submit" variant="primary">Submit</b-button>
                              </div>
                            </b-form>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                <!-- </div> -->
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
      </div>
      <Footer></Footer>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import Footer from "../../components/layout/footer/footer.vue";
import axios from "axios";
// import moment from 'moment';

export default {
  name: "Home",
  components: {
    Footer,
  },
  data() {
    return {
      checked: false,
        options: {
          format: 'YYYY-MM-DD',
          useCurrent: false,
          showClear: true,
          showClose: true,
        },
      nameState:null,
      state:null,
      selectedLoan: "",
      show: true,
      notify: 0,
      savings: {
        minimumSavingsAmount: 0
      },
      form: {
        fname: "",
        mname: "",
        lname: "",
        title: null,
        sex: null,
        marital: null,        
        DoB: new Date(),
        empDate : new Date(),
        email: "",
        Personalemail:"",
        location: null,
        workPhone: "",
        mobileNo: "",
        address1: "",
        address2: "",
        state: null,
        country: null,
        createdBy: null,
        LastModifiedBy: "",    
        EmployeeNo: "",
        MemberType: "",
        minSaving: 0,
      },
      login: {        
        userName:"",
        userType:0,
        userTypeCategory:0
      },
      maritals: [{ text: "Select Status", value: null }, "Single", "Married"],
      gender: [{ text: "Select Gender", value: null }, "Female", "Male"],
      members: [
        { text: "Regular", value: 1, disabled: true },
        { text: "Retiree", value: 2, disabled: true },
        { text: "Expatriate", value: 3, disabled: true }
      ],
      locations: [
        { text: "Select Location", value: null },
        { text: "Lagos", value: "Lagos" },
        { text: "Escravos", value: "Escravos" },
      ],
      states: [
        { text: "Select State", value: null },
        { text: "Lagos", value: 1 },
        { text: "FCT", value: 2 },
        { text: "Ondo", value: 3 },
        { text: "Imo", value: 4 }
      ],
      countrys: [
        { text: "Select Country", value: null },
        { text: "Nigeria", value: "Nigeria" },
        { text: "Ghana", value: "Ghana" },
        { text: "USA", value: "USA" }
      ],
    };
  },
  mounted () {
        document.title = "Register | Chevron CEMCS Corporate"
    },
    computed: {
      mDeduct() {
        return this.form.minSaving.length >= 5
      },
      invalidFeedback() {
        if (this.name.length > 0) {
          return 'Enter at least 5 figures.'
        }
        return 'Please enter something.'
      }
    },
  async created() {
    await this.loginDetails();
    await this.initMinSavings();
  },
  methods: {

    numberFormat(value) {
        this.points = Number(value.replace(/\D/g, ''))
        return value =='0.00' ? '' : this.points.toLocaleString();
      },
    
    async loginDetails() {        
     await axios
        .get(`${process.env.VUE_APP_API_URL}/Members/loginId`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.login = response.data;
        })
        .catch((error) => {
          this.$bvToast.toast(error.message ,{
                title: "Warning",
                variant: "warning",
                solid: true,
                autoHideDelay: 5000
            })
        });
    },

    makeToast(variant = null) {
      this.notify++;
      this.$bvToast.toast(`Member Added`, {
        title: "Successful",
        variant: variant,
        solid: true,
        autoHideDelay: 5000
      });
    },     
    checkFormValidity() {
      const valid = this.$refs.form.checkValidity();
      this.nameState = valid;
      return valid;
    },
    onReset(event) {
      event.preventDefault();

      this.nameState = null;
      this.form.fname = "";
      this.form.mname = "";
      this.form.lname = "";
      this.form.title = "";
      this.form.sex = null;
      this.form.marital = null;
      this.form.DoB = "";
      this.form.email = "";
      this.form.workPhone = "";
      this.form.mobileNo = "";
      this.form.address1 = "";
      this.form.address2 = "";
      this.form.state = "";
      this.form.country = "";
      this.form.EmployeeNo = "";
      this.form.MemberType = null;

      this.show = false;
      this.$nextTick(() => {
        this.show = true;
      });
    },
    async onSubmit(event) {
      event.preventDefault();
      // Exit when the form isn't valid
      if (!this.checkFormValidity()) {
        return;
      }

      let rawData = {
        EmployeeNumber: this.login.userName,
        MemberType: parseInt(this.login.userTypeCategory),
        EmploymentDate: this.form.empDate,
        Location: this.form.location,
        SavingsAmount: parseInt(this.form.minSaving.replace(/,/g, '')),
        Person: {
          FirstName: this.form.fname,
          LastName: this.form.lname,
          MiddleName: this.form.mname,
          Sex: this.form.sex,
          MaritalStatus: this.form.marital,
          DateOfBirth: this.form.DoB,
          Email: this.login.email,
          Personalemail:this.form.Personalemail,
          WorkPhone: this.form.workPhone,
          MobileNumber: this.form.mobileNo,
          Address1: this.form.address1,
          Address2: this.form.address2,
          StateId: this.form.state,
          Country: this.form.country
        }
      };
      rawData = JSON.stringify(rawData);
      await axios
        .post(`${process.env.VUE_APP_API_URL}/Members/Register`, rawData, {
          headers: {
            "Content-Type": "application/json;charset=utf-8",
             Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        })
        .then((response) => {
          this.rawData = response.data;
          this.makeToast(`success`);
          if (this.form.MemberType != 2){       
            window.history.length > this.$router.
            push(`/payment`);
          }
           this.$router.push(`/login`);
        })
        .catch((error) => {
          this.$bvToast.toast(error.message ,{
                title: "Warning",
                variant: "warning",
                solid: true,
                autoHideDelay: 5000
            })
        });
    },
    async initMinSavings() {        
     await axios
        .get(`${process.env.VUE_APP_API_URL}/MinSavings/${this.login.userTypeCategory}`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          }
        })
        .then(response => {
          this.savings = response.data;
        })
        .catch(error => {
         this.$bvToast.toast(error ,{
                title: "Warning",
                variant: "warning",
                solid: true,
                autoHideDelay: 5000
            })
        });
    },
    
  },
};
</script>

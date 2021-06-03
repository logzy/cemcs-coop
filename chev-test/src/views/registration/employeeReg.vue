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
                <div class="header-title">Employee</div>
              </div>
              <div class="date">
                <font-awesome-icon icon="clock" />
                <div class="date-item ml-2">{{ currentDateTime() }}</div>
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
                        <div class="header_background">Employee Registration</div>
                      </div>
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
                      label="Job Title"
                      label-for="input-sm"
                      invalid-feedback="Job title is required"
                    >
                      <b-form-input
                        id="name-input"
                        v-model="form.JobTitle"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="First Name"
                      label-for="input-sm"
                      invalid-feedback="Name is required"
                    >
                      <b-form-input
                        id="name-input"
                        v-model="form.fname"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="Middle Name"
                      label-for="input-sm"
                    >
                      <b-form-input
                        id="name-input"
                        v-model="form.mname"
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="Last Name"
                      label-for="input-sm"
                      invalid-feedback=" Last Name is required"
                    >
                      <b-form-input
                        id="name-input"
                        v-model="form.lname"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="Title"
                      label-for="input-sm"
                    >
                      <b-form-select
                        id="input-3"
                        v-model="form.title"
                        :options="titles"
                      ></b-form-select
                      ><br
                    /></b-form-group>                   

                    <b-form-group
                      label-cols="4"
                      id="input-group-3"
                      label="Department"
                      label-for="input-3"
                      label-cols-lg="2"
                      label-size="sm"
                      invalid-feedback="Department is required"
                    >
                      <b-form-select
                        id="input-3"
                        v-model="form.dept"
                        required
                      >
                      <b-form-select-option :value="null" disabled>
                            -- Select Department -- 
                          </b-form-select-option>
                          <b-form-select-option 
                          v-for="item in departments" 
                          :value="item.id"
                          :key="item.id">
                            {{item.description}} 
                        </b-form-select-option>
                      
                      </b-form-select
                      ><br
                    /></b-form-group>
                    

                    <b-form-group
                      label-cols="4"
                      id="input-group-3"
                      label="Gender"
                      label-for="input-3"
                      label-cols-lg="2"
                      label-size="sm"
                      invalid-feedback="Gendr is required"
                      :state="nameState"
                    >
                      <b-form-select
                        id="input-3"
                        v-model="form.sex"
                        :options="gender"
                        required
                      ></b-form-select
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      id="input-group-3"
                      label="Marital Status"
                      label-for="input-3"
                      label-cols-lg="2"
                      label-size="sm"
                      invalid-feedback="Status is required"
                    >
                      <b-form-select
                        id="input-3"
                        v-model="form.marital"
                        :options="maritals"
                        required
                      ></b-form-select
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="Date Of Birth"
                      label-for="example-datepicker"                      
                      invalid-feedback="Date of Birth is required"
                      :show-decade-nav ="showDecadeNav"
                    >
                      <b-form-datepicker
                        id="example-datepicker"
                        v-model="form.DoB"
                        required
                      ></b-form-datepicker
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="Email address"
                      label-for="input-sm"
                      invalid-feedback="Email is required"
                    >
                      <b-form-input
                        id="name-input"
                        v-model="form.email"
                        type="email"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="Date Of Hire"
                      label-for="example-datepicker1"                      
                      invalid-feedback="Date of Hire is required"
                      :show-decade-nav ="showDecadeNav"
                    >
                      <b-form-datepicker
                        id="example-datepicker1"
                        v-model="form.DateOfHire"
                        required
                      ></b-form-datepicker
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="Annual Salary"
                      label-for="input-sm"
                      invalid-feedback="Annual Salary No is required"
                    >
                      <b-form-input
                        id="name-input"
                        v-model="form.AnnualSalary"
                        placeholder="0.00"
                        type= "number"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>


                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="Basic Salary"
                      label-for="input-sm"
                      invalid-feedback="Basic Salary No is required"
                    >
                      <b-form-input
                        id="name-input"
                        v-model="form.BasicSalary"
                        placeholder="0.00"
                        type= "number"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="Work Phone"
                      label-for="input-sm"
                    >
                      <b-form-input
                        id="name-input"
                        v-model="form.workPhone"
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="Mobile Number"
                      label-for="input-sm"
                      invalid-feedback="Mobile No is required"
                    >
                      <b-form-input
                        id="name-input"
                        v-model="form.mobileNo"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="Address 1"
                      label-for="input-sm"
                      invalid-feedback="Address is required"
                    >
                      <b-form-input
                        id="name-input"
                        v-model="form.address1"
                        required
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="Address 2"
                      label-for="input-sm"
                    >
                      <b-form-input
                        id="name-input"
                        v-model="form.address2"
                      ></b-form-input
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="State"
                      label-for="input-sm"
                      invalid-feedback="State is required"
                    >
                      <b-form-select
                        id="name-input"
                        v-model="form.state"
                        required
                      >

                      <b-form-select-option :value="null" disabled>
                            -- Select State -- 
                          </b-form-select-option>
                          <b-form-select-option 
                          v-for="item in states" 
                          :value="item.id"
                          :key="item.id">
                            {{item.name}} 
                        </b-form-select-option>
                      
                      </b-form-select
                      ><br
                    /></b-form-group>

                    <b-form-group
                      label-cols="4"
                      label-cols-lg="2"
                      label-size="sm"
                      label="Country"
                      label-for="input-sm"
                      invalid-feedback="Country is required"
                    >
                      <b-form-select
                        id="name-input"
                        v-model="form.country"
                        :options="countrys"
                        required
                      ></b-form-select
                      ><br
                    /></b-form-group>                   

                    <b-form-group
                      label-cols="4"
                      id="input-group-3"
                      label="Employee Type"
                      label-for="input-3"
                      label-cols-lg="2"
                      label-size="sm"
                      invalid-feedback="Employee Type is required"
                    >
                      <b-form-select
                        id="input-3"
                        v-model="form.EmployeeType"
                        :options="members"
                        required
                      ></b-form-select
                      ><br
                    /></b-form-group>
                      <div class="row justify-content-md-center">
                        <b-button type="submit" variant="primary">Submit</b-button>
                      </div>                    
                     </b-form>
                      </div>                
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

// import Header from "../../components/layout/headers/headerDashboard.vue";
// import NavBar from "../../components/layout/headers/dashboardNav.vue";
import Menu from "../../components/layout/headers/menus.vue";
// import RightSidebar from "../../components/layout/sidebar/profile-sidebar.vue"
import Footer from "../../components/layout/footer/footer.vue";

import axios from "axios";
export default {
  name: "Home",
  components: {
    // Header,
    // NavBar,
    Menu,
    // RightSidebar,
    Footer
  },
  data() {
    return {
      checked: false,
      show: true,
      notify:0,
      showDecadeNav: true,
      form: {
        JobTitle:"",
        fname: "",
        mname: "",
        lname: "",
        title: null,
        dept: null,
        sex: null,
        marital: null,
        DoB: "",
        email: "",
        DateOfHire:"",
        AnnualSalary:0,
        BasicSalary:0,
        workPhone: "",
        mobileNo: "",
        address1: "",
        address2: "",
        state: null,
        country: null,
        createdBy: null,
        LastModifiedBy: "",
        EmployeeNo: "",
        EmployeeType: null
      },
      states:[],
      departments:[],
      titles: [{ text: "Select Title", value: null }, "Mr.", "Mrs."],
      maritals: [{ text: "Select Marital Status", value: null }, "Single", "Married"],
      gender: [{ text: "Select Sex", value: null }, "Female", "Male"],
      members: [
        { text: "Select One", value: null },
        { text: "Full-Time", value: 1 },
        { text: "Part-Time", value: 2 }
      ],
      creators: [{ text: "Select One", value: null }, "Shola", "Temi", "Lolu"],     
      
      countrys: [
        { text: "Select Country", value: null },
        { text: "Nigeria", value: "Nigeria" },
        { text: "Ghana", value: "Ghana" },
        { text: "USA", value: "USA" }
      ]
    };
  },
  async created() {
    await this.initDept();
    await this.initState();
  },
  methods: {
    currentDateTime() {
      const current = new Date();
      const date = current.toDateString(); //+'-'+(current.getMonth()+1)+'-'+current.getDate();
      const time = current.getHours() + ":" + current.getMinutes(); // + ":" //+ current.getSeconds();
      const dateTime = date + " " + time;

      return dateTime;
    },
    makeToast(variant = null) {
        this.notify++
        this.$bvToast.toast(`Member Added`, {
          title: 'Successful',
          variant: variant,
          solid: true,
          autoHideDelay: 5000,
        })
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

      let empData = {
        Username: this.form.email,
        Password: "Admin@01",
        Email: this.form.email,
        EmailConfirmed: true,
        UserType: 3   
      };
      this.$store.dispatch('createAccout', empData)
      .then(() =>{  this.makeToast(`success`);    })

      let rawData = {
          JobTitle :this.form.JobTitle,
          StateOfOriginId:this.form.state,
          DateOfHire: this.form.DateOfHire,
          AnnualSalary: parseInt(this.form.AnnualSalary),
          BasicSalary: parseInt(this.form.BasicSalary),
          DepartmentId : this.form.dept,
          EmployeeTypeId : this.form.EmployeeType,
         Person: {
          FirstName :this.form.fname,
          LastName :this.form.lname,
          MiddleName :this.form.mname,
          Title :this.form.title,
          Sex :this.form.sex,
          MaritalStatus :this.form.marital,
          DateOfBirth :this.form.DoB,
          Email :this.form.email,
          WorkPhone :this.form.workPhone,
          MobileNumber :this.form.mobileNo,
          Address1 :this.form.address1 ,
          Address2 :this.form.address2,
          StateId :this.form.state,
          Country :this.form.country,
        },        
      };
      rawData = JSON.stringify(rawData);
      await axios
        .post(`${process.env.VUE_APP_API_URL}/add/employee`, rawData, {
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
    },
    async initDept() {        
     await axios
        .get(`${process.env.VUE_APP_API_URL}/Department/All`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8"
          }
        })
        .then(response => {
          this.departments = response.data;
        })
        .catch(error => {
          error.alert("Error");
        });
    },
    async initState() {        
     await axios
        .get(`${process.env.VUE_APP_API_URL}/States/All`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8"
          }
        })
        .then(response => {
          this.states = response.data;
        })
        .catch(error => {
          error.alert("Error");
        });
      } 
  }
};
</script>

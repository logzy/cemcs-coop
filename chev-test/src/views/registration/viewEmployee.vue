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
                          <div class="header_background">Employee Details</div>
                        </div>
                        <div style="position:relative; overflow-y:auto; height:400px">
                          <div>
                            <div class="form-buttons">
                              <span></span>
                              <b-button to="/employee" variant="primary" class="float-sm-left">+</b-button>
                            </div>                            
                            <b-table striped hover small :fields="fields" :items="items.data" responsive="sm">                              
                              <template #cell(index)="data">
                                {{ data.index + 1 }}
                              </template>
                              <template #cell(name)="data">
                                <b class="text-info">{{ data.item.person.lastName.toUpperCase() }}</b>,
                                <b>{{ data.item.person.firstName}}</b>
                              </template>
                              <template #cell(jobTitle)="data">
                                {{ data.item.jobTitle }}
                              </template>
                              <template #cell(annual)="data">
                                  {{data.item.annualSalary}}
                              </template>
                              <template #cell(basic)="data">
                                  {{data.item.basicSalary}}
                              </template>
                              <template #cell(email)="data">                                
                                {{ data.item.person.email }}
                              </template>
                            <!-- <template #cell(show_details)="row">
                                <b-button variant="success" size="sm" @click="row.toggleDetails" class="mr-2">Update</b-button><br>
                                <b-button variant="danger" size="sm" @click="row.Details" class="mr-2">Del</b-button>
                            </template> -->
                            <template #cell(show_details)="row">
                              <b-button variant="light" size="sm" @click="row.toggleDetails" class="mr-2">
                                <b-icon icon="pencil-square" class="primary" variant="primary"></b-icon> </b-button> <br/>
                              <b-button variant="light" size="sm" @click="row.toggleDetails" class="mr-2">
                                <b-icon icon="trash" class="danger" @click.native="row.Details" variant="danger"></b-icon></b-button>
                            </template>
                            </b-table>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div v-if="this.userType == 2">
              <div class="col-md-4">
                  <div class="dashboard-right-side-bar">
                  <div class="header_2">Profile</div>
                      
                      <RightSidebar></RightSidebar>
                  </div>
              </div></div>
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
      items: [],  
      fields: [
        {key: 'index', label: 'S/N'},
        { key: 'name', label: 'Full Name' },
        { key: 'jobTitle', label: 'Job Title' },
        { key: 'annual', label: 'Annual Salary' },        
        { key: 'basic', label: 'Basic Salary' },
        { key: 'email', label: 'E-Mail' },
        {key:'show_details', label: 'Action '}
      ],
    };
  },
  async created() {
    await this.initialize();
  },
  methods: {
    currentDateTime() {
      const current = new Date();
      const date = current.toDateString(); //+'-'+(current.getMonth()+1)+'-'+current.getDate();
      const time = current.getHours() + ":" + current.getMinutes(); // + ":" //+ current.getSeconds();
      const dateTime = date + " " + time;

      return dateTime;
    },
    async initialize() {        
     await axios
        .get( `${process.env.VUE_APP_API_URL}/employees`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8"
          }
        })
        .then(response => {
          this.items = response.data;
        })
        .catch(error => {
          error.alert("Error");
        });
    }, 
  }

};
</script>

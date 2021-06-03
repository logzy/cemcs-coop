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
                <div class="header-title">Members</div>
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
                          <div class="header_background">Personal Details</div>
                        </div>
                        <div>
                          <div>
                            <div class="form-buttons">
                              <span></span>
                              <b-button to="/register" variant="primary" class="float-sm-left">+</b-button>
                            </div>
                            <b-table striped hover small :fields="fields" :items="items.data" responsive="sm">                              
                              <template #cell(index)="data">
                                {{ data.index + 1 }}
                              </template>
                              <template #cell(name)="data">
                                <b class="text-info">{{ data.item.person.lastName.toUpperCase() }}</b>,
                                <b>{{ data.item.person.firstName}}</b>
                              </template>
                              <template #cell(Eno)="data">
                                {{ data.item.employeeNumber }}
                              </template>
                              <template #cell(member)="data">
                                <span v-if="data.item.memberType == 1">Regular</span>
                                <span v-if="data.item.memberType == 2">Retiree</span>
                                <span v-if="data.item.memberType == 3">Expatriate</span>
                              </template>
                              <template #cell(email)="data">                                
                                {{ data.item.person.email }}
                              </template>
                            </b-table>
                          </div>
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
      items: [],  
      fields: [
        {key: 'index', label: 'S/N'},
        { key: 'name', label: 'Full Name' },
        { key: 'Eno', label: 'Emp.No' },
        { key: 'member', label: 'Type' },
        { key: 'email', label: 'E-Mail' }
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
        .get( `${process.env.VUE_APP_API_URL}/Members/All`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
             Authorization: `Bearer ${localStorage.getItem('token')}`
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

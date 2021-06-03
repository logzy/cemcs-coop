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
                <div class="header-title">Approval</div>
              </div>                               
              <div class="date">
                <font-awesome-icon icon="clock" />
                <div class="date-item ml-2">{{new Date().toLocaleString() | humanize}}</div>
              </div>
            </div>
            <div class="line"></div>
            <div class="row">
                <div class="col-md-2">
                  <!-- <div class="dashboard-right-side-bar">
                  <div class="header_2">Profile</div>
                  </div> -->
              </div>
                <div class="col-md-8">
                  <div class="overview-board">
                    <div class="account-overview">                      
                      <div class="account-overview-content">
                        <div class="mt-2">
                          <div class="header_background">Approval Details</div>
                        </div>
                        <div style="position:relative; overflow-y:auto; height:400px">
                          <div>
                            <div>
                                <b-row class="my-1 form-row mb-3 ">
                                  <b-col sm="4">
                                    <b-form-select
                                      v-model="selectedLoan"
                                      @change="initapprove(selectedLoan)"
                                      :options="options"
                                    ></b-form-select>
                                  </b-col>
                                </b-row>
                            </div>
                                <b-table striped hover small :fields="fields" :items="approve.data" responsive="sm">                              
                                    <template #cell(index)="data">
                                        {{ data.index + 1 }}
                                    </template>
                                    <template #cell(name)="data">
                                        <b class="text-info">{{ data.item.person.lastName.toUpperCase() }}</b>,
                                        <b>{{ data.item.person.firstName}}</b>
                                    </template>
                                    <template #cell(employeeNumber)="data">
                                        {{ data.item.employeeNumber }}
                                    </template>
                                    <template #cell(active)="data">
                                        <span v-if="!data.item.approved">Not-Approved</span>
                                        <span v-if="data.item.approved">Approved</span>
                                    </template>
                                    <template #cell(memberType)="data">
                                        <span v-if="data.item.memberType === 1">Regular Member</span>
                                        <span v-if="data.item.memberType === 2">Retiree Member</span>
                                        <span v-if="data.item.memberType === 3">Expatriate Member</span>
                                    </template>
                                    <template #cell(show_details)="data">
                                        <span v-if="!data.item.approved">
                                          <b-button variant="primary" @click="updatePending" class="float-sm-left">Approve</b-button>                                    
                                        </span>
                                        <span v-if="data.item.approved">
                                          <b-button variant="primary" @click="updatePending" class="float-sm-left">Pending</b-button>                                    
                                        </span>
                                        </template>
                                </b-table>
                            <!-- </div> -->
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              <div class="col-md-2">
                  <!-- <div class="dashboard-right-side-bar">
                  <div class="header_2">Profile</div>
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

import Menu from "../../components/layout/headers/menus.vue";
import Footer from "../../components/layout/footer/footer.vue";

import axios from "axios";

export default {
  name: "Home",
  components: {
    Menu,
    Footer
  },
  data() {
    return {
      approve: [],      
      approveData: {
        aprroved:Boolean
      },      
      selectedLoan: null,  
      fields: [
        {key: 'index', label: 'S/N'},
        { key: 'name', label: 'Full Name' },
        { key: 'employeeNumber', label: 'Employee No.' },
        { key: 'active', label: 'Status' },        
        { key: 'memberType', label: 'Member Type' },
        {key:'show_details', label: 'Action '}
      ],
      options: [
        {value: null, text: "Select Option"},
        { value: 0, text: "Pending Approval" },
        { value: 1, text: "Approved Members" }
      ]
    };
  },
  async created() {    
    await this.getAll();
  },
  methods: {
    async getAll() {        
     await axios
        .get( `${process.env.VUE_APP_API_URL}/Members/All`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.approve = response.data;
        })
        .catch(error => {
          error.alert("Error");
        });
    }, 
    // async initapprove(selectedLoan) {  
      async initapprove() {        
     await axios
        // .get( `${process.env.VUE_APP_API_URL}/Members/Approved/${selectedLoan}`,{
          // -----for pending------
        .get( `${process.env.VUE_APP_API_URL}/PendingApproval/Members`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.approve = response.data;
        })
        .catch(error => {
          error.alert("Error");
        });
    },

    async updatePending() { 
      let rawData = {
        approved : true
      };
      rawData = JSON.stringify(rawData);       
     await axios
        .post( `${process.env.VUE_APP_API_URL}/Members`, rawData,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.approve = response.data;
        })
        .catch(error => {
          error.alert("Error");
        });
    },


  }

};
</script>

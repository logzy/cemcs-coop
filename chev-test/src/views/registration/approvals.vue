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
                <div class="header-title">Approvers</div>
              </div>
              <div class="date">
                <font-awesome-icon icon="clock" />
                <div class="date-item ml-2">{{new Date().toLocaleString() | humanize}}</div>
              </div>
            </div>
            <div class="line"></div>
            <div class="row">
            <div class="col-md-8">
                <div class="overview-board">
                  <div class="account-overview">
                    <div class="account-overview-content">
                      <div class="mt-2">
                  <div class="header_background">Module Approver</div>
                </div>
                <div class="col-md-12">
                  <strong>
                    <b-form-group
                    label-cols="4"
                    id="input-group-3"
                    label="Module"
                    label-for="input-3"
                    label-cols-lg="2"
                    label-size="sm"
                  >                  
                  <b-form-select
                    :id="`modules`"
                    v-model="modul"
                    required>
                    <b-form-select-option :value="null" disabled>
                      -- Select Module -- 
                    </b-form-select-option>
                    <b-form-select-option 
                    v-for="item in modules.data" 
                    :value="item.id"
                    :key="item.id">
                      {{item.name}} 
                    </b-form-select-option>                      
                  </b-form-select>
                              <br
                  /></b-form-group>
                  </strong>

                  <strong><b-form-group
                    label-cols="4"
                    label-cols-lg="2"
                    label-size="sm"
                    label="No. of Approvers"
                    label-for="input-sm"
                  >
                    <b-form-input
                      id="name-input"
                      type="number"
                      v-model.lazy.trim="approver"
                    ></b-form-input
                    ><br
                  /></b-form-group></strong>

                  <div class="line"></div>

                  <div>
                    <b-table-simple striped hover>
                      <b-tr>
                        <b-th colspan="2">
                          
                          <b-form-group id="input-3" label="Approvers">
                            
                          </b-form-group>
                          <div v-if="approver !== ''">
                            <div id="#dept" v-for="i in (parseInt(this.approver))" :key="i">
                              <b-row class="my-1 form-row mb-3">
                              <b-form-select
                                :id="`dept-${i}`"
                                v-model="department[i]"
                                required>
                                <b-form-select-option :value="null" disabled>
                                  -- Select Department -- 
                                </b-form-select-option>
                                <b-form-select-option 
                                v-for="item in departments" 
                                :value="item.id"
                                :key="item.id">
                                  {{item.description}} 
                                </b-form-select-option>                      
                              </b-form-select>
                              </b-row>
                            </div>
                          </div>
                        </b-th>
                        <b-th colspan="3">
                          <b-form-group id="input-3" label="Level">
                          </b-form-group>
                          <div v-if="approver !== ''">
                            <div id="#level" v-for="i in (parseInt(this.approver))" :key="i">
                              <b-row class="my-1 form-row mb-3">
                              <b-form-input
                                :id="`level-${i}`"
                                v-model="level[i]"
                              ></b-form-input>
                              </b-row>
                            </div>
                          </div>                         
                        </b-th>
                      </b-tr>                
                    </b-table-simple>
                    <div class="row justify-content-md-center">
                      <b-button type="submit" variant="primary">Submit</b-button>
                    </div>
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
            </div>                       -->
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
// import RightSidebar from "../../components/layout/sidebar/profile-sidebar.vue"
import Footer from "../../components/layout/footer/footer.vue";

import axios from "axios";

export default {
  name: "Home",
  components: {
    Menu,
    // RightSidebar,
    Footer
  },
  data() {
    return {
      approver : "",
      dept:"",
      department: [],
      level:[],
      modules: [],
      modul:null,
      fields: ["Approvers", "Level"],      
    };
  },
  async created() {
    await this.initDept();
    await this.initModules();
  },
  methods: {
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

    async initModules() {        
     await axios
        .get(`${process.env.VUE_APP_API_URL}/ModuleApprovers/Modules`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8"
          }
        })
        .then(response => {
          this.modules = response.data;
        })
        .catch(error => {
          error.alert("Error");
        });
    },

    
    async onSubmit(event) {
      event.preventDefault();
      // Exit when the form isn't valid
      if (!this.checkFormValidity()) {
        return;
      }

      let rawData = {
          // department:
          // level:        
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
  }
};
</script>

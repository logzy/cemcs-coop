<template>
  <div>
          <div class="content-header">Aprrovals</div>
                <div >
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
                      type="text"
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
                            <div v-for="item in approversArray" :key="item.id">
                              <b-row class="my-1 form-row mb-3">
                              <b-form-select
                                :id="dept"
                                v-model="item.department"
                                @change="getApprovals(item.department)"
                                required>
                                <b-form-select-option :value="null" disabled>
                                  -- Select Department -- 
                                </b-form-select-option>
                                <b-form-select-option 
                                v-for="department in departments" 
                                :value="department.id"
                                :key="department.id">
                                  {{department.description}}
                                </b-form-select-option>                      
                              </b-form-select>                              
                              <div v-for="pair in approvals" :key="pair.id" >
                                <div v-if="item.department">                                                    
                                  <b-form-checkbox
                                    v-model="item.usernames"
                                    class="mb-3"
                                    :value="pair.person.email"
                                    :name="`check-`"
                                    inline>
                                    {{ pair.person.firstName }}
                                  </b-form-checkbox>
                              </div></div>
                              </b-row>                      
                            </div>
                             selected  is 
                              <pre>{{approversArray}}</pre>                           
                          
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
                    <button style="display:inline-block" class="app-form-button">Submit</button>
                    
                  </div>
                </div>
              </div>
        
</template>

<script>

import axios from "axios";

export default {
  name: "Home",
  data() {
    return {
      approver: 0,
      approversArray: [],
      dept:"",
      id:0,
      department: [],
      depts:[],
      approvals:[],       
      approvalLevel:"",
      person:{
        email:{},
        firstName:{},
      },
      selected:{},
      level:{},
      modules: [],
      modul:null,
      fields: ["Approvers", "Level"],      
    };
  },
  async created() {
    await this.initDept();
    await this.initModules();   
  },
  watch: {
    approver(){
      let arr = [];
      if (this.approver > 0)
      {
        for (let i=0;i<this.approver;i++)
        {
          arr.push({
            usernames: "",
            level:this.level[i]
          });
        }
      }
      this.approversArray = arr;
      console.log(JSON.stringify(this.approversArray))
    }
  },
  computed: {
    // pairs () {
    //   return this.approvals.map((approve) => {
    //     return {
    //       Name: approve.person.firstName,
    //       Email: approve.person.email
    //     }
    //   })
    // },

  // filteredDept() {
  //   console.log("department is", JSON.stringify(this.department))
  //   console.log("departments are", JSON.stringify(this.departments))
    
  //   if (!this.department.length ) {
  //     return this.departments
  //   }else {
  //     let dept = this.departments.filter(department => department.id !== department[department.length-1])
  //     console.log("item dept", JSON.stringify(this.department.id))
  //     console.log("department index is", JSON.stringify(this.department[this.department.length-1]))
      
  //     return dept
  //   }
  // }
},
  methods: {
    async initDept() {        
     await axios
        .get(`${process.env.VUE_APP_API_URL}/Department/All`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`,
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
            this.$bvToast.toast(error, {
                title: "Error",
                variant: "danger",
                solid: true,
                autoHideDelay: 5000
            });
          });
    },

    async getApprovals(deptId) {
      await axios.get(`${process.env.VUE_APP_API_URL}/employee/dept/${deptId}`,{
        headers:{
          "Content-Type": "application/json;charaset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`,
        }
      }).then (response => {
          this.approvals = response.data
          // this.approversArray[ind].email = response.data.person.email
          // this.approversArray[ind].firstName = response.data.person.firstName
          // // + " "+ response.data.person.lastName
          //  console.log("firstName ", JSON.stringify(this.approversArray[ind].firstName))


      }).catch(error => {
          this.$bvToast.toast(error, {
              title: "Error",
              variant: "danger",
              solid: true,
              autoHideDelay: 5000
          });
        });
    },

      getParam(selected) {
      console.log("Approvers array is ", JSON.stringify(this.approverArray.length))
      console.log("Selected array is ", JSON.stringify(selected.length))
     for (let i = 0; i < selected.length; i++) {
      this.approverArray.push({
            usernames : this.selected,
            approvalLevel: this.level
          });
      }
           console.log("Parameters ", JSON.stringify(this.approverArray))
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

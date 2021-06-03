<template>
            <div class="app-admin-section">
            <div class="app-admin-col-1">
            <Leftbar/>
            </div>
            <div class="app-admin-col-2">
            <div class="admin-top-bar">
        <div class="admin-top-bar-left">
          <div @click="goBack"  class="settings-icon"></div>
          <div @click = "switchView('LoanRequest')" class="admin-top-barlinks" :class="[currentTab == 1 ? currentClass : '']">Loan Request</div>
          <div @click = "switchView('History')" class="admin-top-barlinks" :class="[currentTab == 2 ? currentClass : '']">History</div>
           <div @click = "switchView('Waiver')" class="admin-top-barlinks" :class="[currentTab == 3 ? currentClass : '']">Waiver Form</div>
            <div @click = "switchView('Offset')" class="admin-top-barlinks" :class="[currentTab == 4 ? currentClass : '']">Loan Offset</div>
        </div>
        <div class="admin-top-bar-right">
          <div class="admin-topbar-date">{{new Date().toLocaleString() | humanize}}</div>
        </div>
      </div>
        <div v-show="LoanRequest">
        <LoanRequest/> 
        </div>
            <div v-show="History">
           <History/>       
            </div>
             <div v-show="Waiver">
           <Waiver/>       
            </div>
             <div v-show="Offset">
           <Offset/>       
            </div>
    </div>
      <div class="app-admin-col-3">
              <Rightbar />
            </div>
</div>
</template>


<script>
import Leftbar from '../../components/leftbar/leftbar'
import Rightbar from '../../components/rightbar/rightbar'
import LoanRequest from './requestLoan.vue'
import History from './history.vue'
import Waiver from './waiver.vue'
import Offset from './offset.vue'

export default {
  name: "Home",
  components: {
    Leftbar,
    Rightbar,
    LoanRequest,
    History,
    Waiver,
    Offset

  },
  data(){
      return{
        selectedTab: '',
        LoanRequest: true,
        History: false,
        Waiver: false,
        Offset: false,
        currentTab: 1,
        currentClass: 'admin-active-top-link',
      }
  },
methods:{
      goBack(){
    this.$router.go(-1)
  },
    switchView( selected ){
        if(selected == "LoanRequest"){
      
         this.LoanRequest = true
         this.History = false
          this.Waiver = false
           this.Offset = false
           this.currentTab = 1
        }
        else if(selected == 'History') {
             this.LoanRequest = false
         this.History = true
           this.Waiver = false
           this.Offset = false
            this.currentTab = 2
        }
         else if(selected == 'Waiver') {
             this.LoanRequest = false
         this.History = false
           this.Waiver = true
           this.Offset = false
            this.currentTab = 3
        }
         else if(selected == 'Offset') {
             this.LoanRequest = false
         this.History = false
           this.Waiver = false
           this.Offset = true
            this.currentTab = 4
        }

    },

}
}
</script>

<style src=".././global.css"></style>
<template>
            <div class="app-admin-section">
            <div class="app-admin-col-1">
            <Leftbar/>
            </div>
            <div class="app-admin-col-2">
            <div class="admin-top-bar">
        <div class="admin-top-bar-left">
          <div @click="goBack" class="settings-icon"></div>
          <div @click = "switchView('transfer')" class="admin-top-barlinks" :class="[currentTab == 1 ? currentClass : '']">Transfer</div>
          <div @click = "switchView('withdrawal')" class="admin-top-barlinks" :class="[currentTab == 2 ? currentClass : '']">Withdrawal</div>
        </div>
        <div class="admin-top-bar-right">
          <div class="admin-topbar-date">{{new Date().toLocaleString() | humanize}}</div>
        </div>
      </div>
        <div v-show="transferView">
        <Transfer/> 
        </div>
            <div v-show="withdrawalView">
           <Withdrawal/>       
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
import Transfer from './transfer.vue'
import Withdrawal from './withdrawal.vue'
export default {
  name: "Home",
  components: {
    Leftbar,
    Rightbar,
    Transfer,
    Withdrawal
  },
  data(){
      return{
        selectedTab: '',
        transferView: true,
        withdrawalView: false,
        currentTab: 1,
        currentClass: 'admin-active-top-link'
      }
  },
methods:{
        goBack(){
    this.$router.go(-1)
  },
    switchView( selected ){
        if(selected == "transfer"){
        
         this.transferView = true
         this.withdrawalView = false
         this.currentTab = 1
        }
        else if(selected == 'withdrawal') {
             this.transferView = false
         this.withdrawalView = true
         this.currentTab = 2
        }

    },

}
}
</script>

<style src=".././global.css"></style>
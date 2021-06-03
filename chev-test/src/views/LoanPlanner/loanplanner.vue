<template>
            <div class="app-admin-section">
            <div class="app-admin-col-1">
            <Leftbar/>
            </div>
            <div class="app-admin-col-2">
            <div class="admin-top-bar">
        <div class="admin-top-bar-left">
          <div @click="goBack" class="settings-icon"></div>
          <div @click = "switchView('Regular')" class="admin-top-barlinks" :class="[currentTab == 1 ? currentClass : '']">Loan Planner</div>
          <!-- <div @click = "switchView('Target')" class="admin-top-barlinks" :class="[currentTab == 2 ? currentClass : '']">Target</div> -->

        </div>
        <div class="admin-top-bar-right">
          <div class="admin-topbar-date">{{new Date().toLocaleString() | humanize}}</div>
        </div>
      </div>
        <div v-show="regular">
            <Regular/> 
        </div>
        <div v-show="target">
            <Target/>       
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
import Regular from './regular.vue'
import Target from './target.vue'

export default {
  name: "Home",
  components: {
    Leftbar,
    Rightbar,
    Regular,
    Target
  },
  data(){
      return{
        selected: '',
        regular: true,
        target: false,
        currentTab: 1,
        currentClass: 'admin-active-top-link',
      }
  },
methods:{    
    goBack(){
    this.$router.go(-1)
  },
    switchView( selected ){
        if(selected == 'Regular'){      
         this.regular = true
         this.target = false
          this.currentTab = 1
        }
        else if(selected == 'Target') {
           this.regular = false
           this.target = true
            this.currentTab = 2
        }
    },

}
}
</script>

<style src=".././global.css"></style>
<template>
            <div class="app-admin-section">
            <div class="app-admin-col-1">
            <Leftbar/>
            </div>
            <div class="app-admin-col-2">
            <div class="admin-top-bar">
        <div class="admin-top-bar-left">
          <div @click="goBack" class="settings-icon"></div>
          <div @click = "switchView('regular')" class="admin-top-barlinks" :class="[currentTab == 1 ? currentClass : '']">Regular</div>
          <div @click = "switchView('retiree')" class="admin-top-barlinks" :class="[currentTab == 2 ? currentClass : '']">Retiree</div>
          <div @click = "switchView('expatriate')" class="admin-top-barlinks" :class="[currentTab == 3 ? currentClass : '']">Expatriate</div>
          <div @click = "switchView('allLoans')" class="admin-top-barlinks" :class="[currentTab == 4 ? currentClass : '']">All Loans</div>

        </div>
        <div class="admin-top-bar-right">
          <div class="admin-topbar-date">{{new Date().toLocaleString() | humanize}}</div>
        </div>
      </div>
        <div v-show="regular">
            <Regular/> 
        </div>
        <div v-show="retiree">
            <Retiree/>       
        </div>
        <div v-show="expatriate">
            <Expatriate/>       
        </div>
        <div v-show="allLoans">
            <AllLoans/>       
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
import Retiree from './retiree.vue'
import Expatriate from './expatriate.vue'
import AllLoans from './others.vue'

export default {
  name: "Home",
  components: {
    Leftbar,
    Rightbar,
    Regular,
    Retiree,
    Expatriate,
    AllLoans
  },
  data(){
      return{
        selected: '',
        regular: true,
        retiree: false,
        expatriate: false,
        allLoans: false,
        currentTab: 1,
        currentClass: 'admin-active-top-link',
      }
  },
methods:{    
  goBack(){
    this.$router.go(-1)
  },
    switchView( selected ){
        if(selected == 'regular'){      
         this.regular = true
        this.retiree= false
        this.expatriate= false
        this.allLoans= false
        this.currentTab = 1
        }
        else if(selected == 'retiree') {
           this.regular = false
           this.retiree= true
        this.expatriate= false
        this.allLoans= false
          this.currentTab = 2
        }
        else if(selected == 'expatriate') {
           this.regular = false
           this.retiree= false
        this.expatriate= true
        this.allLoans= false
          this.currentTab = 3
        }
        else if(selected == 'allLoans') {
           this.regular = false
          this.retiree= false
        this.expatriate= false
        this.allLoans= true
          this.currentTab = 4
        }
    },

}
}
</script>

<style src=".././global.css"></style>
<template>
         <div style="position: relative;">
         <div class="app-admin-brand" style="text-align:center"><img height="60px" src="../../assets/images/chevron-cemcs-logo.png" /></div>
      <nav>       
      <router-link class= "active-link" to="/user-dashboard"> 
 <div class="admin-col-link-div">
      <div class="admin-col-1-links"></div>
        <div class="admin-col-link-text">Dashboard</div>
      </div>
      </router-link>
      <router-link class="active-link" to="/vehicle">
        <div class="admin-col-link-div">
        <div class="admin-col-1-links"></div>
        <div class="admin-col-link-text">Vehicle Insurance</div>
      </div>
      </router-link>
      <router-link class= "active-link" to="/house">
 <div class="admin-col-link-div">
        <div class="admin-col-1-links"></div>
        <div class="admin-col-link-text">House Insurance</div>
      </div>
      </router-link>
      <router-link class= "active-link" to="#">
        <div class="admin-col-link-div">
        <div class="admin-col-1-links"></div>
        <div class="admin-col-link-text">House Claims</div>
      </div>
      </router-link>
      <!-- <router-link class= "active-link" to="/savings"> 
        <div class="admin-col-link-div">
          <div class="admin-col-1-links"></div>
          <div class="admin-col-link-text">Law Union and Rock Insurance</div>
        </div>
      </router-link>  -->
      </nav>
      <div @click="logout" class="admin-col-logout" style="cursor:pointer"><span class="text-span-3"></span> Logout</div>
    </div>
</template>


<script>
export default {
	data () {
		return {
			userType: localStorage.getItem('userType')
		}
	},
    mounted () {
        document.title = "Chevron CEMCS Corporative"
		document.images = "../../../assets/images/chevron-cemcs.png"
    },
	// computed : {
    //   isLoggedIn : function(){ return this.$store.getters.isAuthenticated}
    // },
	methods: {
        async logout (){
        await this.$store.dispatch('logout')
		.then(() => this.$router.push('/'))        
      }        
    }
};

</script>
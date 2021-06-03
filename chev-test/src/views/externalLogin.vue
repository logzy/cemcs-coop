<template>
  <div>
      <Loader/>
  </div>
</template>
<script>
import axios from "axios";
import Loader from '../components/ui/loader/loader.vue'
export default {
        components: {
        Loader
    },
    data(){
        return {code:"" };
    },
    mounted() {
        this.externalLogin()
    },
    // created() {
    //     this.code = atob(this.$route.query.ext);
    // },
    methods: {
       async externalLogin() {
            let code = this.$route.query.ext
            console.log("fgfg",code)
            let Body = {code: code, ReturnUrl:`${process.evn.BASE_API_URL}/user-dashboard`}
            Body = JSON.stringify(Body);
            try {
                const resp = await axios.post(`${process.env.VUE_APP_API_URL}/Users/ExternalLogin`, Body, {
            headers: {"Content-Type": "application/json;charset=utf-8"}
            })

          if(resp.status == 200){
               const token = resp.data.token
          const userType = resp.data.userType
         await localStorage.setItem('token', token)
         await localStorage.setItem('userType', userType)
         
         if(localStorage.getItem('token')){
              window.location.href = `${process.evn.BASE_API_URL}/user-dashboard`
            // this.$router.push("/user-dashboard")
         }
           
          }
         
            } catch (error) {
                this.$bvToast.toast(error, {
                    title: "Error",
                    variant: "danger",
                    solid: true,
                    autoHideDelay: 5000
                });
            }
        
        },
    }  
};
</script>
<style></style>
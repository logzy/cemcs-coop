import Loader from '../../components/ui/loader/loader.vue'
export default {   
    components: {
        Loader
    },
    data() {
        return {
            loader: false,
            user: {                
                Username: "",
                Password: ""
            },            
            member: "",
            userType : localStorage.getItem('userType')
        };
      },
    //   computed: {
    //       getMemId (){
    //           return this.$store.state.memberId
    //       }
    //   },
    //   async created() {
    //     axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('token')}`
    //     this.$store.dispatch('memberDetails')
    //   },
        methods: {    
            login () {  
                this.loader = true          
            this.$store.dispatch('login', this.user)
           .then(() => {       
            this.loader = false;  

            if (this.$route.query.path){
                let path = this.$route.query.path.toLowerCase()
                console.log("Path", path)
                this.$router.push(path)
             }
             else{
                this.$router.go({path:'/overview'}) 
             }
                 
           })
           .catch(err => 
           { 
            this.loader = false  
            if (err.response && err.response.status == 400)
                this.$bvToast.toast(err.response.data.message, {
                    title: "Warning",
                    variant: "warning",
                    solid: true,
                    autoHideDelay: 5000
                });
                if (err.response && err.response.status == 401)
                this.$bvToast.toast(err.response.data.message, {
                    title: "Warning",
                    variant: "warning",
                    solid: true,
                    autoHideDelay: 5000
                }); 
            }
            )
          },
    
           Emplogin () {            
            this.$store.dispatch('login', this.user)
           .then(() => {           
                     
            //    if (this.$route.query){
            //        let path = this.$route.query.path.toLowerCase()
            //        console.log("Path", path)
            //        this.$router.push(`/${path}`)               
            //        return
            //     }  
                     window.location.replace('/view_approval')               
           })
           .catch(err => 
           { if (err.response && err.response.status == 400)
                this.$bvToast.toast(err.response.data.message, {
                    title: "Warning",
                    variant: "warning",
                    solid: true,
                    autoHideDelay: 5000
                });
                if (err.response && err.response.status == 401)
                this.$bvToast.toast(err.response.data.message, {
                    title: "Warning",
                    variant: "warning",
                    solid: true,
                    autoHideDelay: 5000
                }); 
            }
            )
          },
        }
    };
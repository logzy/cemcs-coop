<template>
    <section class="container">
            <div class="login-main">
                <div class="row no-gutters">
                    <div class="col-md-6 d-none d-lg-block">
                        <div class="login-slide ">
                            <!-- <img src="assets/images/login-slider.jpg"> -->
                            <!-- <b-carousel
                                id="login-carousel"
                                no-animation
                                indicators
                                img-height="500"
                                img-width="500"
                            >
                                <b-carousel-slide
                                    caption="Slider 1"
                                    img-src="../assets/images/login-slider.jpg"
                                ></b-carousel-slide>
                            </b-carousel> -->
                            <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
                                <ol class="carousel-indicators">
                                    <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
                                    <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
                                    <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
                                </ol>
                                <div class="carousel-inner">
                                    <div class="carousel-item active">
                                        <img class="d-block w-100" src="../../assets/images/login-slider.jpg"
                                            alt="First slide">
                                    </div>
                                    <div class="carousel-item">
                                        <img class="d-block w-100" src="../../assets/images/login-slider2.jpg"
                                            alt="Second slide">
                                    </div>
                                    <div class="carousel-item">
                                        <img class="d-block w-100" src="../../assets/images/login-slider.jpg"
                                            alt="Third slide">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <div class="login-account">
                            <h1>Account Login</h1>
                            <p class="mb-3">Authorized login only.</p>
                            <div class="login-inputs">
                                <div class="memberid">
                                    <div class="icon">
                                        <img src="../../assets/images/icons/user.svg">
                                    </div>
                                    <b-form-input type="text"
                                    v-model="user.Username" required placeholder="Member Employee No." />
                                </div>
                                <div class="password">
                                    <div class="icon">
                                        <img src="../../assets/images/icons/secure.svg">
                                    </div>
                                    <b-form-input type="password" required
                                    v-model="user.Password" placeholder="Password"
                                    data-toggle="tooltip" data-placement="right" title="Combination of lowercase,uppercase 
                                    symbol and number"/>
                                </div>
                            </div>
                                <b-button type="submit" @click="login" class="login-btn">LOGIN</b-button>                            
                            <div class="d-flex justify-content-end">
                                <a href="#">Forgot Password?</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
</template>
<script>
// import axios from "axios";
export default {
data() {
    return {
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
        this.$store.dispatch('login', this.user)
       .then(() => {           
                 
           if (this.$route.query){
               let path = this.$route.query.path.toLowerCase()
               console.log("Path", path)
               this.$router.push(`/${path}`)               
               return
            }
        console.log(localStorage.getItem('userType'))
        if ((localStorage.getItem('userType')) == 2) 
                 window.location.replace('/portal')
        if ((localStorage.getItem('userType')) == 3)
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
</script>
<style></style>
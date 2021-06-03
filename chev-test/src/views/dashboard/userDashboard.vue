<template>
  <div class="landing-page" >
        <div class="hero-bg">
            <header class=" headernav2">
                <nav class="navbar navbar-expand-lg navbar-light container">
                    <a class="navbar-brand" href="index.html"><img src="../../assets/images/chevron-cemcs.svg" alt="CEMCS Co-oprative"></a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent"
                        aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
            
                    <div class="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul class="navbar-nav ml-auto ">
                            <li class="nav-item ">
                                <a class="nav-link" href="user.html">HOME</a>
                            </li>
                            <li class="nav-item ">
                                <a class="nav-link" href="aboutus.html">ABOUT US</a>
                            </li>
                            <li class="nav-item ">
                                <a class="nav-link" href="contact.html">CONTACT US</a>
                            </li>
                            <li class="nav-item ">
                                <a class="nav-link" href="covid19.html">COVID-19</a>
                            </li>
                            <li class="nav-item ">
                                <a class="nav-link" href="https://chevron.mykovi.com/news">NEWS</a>
                            </li>
                            <li class="nav-item register_btn">
                                <a class="nav-link" href="register.html">{{ memberLogin.person.firstName+" "+memberLogin.person.lastName }}</a>
                            </li>
                        </ul>
                    </div>
                </nav>
            </header>
            <section class="hero-section mb-5">
                <div class="mt-5"></div>
            </section>
            <section class="container">
                <div class=" icons-grids">
                    <div class="row no-gutters">
                        <div class="col-md-6">
                            <div class="row no-gutters">
                                <div class="col-4">
                                <router-link to="/overview">
                                 <div class="icons-grid orange-bg hvr-bob">
                                        <img style="margin-bottom: 25px;" src="../../assets/images/icons/loans.svg" alt="CEMCS Co-oprative">
                                        <p>LOANS</p>
                                    </div>
                                </router-link> 
                                </div>
                                <div class="col-4">
                                    <router-link to="#">
                                    <div class="icons-grid green-bg hvr-bob">
                                        <img style="margin-bottom: 25px;" src="../../assets/images/icons/ecommerce.svg" alt="CEMCS Co-oprative">
                                        <p>E-COMMERCE</p>
                                    </div>
                                    </router-link>
                                </div>
                                <div class="col-4">
                                    <router-link to="/insurance">
                                    <div class="icons-grid green2-bg hvr-bob">
                                        <img style="margin-bottom: 25px;" src="../../assets/images/icons/insurance.svg" alt="CEMCS Co-oprative">
                                        <p>INSURANCE</p>
                                    </div>
                                    </router-link>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="row no-gutters">
                                <div class="col-4">
                                    <router-link to="#">
                                    <div class="icons-grid blue-bg hvr-bob">
                                        <img style="margin-bottom: 25px;" src="../../assets/images/icons/deals.svg" alt="CEMCS Co-oprative">
                                        <p>DEALS</p>
                                    </div>
                                    </router-link>
                                </div>
                                <div class="col-4">
                                    <router-link to="#">
                                    <div class="icons-grid blue2-bg hvr-bob">
                                        <img style="margin-bottom: 25px;" src="../../assets/images/icons/travels.svg" alt="CEMCS Co-oprative">
                                        <p>TRAVELS</p>
                                    </div>
                                    </router-link>
                                </div>
                                <div class="col-4">
                                    <router-link to="#">
                                    <div class="icons-grid blue3-bg hvr-bob">
                                        <img style="margin-bottom: 25px;" src="../../assets/images/icons/banking.svg" alt="CEMCS Co-oprative">
                                        <p>BANKING</p>
                                    </div>
                                    </router-link>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!-- <div class="arrow-down text-center">
                <a href="#scrolldown">
                    <img src="../../assets/images/icons/arrow-down.svg" alt="" class="bounce">
                </a>
            </div> -->
        </div>

        <section class="deals-advert" id="scrolldown">
            <div class="container">
                <!-- <div class="deals-logo">
                    <img src="../../assets/images/cemcs-deals.svg">
                </div> -->


                <div class="deals-slider">
                    <!-- Owl Carousel Slider -->
                                    <carousel :autoplay="true" :nav="false" :responsive="responsive" :items= "items">

         <img src="../../assets/images/owl/chevron-cemcs-slide1.jpeg" alt="CEMCS Co-oprative">

    <img src="../../assets/images/owl/chevron-cemcs-slide2.jpeg" alt="CEMCS Co-oprative">

     <img src="../../assets/images/owl/chevron-cemcs-slide3.jpeg" alt="CEMCS Co-oprative">
     <img src="../../assets/images/owl/chevron-cemcs-slide4.jpeg" alt="CEMCS Co-oprative">
</carousel>
                    
                </div>
            </div>
        </section>

        <footer>
            <div class="cc">
                <div class="container">
                    <p>Copyright &copy; 2021 Chevron CEMCS Corporative</p>
                </div>
            </div>
        </footer>
      
    </div>
</template>

<script>
import carousel from 'vue-owl-carousel'
export default {
    components: { carousel },
    data () {
      return {
        items: 1,
        firstName:"",
        lastName:"",
      }
    },
    computed: {
  memberLogin() {
    return this.$store.state.member
  }
},
created() {
  this.$store.dispatch('memberDetails');
},
  }
</script>

<style scoped>
a { text-decoration: none;}
        .headernav2 { background: #FFFFFF; box-shadow: 0 2px 23px 0 rgba(0,0,0,0.16);}
        .register_btn { background: none;}
</style>
<template>
    <div class="landing-page">
        <div class="ad_banner">
            <div class="container">
                <div class="ad_banner2">
                    <div></div>
                    <p>Get up to 50% Discounts on Sales and Transactions <span>with our new open platform</span></p>
                    <a href="register.html" class="create_acc_btn">CREATE ACCOUNT</a>
                    <a href="#" class="close_btn">x</a>
                </div>
            </div>
        </div>
        <div class="hero-bg">
            <header class="sticky-top headernav2">
                <nav class="navbar navbar-expand-lg navbar-light container">
                    <a class="navbar-brand" href="index.html"><img src="../assets/images/chevron-cemcs.svg" alt="CEMCS Co-oprative"></a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent"
                        aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
            
                    <div class="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul class="navbar-nav ml-auto ">
                            <li class="nav-item ">
                                <a class="nav-link" href="/">HOME</a>
                            </li>
                            <li class="nav-item ">
                                <a class="nav-link" href="#">ABOUT US</a>
                            </li>
                            <li class="nav-item ">
                                <a class="nav-link" href="#">CONTACT US</a>
                            </li>
                            <li class="nav-item ">
                                <a class="nav-link" href="https://chevron.mykovi.com/news">NEWS</a>
                            </li>
                            <li class="nav-item ">
                                <a class="nav-link" href="/login">LOGIN</a>
                            </li>
                            <li class="nav-item register_btn">
                                <a class="nav-link" href="/new-account">Register Now</a>
                            </li>
                        </ul>
                    </div>
                </nav>
            </header>
            <section class="hero-section">
                <div class="login-register-form">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-4 ml-auto">
                                <div id="login_page" class="login-page">
                                    <div class="form">
                                        <form class="login-account" id="login_form" action="#" method="POST">
                                            <h1>Account Login</h1>
                                            <div class="create-acc-btn">
                                                <p class="message">
                                                    <a href="#">Create an Account</a>
                                                </p>
                                            </div>
                                            <div class="login-inputs">
                                                <div class="memberid">
                                                    <div class="icon">
                                                        <img src="../assets/images/icons/user.svg" alt="CEMCS Co-oprative">
                                                    </div>
                                                    <input type="text" placeholder="Member ID">
                                                </div>
                                                <div class="password">
                                                    <div class="icon">
                                                        <img src="../assets/images/icons/secure.svg" alt="CEMCS Co-oprative">
                                                    </div>
                                                    <input type="text" placeholder="Password">
                                                </div>
                                            </div>
                                            <button class="login-btn">LOGIN</button>
                                            <div class="d-flex justify-content-end">
                                                <a href="#">Forgot Password?</a>
                                            </div>
                                        </form>
                                        <form class="login-account register-form" id="register_form">
                                            <h1>Member Registration</h1>
                                            <!-- <p>Authorized login only.</p> -->
                                            <div class="create-acc-btn">
                                                <p class="message">
                                                    Already have an Account?
                                                    <a href="#">Login</a>
                                                </p>
                                            </div>
                                            <label for="exampleFormControlSelect1">Sign up as:</label>
                                            <div class="login-inputs">
                                                <div class="memberid">
                                                    <select class="form-control" id="exampleFormControlSelect1">
                                                        <option value="retiree">Retiree Member (RM)</option>
                                                        <option value="staff">Staff Member (SM)</option>
                                                        <option value="expatriate">Expatriate Member (EM)</option>
                                                    </select>
                                                    <div class="chevron-down">
                                                        <img src="../assets/images/icons/chevron-down.svg">
                                                    </div>
                                                </div>
                                                <div class="password">
                                                    <div class="icon">
                                                        <img src="../assets/images/icons/user.svg">
                                                    </div>
                                                    <input type="text" placeholder="Member ID">
                                                </div>
                                            </div>
                                            <button class="login-btn">REGISTER NOW</button>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
                    <ol class="carousel-indicators">
                        <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
                        <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
                        <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
                    </ol>
                    <div class="carousel-inner">
                        <div class="carousel-item active">
                            <img class="d-block w-100" src="../assets/images/bg/chevron-slide.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="carousel-item">
                            <img class="d-block w-100" src="assets/images/bg/chevron-slide2.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="carousel-item">
                            <img class="d-block w-100" src="../assets/images/bg/chevron-slide.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="carousel-item">
                            <img class="d-block w-100" src="../assets/images/bg/chevron-slide2.jpg" alt="CEMCS Co-oprative">
                        </div>
                    </div>
                    <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="sr-only">Previous</span>
                    </a>
                    <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="sr-only">Next</span>
                    </a>
                </div>
            </section>
            <section class="container">
                <div class=" icons-grids">
                    <div class="row no-gutters">
                        <div class="col-md-6">
                            <div class="row no-gutters">
                                <div class="col-4">
                                    <div class="icons-grid orange-bg hvr-bob">
                                        <img src="../assets/images/icons/loans.svg" alt="CEMCS Co-oprative">
                                        <p>LOANS</p>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="icons-grid green-bg hvr-bob">
                                        <img src="../assets/images/icons/ecommerce.svg" alt="CEMCS Co-oprative">
                                        <p>E-COMMERCE</p>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="icons-grid green2-bg hvr-bob">
                                        <img src="../assets/images/icons/insurance.svg" alt="CEMCS Co-oprative">
                                        <p>INSURANCE</p>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="row no-gutters">
                                <div class="col-4">
                                    <div class="icons-grid blue-bg hvr-bob">
                                        <img src="../assets/images/icons/deals.svg" alt="CEMCS Co-oprative">
                                        <p>DEALS</p>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="icons-grid blue2-bg hvr-bob">
                                        <img src="../assets/images/icons/travels.svg" alt="CEMCS Co-oprative">
                                        <p>TRAVELS</p>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="icons-grid blue3-bg hvr-bob">
                                        <img src="../assets/images/icons/banking.svg" alt="CEMCS Co-oprative">
                                        <p>BANKING</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!-- <div class="arrow-down text-center">
                <a href="#scrolldown">
                    <img src="assets/images/icons/arrow-down.svg" alt="" class="bounce">
                </a>
            </div> -->
        </div>

        <section class="deals-advert" id="scrolldown">
            <div class="container">
                <!-- <div class="deals-logo">
                    <img src="assets/images/cemcs-deals.svg">
                </div> -->
                <div class="deals-slider">
                    <!-- Owl Carousel Slider -->
                    <div id="sync1" class="owl-carousel owl-theme">
                        <div class="item">
                            <img src="../assets/images/owl/mtn2.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="item">
                            <img src="../assets/images/owl/glo2.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="item">
                            <img src="../assets/images/owl/9mobile2.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="item">
                            <img src="assets/images/owl/airtel2.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="item">
                            <img src="assets/images/owl/slider-car.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="item">
                            <img src="../assets/images/owl/mtn2.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="item">
                            <img src="../assets/images/owl/9mobile2.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="item">
                            <img src="../assets/images/owl/glo2.jpg" alt="CEMCS Co-oprative">
                        </div>
                    </div>

                    <div id="sync2" class="owl-carousel owl-theme">
                        <div class="item">
                            <img src="../assets/images/owl/mtn.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="item">
                            <img src="../assets/images/owl/glo.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="item">
                            <img src="../assets/images/owl/9mobile.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="item">
                            <img src="assets/images/owl/airtel.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="item">
                            <img src="../assets/images/owl/advert.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="item">
                            <img src="../assets/images/owl/mtn.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="item">
                            <img src="../assets/images/owl/9mobile.jpg" alt="CEMCS Co-oprative">
                        </div>
                        <div class="item">
                            <img src="../assets/images/owl/glo.jpg" alt="CEMCS Co-oprative">
                        </div>
                    </div>
                    <!-- Owl Carousel Slider -->
                </div>
            </div>
        </section>

        <section class="step123-section grey-bg">
            <div class="container">
                <div class="row">
                    <div class="col-md-7 ml-auto mr-auto">
                        <div class="step123-title">
                            <h1>Perspiciatis unde omnis</h1>
                            <p>Sed ut perspiciatis unde omnis iste natus error sit, iste natus error sit Sed Sed ut perspiciatis unde omnis iste natus error sit </p>
                        </div>
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <div class="step123">
                            <div class="step123-icon">
                                <img src="../assets/images/icons/step1.svg" alt="CEMCS Co-oprative">
                            </div>
                            <h5>Sample 1</h5>
                            <h6>Perspiciatis unde omnis</h6>
                            <div class="border-blue"></div>
                            <p>Sed ut perspiciatis unde omnis iste natus error sit, iste natus error sit Sed ut perspiciatis unde omnis iste error sit.</p>
                            
                            <p>Sed ut perspiciatis unde omnis iste natus error sit, iste natus error sit Sed ut perspiciatis unde omnis iste error sit.</p>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="step123">
                            <div class="step123-icon">
                                <img src="../assets/images/icons/step2.svg" alt="CEMCS Co-oprative">
                            </div>
                            <h5>Sample 2</h5>
                            <h6>Perspiciatis unde omnis</h6>
                            <div class="border-blue"></div>
                            <p>Sed ut perspiciatis unde omnis iste natus error sit, iste natus error sit Sed ut perspiciatis unde omnis iste
                                error sit.</p>
                    
                            <p>Sed ut perspiciatis unde omnis iste natus error sit, iste natus error sit Sed ut perspiciatis unde omnis iste
                                error sit.</p>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="step123">
                            <div class="step123-icon">
                                <img src="../assets/images/icons/step3.svg" alt="CEMCS Co-oprative">
                            </div>
                            <h5>Sample 2</h5>
                            <h6>Perspiciatis unde omnis</h6>
                            <div class="border-blue"></div>
                            <p>Sed ut perspiciatis unde omnis iste natus error sit, iste natus error sit Sed ut perspiciatis unde omnis iste
                                error sit.</p>
                    
                            <p>Sed ut perspiciatis unde omnis iste natus error sit, iste natus error sit Sed ut perspiciatis unde omnis iste
                                error sit.</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section class="social-media-panorama">
            <div class="container">
                <div class="row">
                    <div class="col-md-4">
                        <div class="social_media_post">
                            <div class="social_media_title">
                                <h1>Social Media</h1>
                            </div>
                            <div class="facebook_embed">
                                <div class="fb-page" data-href="https://www.facebook.com/chevron/" data-tabs="timeline"
                                    data-width="" data-height="490" data-small-header="false" data-adapt-container-width="true"
                                    data-hide-cover="false" data-show-facepile="true">
                                    <blockquote cite="https://www.facebook.com/chevron/" class="fb-xfbml-parse-ignore"><a
                                            href="https://www.facebook.com/chevron/">Chevron</a></blockquote>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="photorama_slider">
                            <div class="social_media_title">
                                <h1>Photorama</h1>
                            </div>
                            <div class="photorama">
                                <!-- <img src="assets/images/photorama.jpg" alt=""> -->
                                <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
                                    <ol class="carousel-indicators">
                                        <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
                                        <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
                                        <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
                                    </ol>
                                    <div class="carousel-inner">
                                        <div class="carousel-item active">
                                            <img class="d-block w-100" src="../assets/images/photorama.jpg" alt="CEMCS Co-oprative">
                                        </div>
                                        <div class="carousel-item">
                                            <img class="d-block w-100" src="../assets/images/photorama.jpg" alt="CEMCS Co-oprative">
                                        </div>
                                        <div class="carousel-item">
                                            <img class="d-block w-100" src="../assets/images/photorama.jpg" alt="CEMCS Co-oprative">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section class="social-media-panorama grey-bg">
            <div class="container">
                <div class="row">
                    <div class="col-md-7 ml-auto mr-auto">
                        <div class="social_media_title text-center mb-5">
                            <h1>Where you can find us</h1>
                            <p>Sed ut perspiciatis unde omnis iste natus error sit, iste natus error sit Sed Sed ut perspiciatis
                                unde omnis iste natus error sit</p>
                        </div>
                    </div>
                </div>
                <iframe
                    src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3964.6302759851715!2d3.5287287147702933!3d6.441500395339758!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x103bf6f9d75deb93%3A0x2cd900147ad97f5!2sChevron%20Drive%20%26%20Lekki%20-%20Epe%20Expy%2C%20Lekki%20Penninsula%20II%2C%20Lekki!5e0!3m2!1sen!2sng!4v1600535400879!5m2!1sen!2sng"
                    width="100%" height="500" frameborder="0" style="border:0;" allowfullscreen="" aria-hidden="false"
                    tabindex="0"></iframe>
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
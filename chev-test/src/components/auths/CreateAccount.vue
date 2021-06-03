<template>	
    <section class="container">
        <div class="login-main">
            <div class="row no-gutters">
                <div class="col-md-6 d-none d-lg-block">
                    <div class="login-slide ">
                        <!-- <img src="assets/images/login-slider.jpg"> -->
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
                        <h1>Member Registration</h1>
                        <p class="mb-3">Authorized login only.</p>
                        <label for="signUpSelect">Sign up as:</label>
                        <b-form ref="form"
                                @submit="onSubmit"
                                v-if="show"
                                >
                        <div class="login-inputs">                                
                            <div class="memberid">
                                <div class="icon">                                        
                                    <b-icon icon="award-fill" aria-hidden="true"></b-icon>
                                </div>
                                <select class="form-control" v-model="form.UserTypeCategory" 
                                id="exampleFormControlSelect1" required>
                                    <option :value=null>Select Membership Type</option>
                                    <option value= 1>Regular Member (RSM)</option>
                                    <option value= 2>Retiree Member (RM)</option>
                                    <option value= 3>Expatriate Member (EM)</option>
                                </select>
                                <div class="chevron-down">
                                    <img src="../../assets/images/icons/chevron-down.svg">
                                </div>
                            </div>
                            <div class="password">
                                <div class="icon">
                                    <b-icon icon="person-fill"></b-icon>
                                </div>
                                <b-form-input type="number" v-model="form.EmployeeNo" 
                                aria-invalid="Employee Number is required" required placeholder="Employee Number" />
                            </div>
                            <div class="password">
                                <div class="icon">
                                    <b-icon icon="envelope"></b-icon>
                                </div>
                                <b-form-input type="email" v-model="form.email"
                                aria-invalid="Email is required"
                                required placeholder="Email Address" />
                            </div>
                            <div class="password">
                                <div class="icon">
                                    <b-icon icon="lock-fill" aria-hidden="true"></b-icon>
                                </div>                                    
                                <b-form-input id="input-live" v-model="form.password"
                                type="password" :state="password" required
                                placeholder=" Create Password"></b-form-input>                                                                    
                                <!-- <b-form-invalid-feedback id="input-live-feedback">
                                At least an UpperCase, Number and Symbol
                                </b-form-invalid-feedback> -->
                                <!-- description="We'll never share your email with anyone else." -->
                            </div>
                            <div v-if="form.password !== ''">
                                    <code><small id="passwordHelp" class="form-text text-muted">Password should contain
        <span :class="has_lowercase ? 'has_required' : ''">one lowercase letter</span>, 
        <span :class="has_uppercase ? 'has_required' : ''">one uppercase letter</span>, 
        <span :class="has_number ? 'has_required' : ''">One number</span>, and 
        <span :class="has_special ? 'has_required' : ''">one special character.</span></small></code>
                                </div>                             
                        </div>
                        <b-button type="submit" class="login-btn">REGISTER NOW</b-button>
                        </b-form>
                    </div>
                </div>
            </div>
        </div>
    </section>        
</template>

<script>

export default {
    data() {
    return {
        nameState: true,
        show: true,      
        notify: 0,
        has_number: false,
        has_lowercsae: false,
        has_uppercase: false,
        has_special: false,
        form: {
            password: "",
            EmployeeNo: "",
            email: "",
            UserType: null,
            UserTypeCategory: null 
        },
        reg: /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,24}))$/ 
    };
  },
    watch:{
        password(){
            this.has_number    = /\d/.test(this.password);
            this.has_lowercase = /[a-z]/.test(this.password);
            this.has_uppercase = /[A-Z]/.test(this.password);
            this.has_special   = /[!@#%*+=._-]/.test(this.password);
        }
    },
    computed: {
      password() {
        return this.form.password.length >= 8 ? true : false
      }
    },
    methods: {        
        makeToast(variant = null) {
            this.notify++;
            this.$bvToast.toast(`Check your Email for confirmation link`, {
                title: "Successful",
                variant: variant,
                solid: true,
                autoHideDelay: 5000
            });
        },
        isEmailValid() {
      return (this.form.email == "")? "" : (this.reg.test(this.form.email)) ? 'has-success' : 'has-error';
    },
        checkFormValidity() {
        const valid = this.$refs.form.checkValidity();
        this.nameState = valid;
        return valid;
        }, 
    async onSubmit(event) {
      event.preventDefault();
      // Exit when the form isn't valid
      if (!this.checkFormValidity()) {
        return;
      }
      let rawData = {
        Username: this.form.EmployeeNo,
        Password: this.form.password,
        Email: this.form.email,
        UserTypeCategory: parseInt(this.form.UserTypeCategory),
        UserType: 2,
        ReturnUrl: 'http://localhost:8080/confirmation'    
      };
      this.$store.dispatch('createAccout', rawData)
      .then(() =>{  this.makeToast(`success`);
                    // alert(this.$bvModal.msgBoxOk('Action completed'))
                    // this.$router.push('/register')
                    })
        .catch(err => { if (err.response.status == 400)
            this.$bvToast.toast("Kindly Fill-Up the Form", {
                title: "Warning",
                variant: "warning",
                solid: true,
                autoHideDelay: 5000
            });
            if (err.response.status == 401)
            this.$bvToast.toast(err, {
                title: "Warning",
                variant: "warning",
                solid: true,
                autoHideDelay: 5000
            });
            if (err.response.status == 500)
            this.$bvToast.toast(err.response.data.message, {
                title: "Warning",
                variant: "danger",
                solid: true,
                autoHideDelay: 5000
        })}
        )
    },
  },
    
};
</script>

<style></style>

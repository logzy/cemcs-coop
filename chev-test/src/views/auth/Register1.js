import Loader from '../../components/ui/loader/loader.vue'
export default {   
    components: {
        Loader
    },
    data() {
    return {
        loader: false,
        nameState: true,
        show: true,  
        notify: 0,
        has_number: false,
        has_lowercsae: false,
        has_uppercase: false,
        has_special: false,
        form: {
            password: "",
            checkPassword:'',
            EmployeeNo: "",
            email: "",
            UserType: null,
            UserTypeCategory: 0
        },
        rules: [
          { message:'One lowercase letter required.', regex:/[a-z]+/ },
          { message:"One uppercase letter required.",  regex:/[A-Z]+/ },
          { message:"One number required.", regex:/[0-9]+/ },
          { message:'One special character required.', regex:/[!@#%*+=._-]/ }
        ],
        passwordVisible:false,
        submitted:false,
        reg: /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,24}))$/ 
        
      };
  },    
  computed: {
		notSamePasswords () {
			if (this.passwordsFilled) {
        
        console.log(true)
				return (this.form.password !== this.form.checkPassword)
			} else {
				return false
			}
		},
		passwordsFilled () {
			return (this.form.password !== '' && this.form.checkPassword !== '')
		},
		passwordValidation () {
			let errors = []
			for (let condition of this.rules) {
				if (!condition.regex.test(this.form.password)) {
					errors.push(condition.message)
				}
			}
			if (errors.length === 0) {
				return { valid:true, errors }
			} else {
				return { valid:false, errors }
			}
		}
	},
    methods: {
      
      resetPasswords () {
        this.password = ''
        this.checkPassword = ''
        this.submitted = true
        setTimeout(() => {
          this.submitted = false
        }, 2000)
      },
      togglePasswordVisibility () {
        this.passwordVisible = !this.passwordVisible
      },
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
        
    async onSubmit(event) {
      event.preventDefault();
      // Exit when the form isn't valid
      this.loader = true  

      let rawData = {
        Username: this.form.EmployeeNo,
        Password: this.form.password,
        Email: this.form.email,
        UserTypeCategory: parseInt(this.form.UserTypeCategory),
        UserType: 2,
        ReturnUrl: `${process.evn.BASE_API_URL}/confirmation`    
      };
      this.$store.dispatch('createAccout', rawData)
      .then(() =>{  
        this.loader = false;
        this.form.EmployeeNo = ''
         this.form.password = ''
        this.form.email = ''
        this.form.UserTypeCategory = 0

          this.makeToast(`success`);
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
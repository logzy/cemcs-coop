<template>
	<div class="login-page">
		<img src="../../assets/images/login-bg.svg" alt="" id="bg" />
		<header class="sticky-top headernav">
			<nav class="navbar navbar-expand-lg navbar-light container">
				<a class="navbar-brand" href="#"><img src="../../assets/images/chevron-cemcs.svg"/></a>
				<button
					class="navbar-toggler"
					type="button"
					data-toggle="collapse"
					data-target="#navbarSupportedContent"
					aria-controls="navbarSupportedContent"
					aria-expanded="false"
					aria-label="Toggle navigation"
				>
					<span class="navbar-toggler-icon"></span>
				</button>

				<div class="collapse navbar-collapse" id="navbarSupportedContent">
					<ul class="navbar-nav ml-auto ">
						<li class="nav-item ">
							<a class="nav-link" href="#">HOME</a>
						</li>
						<li class="nav-item ">
							<a class="nav-link" href="#">ABOUT US</a>
						</li>
						<li class="nav-item ">
							<a class="nav-link" href="#">CONTACT US</a>
						</li>
						<li class="nav-item ">
							<a class="nav-link" href="#">NEWS</a>
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
        <section class="container">
            <div class="login-main">
                <div class="row no-gutters">
                    <div class="col-md-6">
                         <div class="login-slide ">
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
                    <div class="col-md-6">
                        <div class="login-account">
                            <h1>Member Registration</h1>
                            <p class="mb-3">Authorized login only.</p>
                            <label for="signUpSelect">Sign up as:</label>                            
                        <b-form
                          ref="form"
                          @submit="onSubmit"
                          @reset="onReset"
                          v-if="show"
                        >
                        <!-- <div class="login-inputs"> -->
                              <div style="position: relative; overflow-y: auto; height: 300px">

                                <b-form-group
                            label-cols="4"
                            id="input-group-3"
                            label="Member Type"
                            label-for="input-3"
                            label-cols-lg="2"
                            label-size="sm"
                            invalid-feedback="Member is required"
                          >
                            <b-form-select
                              id="input-3"
                              v-model="form.MemberType"
                              :options="members"
                              required
                            ></b-form-select
                            ><br
                          /></b-form-group>
                          <b-form-group
                            label-cols="2"
                            label-cols-lg="2"
                            label-size="sm"
                            label="First Name"
                            label-for="input-sm"
                            invalid-feedback="Name is required"
                            :state="nameState"
                          >
                            <b-form-input
                              id="name-input"
                              v-model="form.fname"
                              :state="nameState"
                              required
                            ></b-form-input
                            ><br
                          /></b-form-group>

                          <b-form-group
                            label-cols="4"
                            label-cols-lg="2"
                            label-size="sm"
                            label="Middle Name"
                            label-for="input-sm"
                            :state="nameState"
                          >
                            <b-form-input
                              id="name-input"
                              v-model="form.mname"
                              :state="nameState"
                            ></b-form-input
                            ><br
                          /></b-form-group>

                          <b-form-group
                            label-cols="4"
                            label-cols-lg="2"
                            label-size="sm"
                            label="Last Name"
                            label-for="input-sm"
                            invalid-feedback=" Last Name is required"
                            :state="nameState"
                          >
                            <b-form-input
                              id="name-input"
                              v-model="form.lname"
                              :state="nameState"
                              required
                            ></b-form-input
                            ><br
                          /></b-form-group>

                          <b-form-group
                            label-cols="4"
                            label-cols-lg="2"
                            label-size="sm"
                            label="Employee Number"
                            label-for="input-sm"
                            invalid-feedback="Employee No is required"
                            :state="nameState"
                          >
                            <b-form-input
                              id="name-input"
                              v-model="form.EmployeeNo"
                              :state="nameState"
                              required
                            ></b-form-input
                            ><br
                          /></b-form-group>

                          <b-form-group
                            label-cols="4"
                            label-cols-lg="2"
                            label-size="sm"
                            label="Title"
                            label-for="input-sm"
                            :state="nameState"
                          >
                            <b-form-select
                              id="input-3"
                              v-model="form.title"
                              :options="titles"
                            ></b-form-select
                            ><br
                          /></b-form-group>

                          <b-form-group
                            label-cols="4"
                            id="input-group-3"
                            label="Gender"
                            label-for="input-3"
                            label-cols-lg="2"
                            label-size="sm"
                            invalid-feedback="Gendr is required"
                            :state="nameState"
                          >
                            <b-form-select
                              id="input-3"
                              v-model="form.sex"
                              :options="gender"
                              required
                            ></b-form-select
                            ><br
                          /></b-form-group>

                          <b-form-group
                            label-cols="4"
                            id="input-group-3"
                            label="Marital Status"
                            label-for="input-3"
                            label-cols-lg="2"
                            label-size="sm"
                            invalid-feedback="Status is required"
                            :state="nameState"
                          >
                            <b-form-select
                              id="input-3"
                              v-model="form.marital"
                              :options="maritals"
                              required
                            ></b-form-select
                            ><br
                          /></b-form-group>

                          <b-form-group
                            label-cols="4"
                            label-cols-lg="2"
                            label-size="sm"
                            label="Date Of Birth"
                            label-for="example-datepicker"
                            invalid-feedback="Date of Birth is required"
                            :show-decade-nav="showDecadeNav"
                            :state="nameState"
                          >
                            <b-form-datepicker
                              id="example-datepicker"
                              v-model="form.DoB"
                              :state="nameState"
                              required
                            ></b-form-datepicker
                            ><br
                          /></b-form-group>

                          <b-form-group
                            label-cols="4"
                            label-cols-lg="2"
                            label-size="sm"
                            label="Email address"
                            label-for="input-sm"
                            invalid-feedback="Email is required"
                            :state="nameState"
                          >
                            <b-form-input
                              id="name-input"
                              v-model="form.email"
                              type="email"
                              :state="nameState"
                              required
                            ></b-form-input
                            ><br
                          /></b-form-group>

                          <b-form-group
                            label-cols="4"
                            label-cols-lg="2"
                            label-size="sm"
                            label="Work Phone"
                            label-for="input-sm"
                            :state="nameState"
                          >
                            <b-form-input
                              id="name-input"
                              v-model="form.workPhone"
                              :state="nameState"
                            ></b-form-input
                            ><br
                          /></b-form-group>

                          <b-form-group
                            label-cols="4"
                            label-cols-lg="2"
                            label-size="sm"
                            label="Mobile Number"
                            label-for="input-sm"
                            invalid-feedback="Mobile No is required"
                            :state="nameState"
                          >
                            <b-form-input
                              id="name-input"
                              v-model="form.mobileNo"
                              :state="nameState"
                              required
                            ></b-form-input
                            ><br
                          /></b-form-group>

                          <b-form-group
                            label-cols="4"
                            label-cols-lg="2"
                            label-size="sm"
                            label="Address 1"
                            label-for="input-sm"
                            invalid-feedback="Address is required"
                            :state="nameState"
                          >
                            <b-form-input
                              id="name-input"
                              v-model="form.address1"
                              :state="nameState"
                              required
                            ></b-form-input
                            ><br
                          /></b-form-group>

                          <b-form-group
                            label-cols="4"
                            label-cols-lg="2"
                            label-size="sm"
                            label="Address 2"
                            label-for="input-sm"
                            :state="nameState"
                          >
                            <b-form-input
                              id="name-input"
                              v-model="form.address2"
                              :state="nameState"
                            ></b-form-input
                            ><br
                          /></b-form-group>

                          <b-form-group
                            label-cols="4"
                            label-cols-lg="2"
                            label-size="sm"
                            label="State"
                            label-for="input-sm"
                            invalid-feedback="State is required"
                            :state="nameState"
                          >
                            <b-form-select
                              id="name-input"
                              v-model="form.state"
                              :options="states"
                              :state="nameState"
                              required
                            ></b-form-select
                            ><br
                          /></b-form-group>

                          <b-form-group
                            label-cols="4"
                            label-cols-lg="2"
                            label-size="sm"
                            label="Country"
                            label-for="input-sm"
                            invalid-feedback="Country is required"
                            :state="nameState"
                          >
                            <b-form-select
                              id="name-input"
                              v-model="form.country"
                              :options="countrys"
                              :state="nameState"
                              required
                            ></b-form-select
                            ><br
                          /></b-form-group>
                          
                              </div>            
                            <!-- </div> -->
                            <b-button type="submit" class="login-btn">REGISTER NOW</b-button>                            
                        </b-form>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <footer>
            <div class="container">
                <div class="footer">
                    <img src="../../assets/images/icons/lock.svg">
                    <p>Your transactions, credentials are secure.<br>
                        Don’t share login credentials with anyone.</p>
                </div>
            </div>
            <!-- <div class="cc">
                <div class="container">
                    <p>Copyright &copy; 2021 Chevron CEMCS Corporative</p>
                </div>
            </div> -->
        </footer>
	</div>
</template>

<script>
export default {
  data() {
    return {
      checked: false,
      selectedLoan: "",
      show: true,
      notify: 0,
      showDecadeNav: true,
      form: {
        fname: "",
        mname: "",
        lname: "",
        title: null,
        sex: null,
        marital: null,
        DoB: "",
        email: "",
        workPhone: "",
        mobileNo: "",
        address1: "",
        address2: "",
        state: null,
        country: null,
        createdBy: null,
        LastModifiedBy: "",
        EmployeeNo: "",
        MemberType: null
      },
      titles: [{ text: "Select One", value: null }, "Mr.", "Mrs."],
      maritals: [{ text: "Select One", value: null }, "Single", "Married"],
      gender: [{ text: "Select One", value: null }, "Female", "Male"],
      members: [
        { text: "Select One", value: null },
        { text: "Regular", value: 1 },
        { text: "Retiree", value: 2 },
        { text: "Expatriate", value: 3 }
      ],
      creators: [{ text: "Select One", value: null }, "Shola", "Temi", "Lolu"],
      states: [
        { text: "Select State", value: null },
        { text: "Lagos", value: 1 },
        { text: "FCT", value: 2 },
        { text: "Ondo", value: 3 },
        { text: "Imo", value: 4 }
      ],
      countrys: [
        { text: "Select Country", value: null },
        { text: "Nigeria", value: "Nigeria" },
        { text: "Ghana", value: "Ghana" },
        { text: "USA", value: "USA" }
      ],
    };
  },
  mounted () {
      document.title = "Register | Chevron CEMCS Corporate"
  }
};
</script>

<style></style>

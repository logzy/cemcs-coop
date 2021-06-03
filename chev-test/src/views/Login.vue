<template>
	<div class="login-page">
		<img src="../assets/images/login-bg.svg" alt="" id="bg" />
		<header class="sticky-top headernav">
			<nav class="navbar navbar-expand-lg navbar-light container">
				<a class="navbar-brand" href="#"><img src="../assets/images/chevron-cemcs.svg"/></a>
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
							<a class="nav-link" href="/">HOME</a>
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
        <Login-Form></Login-Form>
        <footer>
            <div class="container">
                <div class="footer">
                    <img src="../assets/images/icons/lock.svg">
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

// import axios from "axios"
import LoginForm from "../components/auths/LoginForm.vue"

export default {
    name: "Home",
  components: {
      LoginForm,
  },    
    mounted () {
        document.title = "Login | Chevron CEMCS Corporative"
    },
};

</script>

<style></style>

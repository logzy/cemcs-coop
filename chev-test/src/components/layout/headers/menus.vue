<template>
    <header class="sticky-top headernav">
		
		<b-navbar toggleable="lg" class="chevron-navbar container">
			<b-navbar-brand href="#"><img src="../../../assets/images/chevron-cemcs.svg"/></b-navbar-brand>
			<b-navbar-toggle target="nav-collapse"></b-navbar-toggle>


			<b-collapse id="nav-collapse" is-nav>
				<b-navbar-nav class="ml-auto">					
					<b-nav-item to="/" class="chevron-nav-link">Home</b-nav-item>
					<!-- <b-nav-item to="/transfer" class="chevron-nav-link">Transfer</b-nav-item>
					<b-nav-item to="/withdrawal" class="chevron-nav-link">Withdrawal</b-nav-item> -->
					<div v-if="this.userType == 2">
						<b-collapse id="nav-collapse" is-nav>
				<b-navbar-nav class="ml-auto">	
						<b-nav-item-dropdown text="Transactions" class="chevron-nav-link">
							<b-dropdown-item to="/transfer" class="chevron-nav-dropdown-link" >Transfer</b-dropdown-item>
							<b-dropdown-item to="/withdrawal" class="chevron-nav-dropdown-link" >Withdrawal</b-dropdown-item>
						</b-nav-item-dropdown>
						<b-nav-item-dropdown text="Loan" class="chevron-nav-link">
							<b-dropdown-item to="/loan" class="chevron-nav-dropdown-link" >Apply for Loan</b-dropdown-item>
							<b-dropdown-item to="/loan_history" class="chevron-nav-dropdown-link">History</b-dropdown-item>
						</b-nav-item-dropdown>
						<b-nav-item-dropdown text="Savings" class="chevron-nav-link">
							<b-dropdown-item to="/cash_addition" class="chevron-nav-dropdown-link" >Cash Addition</b-dropdown-item>
							<b-dropdown-item to="/decrease_increase" class="chevron-nav-dropdown-link" >Decrease/Incease Savings</b-dropdown-item>
						</b-nav-item-dropdown>
						</b-navbar-nav>
			</b-collapse>
						</div>
						<div v-if="this.userType == 3">
						<b-nav-item-dropdown text="Account Settings" class="chevron-nav-link">
							<b-dropdown-item to="/members" class="chevron-nav-link">Members</b-dropdown-item>
							<b-dropdown-item to="/Approvals" class="chevron-nav-dropdown-link">Approval Route</b-dropdown-item>
							<b-dropdown-item to="/setup" class="chevron-nav-dropdown-link" >Create Loan</b-dropdown-item>
							<b-dropdown-item to="/view" class="chevron-nav-dropdown-link">Employees</b-dropdown-item>
							<b-dropdown-item to="/config" class="chevron-nav-dropdown-link">Loan Config</b-dropdown-item>        
						</b-nav-item-dropdown></div>
					<b-nav-item @click="logout" class="chevron-nav-link">logout </b-nav-item>
				</b-navbar-nav>
			</b-collapse>
		</b-navbar>
	</header>
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
		.then(() => this.$router.push('/login'))        
      }        
    }
};

</script>
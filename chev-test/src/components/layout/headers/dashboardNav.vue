<template>
  <div>
    <b-navbar toggleable="lg" class="chevron-navbar">
      <b-navbar-brand href="#"></b-navbar-brand>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>
          <b-nav-item to="/" class="chevron-nav-link">Home</b-nav-item>
          <b-nav-item to="/transfer" class="chevron-nav-link">Transfer</b-nav-item>
          <b-nav-item to="/withdrawal" class="chevron-nav-link">Withdrawal</b-nav-item>
          <b-nav-item-dropdown text="Loan" class="chevron-nav-link">
          <b-dropdown-item to="/loan" class="chevron-nav-dropdown-link" >Apply for Loan</b-dropdown-item>
          <b-dropdown-item to="/loan_history" class="chevron-nav-dropdown-link">History</b-dropdown-item>
        </b-nav-item-dropdown>
         <b-nav-item-dropdown text="Loan Investment Savings" class="chevron-nav-link">
          <b-dropdown-item to="/" class="chevron-nav-dropdown-link" >Fixed Deposit</b-dropdown-item>
          <b-dropdown-item to="/loan_history" class="chevron-nav-dropdown-link">Special Deposit</b-dropdown-item>
        </b-nav-item-dropdown>
          <!-- <b-nav-item to="/" class="chevron-nav-link">History</b-nav-item> -->
        <b-nav-item-dropdown text="Account Settings" class="chevron-nav-link">
          <b-dropdown-item to="/members" class="chevron-nav-link">Members</b-dropdown-item>
          <b-dropdown-item to="/Approvals" class="chevron-nav-dropdown-link">Approval Route</b-dropdown-item>
          <b-dropdown-item to="/setup" class="chevron-nav-dropdown-link" >Create Loan</b-dropdown-item>
          <b-dropdown-item to="/view" class="chevron-nav-dropdown-link">Employees</b-dropdown-item>
          <b-dropdown-item to="/config" class="chevron-nav-dropdown-link">Loan Config</b-dropdown-item>        
        </b-nav-item-dropdown>
          <b-nav-item to="/register" class="chevron-nav-link"> </b-nav-item>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </div>
</template>

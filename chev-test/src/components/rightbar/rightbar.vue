<template>
<div>
       <div class="settings-div">
        <div class="settings-icon"></div>
        <div class="settings-icon"></div>
      </div>
      <div class="div-block">
        <!-- <div class="profile-photo"></div> -->
        <div class="profile-header"> {{memberLogin.person.lastName +", " + memberLogin.person.firstName+" "}}</div>
        <div class="profile-sub">Employee Number: {{memberLogin.employeeNumber}}</div>
      </div>
      <div class="admin-divider"></div>
      <div class="admin-calender-div"></div>
      <div class="admin-divider"></div>
      <div class="misc-card">
        <div class="misc-card-header">Loan Requests</div>
        <div class="misc-card-sub">1000</div>
      </div>
       </div>
  
</template>

<script>


export default {
    data() {
        return {
            user : null,
            lastName:"",
            firstName:"",
        };
  },
computed: {
  memberLogin() {
    return this.$store.state.member
  }
},
created() {
  this.$store.dispatch('memberDetails');
},
    
};
</script>
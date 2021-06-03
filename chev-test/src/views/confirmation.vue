<template>
  <div>
    <div class="right-side-bar">
        <div class="background-design"></div>
      <div class="profile-content">
        <div class="profile-image">          
          <p class="profile-name"><strong><h2>Thank You!!!</h2>
          <h4>Your CEMCS account has been confirmed</h4>
            </strong>
          </p>
        </div>           
        <div class="profile-image">
            <span><a :href="`${process.evn.BASE_API_URL}/register`">Click here to complete your CEMCS Membership Registration</a></span>
        </div>
      </div>
    </div>
  </div>
</template>


<script>



export default {
    data() {
        return {

        };
    },  
};
</script>
<style></style>
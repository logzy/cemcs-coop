<template>
            <div>
             <div class="app-login-section">
    <div class="app-login-col-1">
      <div class="app-login-bg-cover">
        <div style="text-align:center" class="app-login-logo"><img src="../../assets/images/chevron-cemcs-logo.png" width="150px" alt="" class="app-logo-img"/></div>
      </div>
    </div>
    <div class="app-login-col-2">
      <div v-show="loader">
         <Loader/>
      </div>

      <!-- { errorAlert ? <ErrorAlert closeErrorAlert = { closeErrorAlert } errorAlertText = { errorText } /> : '' } -->
      <h2 class="login-header">Welcome Back!</h2>
      <div class="login-sub">Kindly fill in your credentials to login into your account.</div>
      <form @submit.prevent="login">
      <div class="w-form">  
          <div class="app-login-form-group">
        <label class="login-label">Member Employee No</label>
        <input type="text" class="app-login-text-field w-input"  v-model="user.Username" required placeholder="Member Employee No." />
        </div>
          <div class="app-login-form-group">
              <label class="login-label">Password</label>
              <input type="password" class="app-login-text-field w-input" v-model="user.Password" placeholder="Password" required />
              </div>
      </div>
      <button type="submit" class="app-login-button">Login into Account</button>
      </form>
      <br>
       <router-link to="/new-account"> <div style="text-align:center;text-decoration:underline;">Don't have an account? Register for an account</div> </router-link>
      <div class="app-login-divider"></div>
      <div class="app-login-notice">
        <div class="app-login-notice-col-1"></div>
        <!-- <div class="app-login-notice-col-2">Experiencing any problem with loging into your account ? Kindly contact the super admin to help resolve your issues.</div> -->
        <div class="app-login-notice-col-2">For support kindly contact  the Administrator (admin-email@whereever.com).</div>
      </div>
    </div>
  </div>
            </div>
</template>

<script src ="./Login.js"></script>


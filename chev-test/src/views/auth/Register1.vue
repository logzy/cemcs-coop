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
      <h2 class="login-header">Register for an Account</h2>
      <div class="login-sub">Kindly fill in the following to register for an account.</div>
      <form @submit.prevent="onSubmit">
      <div class="w-form">  
            <div class="app-login-form-group">
                 <label class="login-label">Select Membership Type</label>
              <select class="app-select w-select" v-model="form.UserTypeCategory" 
                                id="exampleFormControlSelect1" required>
                                    <option disabled value= 0>Select Membership Type</option>
                                    <option value= 1>Regular Member (RSM)</option>
                                    <option value= 2>Retiree Member (RM)</option>
                                    <option value= 3>Expatriate Member (EM)</option>
                                </select>
            </div>
          <div class="app-login-form-group">
        <label class="login-label">Member Employee No</label>
        <input type="text" class="app-login-text-field w-input"  v-model="form.EmployeeNo"  required placeholder="Member Employee No." />
        </div>
         <div class="app-login-form-group">
          <label class="login-label">Email Address</label>
          <input type="email" class="app-login-text-field w-input"  v-model="form.email" required placeholder="Email Address" />
        </div>        
          <div class="app-login-form-group">
              <label class="login-label">Password</label>
              <div>
                <input class="app-login-text-field w-input"
                  :type="passwordVisible ? 'text' : 'password'"
                  v-model="form.password" placeholder="Password" required />
                    <button class="visibility" tabindex='-1' @click.prevent="togglePasswordVisibility" 
                      :arial-label="passwordVisible ? 'Hide password' : 'Show password'">                  
                        <b-icon :icon="passwordVisible ? 'eye-fill' : 'eye-slash-fill' "></b-icon>
                    </button>
              </div>
                <div v-if="passwordValidation.errors.length > 0 && !submitted && form.password !== ''"  class='hints'>
                  <p v-for="error in passwordValidation.errors" :key="error"><code>{{error}}</code></p>
                </div>
              </div>
            <!-- <div class="app-login-form-group">
              <label class="login-label">Confirm Password</label>
              <input type="password" class="app-login-text-field w-input" v-model.lazy="form.checkpassword" placeholder="Password" required />             
            </div>
             <div class="matches" v-if="notSamePasswords"><p><code>Passwords don't match.</code></p></div>             -->
      </div>
      <button type="submit" class="app-login-button">Register</button>
      </form>
      <div class="app-login-divider"></div>
     <router-link to="/"> <div style="text-align:center;text-decoration:underline;">Already have an account? Log into your account</div> </router-link>
    </div>
  </div>
            </div>
</template>

<script src ="./Register1.js"></script>


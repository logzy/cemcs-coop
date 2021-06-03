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
      <h2 class="login-header">Complete Registration</h2>
      <div class="login-sub">Kindly fill in the following to complete your registration.</div>

                            <b-form
                              ref="form"
                              @submit="onSubmit"
                              @reset="onReset"
                              v-if="show"
                            >
                            <strong>
                            <b-form-group
                                label-cols="4"
                                id="input-group-3"
                                label="Member Type"
                                label-for="input-sm"
                                label-cols-lg="3"
                                label-size="sm"
                              >
                                <b-form-select
                                  id="input-3"
                                  v-model="login.userTypeCategory"
                                  :options="members"
                                  disabled
                                ></b-form-select
                                ></b-form-group>
                                <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Employee Number"
                                label-for="input-sm"
                              >
                                <b-form-input
                                  id="name-input"
                                  v-model="login.userName"
                                  disabled
                                  required
                                ></b-form-input>
                                </b-form-group>

                                <b-form-group
                                label-cols="4"
                                id="input-group-3"
                                label="Location"
                                label-for="input-sm"
                                label-cols-lg="3"
                                label-size="sm"
                                invalid-feedback="Location is required"
                                :state="nameState"
                              >
                                <b-form-select
                                  id="input-3"
                                  :state="nameState"
                                  v-model="form.location"
                                  :options="locations"
                                  required
                                ></b-form-select
                                ></b-form-group>

                                <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Date Of Employment"
                                label-for="example-datepicker"
                                invalid-feedback="Date of Employment is required"
                                :state="nameState"
                              >
                            
                                <!-- <b-form-datepicker
                                  id="example-datepicker"
                                  v-model="form.DoB"
                                  :state="nameState"
                                  required
                                ></b-form-datepicker> -->

                                <date-picker class='input-group date down' :state="nameState"
                                 v-model="form.empDate" :config="options"></date-picker>
                                </b-form-group>

                                <span v-if="login.userTypeCategory == 2">
                                  <b-form-group
                                  label-cols="4"
                                  label-cols-lg="3"
                                  label-size="sm"
                                  label="Date Of Retirement"
                                  label-for="example-datepicker"
                                  invalid-feedback="Date of Retirement is required"
                                  :state="nameState">

                                  <date-picker class='input-group date down' :state="nameState"
                                  v-model="form.RetDate" :config="options"></date-picker>
                                  </b-form-group>
                                </span>


                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label-for="input-sm"> 
                                <span v-if="savings.minimumSavingsAmount !==''">
                                <code>Minimum Savings of ₦{{savings.minimumSavingsAmount | numberFormat}}
                                   ({{savings.minimumSavingsAmount | NumbersToWords | capitalize}}) Naira Only
                                </code></span>
                                </b-form-group>

                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Monthly Deduction"
                                label-for="input-sm"
                              >
                                <b-form-input
                                  id="input-1"
                                  v-model="form.minSaving"
                                  :formatter="numberFormat"
                                  required
                                ></b-form-input>

                                </b-form-group>
                                <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label-for="input-sm"> 
                                <span v-if="( (parseFloat(this.form.minSaving.replace(/,/g, ''))) < savings.minimumSavingsAmount ) && (form.minSaving !== '')">
                  <p style="color:red;font-size:12px;" >Monthly Deduction must be greater than or equal to 
                    ₦{{savings.minimumSavingsAmount | numberFormat}}
                  </p>
                </span></b-form-group>
                                <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label-for="input-sm"> 
                                 <span v-if="form.minSaving!= ''"><code>
    {{parseFloat(this.form.minSaving.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
                                </b-form-group>
                                
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                id="input-group-3"
                                label="First Name"
                                label-for="input-sm"
                              >
                                <b-form-input
                                  id="name-input"
                                  v-model="form.fname"
                                  required
                                ></b-form-input
                                ></b-form-group>
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
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
                                ></b-form-group>
                              <b-form-group
                                label-cols="4"
                                id="input-group-3"
                                label-cols-lg="3"
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
                                ></b-form-group>
                              <b-form-group
                                label-cols="4"
                                id="input-group-3"
                                label="Gender"
                                label-for="input-sm"
                                label-cols-lg="3"
                                label-size="sm"
                                invalid-feedback="Gender is required"
                                :state="nameState"
                              >
                                <b-form-select
                                  id="input-3"
                                  v-model="form.sex"
                                  :options="gender"
                                  :state="nameState"
                                  required
                                ></b-form-select
                                ></b-form-group>
                              <b-form-group
                                label-cols="4"
                                id="input-group-3"
                                label="Marital Status"
                                label-for="input-sm"
                                label-cols-lg="3"
                                label-size="sm"
                                invalid-feedback="Status is required"
                                :state="nameState"
                              >
                                <b-form-select
                                  id="input-3"
                                  v-model="form.marital"
                                :state="nameState"
                                  :options="maritals"
                                  required
                                ></b-form-select
                                ></b-form-group>
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Date Of Birth"
                                label-for="example-datepicker"
                                :state="nameState"
                              >
                                <!-- <b-form-datepicker
                                  id="example-datepicker"
                                  v-model="form.DoB"
                                  :state="nameState"
                                  required
                                ></b-form-datepicker > -->
                                <date-picker class='input-group date down'
                                :state="nameState"
                                 v-model="form.DoB"  :config="options"></date-picker>
                                </b-form-group>
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Work Email address"
                                label-for="input-sm"
                                invalid-feedback="Email Address is required"
                                :state="nameState">
                                <span class="input-group-addon" id="basic-addon1"><span class="fa fa-envelope"></span></span>
                                <b-form-input
                                  id="name-input"
                                  v-model="login.email"
                                  type="email" disabled
                                  :state="nameState"
                                  required
                                ></b-form-input
                                ></b-form-group>
                                <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Personal Email address"
                                label-for="input-sm"
                              >
                                <b-form-input
                                  id="name-input"
                                  v-model="form.Personalemail"
                                  type="Email"
                                ></b-form-input
                                ></b-form-group>
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Phone Extention"
                                label-for="input-sm"
                                :state="nameState"
                              >
                                <b-form-input
                                  id="name-input"
                                  v-model="form.workPhone"
                                  :state="nameState"
                                ></b-form-input
                                ></b-form-group>

                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Mobile Number"
                                label-for="input-sm"
                              >
                                <b-form-input
                                  id="name-input"
                                  v-model="form.mobileNo"
                                ></b-form-input
                                ></b-form-group>
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
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
                                ></b-form-group>
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Address 2"
                                label-for="input-sm"
                              >
                                <b-form-input
                                  id="name-input"
                                  v-model="form.address2"
                                ></b-form-input
                                >
                                </b-form-group>
                              <b-form-group
                                label-cols="4"
                                label-cols-lg="3"
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
                                ></b-form-group>
                              <b-form-group
                                label-cols="3"
                                label-cols-lg="3"
                                label-size="sm"
                                label="Country"
                                label-for="input-sm"
                                invalid-feedback="Country is required"
                                :state="state"
                              >
                                <b-form-select
                                  id="name-input"
                                  v-model="form.country"
                                  :options="countrys"
                                  :state="state"
                                ></b-form-select
                                ></b-form-group>
                            </strong>
                             <button type="submit" class="app-login-button">Complete Registration</button>
                            </b-form>
    </div>
  </div>
            </div>
</template>

<script src ="./Register2.js"></script>


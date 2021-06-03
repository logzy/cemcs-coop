<template>
<div>        
  <div v-show="loader">
         <Loader/>
      </div>
       <Status :state="state" :closeModal = "closeModal" :message = "message" :resetState="resetState" v-if="status"/>
          <div class="content-header">VEHICLE INSURANCE APPLICATION</div>
      <!-- <div class="content-sub">  Withdraw From Special Deposit or Fixed Deposit Account</div> -->
    <form @submit.prevent="onSubmit">      
          <div class="form-flex-col">
            <label class="login-label">Subscription Type</label>
            <b-form-select
              class="app-text-field w-input"
              id="input-3"
              v-model="Subscription"
              :options="Subscriptions" required>
            </b-form-select>
          </div>
      <div class="content-sub"><b> VEHICLE DETAILS </b></div>
    <div class="form-flex">   
        <div class="form-flex-col">
            <label class="login-label">Car</label>
            <input type="text" class="app-text-field w-input" />
        </div>
        <div class="form-flex-col">
            <label class="login-label">Make</label>
            <input type="text" class="app-text-field w-input" />
        </div>
        <div class="form-flex-col">
            <label class="login-label">Registration Number</label>
            <input type="text" class="app-text-field w-input" />
        </div>
        <div class="form-flex-col">
            <label class="login-label">Owner's Name</label>
            <input type="text" class="app-text-field w-input" />
        </div>
         <div class="form-flex-col">
            <label class="login-label">Purpose of Use</label>
            <b-form-select
              class="app-text-field w-input"
              id="input-3"
              v-model="purposeOfUse"
              :options="purposeOfUses" required>
            </b-form-select>
          </div>
          <div class="form-flex-col">
            <label class="login-label">Vehicle Type</label>
            <b-form-select
              class="app-text-field w-input"
              id="input-3"
              v-model="vehicleType"
              :options="vehicles" required>
            </b-form-select>
          </div>
          <div class="form-flex-col">
            <label class="login-label">Payment mode</label>
            <b-form-select
              class="app-text-field w-input"
              id="input-3"
              v-model="paymentMode"
              :options="paymentModes" required>
            </b-form-select>
          </div>
            <div class="form-flex-col">
                <label class="login-label">Rate</label>
                <input type="text" class="app-text-field w-input"
                v-model="rate" v-mask="mask" required placeholder="Type Here" />
                <span v-if="rate != ''"><p style="color:red;font-size:12px;" >
    {{parseFloat(this.rate.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </p></span>
            </div>
            <div class="form-flex-col">
                <label class="login-label">Value of Vehicle</label>
                <input type="text" class="app-text-field w-input" v-model="valueOfVehicle" v-mask="mask" required placeholder="Type Here" />
                <span v-if="valueOfVehicle != ''"><p style="color:red;font-size:12px;" >
    {{parseFloat(this.valueOfVehicle.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </p></span>
            </div>
            <div class="form-flex-col">
                <label class="login-label">Premium</label>
                <input type="text" class="app-text-field w-input" />
            </div>
            <div class="form-flex-col">
                <label class="login-label">Tenor</label>
                <b-form-select
                class="app-text-field w-input"
                id="input-3"
                v-model="tenor"
                :options="tenors" required>
                </b-form-select>
            </div>
            <div class="form-flex-col">
                <label class="login-label">Proof of Ownership</label>
                <b-form-select
                class="app-text-field w-input"
                id="input-3"
                v-model="proofOfOwner"
                :options="proofOfOwnerShip" required>
                </b-form-select>
            </div>
            <div class="form-flex-col">
                <label class="login-label">Payment Evidence</label>
                <input type="text" class="app-text-field w-input" />
            </div>

      </div >
      <div class="row justify-content-md-center">
        <b-col lg="2" class="pb-2"><b-button type="submit" variant="primary">Submit</b-button></b-col>
        <b-col lg="2" class="pb-2"><b-button variant="outline-primary">Reset</b-button></b-col>
      </div>
      </form>
  
      </div>
    </template>
    <script src="./vehicle.js"></script>
<template>
<div>        
  <div v-show="loader">
         <Loader/>
      </div>
       <Status :state="state" :closeModal = "closeModal" :message = "message" :resetState="resetState" v-if="status"/>
          <div class="content-header">Make a Withdrawal</div>
      <div class="content-sub">  Withdraw From Special Deposit or Fixed Deposit Account</div>
      <form @submit.prevent="onSubmit">
              <div class="form-flex">
                
          <!-- <div class="form-flex-col">
            <label class="login-label">Employee No.</label>
            <input type="number" class="app-text-field w-input" required placeholder="Type Here" />
          </div>
          <div class="form-flex-col">
            <label class="login-label">Name</label>
            <input type="text" class="app-text-field w-input" required placeholder="Type Here" />
          </div> -->
          <div class="form-flex-col">
            <label class="login-label">Account Type</label>
            <b-form-select
              class="app-text-field w-input"
              id="input-3"
              v-model="account"
              :options="accountTypes" required disabled>
            </b-form-select>
          </div>
          <div class="form-flex-col">
            <label class="login-label">Balance</label>
            <input type="text" class="app-text-field w-input" 
            v-model="memberBalance.specialDepositBalance" v-mask="mask" required disabled />
            <span v-if="memberBalance.specialDepositBalance != ''"><p style="color:red;font-size:12px;" >
                  {{memberBalance.specialDepositBalance | NumbersToWords | capitalize}} Naira Only</p></span>
          </div>
          
          <div class="form-flex-col">
              <label class="login-label">Amount</label>
              <input type="text" class="app-text-field w-input" 
              v-model="amount" v-mask="mask" required placeholder="Type Here" />
              <span v-if="amount != ''"><p style="color:red;font-size:12px;" >
    {{parseFloat(this.amount.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </p></span>
          </div>
          <div class="form-flex-col">
            <label class="login-label">Date</label>
            <date-picker class='input-group date down'
                v-model="effectiveDate" :config="options"></date-picker>
          </div>         
      </div >
        <div class="header_2">Method Of Collection</div>
      <!-- <div class="content-sub">  Personal cheque should be drawn to:</div> -->
      <div class="form-flex">
        <b-form-group label="Select Option" v-slot="{ ariaDescribedby }">
          <b-form-radio v-model="selected" 
            :aria-describedby="ariaDescribedby"
            name="some-radios" value="A">
            Cheque should be drawn to:
          </b-form-radio>
            <span v-if="selected == 'A'">
                  <div class="form-flex-col">
                    <label class="login-label">Bank</label>
                    <b-form-select
                    id="banks"
                    class="app-text-field w-input"
                    v-model="bankcode"
                    required>                                               
                    <b-form-select-option :value="null" disabled>
                        -- Select Bank -- 
                    </b-form-select-option>
                    <b-form-select-option 
                    v-for="item in banks.data" 
                    :value="item.bankCode"
                    :key="item.id">
                        {{item.bankName}} 
                    </b-form-select-option>
                    </b-form-select >
                  </div>
                  <div class="form-flex-col">
                    <label class="login-label">Location</label>
                    <input type="text" 
                      class="app-text-field w-input" v-model="location"
                      required placeholder="Type Here" />
                  </div>
            </span>
          <b-form-radio v-model="selected" 
            :aria-describedby="ariaDescribedby"
            name="some-radios" 
            value="B">Account
          </b-form-radio>
          <span v-if="selected == 'B'">
                <div class="form-flex-col">
                    <label class="login-label">Bank</label>
                    <b-form-select
                    id="banks"
                    class="app-text-field w-input"
                    v-model="bankcode"
                    required>                                               
                    <b-form-select-option :value="null" disabled>
                        -- Select Bank -- 
                    </b-form-select-option>
                    <b-form-select-option 
                    v-for="item in banks.data" 
                    :value="item.bankCode"
                    :key="item.id">
                        {{item.bankName}} 
                    </b-form-select-option>
                  </b-form-select ></div>
              <div class="form-flex-col">
                <label class="login-label">Account Number</label>
                  <b-form-input
                    id="`Account Number`"
                    v-model="accountNumber"
                    @blur="verifyAcc"
                    type="number"
                    aria-describedby="input-live-help input-live-feedback"
                    trim
                    required
                ></b-form-input>
                <div v-if="accountNumber.length !== 10 && accountNumber !== ''">
                  <p style="color:red;font-size:12px;" >Account Number must be 10 digits</p>
                </div>
              </div>

          <!-- <div class="form-flex-col">
              <label class="login-label">Account Name</label>
              <input type="text" class="app-text-field w-input" required placeholder="Type Here" />
              <div><a type="button"><b>ADD NEW</b></a></div>
          </div> -->
           <div class="form-flex-col">
              <label class="login-label">Account Name</label>                
                <b-form-input
                    disabled
                    id="`Beneficiary`"
                    v-model="name.data"
                    type="text"
                    required
                ></b-form-input>
           </div>
          </span>
        </b-form-group>

              
              
                  
      </div>
      <div class="row justify-content-md-center">
        <b-col lg="2" class="pb-2"><b-button type="submit" variant="primary">Submit</b-button></b-col>
        <b-col lg="2" class="pb-2"><b-button variant="outline-primary">Reset</b-button></b-col>
      </div>
      </form>
  
      </div>
    </template>
    <script src="./withdrawal.js"></script>
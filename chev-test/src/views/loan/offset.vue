<template>
<div>
          <div class="content-header">Loan Offset</div>
      <div class="content-sub">Fill the following to continue</div>
      <form  @submit="onSubmit" @reset="onReset" v-if="show">
              <div class="form-flex">
                   <div class="form-flex-col">
                <label class="login-label">Loan Type</label>
                <select v-model="form.loanType" class="app-select w-input">
                <option value="">Select Loan Type</option>
                 <option value="short">Short term</option>
                  <option value="long">Long term</option>
                </select>
          </div>
          <div class="form-flex-col">
               <label class="login-label">Date</label>
        <input type="date" class="app-text-field w-input" required placeholder="Type Here" />
          </div>
           <!-- <div class="form-flex-col">
                <label class="login-label">Applicant Number</label>
        <input type="number" class="app-text-field w-input" required placeholder="Type Here" />
          </div>
             <div class="form-flex-col">
                <label class="login-label">Applicant Name</label>
        <input type="text" class="app-text-field w-input" required placeholder="Type Here" />
          </div> -->
            <div class="form-flex-col">
                <label class="login-label">Location</label>
        <input type="text" class="app-text-field w-input" required placeholder="Type Here" />
          </div>
            <!-- <div class="form-flex-col">
                <label class="login-label">Tel. Extention</label>
        <input type="text" class="app-text-field w-input" required placeholder="Type Here" />
          </div> -->
            <div class="form-flex-col">
                <label class="login-label">Repayment Period</label>
        <input type="text" class="app-text-field w-input" required placeholder="Type Here" />
          </div>
           <div class="form-flex-col">
                <label class="login-label">Amount of Loan Collected</label>
        <input type="text" class="app-text-field w-input" required placeholder="Type Here" />
          </div>
                    <div class="form-flex-col">
               <label class="login-label">Deduction Starts After</label>
        <input type="date" class="app-text-field w-input" required placeholder="Type Here" />
          </div>
            <div class="form-flex-col">
               <label class="login-label">Offset to be calcuated after</label>
        <input type="date" class="app-text-field w-input" required placeholder="Type Here" />
          </div>
          <div class="form-flex-col">
                <label class="login-label">Mode of Offset</label>
                <select  class="app-select w-input">
                <option value="">Select Offset Mode</option>
                 <option value="">Cash</option>
                  <option value="">Transfer</option>
                </select>
          </div>
                       <div class="form-flex-col">
               <label class="login-label">Offset Amount</label>
        <input type="text" v-model="form.offsetAmount" v-mask="mask"
        class="app-text-field w-input" required placeholder="Type Here" />
        <span v-if="form.offsetAmount != ''"><code>
    {{parseFloat(this.form.offsetAmount.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
          </div>
             <div class="form-flex-col">
               <label class="login-label">Cash Amount</label>
        <input type="text" v-model="form.cashAmount" v-mask="mask"
         class="app-text-field w-input" required placeholder="Type Here" />
        <span v-if="form.cashAmount != ''"><code>
    {{parseFloat(this.form.cashAmount.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
          </div>
            <div class="form-flex-col">
               <label class="login-label">Deposit Amount</label>
        <input type="text" v-model="form.depositAmount" v-mask="mask" 
        class="app-text-field w-input" required placeholder="Type Here" />
        <span v-if="form.depositAmount != ''"><code>
    {{parseFloat(this.form.depositAmount.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
          </div>

           <div class="form-flex-col">
               <label class="login-label">Receipt Dated</label>
        <input type="date" class="app-text-field w-input" required placeholder="Type Here" />
          </div>
            <div class="form-flex-col">
               <label class="login-label">Monthly Savings</label>
        <input type="text" v-model="form.MonthSaving" v-mask="mask" 
        class="app-text-field w-input" required placeholder="Type Here" />
        <span v-if="form.MonthSaving != ''"><code>
    {{parseFloat(this.form.MonthSaving.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
          </div>
             <div class="form-flex-col">
               <label class="login-label">Savings Balance</label>
        <input type="text" v-model="memberBalance.savingsBalance" v-mask="mask" 
        class="app-text-field w-input" required placeholder="Type Here" />
        <span v-if="memberBalance.savingsBalance != ''"><code>
    {{parseFloat(this.memberBalance.savingsBalanc) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
          </div>
           <div class="form-flex-col">
               <label class="login-label">Special Deposit Balance</label>
        <input type="text" v-model="memberBalance.specialDepositBalance" v-mask="mask"
        class="app-text-field w-input" required placeholder="Type Here" />
        <span v-if="memberBalance.specialDepositBalance != ''"><code>
    {{parseFloat(memberBalance.specialDepositBalance) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
          </div>
          
                       <div class="form-flex-col">
               <label class="login-label">Evidence of Payment</label>
        <input type="file" id="myFile" name="filename">
          </div>
      </div>
          <div style="margin-top:50px;" class="button-group">
          <!-- <div type="reset" style="background:#c00;display:inline-block;margin-right:30px" class="app-form-button">Cancel</div>
           <div type="submit" style="display:inline-block" class="app-form-button">Submit</div> -->
            <b-button type="reset" style="background:#c00;display:inline-block;margin-right:30px" class="app-form-button">Cancel</b-button>
           <b-button type="submit" style="display:inline-block" class="app-form-button">Submit</b-button>
    </div>
      </form>
  
      </div>
    </template>

    <script src ="./offset.js"></script>
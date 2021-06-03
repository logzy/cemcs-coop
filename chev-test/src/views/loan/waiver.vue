<template>
<div>
          <div class="content-header">Waiver Application Form</div>
      <div class="content-sub">Fill the following to continue</div>
      <form @submit="addWaiver">
              <div class="form-flex">
          <!-- <div class="form-flex-col">
               <label class="login-label">Applicant Number</label>
        <input type="number" class="app-text-field w-input" required placeholder="Type Here" />
          </div>
           <div class="form-flex-col">
                <label class="login-label">Applicant Name</label>
        <input type="text" class="app-text-field w-input" required placeholder="Type Here" />
          </div> -->
          <div class="form-flex-col">
                <label class="login-label">Payment Mode</label>
                <select onChange= "formState" class="app-select w-input">
                <option value="" disabled>Select Payment Mode</option>
                 <option v-for="item in modeOfPay" 
                                :value="item.id"
                                :key="item.id">
                                  {{item.description}}</option>
                </select>
          </div>
            <div class="form-flex-col">
                <label class="login-label">Loan Type</label>
                <select v-model="waiverForm.loanType" class="app-select w-input" required>
                <option value="">Select Loan Type</option>
                 <option value="short">Short term</option>
                  <option value="long">Long term</option>
                </select>
          </div>
           <div class="form-flex-col">
                <label class="login-label">Waiver Type</label>
                <select v-model="waiverForm.waiverType" class="app-select w-input" required>
                <option value="">Select Waiver Type</option>
                 <option value="short">Short term</option>
                  <option value="long">Long term</option>
                </select>
          </div>
             <div class="form-flex-col">
               <label class="login-label">Interest</label>
        <input type="number" class="app-text-field w-input" required placeholder="Type Here" />
          </div>
             <div class="form-flex-col">
               <label class="login-label">New Loan Amount</label>
                <input v-model.trim="waiverForm.amount"
                v-mask="mask"
                  type="text" class="app-text-field w-input" 
                  required placeholder="Type Here" />
                  <span v-if="waiverForm.amount != ''"><code>
    {{parseFloat(this.waiverForm.amount.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira
                    </code></span>
          </div>
             <div class="form-flex-col">
               <label class="login-label">Tenor/Month</label>
        <input type="number" class="app-text-field w-input" required placeholder="Type Here" />
          </div>
             <div class="form-flex-col">
               <label class="login-label">Fee</label>
        <input v-model="waiverForm.fee" v-mask="mask"  type="text" class="app-text-field w-input" required placeholder="Type Here" />
          <span v-if="waiverForm.fee != ''"><code>
    {{parseFloat(this.waiverForm.fee.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira
                    </code></span>
          </div>
                       <div class="form-flex-col">
               <label class="login-label">Payment Receipt</label>
        <input type="file" id="myReceipt" name="receipt">
          </div>
           <div class="form-flex-col">
               <label class="login-label">Snapshot of intended loan form</label>
        <input type="file" id="myLoanForm" name="loanForm">
          </div>
      </div>
          <!-- <div style="margin-top:50px;" class="button-group">
          <div  @click="cal" style="background:#c00;display:inline-block;margin-right:30px" class="app-form-button">Calculate</div>
           <div @click="addWaiver" style="display:inline-block" class="app-form-button">Add</div>
    </div> -->
    <div class="form-buttons">
                        <b-button type="submit" style="background:#c00;display:inline-block;margin-right:30px" class="app-form-button">Calculate</b-button>
                        <b-button type="submit" style="display:inline-block" class="app-form-button">Add</b-button>
                        </div>
    <div v-if="waiverDetails.length < 0">
            <hr>
       <h3>Waiver Fee Details</h3> 
         <table class="app-table2">
                                  <thead>
                                      <tr class="app-table2-row">
                                      <th class="app-table2-header">Loan Type</th>
                                      <th class="app-table2-header">Waiver Type</th>
                                      <th class="app-table2-header">Amount</th>
                                      <th class="app-table2-header">Fee</th>

                                  </tr>
                                  </thead>
                                  <tbody>
                                           <tr class="app-table2-row">
                                  <td class="app-table2-data">Null</td>
                                      <td class="app-table2-data">Null</td>
                                      <td class="app-table2-data">Null</td>
                                      <td class="app-table2-data">null</td>

                                      </tr>
                             
                                  </tbody>
                                  </table>
    </div>
    
      </form>
  
      </div>
    </template>

    <script src ="./waiver.js"></script>
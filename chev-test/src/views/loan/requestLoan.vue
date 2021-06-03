<template>
<div>
    <div v-show="loader">
         <Loader/>
      </div>
       <Status :state="state" :closeModal = "closeModal" :message = "message" :resetState="resetState" v-if="status"/>
          <div class="content-header">Apply for a Loan</div>
      <div class="content-sub">Make a loan request</div>
        <div v-if="this.errors != ''">            
            <b-alert
                variant="danger"
                dismissible
                fade
                :show="showDismissibleAlert"
                @dismissed="showDismissibleAlert=false"
            >{{this.errors}} 
            </b-alert>
        </div>
        <div v-if="result.errors.length > 0">
            <b-alert variant="danger"
                dismissible
                fade
                :show="showDismissibleAlert"
                @dismissed="showDismissibleAlert=false" >
                <ul>
                    <li v-for="item in result.errors" :key="item.errors">
                        {{item.errorMessage}}
                    </li>
                </ul>              
            </b-alert>
        </div>
            
            <b-form @submit="onSubmit" @reset="onReset" v-if="show">
            <b-form-group
            class="pt-1 form-label"
            label-cols="4"
            label-cols-lg="4"
            label-size="sm"
            label="Loan Type"
            label-for="sm"
            >
            <b-form-select
                id="loantype"
                v-model="selectedLoan"
                @change="getConfig(selectedLoan)"
                required
            >                                               
                <b-form-select-option :value="null" disabled>
                    -- Select LoanType -- 
                </b-form-select-option>
                <b-form-select-option 
                v-for="item in items" 
                :value="item.loanId"
                :key="item.loan.id">
                    {{item.loan.name}} 
                </b-form-select-option>
            </b-form-select >                      
            <br/>
            </b-form-group>
            <div v-if="mType === 1">
                <b-row class="my-1 form-row mb-3 ">
                <b-col sm="4">
                <label class="pt-1 form-label"
                    >Expected Lump Sum Type<code>*</code></label
                >
                </b-col>
                <b-col sm="8">
                <b-form-select
                    v-model="expectedLumpSum"
                    :options="lumpSums"
                    @change="LumpSumDate(expectedLumpSum)"
                ></b-form-select>
                </b-col>
                </b-row> 
                
                <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                    <label
                    class="pt-1 form-label"
                    >Date Expected MM/YY <code>*</code></label
                    >
                </b-col>
                <b-col sm="4">
                    <b-form-select
                    v-model="effectMonth" disabled
                    :options="Months"
                    value-field="value"
                    text-field="name"
                ></b-form-select></b-col>
                    <b-col sm="3"><b-form-input
                    :id="`DateExpected`"
                    v-model="effectYear" disabled
                    type="text"
                    ></b-form-input>
                </b-col>
                </b-row>
                <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                    <label
                    class="pt-1 form-label"
                    >Amount Expected <code>*</code></label
                    >
                </b-col>
                <b-col sm="8">
                    <b-form-input
                    :id="`AmountExpected`"
                    v-model="amountExpected"
                    :formatter="numberFormat"
                    type="text"
                    ></b-form-input>
                    <span v-if="amountExpected != ''"><code>
    {{parseFloat(this.amountExpected.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
                </b-col>
                </b-row>
                <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                    <label
                    class="pt-1 form-label"
                    >Amount Desired <code>*</code></label
                    >
                </b-col>
                <b-col sm="8">
                    <b-form-input
                    :id="`AmountDesire`"
                    type="text"
                    v-model.trim="loanAmount"                                                                                
                    :formatter="numberFormat"
                    @blur="AmountValidation"
                    required
                    ></b-form-input>
                    <span v-if="loanAmount != ''"><code>
    {{parseFloat(this.loanAmount.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
                </b-col>
                </b-row>
            </div>
            <div v-if="mType  >= 2">
                <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                    <label
                    class="pt-1 form-label"
                    >Loan Amount <code>*</code></label
                    >
                </b-col>                                    
                <b-col sm="8">
                    <b-form-input
                    :id="`Amount`"
                    v-model.lazy.trim="loanAmount"
                    :max="maxLoanAmount" :min="minLoanAmount"
                    @blur="AmountValidation"
                    @change="getGuarantor"
                    type="text"                                                                               
                    :formatter="numberFormat"
                    required
                    ></b-form-input>
                    <span v-if="loanAmount != ''"><code>
    {{parseFloat(this.loanAmount.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
                </b-col>
                
                </b-row>
                <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                    <label
                    class="pt-1 form-label"
                    >Effective Date MM/YY <code>*</code></label
                    >
                </b-col>
                <b-col sm="3">
                    <b-form-select
                    v-model="this.effectiveMonth" disabled
                    :options="Months"
                    value-field="value"
                    text-field="name"
                ></b-form-select></b-col>
                    <b-col sm="3"><b-form-input
                    :id="`DateExpected`"
                    v-model="effectiveYear" disabled
                    type="text"
                    ></b-form-input>
                </b-col>
                </b-row>
                
                <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                    <label
                    class="pt-1 form-label"
                    >Repayment Period (months)
                    <code>*</code></label
                    > 
                </b-col>                                                                       
                <b-col sm="8">
                    <b-form-input
                    :id="`payment period`"
                    v-model.lazy="minMonthlyRepayPeriod"
                    :max="this.MaxRepayPeriod" :min="this.MinRepayPeriod"
                    type="number"
                    @blur="RepaymentValidation" 
                    ></b-form-input>
                </b-col>
                </b-row>

                <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                    <label class="pt-1 form-label"
                    >Rate (%) <code>*</code></label
                    >
                </b-col>
                <b-col sm="8">
                    <b-form-input
                    disabled
                    :id="`Rate`"
                    v-model="details.intrestRate"
                    type="text"
                    ></b-form-input>
                </b-col>
                </b-row>
            </div>

            <div v-if="(mType === 2 && loanAmount.length > 9) || (selectedLoan === 4) || (selectedLoan === 1)" >              
                            
              <span v-if="guarantorArray.length !== 0">
              <div class="header_2">Guarantors</div>
                <div style="font-weight:600;font-size:14px;margin-bottom:20px">Fill in your guarantor details or <span v-if="togglePastGuarantors" @click="toggleGuarantors" style="color:#c00;cursor:pointer;text-decoration:underline">Hide recent guarantors / Edit guarantors</span> <span v-else @click="toggleGuarantors" style="color:#c00;cursor:pointer;text-decoration:underline">Select from recent guarantors</span></div>
                <div style="margin-bottom:20px;" v-show="togglePastGuarantors">
                    <div style="margin-bottom:10px">Click to select a guarantor from the list below</div>
                    <div @click="selectGuarantor(result.employeeNumber)" class="guarantor-chip" v-for="(result,index) in pastGuarantors" :key="index">Guarantor: {{ result.employeeNumber }}</div>
                </div>
                <div >
                <div  class="wrapper" v-for="(guarantor, index) in guarantorArray" :key="index">          
                  <b-row class="my-1 form-row mb-3">
                  <b-col sm="4">
                      <label
                      class="pt-1 form-label"                                       
                      >Employee Number<code>*</code></label
                      >
                  </b-col>
                  <!-- {{ this.guarantorNumber }} -->
                  <b-col sm="8">
                      <b-form-input
                      :readonly="togglePastGuarantors"
                      v-model.lazy.trim="guarantor.guarantorNumber"
                      @blur="getGuarantorInfo(guarantor.guarantorNumber, index)"
                      type="number"
                      ></b-form-input>
                  </b-col>
                  </b-row>
                  <b-row class="my-1 form-row mb-3">
                  <b-col sm="4">
                      <label
                      class="pt-1 form-label"
                      >Name <code>*</code></label>
                  </b-col>
                  <b-col sm="8">
                      <b-form-input
                      readonly
                      :value="guarantor.guarantorName"
                      type="text"
                      ></b-form-input>
                  </b-col>
                  </b-row>
                  <b-row class="my-1 form-row mb-3">
                  <b-col sm="4">
                      <label
                      class="pt-1 form-label"
                      >Email Address  <code>*</code></label>
                  </b-col>
                  <b-col sm="8">
                      <b-form-input
                      readonly
                     :value="guarantor.guarantorEmail"
                      type="text"
                      ></b-form-input>
                  </b-col>
                  </b-row>
                </div>
                </div>
              </span>
              <!-- <span v-if="guarant.data === 0">
                <div>
                  <strong> Not Enough Years of Experience for Loan the Request</strong><br/>
                </div>
              </span> -->
            </div>
            <div class="header_2">Method Of Collection</div>
            <b-row class="my-1 form-row mb-3 ">
                <b-col sm="4">
                <label class="pt-1 form-label" 
                    >Bank <code>*</code></label
                >
                </b-col>
                <b-col sm="8">
                <!-- <b-form-select
                    v-model="form.bankcode"
                    :options="banks"
                    required
                ></b-form-select> -->
                <b-form-select
                id="banks"
                v-model="form.bankcode"
                required
            >                                               
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
                </b-col>
            </b-row>
            <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                <label class="pt-1 form-label" 
                    >Account Number <code>*</code></label
                >
                </b-col>
                <b-col sm="8">
                <b-form-input
                    id="`Account Number`"
                    v-model="form.accountNumber"
                    @blur="verifyAcc"
                    type="number"
                    aria-describedby="input-live-help input-live-feedback"
                    trim
                    required
                ></b-form-input>
               <div v-if="form.accountNumber.length !== 10 && form.accountNumber !== ''">
                  <p style="color:red;font-size:12px;" >Account Number must be 10 digits</p>
                </div>
                </b-col>
            </b-row>
            <b-row class="my-1 form-row mb-3">
                <b-col sm="4">
                <label
                    class="pt-1 form-label"                                      
                    >Beneficiary Name <code>*</code></label
                >
                </b-col>
                <b-col sm="8">
                <b-form-input
                    disabled
                    id="`Beneficiary`"
                    v-model="name.data"
                    type="text"
                    required
                ></b-form-input>
                </b-col>
            </b-row>
            <span v-if="memberLogin.memberType == 2">
                <div class="large-12 medium-12 small-12 cell">
                    <div v-if="file" class="progress">
                        <div
                            class="progress-bar progress-bar-info progress-bar-striped"
                            role="progressbar"
                            :aria-valuenow="progress"
                            aria-valuemin="0"
                            aria-valuemax="100"
                            :style="{ width: progress + '%' }">
                            {{ progress }}%
                        </div></div>
                    <label>Upload a Document
                        <br/><input type="file" id="file" ref="file" accept="image/*,.pdf"
                        v-on:change="FileUpload()"/>
                    </label>
                        <!-- <img v-bind:src="imagePreview" v-show="showPreview"/> -->
                </div>
                <!-- <FileUpload/> -->
            </span>
            <div class="form-flex"><b>
                I hereby agree to repay this loan through direct monthly deductions from my salary until the loan principal is fully repaid.
                In addition to my shares in the Credit Society, I hereby pledge all my Chevron entitlements as security for this loan.
            </b><div>
                <b-form-radio v-model="selectedRadio" name="some-radios" 
                    value="Yes">Yes</b-form-radio></div>
                <div><b-form-radio v-model="selectedRadio" name="some-radios" 
                    value="No">No</b-form-radio></div>
            </div>
    <div style="margin-top:50px;" class="button-group">
          <button type="reset" style="background:#c00;display:inline-block;margin-right:30px" class="app-form-button">Reset</button>
           <span v-if="selectedRadio === 'No'">
           <button type="submit" style="display:inline-block" class="app-form-button" disabled>Submit</button></span>
            <span v-if="selectedRadio === 'Yes'">
           <button type="submit" style="display:inline-block" class="app-form-button">Submit</button></span>
    
    </div>
            </b-form>
      </div>
    </template>


<script src="./requestLoan.js"></script>

<style scoped>
    div.container img{
    max-width: 200px;
    max-height: 200px;
  }
.guarantor-chip{
cursor:pointer;background:#57B4DC;color:white;display:inline-block;padding:0 10px;height:30px;line-height:30px;margin-right:15px
}
.guarantor-chip:hover{ background: #c00; color: white;}
</style>
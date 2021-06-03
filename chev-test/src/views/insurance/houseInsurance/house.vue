<template>
<div>        
  <div v-show="loader">
         <Loader/>
      </div>
       <Status :state="state" :closeModal = "closeModal" :message = "message" :resetState="resetState" v-if="status"/>
          <div class="content-header">HOUSE/BUILDING INSURANCE APPLICATION</div>
      <div class="content-sub"><b> Personal Details</b></div>
    <form @submit.prevent="onSubmit">
    <div class="form-flex">   
        <div class="form-flex-col">
            <label class="login-label">Full Name</label>
            <input type="text" class="app-text-field w-input" />
        </div>
        <div class="form-flex-col">
            <label class="login-label">Address</label>
            <input type="text" class="app-text-field w-input" />
        </div>
        <div class="form-flex-col">
            <label class="login-label">Occupation</label>
            <input type="text" class="app-text-field w-input" />
        </div>
        <div class="form-flex-col">
            <label class="login-label">Email Address</label>
            <input type="text" class="app-text-field w-input" />
        </div>
         <div class="form-flex-col">
            <label class="login-label">Location of Property to be Insured</label>
            <input type="text" class="app-text-field w-input" />
          </div>
          <div class="form-flex-col">
            <label class="login-label">Transaction Date</label>
            <date-picker class='input-group date'
              v-model="transactionDate" :config="options"></date-picker>
          </div>         
            <div class="form-flex-col">
                <label class="login-label">Chq./R/N</label>
                <input type="text" class="app-text-field w-input" />
            </div>
      </div >
       <table class="app-table">
            <thead>
                <tr class="app-table-row">
                <th class="app-table-header">S/N                  </th>
                <th class="app-table-header"> ITEM DESCRIPTION</th>
                <th class="app-table-header">VALUE (N)</th>
                <th class="app-table-header">RATE (%)</th>
                <th class="app-table-header">PREMIUM AMOUNT</th>
                <!-- <b-form-checkbox v-model="selected"> </b-form-checkbox> -->
                </tr>
            </thead>
                <tbody>
            <tr class="app-table-row">
                <td  class="app-table-data">1</td>
                <td class="app-table-data">
                    BUILDING
                </td>
                <td class="app-table-data">
                    <input type="text" class="app-text-field w-input" />
                </td>
                <td class="app-table-data">
                    <input type="text" v-model="buildR" class="app-text-field w-input" />
                </td>
                <td class="app-table-data">
                    <input type="text" class="app-text-field w-input" />
                </td>
            </tr>
                <tr class="app-table-row">
                <td  class="app-table-data">2</td>
                <td class="app-table-data">
                    CONTENTS
                </td>
                <td class="app-table-data">
                    <input type="text"  class="app-text-field w-input" />
                </td>
                <td class="app-table-data">
                    <input type="text" v-model="contentR" class="app-text-field w-input" />
                </td>
                <td class="app-table-data">
                    <input type="text" class="app-text-field w-input" />
                </td>
            </tr>
                <tr class="app-table-row">
                <td  class="app-table-data">3</td>
                <td class="app-table-data">
                    GENERATORS
                </td>
                <td class="app-table-data">
                    <input type="text" class="app-text-field w-input" />
                </td>
                <td class="app-table-data">
                    <input type="text" v-model="generatorR" class="app-text-field w-input" />
                </td>
                <td class="app-table-data">
                    <input type="text" class="app-text-field w-input" />
                </td>
            </tr>
            <tr class="app-table-row">
                <td  class="app-table-data">4</td>
                <td class="app-table-data">
                    CASH
                </td>
                <td class="app-table-data">
                    <input type="text" class="app-text-field w-input" />
                </td>
                <td class="app-table-data">
                    <input type="text" v-model="cashR" class="app-text-field w-input" />
                </td>
                <td class="app-table-data">
                    <input type="text" class="app-text-field w-input" />
                </td>
            </tr>
            <tr class="app-table-row">
                <td  class="app-table-data">5</td>
                <td class="app-table-data">
                    LOSS OF RENT
                </td>
                <td class="app-table-data">
                    <input type="text" class="app-text-field w-input" />
                </td>
                <td class="app-table-data">
                    <input type="text" v-model="rentR" class="app-text-field w-input" />
                </td>
                <td class="app-table-data">
                    <input type="text" class="app-text-field w-input" />
                </td>
            </tr>
            <tr class="app-table-row">
                <td  class="app-table-data">6</td>
                <td class="app-table-data">
                    JEWELRIES
                </td>
                <td class="app-table-data">
                    <input type="text"  class="app-text-field w-input" />
                </td>
                <td class="app-table-data">
                    <input type="text" v-model="jewelriesR" class="app-text-field w-input" />
                </td>
                <td class="app-table-data">
                    <input type="text" class="app-text-field w-input" />
                </td>
            </tr>
            <tr class="app-table-row">
                <td  class="app-table-data">7</td>
                <td class="app-table-data">
                    LAPTOP/IPOD/IPAD
                </td>
                <td class="app-table-data">
                    <input type="text" class="app-text-field w-input" />
                </td>
                <td class="app-table-data">
                    <input type="text" v-model="lapR" class="app-text-field w-input" />
                </td>
                <td class="app-table-data">
                    <input type="text" class="app-text-field w-input" />
                </td>
            </tr>
            <tr class="app-table-row">
                <td  class="app-table-data">8</td>
                <td class="app-table-data">
                    PERSONAL LIABILITY EXTENSION
                </td>
                <td class="app-table-data">
                    <input type="text" class="app-text-field w-input" />
                </td>
                <td class="app-table-data">
                    <input type="text" v-model="liabilityR" class="app-text-field w-input" />
                </td>
                <td class="app-table-data">
                    <input type="text" class="app-text-field w-input" />
                </td>
            </tr>
            <tr class="app-table-row">
                <td  class="app-table-data"> </td>
                <td class="app-table-data">
                    <b>TOTAL</b>
                </td>                
            </tr>
                </tbody>
                
                
                </table>
      <div class="row justify-content-md-center">
        <b-col lg="2" class="pb-2"><b-button type="submit" variant="primary" disabled>Submit</b-button></b-col>
        <b-col lg="2" class="pb-2"><b-button variant="outline-primary" disabled>Reset</b-button></b-col>
      </div>
      </form>
  
      </div>
    </template>
    <script src="./house.js"></script>
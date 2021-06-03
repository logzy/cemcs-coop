<template>
<div>
          <div class="content-header">Cash Addition</div>
      <div class="content-sub">Make a cash addition</div>
                      <div >
                        <b-form @submit="onSubmit" @reset="onReset" v-if="show">
                        <strong>
                          <b-form-group
                            label-cols="2"
                            label-cols-lg="3"
                            label-size="sm"
                            label="Name"
                            label-for="input-sm"
                          >
                            {{
                              user.data.person.lastName +
                                ', ' +
                                user.data.person.firstName +
                                ' ' +
                                user.data.person.middleName
                            }}
                            <!-- <b-form-input
                                  id="name-input"
                                  v-model="user.data.person.firstName"
                                  required
                                ></b-form-input >-->
                          </b-form-group>

                          <b-form-group
                            label-cols="2"
                            label-cols-lg="3"
                            label-size="sm"
                            label="Employee Number"
                            label-for="input-sm"
                          >
                            <b-form-input
                              id="number-input"
                              v-model="user.data.employeeNumber"
                              disabled
                              required
                            ></b-form-input>
                          </b-form-group>

                          <b-form-group
                            label-cols="2"
                            label-cols-lg="3"
                            label-size="sm"
                            label="Effective Date"
                            label-for="example-datepicker"
                          >
                          
                            <date-picker class='input-group date down'
                                 v-model="effectiveDate" :config="options"></date-picker>
                          </b-form-group>

                          <b-form-group
                            label-cols="2"
                            label-cols-lg="3"
                            label-size="sm"
                            label="Account Type"
                            label-for="input-sm"
                          ><b-form-select
                              id="input-3"
                              name="input-account"
                              v-model="form.account"
                              :options="accountTypes"
                              required
                            ></b-form-select
                          ></b-form-group>

                          <b-form-group
                            label-cols="2"
                            label-cols-lg="3"
                            label-size="sm"
                            label="Amount Desired"
                            label-for="input-sm"
                            required
                          ><b-form-input
                              id="input-4"
                              v-model.trim="form.amount"
                              :formatter="numberFormat"
                              placeholder="Enter amount desired"
                              required                              
                            ></b-form-input >
                          <span v-if="form.amount != ''"><code>
    {{parseFloat(this.form.amount.replace(/,/g, '')) | NumbersToWords | capitalize}} Naira Only
                    </code></span>
                          </b-form-group>
                        </strong>
                        <div class="form-buttons">
                        <b-button type="submit" variant="primary">Submit</b-button>
                        <b-button type="reset" variant="danger">Reset</b-button>
                        </div>
                        </b-form>

                       <div style="margin-top:50px;" class="button-group">
           <!-- <button type="submit" @click="onSubmit" style="display:inline-block" class="app-form-button">Submit</button> -->
           
    </div>
                      </div>
      </div>
    </template>


<script src="./cashAddition.js"></script>
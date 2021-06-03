<template>
    <div class="basic-table-card">
        <div class="table-header">
            <div class="content-header-2">Loan History</div>
        </div>

        <b-table class="app-table" 
            id="my-table"
        :per-page="perPage"
        :current-page="currentPage"
        small
        striped hover :fields="fields" :items="users" responsive="sm">
        <template #cell(index)="data">
                  {{ data.index + 1 }}
              </template>
              <template #cell(id)="data">
                  <a type="button" @click="getLoanDetails(data.item.id)" variant="primary">
                  <b class="text-info">{{ data.item.id }}</b></a>
              </template>
              <template #cell(loanAmount)="data">
                  <a type="button" @click="getLoanDetails(data.item.id)" variant="primary">
                  <b class="text-info">{{ data.item.loanAmount | price }}</b></a>
              </template>
              <template #cell(repaymentPeriod)="data">
                  <a type="button" @click="getLoanDetails(data.item.id)" variant="primary">
                 <b class="text-info"> {{ data.item.repaymentPeriod }}</b></a>
              </template>
              
              <template #cell(dateSubmitted)="data">
                  <a type="button" @click="getLoanDetails(data.item.id)" variant="primary">                  
                  <b class="text-info">{{ data.item.dateSubmitted | hum }}</b></a>
              </template>

            
        </b-table>
        <b-pagination
            v-model="currentPage"
            :total-rows="totalRows"
            :per-page="perPage"
            aria-controls="my-table"
            ></b-pagination>
        <b-modal
            v-model="show"
            title="Loan Application"
            :header-bg-variant="headerBgVariant"
            :header-text-variant="headerTextVariant"
            :body-bg-variant="bodyBgVariant"
            :body-text-variant="bodyTextVariant"
            >
      <b-container class="justify-content-md-center">
          <b-row class="mb-1 text-left">
          <b-col cols="5"><b>Loan Application ID</b></b-col>          
          <b-col cols="3"></b-col>
          <b-col>
            <b>{{loanDetails.id}}</b>
          </b-col>          
        </b-row>

        <b-row class="mb-1 text-left">
          <b-col cols="5">Loan Amount</b-col>       
          <b-col cols="3"></b-col>
          <b-col>
            {{loanDetails.loanAmount | price}}
          </b-col>          
        </b-row>
        
        <b-row class="mb-1 text-left">
          <b-col cols="5">Principal Amount</b-col>       
          <b-col cols="3"></b-col>
          <b-col>
            {{loanDetails.principal | price}}
          </b-col>          
        </b-row>
        <b-row class="mb-1 text-left">
          <b-col cols="5">Interest Amount</b-col>       
          <b-col cols="3"></b-col>
          <b-col>
            {{loanDetails.interest | price}}
          </b-col>          
        </b-row>
        <b-row class="mb-1 text-left">
          <b-col cols="5">Repayment Period</b-col>       
          <b-col cols="3"></b-col>
          <b-col>
              {{loanDetails.repaymentPeriod}}
          </b-col>          
        </b-row>
        <b-row class="mb-1 text-left">
          <b-col cols="5">Loan Application Date</b-col>       
          <b-col cols="3"></b-col>
          <b-col cols="4">
            {{loanDetails.dateSubmitted | hum}}
          </b-col>          
        </b-row>
        

        <!-- <b-row class="mb-1">
          <b-col cols="3">Header</b-col>
          <b-col>
            <b-form-select
              v-model="headerBgVariant"
              :options="variants"
            ></b-form-select>
          </b-col>
          <b-col>
            <b-form-select
              v-model="headerTextVariant"
              :options="variants"
            ></b-form-select>
          </b-col>
        </b-row> -->

        <!-- <b-row class="mb-1">
          <b-col cols="3">Body</b-col>
          <b-col>
            <b-form-select
              v-model="bodyBgVariant"
              :options="variants"
            ></b-form-select>
          </b-col>
          <b-col>
            <b-form-select
              v-model="bodyTextVariant"
              :options="variants"
            ></b-form-select>
          </b-col>
        </b-row> -->

        <!-- <b-row>
          <b-col cols="3">Footer</b-col>
          <b-col>
            <b-form-select
              v-model="footerBgVariant"
              :options="variants"
            ></b-form-select>
          </b-col>
          <b-col>
            <b-form-select
              v-model="footerTextVariant"
              :options="variants"
            ></b-form-select>
          </b-col>
        </b-row> -->

      </b-container>

      <template #modal-footer>
        <div class="w-100">
          <p class="float-left">CECMS</p>
          <b-button
            variant="primary"
            size="sm"
            class="float-right"
            @click="show=false"
          >
            Close
          </b-button>
        </div>
      </template>
    </b-modal>
    </div>
</template>

<script src="./history.js"></script>

<style src=".././global.css"></style>
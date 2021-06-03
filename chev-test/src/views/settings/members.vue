<template>
<div>
          <div class="content-header">Personal Details</div>

         <div>
                          <div>
                            <!-- <div class="form-buttons">
                              <span></span>
                              <b-button to="/register" variant="primary" class="float-sm-left">+</b-button>
                            </div> -->
                            <b-table striped hover small :fields="fields" :items="items.data" responsive="sm">                              
                              <template #cell(index)="data">
                                {{ data.index + 1 }}
                              </template>
                              <template #cell(name)="data">
                                <b class="text-info">{{ data.item.person.lastName.toUpperCase() }}</b>,
                                <b>{{ data.item.person.firstName}}</b>
                              </template>
                              <template #cell(Eno)="data">
                                {{ data.item.employeeNumber }}
                              </template>
                              <template #cell(member)="data">
                                <span v-if="data.item.memberType == 1">Regular</span>
                                <span v-if="data.item.memberType == 2">Retiree</span>
                                <span v-if="data.item.memberType == 3">Expatriate</span>
                              </template>
                              <template #cell(email)="data">                                
                                {{ data.item.person.email }}
                              </template>
                            </b-table>
                          </div>
                        </div>
  
      </div>
    </template>


<script src="./members.js"></script>
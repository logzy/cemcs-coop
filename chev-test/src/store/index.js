import Vue from "vue";
import Vuex from "vuex";

import axios from "axios"

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    status: '',
    token: localStorage.getItem('token') || '',
    user : {},
    member: {},
    balance:{},
  },  
  actions: {
    login({commit}, user){
      return new Promise((resolve,reject) => {
        commit('auth_request')
        axios({url: `${process.env.VUE_APP_API_URL}/Users/login`, data: user, method: 'POST' })
        .then(resp => {
          const token = resp.data.token
          const user = resp.data.user
          const userType = resp.data.userType
          localStorage.setItem('token', token)
          localStorage.setItem('userType', userType)
          axios.defaults.headers.common['Authorization'] = token
          commit('auth_success', token, user)
          resolve(resp)
        })
        .catch(err => {
          commit('auth_error')
          localStorage.removeItem('token')
          reject(err)
        })
        
      })
    },

    memberDetails(){
      return new Promise((resolve, reject) => {
        axios({url: `${process.env.VUE_APP_API_URL}/Members/Usertype`, method: 'GET',headers: {
          'Content-Type': 'application/json;charset=utf-8',
          Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        })
        .then(response => {         
          // const member = response.data JSON.parse(localStorage.getItem("LoanPlan"));
          const member = response.data.data
          this.commit('member', member)    
          resolve(response)
        })
        .catch(err => {
              reject(err)
        });      
      })
    },

    memberBalance(){
      return new Promise((resolve, reject) => {
        axios({url: `${process.env.VUE_APP_API_URL}/MemberBalances/balance`, method: 'GET',headers: {
          'Content-Type': 'application/json;charset=utf-8',
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      })
        .then(response => {
          const balance = response.data.data
          this.commit('balance', balance) 
          resolve(response)
        })
        .catch(err => {
              reject(err)
        });      
      })
    },    
    createAccout({commit}, Newuser){
      return new Promise((resolve, reject) => {
        commit('auth_request')
        axios({url: `${process.env.VUE_APP_API_URL}/Users/Register`, data: Newuser, method: 'POST' })
        .then(resp => {
          const token = resp.data.token
          const memType = resp.data.userType
          const empNum = resp.data.userName
          const email = resp.data.email
          axios.defaults.headers.common['Authorization'] = token
          commit('auth_success', token, empNum,email,memType)
          resolve(resp)
        })
        .catch(err => {
          commit('auth_error', err)
          reject(err)
        })
      })
    },
    
    logout({commit}){
      return new Promise((resolve) => {
        commit('logout')
        localStorage.removeItem('token')        
        localStorage.removeItem('memType')
        localStorage.removeItem('empNum')
        localStorage.removeItem('email')
        localStorage.removeItem('memberId')
        localStorage.removeItem('LoanPlan')
        localStorage.removeItem('userType')
        delete axios.defaults.headers.common['Authorization']
        resolve()
      })
    },

    getLoanApplication(Id) {
      return new Promise((resolve, reject) => {
        axios({url: `${process.env.VUE_APP_API_URL}/Loans/${Id}`, method: 'GET'})
        .them(response => {
          this.commit("AppLoanId", response.data)
          resolve(response)
        }).catch(err => {
          reject(err)
        })
      })
    },

    states(){
      return new Promise((resolve, reject) => {
        axios({url: `${process.env.VUE_APP_API_URL}/States/All`, method: 'GET'})
        .then(response => {         
          const state = response.data
          console.log(state);    
          resolve(response)
        })
        .catch(err => {
              reject(err)
        });      
      })
    },
  },

  mutations: {
    auth_request(state){
      state.status = 'loading'
    },
    auth_success(state, token, user){
      state.status = 'success'
      state.token = token
      state.user = user
    },
    auth_error(state){
      state.status = 'error'
    },
    logout(state){
      state.status = ''
      state.token = ''
    },
    member(state, member) {
      state.member =  member;
    },
    balance(state,balance) {
      state.balance = balance;
    },
    AppLoanId(state, appLoan){
      state.appLoan = appLoan.data
    },
    setShowAlert(state, value) {
      state.showAlert = value
    },

  },
  getters : {
    isLoggedIn: state => !!state.token,
    authStatus: state => state.status,
    balance:state => state.balance,
    member:state => state.member
    // variant: state => state.variant,
    // message: state => state.message,
    // showAlert: state => state.showAlert,

  }, 

});

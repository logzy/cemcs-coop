<template>
  <div>
    <div class="right-side-bar">
      <div class="profile-content">
        <div class="profile-image">
          <img
            src="../../../assets/images/ademola.jpg"
            class="profile-picture"
            alt=""
            srcset=""
          />
          <p class="profile-name"><strong><code>{{user.data.employeeNumber}}</code></strong>
          </p>
          <p class="profile-name"><strong>
            {{user.data.person.lastName+", " +user.data.person.firstName+" " 
                              +user.data.person.middleName}}</strong>
          </p>
        </div>
        <div class="line"></div>
        <div class="profile-balances">
          <!-- <div class="profile-balance">
            <div class="profile-balance-type">
              Savings Balance:
            </div>
            <h3 class="profile-balance-amount">
              N 100,000
            </h3>
          </div> -->
          <!-- <div class="profile-balance">
            <div class="profile-balance-type">
              Loans Balance:
            </div>
            <h3 class="profile-balance-amount">
              N 100,000
            </h3>
          </div> -->
        </div>
        <div id="last-login">
          <div class="last-login">Your last login:</div>
          <div class="last-login-time">21-Feb-2021 5:03:42 PM</div>
        </div>
      </div>
      <!-- <div id="ads">
               <img src="../../../assets/images/mtn_ads.jpg" class="ads-picture" alt="" srcset="">
           </div> -->
    </div>
  </div>
</template>


<script>

import axios from "axios";

export default {
    data() {
        return {
          user:[]
        };
  },

  // mounted() {
  //     this.member = localStorage.getItem('data');
  // },
  // mounted() {
  //   if (localStorage.member) {
  //     this.member = localStorage.member;
  //   }
  // },
  // computed: {
  //   getmember: {
  //     get () {
  //       return this.$store.state.member;
  //     }      
  //   }
  // },
  async created() {
      await this.initUser();
  },
  methods: {

    async initUser() {
      await axios
        .get(`${process.env.VUE_APP_API_URL}/Members/Usertype`, {
          headers: {
            'Content-Type': 'application/json;charset=utf-8',
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        })
        .then((response) => {
          this.user = response.data;
        })
        .catch((error) => {
          error.alert('Error');
        });
    },
  }
    
};
</script>
<style></style>
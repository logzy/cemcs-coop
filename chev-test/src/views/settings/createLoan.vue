<template>
  <div>

          <div class="content-header">Create Loan</div>
                                        <b-form
                                  ref="form"
                                  @submit="onSubmit"
                                  @reset="onReset"
                                >
                                <b-row class="my-1 form-row mb-3 ">
                                  <b-col sm="4">
                                    <label class="pt-1 form-label" :for="code"
                                      >Loan Code <code>*</code></label
                                    >
                                  </b-col>
                                  <b-col sm="8">
                                    <b-form-input
                                      v-model="LoanCode"
                                    ></b-form-input>
                                  </b-col>
                                </b-row>
                                <div>
                                  <b-row class="my-1 form-row mb-3 ">
                                    <b-col sm="4">
                                      <label
                                        class="pt-1 form-label"
                                        :for="Name"
                                        >Loan Name
                                        <code>*</code></label
                                      >
                                    </b-col>
                                    <b-col sm="8">
                                      <b-form-input
                                        v-model="name"
                                        type="text"
                                      ></b-form-input>
                                    </b-col>
                                  </b-row>
                                  <b-row class="my-1 form-row mb-3">
                                    <b-col sm="4">
                                      <label
                                        class="pt-1 form-label"
                                        >Date Description </label
                                      >
                                    </b-col>
                                    <b-col sm="8">
                                      <b-form-textarea
                                        id="description"
                                        v-model="description"
                                        placeholder="Enter description..."
                                        rows="3"
                                        max-rows="6"
                                      ></b-form-textarea>
                                    </b-col>
                                  </b-row>
                                  <b-row class="my-1 form-row mb-3">
                                    <b-col sm="4">
                                      <label
                                        class="pt-1 form-label"
                                        :for="LoanType"
                                        >Loan Type<code>*</code></label
                                      >
                                    </b-col>
                                    <b-col sm="8">
                                      <b-form-select
                                      v-model="loanType"
                                      :options="loanTypes"
                                    ></b-form-select>
                                    </b-col>
                                  </b-row>
                                </div>
                                  <div style="margin-top:50px;" class="button-group">
          <button  style="background:#c00;display:inline-block;margin-right:30px" class="app-form-button">Reset</button>
           <button type="submit" style="display:inline-block" class="app-form-button">Submit</button>
    </div>
                                                        
                              </b-form>

</div>

</template>


<script>
import axios from "axios";

export default {
  name: "Home",
  data() {
    return {
      checked: false,
      selectedLoan: "",

      loanType: null,
      LoanCode: "",
      Name: "",
      description: "",

      loanTypes: [
        {value: null, text: "Select Loan Type"},
        { value: 1, text: "Target special loan" },
        { value: 2, text: "Executive loan" },
        { value: 3, text: "Regular loan" }
        
        
      ],
      exepctedLumpSumTypes: [
        {value: null, text: "Select"},
        { value: "1", text: "Lump type 1" },
        { value: "2", text: "Lump type 2" },
        { value: "3", text: "Lump type 3" }
      ]
    };
  },

  methods: {

    makeToast(variant = null) {
        this.notify++
        this.$bvToast.toast(`Loan Added`, {
          title: 'Successful',
          variant: variant,
          solid: true,
          autoHideDelay: 5000,
        })
      },

    onReset(event) {
      event.preventDefault();

      this.LoanCode = "",
      this.name = "",
      this.description = "",
      this.loanType = null,

      this.$nextTick(() => {
        this.show = true;
      });
    },
    
    async onSubmit(event) {
      event.preventDefault();
      // // Exit when the form isn't valid
      // if (!this.checkFormValidity()) {
      //   return;
      // }
      let data = {
        LoanCode : this.LoanCode,
        Name : this.name,
        Description : this.description,
        Active: true,
        LoanType : this.loanType
      }      
      data = JSON.stringify(data);
      await axios
        .post(`${process.env.VUE_APP_API_URL}/Loans`, data, {
          headers: {
            "Content-Type": "application/json;charset=utf-8"
          }
        })
        .then(response => {
          this.rawData = response.data;
          this.makeToast(`success`);
        })
        .catch(error => {
          alert(error);
        });
    }

  }



};
</script>

<template>
  <div class="account-overview-content">
    <div class="mt-2">
      <div class="header_background" @setParamResp="Onset">Loan Schedule</div>
    </div>    
    <span v-if="loanPlan.data !== null">
    <div class=col-md-12>
        <b-table striped hover small :fields="fields" :items="loanPlan.data" responsive="sm">                              
              <template #cell(index)="data">
                  {{ data.index + 1 }}
              </template>
              <template #cell(loanApplicationId)="data">
                  <b class="text-info">{{ data.item.loanApplicationId }}</b>
              </template>
              <template #cell(repaymentDate)="data">
                  {{ data.item.repaymentDate | hum }}
              </template>
              <template #cell(principal)="data">
                  <b class="text-info">{{ data.item.principal | price }}</b>
              </template>
              <template #cell(interest)="data">
                  <b class="text-info">{{ data.item.interest | price }}</b>
              </template>
              <template #cell(totalPayment)="data">
                  <b class="text-info">{{ data.item.totalPayment | price }}</b>
              </template>
          </b-table>
        </div>
        </span>
        <span v-if="loanPlan.data === null">
    <div>
      <div class="row">
        <div class="col-md-12">
          <div>
            <ul id="example-2">
              <li v-for="item in loanPlan.errors" :key="item">
               <p>
                <!--  <strong> Error Code: </strong>  {{item.errorCode}}<br/> -->
                  <strong>  {{item.errorMessage}}</strong> </p>
              </li>
            </ul>            
          </div>
        </div>
      </div>
    </div>
    </span>
  </div>
</template>

<script>


export default {
  name: "Home",
  components: {
  },
  data() {
    return {
      loanApplicationId:"",
      repaymentDate:"",
      principal:"",
      interest:"",
      totalPayment:"",
      data: "",
      message:"",
      errors: {
        errorCode: "",
        errorMessage: "",
    },
      
      fields: [
        {key: 'index', label: 'S/N'},
        { key: 'loanApplicationId', label: 'Loan ID' },
        { key: 'repaymentDate', label: 'Repayment Date.' },
        { key: 'principal', label: 'Principal(₦)' },        
        { key: 'interest', label: 'Interest(₦)' },
        {key:'totalPayment', label: 'Total(₦)'}
      ],    
    };
  },
  async created() {
    this.loanPlan = JSON.parse(localStorage.getItem("LoanPlan"));
  },
  methods: {    
    async Onset(result) {
    this.set = await this.result;
    console.log(result);
  }

  },

};
</script>

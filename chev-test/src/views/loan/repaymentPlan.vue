<template>
            <div class="app-admin-section">
            <div class="app-admin-col-1">
            <Leftbar/>
            </div>
            <div class="app-admin-col-2">
            <div class="admin-top-bar">
        <div class="admin-top-bar-left">
          <div class="settings-icon" @click="$router.go(-1)"></div>
        </div>
        <div class="admin-top-bar-right">
          <div class="admin-topbar-date">{{new Date().toLocaleString() | humanize}}</div>
        </div>
      </div>
        <div class="content-header" @setParamResp="Onset">Loan Schedule</div>
            <!-- <div class="content-sub2"> Repayment Plan</div> -->
                <div class="account-overview-content">
                    <!-- <div class="mt-2">
                        <div class="header_background" @setParamResp="Onset">Loan Schedule</div>
                    </div>     -->
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
                </div>
                </div>
            <div class="app-admin-col-3">
              <Rightbar />
            </div>
          </div>
</template>


<script>
import Leftbar from '../../components/leftbar/leftbar'
import Rightbar from '../../components/rightbar/rightbar'
export default {
  components: {
    Leftbar,
    Rightbar
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


}

</script>

<style src=".././global.css"></style>
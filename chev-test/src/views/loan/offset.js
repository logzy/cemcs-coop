import createNumberMask from 'text-mask-addons/dist/createNumberMask';
  const currencyMask = createNumberMask({
    prefix: '',
    allowDecimal: true,
    decimalLimit :2,
    includeThousandsSeparator: true,
    allowNegative: false,
  });
export default {
    data() {
        return {
            show: true,
            mask: currencyMask,
            form: {
                loanType: "Select a Loan Type",
                waiverType: "Select a Waiver Type",
                offsetAmount:"",
                cashAmount:"",
                depositAmount:"",
                MonthSaving:"",
                balance:"",
                depositBalance:""
            },
           waiverDetails: []
        };
    },
    computed: {
      memberBalance() {
        return this.$store.state.balance
      }
    },
    created() {
      this.$store.dispatch('memberBalance');
    },
    methods: {

        onReset(event) {
            event.preventDefault()
            this.form.loanType = null,
            this.form.waiverType = null,
            this.form.offsetAmount ="",
            this.form.cashAmount ="",
            this.form.depositAmount ="",
            this.form.MonthSaving ="",
            this.form.balance ="",
            this.form.depositBalance =""
            this.show = false
            this.$nextTick(() => {
              this.show = true
            })
          },
        async onSubmit(event) {
            event.preventDefault()
            console.log('click')
            let value = this.waiverForm
            console.log("d?>>>", value)
            this.waiverDetails.push(value)
            console.log(this.waiverDetails)
        }
    }, 
};

import axios from 'axios';
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
            mask: currencyMask,
            waiverForm: {
                loanType: "Select a Loan Type",
                waiverType: "Select a Waiver Type",
                amount: "",
                fee: ""
            },
           waiverDetails: []
        };
    },
    async mounted() {
        await this.paymentMode();
      },
    methods: {
        addWaiver(event) {
            event.preventDefault()
            
            console.log('click')
            let value = this.waiverForm
            console.log("d?>>>", value)
            this.waiverDetails.push(value)
            console.log(this.waiverDetails)
        },

        async paymentMode() {
            await axios
              .get(`${process.env.VUE_APP_API_URL}/Payments/modes/All`, {
                headers: {
                  'Content-Type': 'application/json;charset=utf-8'
                },
              })
              .then((response) => {
                this.modeOfPay = response.data.data;
              })
          },
    }, 
};

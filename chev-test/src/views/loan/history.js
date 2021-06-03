import axios from 'axios'

export default {
    data() {
      return {
        perPage: 10,
        currentPage: 1,
        users: [],
        loanDetails:{},
        loanAmount:"",
        show: false,
        variants: ['primary', 'secondary', 'success', 'warning', 'danger', 'info', 'light', 'dark'],
        headerBgVariant: 'primary',
        headerTextVariant: 'light',
        bodyBgVariant: 'light',
        bodyTextVariant: 'dark',
        footerBgVariant: 'danger',
        footerTextVariant: 'light',
        fields: [
            {key: 'index', label: 'S/N'},
            { key: 'id', label: 'Loan ID' },
            { key: 'loanAmount', label: 'Loan Amount' },
            { key: 'repaymentPeriod', label: 'Repayment Period' },
            { key: 'dateSubmitted', label: 'Loan Application Date' },
          ],  
      };
    },
    async mounted() {    
      await this.initUser();
  },
  created() {
    for (let i = 0; i < 1000; i++) {
        this.users.push({
          id: i + 1
        });
      }
    },
    computed: {
      totalRows() {
        return this.users.length
      }
    },
    methods: {  
      async initUser() {
        await axios
          .get(`${process.env.VUE_APP_API_URL}/Loans/employeenumber`, {
            headers: {
              'Content-Type': 'application/json;charset=utf-8',
              Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
          })
          .then((response) => {
            this.users = response.data.data;
          })
          .catch((error) => {
            if (error.response.status == 401) {
              // auto logout if 401 response returned from api
              localStorage.removeItem('token')
              this.$router.push('/')
          }
          });
      },

      async getLoanDetails(id) {
        await axios
        .get(`${process.env.VUE_APP_API_URL}/Loans/application/${id}`, {
          headers: {
            'Content-Type': 'application/json;charset=utf-8',
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        })
        .then((response) => {
          this.loanDetails = response.data.data;
          this.show=true
        })
        .catch((error) => {
          if (error.response.status == 401) {
            // auto logout if 401 response returned from api
            localStorage.removeItem('token')
            this.$router.push('/')
        }
        });

      }
    }
  }
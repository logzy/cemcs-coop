
import Menu from '../../components/layout/headers/menus.vue';
import RightSidebar from '../../components/layout/sidebar/profile-sidebar.vue';
import Footer from '../../components/layout/footer/footer.vue';

import moment from 'moment'
import axios from 'axios';
export default {
  name: 'Home',
  components: {
    Menu,
    RightSidebar,
    Footer,
  },
  data() {
    return {
      show: true,
      notify: 0,
      options: {
          format: 'YYYY-MM-DD',
          useCurrent: false,
          showClear: true,
          showClose: true,
        },
      user: {},
      employeeNumber: '',
      name: '',
      form: {
        account: null,
        amount: '',
      },
      effectiveDate: new Date(),
      effectiveMonth: "",
      effectiveYear: moment(new Date().toLocaleString()).format("YYYY"),
      accountTypes: [
        { text: "---Select Account Type---", value: null, disabled: true },
        { value: 1, text: "Savings " },
        { value: 2, text: "Special Deposit " }
      ],

      Months: [
        { name: "January", value: 0 },
        { name: "February", value: 1 },
        { name: "March", value: 2 },
        { value: 3, name: "April" },
        { value: 4, name: "May" },
        { value: 5, name: "June"},
        { value: 6, name: "July" },
        { value: 7, name: "August" },
        { value: 8, name: "September" },
        { value: 9, name: "October"},
        { value: 10, name: "November" },
        { value: 11, name: "December"}
      ],
    };
  },
  async mounted() {
    await this.initUser();
    this.effectDate();
  },
  methods: {


    effectDate () {
      const current = new Date();
      const currentDate = current.getDate();
      if (currentDate < 15) {
        return this.effectiveMonth = current.getMonth();
      }
      else {
       return  this.effectiveMonth = current.getMonth() +1;
      }    
    },

    numberFormat(value) {
        this.points = Number(value.replace(/\D/g, ''))
        return value == '0.00' ? '' : this.points.toLocaleString();
      },
      
    makeToast(variant = null) {
      this.notify++;
      this.$bvToast.toast(`Cash Addition Added`, {
        title: "Successful",
        variant: variant,
        solid: true,
        autoHideDelay: 5000
      });
    },

    onReset(event) {
      event.preventDefault()
      // Reset our form values
      this.form.amount = ''
      this.form.account = null
      // Trick to reset/clear native browser form validation state
      this.show = false
      this.$nextTick(() => {
        this.show = true
      })
    },

    async onSubmit(event) {
      event.preventDefault()

      let rawData = {
        TransactionDate: this.effectiveDate,
        MemberId: this.user.data.id,
        DepositAmount: parseInt(this.form.amount.replace(/,/g, '')),
        SavingsType: this.form.account,
        TransactionTypeId: 3,
      };
      rawData = JSON.stringify(rawData);
      await axios
        .post(
          `${process.env.VUE_APP_API_URL}/SavingDepositTransactions`,
          rawData,
          {
            headers: {
              'Content-Type': 'application/json;charset=utf-8'
            },
          }
        )
        .then((response) => {
          this.rawData = response.data;
          this.makeToast(`success`);
          if (this.form.MemberType != 2) {
            window.history.length >
              this.$router.push(`/payment}`);
          }
        })
        .catch(error => {
          this.$bvToast.toast(error.message, {
                title: "Error",
                variant: "danger",
                solid: true,
                autoHideDelay: 5000
            });
        });
    },

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
    },
  },
};
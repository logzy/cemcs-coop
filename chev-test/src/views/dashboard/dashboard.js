import Leftbar from '../../components/leftbar/leftbar'
import Rightbar from '../../components/rightbar/rightbar'
// import axios from 'axios';


export default {
  name: "Home",
  components: {
    Leftbar,
    Rightbar
  },
  data() {
    return {
      user: null,
      balance:null,
      firstName:"",
    userType: localStorage.getItem('userType'),
      transactionHistory: [
        { amount: "0", type: "Loan", date: `20-12-2020 9:00pm` },
        { amount: "0", type: "withdraw", date: "20-12-2020 9:00pm" },
        { amount: "0", type: "Transfer", date: "20-12-2020 9:00pm" },
        { amount: "0", type: "Saving", date: "20-12-2020 9:00pm" }
      ]
    };
  },
computed: {
  memberBalance() {
    return this.$store.state.balance
  },
  memberLogin() {
    return this.$store.state.member
  }
},
created() {
  this.$store.dispatch('memberBalance');
  this.$store.dispatch('memberDetails');
},
  methods: {}
}
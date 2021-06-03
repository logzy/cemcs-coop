import LeftbarInsurance from '../../../components/leftbar/leftbarInsurance'
import Rightbar from '../../../components/rightbar/rightbar'
// import axios from 'axios';


export default {
  name: "Home",
  components: {
    LeftbarInsurance,
    Rightbar
  },
  data() {
    return {
      user: null,
      balance:null,
      firstName:"",
    userType: localStorage.getItem('userType'),
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
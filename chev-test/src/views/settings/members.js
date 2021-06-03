import axios from "axios";

export default {
  name: "Home",
  data() {
    return {
      items: [],  
      fields: [
        {key: 'index', label: 'S/N'},
        { key: 'name', label: 'Full Name' },
        { key: 'Eno', label: 'Emp.No' },
        { key: 'member', label: 'Type' },
        { key: 'email', label: 'E-Mail' }
      ],
    };
  },
  async created() {
    await this.initialize();
  },
  methods: {
    currentDateTime() {
      const current = new Date();
      const date = current.toDateString(); //+'-'+(current.getMonth()+1)+'-'+current.getDate();
      const time = current.getHours() + ":" + current.getMinutes(); // + ":" //+ current.getSeconds();
      const dateTime = date + " " + time;

      return dateTime;
    },
    async initialize() {        
     await axios
        .get( `${process.env.VUE_APP_API_URL}/Members/All`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
             Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.items = response.data;
        })
        .catch(error => {
          error.alert("Error");
        });
    }, 
  }

}
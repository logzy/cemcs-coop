import axios from 'axios'

export default {
  data() {
    return {
        perPage: 10,
        currentPage: 1,
        show: false,
        variants: ['primary', 'secondary', 'success', 'warning', 'danger', 'info', 'light', 'dark'],
        headerBgVariant: 'primary',
        headerTextVariant: 'light',
        bodyBgVariant: 'light',
        bodyTextVariant: 'dark',
        footerBgVariant: 'danger',
        footerTextVariant: 'light',
       modules: [],
      selectedModule:null,
      approve: [],
      EmpNo:"",
      Name:"",
      LoanAmount:0,
      dateSubmitted:"",
      effectiveDate:"",
      interest:0,
      principal:0,
      repayment:0,
      fileDownload:"",
      Paid:Boolean,
      moduleApproverId:0,
      id:0,
      itemId:0,    
      approveData: {
        aprroved:Boolean
      },      
      selectedOpt: null,  
      fields: [
        {key: 'index', label: 'S/N'},
        { key: 'name', label: 'Full Name' },
        { key: 'employeeNumber', label: 'Employee No.' },
        { key: 'active', label: 'Status' },        
        { key: 'memberType', label: 'Member Type' },
        {key:'show_details', label: 'Action '}
      ],
      options: [
        {value: null, text: "Select Option"},
        { value: 0, text: "Pending Approval" },
        { value: 1, text: "Approved Members" }
      ]
    };
  },
  async created() {    
    await this.getAll();
    await this.initModules();
    // await this.initLoan();
    // await this.initMember()


  },
  methods: {
    Details(detail,index) {
        if (this.selectedModule == 2){
          this.EmpNo = detail[index].itemData.employeeNumber
          this.Name = detail[index].itemData.person.lastName.toUpperCase() +", " + detail[index].itemData.person.firstName
          this.Paid = detail[index].itemData.hasPaidFee
          this.id = detail[index].id
          this.moduleApproverId = detail[index].moduleApproverId
          this.itemId = detail[index].itemId
          this.show=true
        }
        if (this.selectedModule == 3){
          this.EmpNo = detail[index].itemData.member.employeeNumber
          this.Name = detail[index].itemData.member.person.lastName.toUpperCase() +", " + detail[index].itemData.member.person.firstName
          this.Paid = detail[index].itemData.isPaid
          this.id = detail[index].id
          this.moduleApproverId = detail[index].moduleApproverId
          this.itemId = detail[index].itemId
          this.LoanAmount = detail[index].itemData.loanAmount
          this.dateSubmitted = detail[index].itemData.dateSubmitted
          this.effectiveDate = detail[index].itemData.effectiveDate
          this.interest = detail[index].itemData.interest
          this.principal = detail[index].itemData.principal
          this.repayment = detail[index].itemData.repaymentPeriod
          this.fileDownload = [detail[index].itemData.filePath]
          console.log('file 1: ',JSON.stringify(this.fileDownload))

          this.show=true
        }
      },
      download(fileDownload) {
        // new Blob(['{"name": "test"}'])
        var file = (window.URL || window.webkitURL).createObjectURL(new Blob([fileDownload], {type: "image/png"}));
        // ( /\.(pdf|jpe?g|png|gif)$/i.test( this.file.name ) )
        var fileLink = document.createElement('a');   
        fileLink.href = file;
        fileLink.setAttribute('download', 'paySlip.png');
        // document.body.appendChild(fileLink);
        fileLink.click();
      },
    async getAll() {        
     await axios
        .get( `${process.env.VUE_APP_API_URL}/Members/All`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.approve = response.data;
        })
        .catch(error => {
          error.alert("Error");
        });
    }, 

    async initModules() {        
     await axios
        .get(`${process.env.VUE_APP_API_URL}/ModuleApprovers/Modules`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8"
          }
        })
        .then(response => {
          this.modules = response.data;
        })
        .catch(error => {
            this.$bvToast.toast(error, {
                title: "Error",
                variant: "danger",
                solid: true,
                autoHideDelay: 5000
            });
          });
    },
    async initMember() {
     await axios
        .get( `${process.env.VUE_APP_API_URL}/PendingApproval/Members`,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
          this.approve = response.data;
        })
        .catch(error => {
          error.alert("Error");
        });
    },

    async init(selectedModule) {
      if (selectedModule == 2) {    
        return await this.initMember();        
      }
      if (selectedModule == 3) {
        return await this.initLoan();        
      }
    },

    async initLoan() {
      await axios
         .get( `${process.env.VUE_APP_API_URL}/PendingApproval/Loan`, {          
          headers: {
             "Content-Type": "application/json;charset=utf-8",
             Authorization: `Bearer ${localStorage.getItem('token')}`,
             "responseType": 'blob',
           }
         })
         .then(response => {
           this.approve = response.data;
         })
         .catch(error => {
           alert(error);
         });
     },

    async ApproveModuleRequest(Id,ModuleApproverId,itemId) { 
      let rawData = {
        Id: Id,
        ModuleApproverId:ModuleApproverId,
        itemId:itemId,
        Approved : true
      };
      rawData = JSON.stringify(rawData);       
     await axios
        .post( `${process.env.VUE_APP_API_URL}/PendingApproval/Approve`, rawData,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
            this.approve = response.data;            
            this.show=false
            this.initapprove();
        })
        .catch(error => {
          error.alert("Error");
        });
    },
    async RejectModuleRequest(Id,ModuleApproverId,itemId) { 
      let rawData = {
        Id: Id,
        ModuleApproverId:ModuleApproverId,
        itemId:itemId,
        Approved : false
      };
      rawData = JSON.stringify(rawData);       
     await axios
        .post( `${process.env.VUE_APP_API_URL}/PendingApproval/Approve`, rawData,{
          headers: {
            "Content-Type": "application/json;charset=utf-8",
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        })
        .then(response => {
            this.approve = response.data;
            this.show=false
        })
        .catch(error => {
          error.alert("Error");
        });
    }
}

};
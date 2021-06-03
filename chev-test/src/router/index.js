import Vue from "vue";
import VueRouter from "vue-router";
// import Home from "../views/Home.vue";
import Overview from "../views/dashboard/dashboard.vue";
import Dashboard from "../views/dashboard/userDashboard.vue";
import Transfer from "../views/tranfer/transfer.vue";
import Transfer_ from "../views/tranfer/transfer_.vue";
import Withdrawal from "../views/withdrawal/withdrawal.vue";
import Members from "../views/registration/members.vue";
import Loan from "../views/loans/loans.vue";
import Plan from "../views/loan/repaymentPlan.vue";
import Setup from "../views/loanSetUp/setup.vue";
import Config from "../views/loanSetUp/config.vue";
import PriCon from "../views/loanSetUp/pry_con_setup.vue";
import Register from "../views/auth/Register2.vue";
import Cash_addition from "../views/cash/cashAddition.vue";
import Dec_Inc from "../views/cash/decrease_increase.vue";
import Register1 from "../views/registration/reg.vue";
import NewAccount from "../views/auth/Register1.vue";
import Guarantor from "../views/guarantorPage.vue";
import Payment from "../views/payment.vue";
import Login from "../views/auth/Login.vue";
import store from "../store";
import Confirmation from "../views/confirmation.vue";
import Employee from "../views/settings/employeeReg.vue";
import mEmployee from "../views/registration/viewEmployee.vue";
import mApproval from "../views/settings/viewApproval.vue";
import Approvals from "../views/registration/approvals.vue";


import Transactions from "../views/transactions/transactions.vue";
import Savings from "../views/savings/savings.vue";
import LoanOption from "../views/loan/loan.vue";
import Settings from "../views/settings/settings.vue";
import LoanPlanner from "../views/LoanPlanner/loanplanner.vue";
import LoanGuidelines from "../views/loanGuidelines/loanGuidelines.vue";
import ExternalLogin from "../views/externalLogin.vue";

import Insurance from "../views/insurance/dashboard/dashboard.vue";
import vehicleInsurance from "../views/insurance/vehicleInsurance/options.vue";
import houseInsurance from "../views/insurance/houseInsurance/options.vue";


Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Login",
    component: Login,
    meta: { guest: true },
  },
  {
    path: "/ExternalLogin",
    name: "ExternalLogin",
    component: ExternalLogin,
  },
  {
    path: "/settings",
    name: "Settings",
    component: Settings,
    meta: {requiresAuth: true},
  },
  {
    path: "/transactions",
    name: "Transactions",
    component: Transactions,
    meta: {requiresAuth: true},
  },
  {
    path: "/loan-option",
    name: "LoanOption",
    component: LoanOption,
    meta: {requiresAuth: true},
  },
  {
    path: "/savings",
    name: "Savings",
    component: Savings,
    meta: {requiresAuth: true},
  },
  {
    path: "/loanplanner",
    name: "LoanPlanner",
    component: LoanPlanner,
    meta: {requiresAuth: true},
  },
  {
    path: "/guidelines",
    name: "LoanGuidelines",
    component: LoanGuidelines,
    meta: {requiresAuth: true},
  },
  {
    path: "/confirmation",
    name: "Confirmation",
    component: Confirmation,
  },
  {
    path: "/overview",
    name: "Overview",
    component: Overview,    
    meta: {requiresAuth: true},
  },
  {
    path: "/insurance",
    name: "Insurance",
    component: Insurance,    
    meta: {requiresAuth: true},
  },
  {
    path: "/vehicle",
    name: "vehicleInsurance",
    component: vehicleInsurance,    
    meta: {requiresAuth: true},
  },
  {
    path: "/house",
    name: "houseInsurance",
    component: houseInsurance,    
    meta: {requiresAuth: true},
  },
  {
    path: "/user-dashboard",
    name: "Dashboard",
    component: Dashboard,    
    meta: {requiresAuth: true},
  },
  {
    path: "/transfer",
    name: "Transfer",
    component: Transfer
  },
  {
    path: "/transfer_",
    name: "Transfer_",
    component: Transfer_
  },
  {
    path: "/withdrawal",
    name: "withdrawal",
    component: Withdrawal
  },
  {
    path: "/cash_addition",
    name: "cash_addition",
    component: Cash_addition,
    meta: {requiresAuth: true},
  },
  {
    path: "/decrease_increase",
    name: "decrease_increase",
    component: Dec_Inc
  },
  {
    path: "/members",
    name: "members",
    component: Members
  },
  {    
    path: "/register",
    name: "register",
    // beforeEnter : guardMyroute,
    component: Register,
    meta: {requiresAuth: true},
  },
  {
    path: "/reg",
    name: "reg",
    component: Register1
  },
  {
    path: "/employee",
    name: "employee",
    component: Employee,
  },
  {
    path: "/view",
    name: "view",
    component: mEmployee,
  },
  {
    path: "/view_approval",
    name: "viewApproval",
    component: mApproval,
  },
  {
    path: "/setup",
    name: "setup",
    component: Setup,
  },
  {
    path: "/config",
    name: "config",
    component: Config,
  },{
    path: "/loan2",
    name: "loan2",
    component: PriCon
  },
  {
    path: "/approvals",
    name: "approvals",
    component: Approvals
  },
  {
    path: "/loan",
    name: "Loan",
    component: Loan,
    meta: {requiresAuth: true},
  },
  {
    path: "/repayment_plan",
    name: "Plan",
    component: Plan
  },
  {
    path: "/new-account",
    name: "NewAccount",
    component: NewAccount
  },
  {
    path: "/guarantor",
    name: "Gurantor",
    component: Guarantor,
    meta: {requiresAuth: true},
  },
  {
    path: "/payment",
    name: "payment",
    component: Payment
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});


router.beforeEach((to, from, next) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (store.getters.isLoggedIn) {
      next();
      return;
    }else if (to.name !== 'Login') {            
        return next({
          path: "/?path="+to.path
        });    
    }
    next("/");
  } else {    
    next();
  }
});

router.beforeEach((to, from, next) => {
  if (to.matched.some((record) => record.meta.guest)) {
    if (store.getters.isLoggedIn) {
      next("/user-dashboard");
      return;
    }
    next();
  } else {
    next();
  }
});

// function guardMyroute(to, from, next) {
//   var isRegistered = false;
//   if (localStorage.getItem('token') && localStorage.getItem('memberId')) {
//     isRegistered = true;
//   } else {
//     isRegistered = false;
//   }
//   if (isRegistered) {
//     next('portal'); // allow to enter route
//   } else {
//     next(); // go to '/login';
//   }
// }

export default router;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.BusinessEntities
{
    public class EmployeeDetail
    {
        [Key]
        public string EMPNO   {get; set;}
        public string PINNO { get; set; }
        public string STAFF_GRP { get; set; }
        public string EMPLOYEE_DEPT { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string TITLES { get; set; }
        public string SURNAME { get; set; }
        public string FIRSTNAME { get; set; }
        public string OTHERNAME { get; set; }
        public string DATE_BIRTH { get; set; }
        public string GENDER { get; set; }
        public string MARITAL_STATUS { get; set; }
        public string WORK_PHONE { get; set; }
        public string MOBILE_PHONE { get; set; }
        public string DATE_EMPLOY { get; set; }
        public string EMAIL_ADDR { get; set; }
        public string JOB_TITLE { get; set; }
        public string STATEORIGING { get; set; }
        //public string RES_ADDR { get; set; }
        public string EMPLOYEE_TYPE { get; set; }
        public string EMPLOYEE_DEPT_1 { get; set; }
        public string MONTH_BASIC_SAL { get; set; }
        public string ANNUAL_BASIC_SAL { get; set; }
    }
}

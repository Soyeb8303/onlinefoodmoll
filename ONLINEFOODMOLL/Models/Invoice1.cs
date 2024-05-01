using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ONLINEFOODMOLL.Models
{
    public class Invoice1
    {
        [DisplayName("ID")]
        [Required(ErrorMessage = "** Id is Mandatory")]
        public int EmployeeId { get; set; }

        [DisplayName("NAME")]
        [Required(ErrorMessage = "** Name is Mandatory")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "** Name should be under 3 to 50")]
        public string EmployeeName { get; set; }

        [DisplayName("AGE")]
        [Required(ErrorMessage = "** Age is Required")]
        [Range(20, 60, ErrorMessage = "** Age should be under 20 and 60")]
        public int? EmployeeAge { get; set; }

        [DisplayName("GENDER")]
        [Required(ErrorMessage = "** Gender is Required")]
        public string EmployeeGender { get; set; }

        [DisplayName("EMAIL")]
        [Required(ErrorMessage = "** Email is Required")]
        [RegularExpression("^([0-9a-zA-Z]([-\\.\\w]*[--9a-zA-Z])+@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$", ErrorMessage = "** Invalid Email")]
        public string EmployeeEmail { get; set; }

        [DisplayName("PASSWORD")]
        [Required(ErrorMessage = "** Employee Password is mandatory")]
        [RegularExpression(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "** UpperCase,LowerCase,Numbers,Symbols,8 Characters")]
        [DataType("Password")]
        public string EmployeePassword { get; set; }

        [DisplayName(" CONFIRM_PASSWORD")]
        [Required(ErrorMessage = "** Employee Confirm Password is mandatory")]
        [Compare("EmployeePassword", ErrorMessage = "** Password is not match")]
        [DataType("Password")]
        public string EmployeeConfirmPassword { get; set; }

        [DisplayName("ORGANIZATION NAME")]
        [ReadOnly(true)]
        public string EmployeeOrganizationName { get; set; }

        [DisplayName("EMPLOYEE ADDRESS")]
        [Required(ErrorMessage = "** Employee Address is mandatory")]
        [DataType(DataType.MultilineText)]
        public string EmployeeAddress { get; set; }


        [DisplayName("EMPLOYEE JOINING DATE")]
        [Required(ErrorMessage = "** Date is mandatory")]
        [DataType(DataType.Date)]
        public string EmployeeJoiningDate { get; set; }

        [DisplayName("EMPLOYEE JOINING TIME")]
        [Required(ErrorMessage = "** Time is mandatory")]
        [DataType(DataType.Time)]
        public string EmployeeJoiningTime { get; set; }

    }
}
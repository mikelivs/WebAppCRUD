using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webAppCRUD.Models
{
    [MetadataType(typeof(EmployeeMetaData))]
    public partial class Employee
    {
    }

    //Employee validation 
    public class EmployeeMetaData
    {
        //check for first and last name
        [Required (AllowEmptyStrings =false, ErrorMessage = "Please Provide First Name")]
        public string FirstName { get; set;}
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Last Name")]
        public string LastName { get; set; }



    }
}
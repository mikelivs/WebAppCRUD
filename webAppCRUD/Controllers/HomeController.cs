using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webAppCRUD.Models;

namespace webAppCRUD.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //Get employee info from model. Return Json data to table
        public ActionResult GetEmployees()
        {
            using (myDbEntities dc = new myDbEntities())
            {
                var emplyees = dc.Employees.OrderBy(a => a.FirstName).ToList();

                return Json(new { data = emplyees }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (myDbEntities dc = new myDbEntities())
            {
                var view = dc.Employees.Where(a => a.EmployeeID == id).FirstOrDefault(); //search model where EmployeeID
                return View(view);
            }
        }

        [HttpPost]
        public ActionResult Save(Employee emp)
        {
            bool status = false;
            if(ModelState.IsValid)
            {
                using (myDbEntities dc = new myDbEntities())
                {
                    if(emp.EmployeeID>0)
                    {
                        //Edit
                        var view = dc.Employees.Where(a => a.EmployeeID == emp.EmployeeID).FirstOrDefault(); //search model where EmployeeID
                        if(view != null)
                        {
                            view.FirstName = emp.FirstName;
                            view.LastName = emp.LastName;
                            view.EmailID = emp.EmailID;
                            view.City = emp.City;
                            view.Country = emp.Country;

                          
                        }
                    }
                    else
                    {
                        //Save
                        dc.Employees.Add(emp);
                     }
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (myDbEntities dc = new myDbEntities())
            {
                var view = dc.Employees.Where(a => a.EmployeeID == id).FirstOrDefault(); //search model where EmployeeID
                if(view != null)
                {
                    return View(view);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            bool status = false;
            using (myDbEntities dc = new myDbEntities())
            {
                var view = dc.Employees.Where(a => a.EmployeeID == id).FirstOrDefault(); //search model where EmployeeID
                if(view != null)
                {
                    dc.Employees.Remove(view);
                    dc.SaveChanges();
                    status = true; 
              
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }

}
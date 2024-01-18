using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    //to be able to return JSON data or a view (.cshtml file), we have to make the homecontroller
    //class derive from the MVC controller class
    public class HomeController : Controller
    {
        //returning a string
        /*public string Index ()
        {
            return "Hello from MVC";
        }*/
        

        //returning JSON data
/*        public JsonResult Index()
        {
            return Json(new { id=1, name="Jerry"});
        }*/

        private IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

        }

        public string Index()
        {
            return _employeeRepository.GetEmployee(1).Name;
        }
    }
}

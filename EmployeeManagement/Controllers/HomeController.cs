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

        private readonly IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

        }

        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }

        //using JSON result
        /*public JsonResult Detail()
        {
            Employee model = _employeeRepository.GetEmployee(1);
            return Json(model);
        }*/

        //uisng Object result
        /*public ObjectResult Detail()
        {
            Employee model = _employeeRepository.GetEmployee(1);
            return new ObjectResult(model);
        }*/

        //using view
        public ViewResult Details(int? id)
        {
            Employee model = _employeeRepository.GetEmployee(id??1);
            //with viewdata, we use string keys, with view bag, we use dynamic properties
            //ViewData["Employee"] = model;
            //ViewData["Page Title"] = "Employee Details";
            //ViewBag.Employee = model;
            ViewBag.PageTitle = "Employee Details";
            return View(model);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if(ModelState.IsValid)
            {
                Employee newEmployee = _employeeRepository.Add(employee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
            
        }
    }
}

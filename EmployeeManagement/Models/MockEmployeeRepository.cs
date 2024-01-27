namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository() 
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Jane", Department = "HR", Email = "jane@aquatech.com"},
                new Employee() { Id = 2, Name = "Sam", Department = "IT", Email = "sam@aquatech.com"},
                new Employee() { Id = 3, Name = "Tim", Department = "IT", Email = "tim@aquatech.com"}
            };
        
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }
    }
}

using System.Data.Entity;

namespace TestNinja.Mocking
{
    public class EmployeeController
    {
        private readonly IEmployeeRepo _repo;
        public EmployeeController(IEmployeeRepo repo)
        {
            _repo = repo;
        }

        public ActionResult DeleteEmployee(int id)
        {
            _repo.DeleteEmployee(id);

            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }

    public class RedirectResult : ActionResult { }

    public class EmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }
}
using System.Collections;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvc_dotnet.DBContext;
using mvc_dotnet.Models;
using MySql.Data.MySqlClient;

namespace mvc_dotnet.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IConfiguration configuration;
    public HomeController(ILogger<HomeController> logger, IConfiguration iconfig)
    {
        _logger = logger;
        configuration = iconfig;
    }


    public IActionResult Index()
    {

        List<EmployeeSalarysModel> employees = new List<EmployeeSalarysModel>();
        // ConfigurationManager configurationManager=new ConfigurationManager();
        // string constr =  configurationManager.GetConnectionString("");

        string constr = configuration.GetSection("ConnectionStrings").GetSection("myconn").Value;
        // Console.WriteLine("cstring="+constr);
        using (MySqlConnection con = new MySqlConnection(constr))
        {
            string query = "SELECT e.Id,e.Name,e.Post,es.Salary FROM Employee as e left outer join Employee_salary as es on e.Id=es.Emp_id";
            using (MySqlCommand cmd = new MySqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (MySqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        employees.Add(new EmployeeSalarysModel
                        {
                            Id = sdr["Id"].ToString(),
                            Name = sdr["Name"].ToString(),
                            Post = sdr["Post"].ToString(),
                            Salary=sdr["Salary"].ToString()
                        });
                    }
                }
                con.Close();
            }

        }
        return View(employees);
    }
    [HttpPost]
 public ActionResult See_and_add(string id, EmployeeSalarysModel e)
    {
        // Console.WriteLine("id="+id);
        string name=e.Name;
        string post=e.Post;
        string salary=e.Salary;
        
 string result = "";
        //  List<EmployeeSalaryModel> employeesalary = new List<EmployeeSalaryModel>();
        // ConfigurationManager configurationManager=new ConfigurationManager();
        // string constr =  configurationManager.GetConnectionString("");

        string constr = configuration.GetSection("ConnectionStrings").GetSection("myconn").Value;
        // Console.WriteLine("cstring="+constr);
        try
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                string query = $"UPDATE Employee SET Name = '{name}',Post = '{post}' WHERE Id = '{id}'";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = conn;
                    conn.Open();

                    int aff = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                // string query = $"INSERT INTO Employee(Id,Name,Post) VALUES ('{id}','{name}','{post}')";
                string query = $"UPDATE Employee_salary SET Salary= '{salary}' WHERE Emp_id = '{id}'";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = conn;
                    conn.Open();

                    int aff = cmd.ExecuteNonQuery();
conn.Close();
                }
            }
        }
        catch
        {
            result = name + " not updated successfully!.";

        }
        if (result == "")
        {
            result = name + " updated successfully!.";
        }
        ViewBag.Nam = result;
        return View(e);
    }
[HttpGet]
    public ActionResult See_and_add(string id)
    {
        // Console.WriteLine("id1="+id);
        // List<EmployeeSalarysModel>
    string id1=id;
       EmployeeSalarysModel person = new EmployeeSalarysModel();
        string constr = configuration.GetSection("ConnectionStrings").GetSection("myconn").Value;
        // Console.WriteLine("cstring="+constr);
        using (MySqlConnection con = new MySqlConnection(constr))
        {
            string query = $"SELECT e.Id,e.Name,e.Post,es.Salary FROM Employee as e left outer join Employee_salary as es on e.Id=es.Emp_id where e.Id='{id1}'";
            using (MySqlCommand cmd = new MySqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (MySqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        // employees.Add(new EmployeeSalarysModel
                        // {
                        //     Id = sdr["Id"].ToString(),
                        //     Name = sdr["Name"].ToString(),
                        //     Post = sdr["Post"].ToString(),
                        //     Salary=sdr["Salary"].ToString()
                        // });
                        person.Id=sdr["Id"].ToString();
                        person.Name=sdr["Name"].ToString();
                        person.Post=sdr["Post"].ToString();
                        person.Salary=sdr["Salary"].ToString();
                    }
                }
                con.Close();
            }

        }
        
 
        return View(person);
    }
    public IActionResult AddEmployee()
    {
        
        return View();
    }
    [HttpPost]
    public IActionResult AddEmployee(string id, string name, string post, string salary)
    {
        string result = "";
        //  List<EmployeeSalaryModel> employeesalary = new List<EmployeeSalaryModel>();
        // ConfigurationManager configurationManager=new ConfigurationManager();
        // string constr =  configurationManager.GetConnectionString("");

        string constr = configuration.GetSection("ConnectionStrings").GetSection("myconn").Value;
        // Console.WriteLine("cstring="+constr);
        try
        {
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                string query = $"INSERT INTO Employee(Id,Name,Post) VALUES ('{id}','{name}','{post}')";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = conn;
                    conn.Open();

                    int aff = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                // string query = $"INSERT INTO Employee(Id,Name,Post) VALUES ('{id}','{name}','{post}')";
                string query = $"INSERT INTO Employee_salary(Emp_id,Salary) VALUES ('{id}','{salary}')";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = conn;
                    conn.Open();

                    int aff = cmd.ExecuteNonQuery();
conn.Close();
                }
            }
        }
        catch
        {
            result = name + " not added successfully!.";

        }
        if (result == "")
        {
            result = name + " added successfully!.";
        }
        ViewBag.Name = result;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}

using System.Collections;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using mvc_dotnet.DBContext;
using mvc_dotnet.Models;
using MySql.Data.MySqlClient;

namespace mvc_dotnet.Controllers;

public class IndexController : Controller
{
    private readonly ILogger<IndexController> _logger;
    private IConfiguration configuration;
    public IndexController(ILogger<IndexController> logger, IConfiguration iconfig)
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
   
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}

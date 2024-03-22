using System.Data;
using System.Diagnostics;
using System.Reflection;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using NurshingHomeManagementSystem.Data;
using NurshingHomeManagementSystem.Models;
using rdlc.Models;
using System.Security.Permissions;

namespace rdlc.Controllers;

public class HomeController : Controller
{
    private readonly DatabaseService _logger;
    private IWebHostEnvironment webHostEnvironment;
    public HomeController(DatabaseService logger, IWebHostEnvironment webHostEnvironment1)
    {
        _logger = logger;
        webHostEnvironment = webHostEnvironment1;
    }

    public IActionResult Index()
    {
        return View();
    }
    public FileContentResult DownloadReport()
    {
        string mimeType = "application/pdf";
        string reportPath = $"{this.webHostEnvironment.WebRootPath}\\Reports\\DemoBedReport.rdlc";
        //string reportPath = "C:\Users\MIPL\Desktop\rdlc\wwwroot\Reports\DemoBedReport.rdlc";

        List<Bed_dept_master> bl = _logger.GetBeds().ToList();
        DataTable dtable= new DataTable(typeof(Bed_dept_master).Name);
        PropertyInfo[] props=typeof(Bed_dept_master).GetProperties(BindingFlags.Public|BindingFlags.Instance);
        foreach(var prop in props)
        {
            dtable.Columns.Add(prop.Name);
        }
        foreach(var items in bl)
        {
            var values=new object[props.Length];
            for(int i=0;i<props.Length;i++) {
                values[i] = props[i].GetValue(items);
            }
            dtable.Rows.Add(values);
        }
        Console.WriteLine(dtable.ToString());
        var localreport = new LocalReport(reportPath);
        localreport.AddDataSource("Bed_dataset", dtable);
        var res = localreport.Execute(RenderType.Pdf, 1, null, mimeType);
        return File(res.MainStream,mimeType);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

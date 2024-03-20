using System.ComponentModel.DataAnnotations;


namespace mvc_dotnet.DBContext
{
    public class EmployeeSalarysModel
    {
        [Key]
        public string Id { get; set; }
        
        public string Name { get; set; }
        public string Post { get; set; }
        
        public string Salary { get; set; }
    }
}

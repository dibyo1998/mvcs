
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NurshingHomeManagementSystem.Models
{
    public class Admission_master
    {
       
    }
    public class UserMaster
    {
        [Key]
        public long User_id { get; set; }
        public   string User_name { get; set; }
        [EmailAddress]
        public string? User_email { get; set; }
        public string? User_phone { get; set; }
        public   string User_password { get; set; }
        public   DateTime creationdate { get; set; }
        public   DateTime lastmodification { get; set; }
        public bool? Index { get; set; }
        public bool? Create { get; set; }
        public bool? Update { get; set; }
        public bool? Delete { get; set; }
    }
    public class Department_master
    {
        [Key]
        public long Dep_id { get; set; }
        public   string Dep_name { get; set; }
        public   string Dep_s_name { get; set; }
        public int Bp { get; set; }
        public   DateTime creationdate { get; set; }
        public   DateTime lastmodification { get; set; }
        public bool Dep_status { get; set; }
        [ForeignKey("User")]
        public long userid { get; set; }
       // public virtual   UserMaster User { get; set; }
    }
    public class Doctor_master
    {
        [Key]
        public long Doctor_id { get; set; }
        public   string Doctor_name { get; set; }
        public   string Doctor_address1 { get; set; }
        public  string? Doctor_address2 { get; set; }
        public string? Doctor_address3 { get; set; }
        public   string Doctor_phone { get; set; }
        public   string Doctor_identification { get; set; }

        public Decimal Indor_charg { get; set; }
        public Decimal Fixed_discount { get; set; }
        public Decimal Attending_charg { get; set; }


    }
    public class referal_Masters
    {
        [Key]
        public long Referal_id { get; set; }
        public   string Referal_name { get; set; }
        public   string Referal_mobile { get; set; }
        public string? Referal_address { get; set; }
        public   bool Referal_status { get; set; }
        public long Userid { get; set; }

    }
    public class Bed_master
    {
        [Key]
        public   long Bed_id { get; set; }
        public   string Bed_name { get; set; }
        public Decimal Bed_rate { get; set; }
        public bool Bed_status { get; set; }
        public bool Bed_condition { get; set; }
        public Decimal Attendant_charge { get; set; }
        public Decimal Totalcharge { get; set; }
        public  string Bed_sname { get; set; }
        public   DateTime creationdate { get; set; }
        public   DateTime lastmodification { get; set; }
        [ForeignKey("User")]
        public long? Userid { get; set; }
        //public virtual   UserMaster User { get; set; }
       // public virtual  UserMaster User { get; set; }

        public string? Depid { get; set; }

    }
    public class Bed_dept_master
    {
        [Key]
        public   long Bed_id { get; set; }
        public   string Bed_name { get; set; }
        public Decimal Bed_rate { get; set; }
        public bool Bed_status { get; set; }
       
        public Decimal Totalcharge { get; set; }
      

    }
}

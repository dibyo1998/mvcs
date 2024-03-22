using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Hosting;
using NurshingHomeManagementSystem.Models;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace NurshingHomeManagementSystem.Data
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Bed_dept_master> GetBeds()
        {
            var beds = new List<Bed_dept_master>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("GetAllBeds", connection)) // Assuming the name of your stored procedure is GetAllBeds
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            beds.Add(new Bed_dept_master
                            {
                                Bed_id = reader.GetInt64(0),
                                Bed_name = reader.GetString(1),
                                Bed_rate = reader.GetDecimal(2),
                                Bed_status = reader.GetBoolean(3),
                              
                                Totalcharge = reader.GetDecimal(6),
                                
                            // Depid = reader.GetInt64(11)

                               
                                //Userid = reader.GetInt64(10),
                                //Depid = reader.GetString(11)
                            });
                        }
                    }
                }
                connection.Close();
            }
            
            return beds;
        }
       
	}
}

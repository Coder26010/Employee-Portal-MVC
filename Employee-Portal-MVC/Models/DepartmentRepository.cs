using Employee_Portal_MVC.ModelEntity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Employee_Portal_MVC.Models
{
    public class DepartmentRepository
    {
        public List<DepartmentEntity> DepartmentList
        {
            get
            {
                List<DepartmentEntity> DepartmentList = new List<DepartmentEntity>();

                string cs = ConfigurationManager.ConnectionStrings["EMPDB"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand command = new SqlCommand("Select * from TDEPARTMENT", con);

                    con.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            DepartmentList.Add(new DepartmentEntity()
                            {
                                Id = Convert.ToInt32(dataReader["DepartmentId"]),
                                DepartmentCode = dataReader["DepartmentCode"].ToString(),
                                DepartmentName = dataReader["DepartmentName"].ToString()
                            });
                        }
                    }

                }

                return DepartmentList;
            }
        }

        public void Add(DepartmentEntity entity)
        {
            //Ado code to insert data
        }

        public void Update(DepartmentEntity entity)
        {
            //Ado code to Update data
        }

        public void Remove(int DepartmentId)
        {
            //Ado code to delete data
        }
    }
}
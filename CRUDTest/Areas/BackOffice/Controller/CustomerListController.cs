using CRUDTest.ModelsDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDTest.Areas.BackOffice.Controller
{
    [Route("backoffice/[controller]")]
    [ApiController]
    public class CustomerListController : ControllerBase
    {
        private readonly DB_Context db_context;
        public CustomerListController(DB_Context context)
        {
            this.db_context = context;
        }

        [HttpGet]
        [Route("GetCustomerList")]
        public IActionResult GetCustomerListAsync(int? page= 1, int? limit= 10)
        {
            object Data = null;
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@page",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = page
                        },
                       new SqlParameter() {
                            ParameterName = "@limit",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = limit
                        },
                        new SqlParameter() {
                            ParameterName = "@TotalRecord",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Output,
                        }
            };
            try
            {
                var customer_list = db_context.Set<res_customer_list>().FromSqlRaw("EXEC [dbo].[sp_customer_list] @page,@limit,@TotalRecord OUTPUT", param).ToList();
                int total = Convert.ToInt32(param[2].Value);
                Data = new
                {
                    status = "success",
                    data = new
                    {
                        total_records = total,
                        customer_list = customer_list,
                    },
                };
            }
            catch
            {
                throw;
            }
            return Ok(Data);
        }
    }
}

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
        public IActionResult GetCustomerListAsync(string search, int? page= 1, int? limit= 10)
        {
            object Data;
            try
            {  
                    var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@search",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size = 250,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = (search == null) ? "%" : search
                        },
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
                        },
                        new SqlParameter() {
                            ParameterName = "@TotalFilterRecord",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Output,
                        }
                    };
                   dynamic customer_list = db_context.Set<res_customer_list>().FromSqlRaw("EXEC [dbo].[sp_customer_list] @search,@page,@limit,@TotalRecord OUTPUT,@TotalFilterRecord OUTPUT", param).ToList();
                   int total = Convert.ToInt32(param[3].Value);
                   int  total_filter = Convert.ToInt32(param[4].Value);
                Data = new
                {
                    status = "success",
                    data = new
                    {
                        total_records = total,
                        total_filter = total_filter,
                        customer_list = customer_list,
                    },
                };
            }
            catch(Exception ex)
            {
                throw(ex);
            }
            return Ok(Data);
        }
    }
}

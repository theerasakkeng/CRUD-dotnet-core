using CRUDTest.Areas.Api.Models;
using CRUDTest.ModelsDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDTest.Areas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DB_Context db_context;
        public CustomersController(DB_Context context)
        {
            this.db_context = context;
        }
        [HttpGet]
        [Route("GetCustomerList")]
        public IActionResult GetCustomerList(int? start, int? limit)
        {
            object Data = null;
            try
            {
                List<customer> customer_list = db_context.customers.ToList();
                int total = customer_list.Count();
                var skip = limit * (start - 1);
                var data_pay = customer_list.Select(o => o).Skip((int)skip).Take((int)limit).ToArray();
                Data = new
                {
                    status = "success",
                    data = new
                    {
                        total= total,
                        activity_list = data_pay,
                    },
                };
            }
            catch
            {
                throw;
            }
            return Ok(Data);
        }
        [HttpPost]
        [Route("InsertCustomer")]
        public IActionResult InsertCustomer([FromBody]customer req)
        {
            object Data = null;
            try
            {
                customer customer_data = new customer();
                {
                    customer_data = req;
                }
                db_context.customers.Add(customer_data);
                db_context.SaveChanges();
                Data = new
                {
                    status = "success",
                    data = customer_data
                };
            }
            catch
            {
                throw;
            }
            return Ok(Data);
        }
        [HttpPut]
        [Route("UpdateCustomer")]
        public IActionResult UpdateCustomer([FromBody] Req_Update_Customer req)
        {
            object Data = null;
            try
            {
                customer customer_data = new customer();
                customer_data = db_context.customers.Single(o => o.customer_id == req.customer_id);
                {
                    customer_data.first_name = req.first_name;
                    customer_data.last_name = req.last_name;
                    customer_data.phone = req.phone;
                    customer_data.email = req.email;
                    customer_data.street = req.street;
                    customer_data.city = req.city;
                    customer_data.state = req.state;
                    customer_data.zip_code = req.zip_code;
                }
                db_context.customers.Update(customer_data);
                db_context.SaveChanges();
                Data = new
                {
                    status = "success",
                    data = customer_data
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

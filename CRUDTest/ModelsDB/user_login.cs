using System;
using System.Collections.Generic;

#nullable disable

namespace CRUDTest.ModelsDB
{
    public partial class user_login
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string salt_key { get; set; }
        public DateTime created_time { get; set; }
    }
}

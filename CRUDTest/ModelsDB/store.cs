using System;
using System.Collections.Generic;

#nullable disable

namespace CRUDTest.ModelsDB
{
    public partial class store
    {
        public store()
        {
            orders = new HashSet<order>();
            staff = new HashSet<staff>();
            stocks = new HashSet<stock>();
        }

        public int store_id { get; set; }
        public string store_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip_code { get; set; }

        public virtual ICollection<order> orders { get; set; }
        public virtual ICollection<staff> staff { get; set; }
        public virtual ICollection<stock> stocks { get; set; }
    }
}

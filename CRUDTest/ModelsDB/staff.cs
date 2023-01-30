using System;
using System.Collections.Generic;

#nullable disable

namespace CRUDTest.ModelsDB
{
    public partial class staff
    {
        public staff()
        {
            Inversemanager = new HashSet<staff>();
            orders = new HashSet<order>();
        }

        public int staff_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public byte active { get; set; }
        public int store_id { get; set; }
        public int? manager_id { get; set; }

        public virtual staff manager { get; set; }
        public virtual store store { get; set; }
        public virtual ICollection<staff> Inversemanager { get; set; }
        public virtual ICollection<order> orders { get; set; }
    }
}

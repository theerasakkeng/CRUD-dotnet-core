using System;
using System.Collections.Generic;

#nullable disable

namespace CRUDTest.ModelsDB
{
    public partial class brand
    {
        public brand()
        {
            products = new HashSet<product>();
        }

        public int brand_id { get; set; }
        public string brand_name { get; set; }

        public virtual ICollection<product> products { get; set; }
    }
}

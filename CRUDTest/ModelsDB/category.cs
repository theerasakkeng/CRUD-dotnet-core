using System;
using System.Collections.Generic;

#nullable disable

namespace CRUDTest.ModelsDB
{
    public partial class category
    {
        public category()
        {
            products = new HashSet<product>();
        }

        public int category_id { get; set; }
        public string category_name { get; set; }

        public virtual ICollection<product> products { get; set; }
    }
}

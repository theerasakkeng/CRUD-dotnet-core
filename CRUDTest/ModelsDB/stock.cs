using System;
using System.Collections.Generic;

#nullable disable

namespace CRUDTest.ModelsDB
{
    public partial class stock
    {
        public int store_id { get; set; }
        public int product_id { get; set; }
        public int? quantity { get; set; }

        public virtual product product { get; set; }
        public virtual store store { get; set; }
    }
}

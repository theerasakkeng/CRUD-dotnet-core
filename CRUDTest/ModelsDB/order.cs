using System;
using System.Collections.Generic;

#nullable disable

namespace CRUDTest.ModelsDB
{
    public partial class order
    {
        public order()
        {
            order_items = new HashSet<order_item>();
        }

        public int order_id { get; set; }
        public int? customer_id { get; set; }
        public byte order_status { get; set; }
        public DateTime order_date { get; set; }
        public DateTime required_date { get; set; }
        public DateTime? shipped_date { get; set; }
        public int store_id { get; set; }
        public int staff_id { get; set; }

        public virtual customer customer { get; set; }
        public virtual staff staff { get; set; }
        public virtual store store { get; set; }
        public virtual ICollection<order_item> order_items { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace CRUDTest.ModelsDB
{
    public partial class product
    {
        public product()
        {
            order_items = new HashSet<order_item>();
            stocks = new HashSet<stock>();
        }

        public int product_id { get; set; }
        public string product_name { get; set; }
        public int brand_id { get; set; }
        public int category_id { get; set; }
        public short model_year { get; set; }
        public decimal list_price { get; set; }

        public virtual brand brand { get; set; }
        public virtual category category { get; set; }
        public virtual ICollection<order_item> order_items { get; set; }
        public virtual ICollection<stock> stocks { get; set; }
    }
}

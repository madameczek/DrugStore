using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveRecord.DataModels
{
    class OrderDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MedicineId { get; set; }
        public int? Quantity { get; set; }

    }
}

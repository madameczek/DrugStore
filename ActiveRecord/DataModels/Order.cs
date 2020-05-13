using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveRecord.DataModels
{
    class Order
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? PrescriptionId { get; set; }
    }
}

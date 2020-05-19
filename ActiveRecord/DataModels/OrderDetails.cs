using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace ActiveRecord.DataModels
{
    public sealed class OrderDetails : ActiveRecord
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MedicineId { get; set; }
        public int? Quantity { get; set; }
        public DateTimeOffset? DeliveredOn { get; set; }

        public override bool Save()
        {
            throw new NotImplementedException();
        }

        public override void Reload()
        {
            throw new NotImplementedException();
        }

        public override bool Remove()
        {
            throw new NotImplementedException();
        }

        private void ParseReader(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveRecord.DataModels
{
    class Medicine : ActiveRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManufacturerId { get; set; }
        public decimal? Price { get; set; }
        public int? StockQty { get; set; }
        public bool? IsPrescription { get; set; }

        public override void Reload()
        {
            throw new NotImplementedException();
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}

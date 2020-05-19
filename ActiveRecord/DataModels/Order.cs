using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ActiveRecord.DataModels
{
    class Order : ActiveRecord
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? PrescriptionId { get; set; }

        public override bool Save()
        {
            throw new NotImplementedException();
        }

        public override void Reload()
        {
            throw new NotImplementedException();
        }

        private void ParseReader(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public override bool Remove()
        {
            throw new NotImplementedException();
        }
    }
}

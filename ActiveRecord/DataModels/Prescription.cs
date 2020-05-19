using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ActiveRecord.DataModels
{
    public sealed class Prescription : ActiveRecord
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Pesel { get; set; }
        public string PrescriptionNo { get; set; }

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

        public override void ParseReader(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}

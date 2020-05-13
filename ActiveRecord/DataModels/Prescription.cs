using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveRecord.DataModels
{
    class Prescription
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Pesel { get; set; }
        public string PrescriptionNo { get; set; }
    }
}

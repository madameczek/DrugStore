using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveRecord.DataModels
{
    public class DbResultErrorException : Exception
    {
        public DbResultErrorException(string message) : base(message) { }
    }
}

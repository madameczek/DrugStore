using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveRecord.DataModels
{
    public class DbResultException : Exception
    {
        public DbResultException(string message) : base(message) { }
    }
}

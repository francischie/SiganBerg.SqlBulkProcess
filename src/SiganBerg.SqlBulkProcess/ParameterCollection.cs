using System;
using System.Collections.Generic;
using System.Data;

namespace SiganBerg.SqlBulkProcess
{
    internal class ParameterCollection : List<DataParameter>, IDataParameterCollection
    {
        public bool Contains(string parameterName)
        {
            return false;
        }

        public int IndexOf(string parameterName)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(string parameterName)
        {
            throw new NotImplementedException();
        }

        public object this[string parameterName]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
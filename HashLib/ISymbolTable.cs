using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashLib
{
    public interface ISymbolTable<KeyType, ValueType> where
        KeyType : IComparable<KeyType>
    {
        void Put(KeyType key, ValueType value);
        void Delete(KeyType key);
        bool Contains(KeyType key);
        ValueType Get(KeyType key);
    }
}

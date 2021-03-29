using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashLib
{
    public class HashTableEntry<KeyType, ValueType>
        : IComparable<HashTableEntry<KeyType, ValueType>>,
        IEquatable<HashTableEntry<KeyType, ValueType>>
        where KeyType : IComparable<KeyType>
    {
        public HashTableEntry(KeyType key, ValueType value)
        {
            Key = key;
            Payload = value;
        }
        // Ask DR. Ribler about changing accessabilty of keys and value variables.
        //And if we should implement the hashtable with parallel arrays 
        public KeyType Key { get; set; }
        public ValueType Payload { get; set; }

        public int CompareTo(HashTableEntry<KeyType, ValueType> other)
        {
            int toReturn = -2;
            if (this.Key.CompareTo(other.Key) == 0)
            {
                toReturn = 0;
            }
            else if (this.Key.CompareTo(other.Key) == -1)
            {
                toReturn = -1;   
            }
            else if (this.Key.CompareTo(other.Key) == 1)
            {
                toReturn = 1;
            }
            return toReturn;
            //return this.CompareTo(other);
        }

        public bool Equals(HashTableEntry<KeyType, ValueType> other)
        {

             return Equals(other as HashTableEntry<KeyType, ValueType>);
           // return (Key == other.Key && Payload == other.Payload);
        }

        public override int GetHashCode()
        {
           
            return Key.GetHashCode() ^ Payload.GetHashCode();

        }
    }
}

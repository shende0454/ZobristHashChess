using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashLib
{
    public class HashWithLinearProbe<KeyType, ValueType>
        : IEnumerable<HashTableEntry<KeyType, ValueType>>,
        ISymbolTable<KeyType, ValueType> where
        KeyType : IComparable<KeyType>
    {
        private int numPairs;
        private int capacityOfTable;
        private HashTableEntry<KeyType, ValueType>[] hashTable;

        public HashWithLinearProbe(int tableSize)
        {
            capacityOfTable = tableSize;
            hashTable = new HashTableEntry<KeyType, ValueType>[tableSize];
        }
       
        private void resize(int capacity)
        {
            HashWithLinearProbe<KeyType, ValueType> t;
            t = new HashWithLinearProbe<KeyType, ValueType>(capacity);
            for (int i = 0; i < capacityOfTable; i++)
            {
                if (hashTable[i] != null)
                {
                    t.Put(hashTable[i].Key, hashTable[i].Payload);
                }
            }
            hashTable = t.hashTable;
            capacityOfTable = t.capacityOfTable;
        }
            public uint Hash(KeyType key)
        {
            //Is casting to uint needed? GetHasHcODE OR HASHCODE 
            uint value;
            value = (uint)((key.GetHashCode() & 0x7fffffff) % capacityOfTable);
            while(value > capacityOfTable || value < 0)
            {
                value = (uint)((key.GetHashCode() & 0x7fffffff) % capacityOfTable);
            }
            return value;
        }

        public void Put(KeyType key, ValueType value)
        {
           
            int counter = (int)Hash(key);
            Boolean put = false;
            while (put == false)
            {
                if (hashTable[counter] == null)
                {
                    hashTable[counter] = new HashTableEntry<KeyType, ValueType>(key, value);
                    put = true;
                }
                else
                {//compare
                    if (hashTable[counter].Key.CompareTo(key) == 0)
                    {
                        hashTable[counter].Payload = value;
                        put = true;
                    }
                    else
                    {
                        counter += 1 % capacityOfTable;
                    }
                }
            }
            if (numPairs >= capacityOfTable / 2)
            {
                resize(capacityOfTable * 2);
            }

            numPairs++;

        }

        public void Delete(KeyType key)
        {
            if (Contains(key))
            {
                int i = (int)Hash(key);
                while (!key.Equals(hashTable[i].Key))
                {
                    i = (i + 1) % capacityOfTable;
                }
                hashTable[i] = null;
                i = (i + 1) % capacityOfTable;
                while (hashTable[i] != null)
                {
                    KeyType k = hashTable[i].Key;
                    ValueType v = hashTable[i].Payload;
                    hashTable[i] = null;
                    numPairs--;
                    Put(k, v);
                    i = (i + 1) % capacityOfTable;
                }
                numPairs--;
                if (numPairs > 0 && numPairs == capacityOfTable / 8)
                {
                    resize(capacityOfTable / 2);
                }
            }
            else
            {
                return;

            }
        }
        
        public bool Contains(KeyType key)
        {

            bool isPresent = false;
            if (numPairs != 0)
            {
                for (int i = (int)Hash(key); hashTable[i] != null; i = (i + 1) % capacityOfTable)
                {
                    if (hashTable[i].Key.Equals(key))
                    {
                        isPresent = true;
                    }
                }
            }
            return isPresent;
        }

        public ValueType Get(KeyType key)
        {
            int offset;
            bool found = false;
            ValueType theValue= default;
            for (int i = (int)Hash(key); found == false; i = (i + 1) % capacityOfTable)
            {
                if (hashTable[i] != null)
                {
                    if (hashTable[i].Key.Equals(key))
                    {
                        theValue = hashTable[i].Payload;
                        found = true;
                    }
                }
            }
            return theValue;
           
        }

        public IEnumerator<HashTableEntry<KeyType, ValueType>> GetEnumerator()
        {
            foreach (HashTableEntry<KeyType, ValueType> entry in hashTable)
            {
                if (entry != null)
                {
                    yield return entry;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        
    }
}


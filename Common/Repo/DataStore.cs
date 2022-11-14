using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Common.Repo
{
   
    public class DataStore<T>
    {
        private static List<T> Records;
        private static DataStore<T> instance;
        private DataStore()
        {
            if (Records == null)
            Records = new List<T>();
        }
        public static DataStore<T> GetInstance()
        {
           if (instance == null)
            {
                lock(new object())
                { 
                List<T> Records = new List<T>();
                instance = new DataStore<T>();
                }
            }
            return instance;
        }
        public void AddRecord(T record)
        {
            lock (new object())
            {
                Records.Add(record);
            }
        }
        public void RemoveRecord (T record)
        {
            lock (new object())
            {
                Records.Remove(record);
            }
        }
        public IEnumerable<T> GetRecords(Predicate<T> match)
        {
            return Records.FindAll(match) as IEnumerable<T>;
        }
        
    }
}

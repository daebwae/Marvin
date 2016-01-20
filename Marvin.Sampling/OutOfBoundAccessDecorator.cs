using System;
using System.Collections;
using System.Collections.Generic;

namespace Marvin.Sampling
{
    internal class OutOfBoundAccessDecorator<T>: IList<T>
    {
        private IList<T> values;
        private T outOfBoundsValue; 

        public OutOfBoundAccessDecorator(IList<T> values)
        {
            if(values == null)
            {
                throw new ArgumentNullException(nameof(values)); 
            }
            this.values = values;
            this.outOfBoundsValue = default(T); 
        }

        public T this[int index]
        {
            get
            {
                if(index >= 0 && index < values.Count)
                {
                    return values[index]; 
                }

                return outOfBoundsValue; 
            }

            set
            {
                values[index] = value; 
            }
        }

        public int Count
        {
            get
            {
                return values.Count; 
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return values.IsReadOnly; 
            }
        }

        public void Add(T item)
        {
            values.Add(item); 
        }

        public void Clear()
        {
            values.Clear(); 
        }

        public bool Contains(T item)
        {
            return values.Contains(item); 
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            values.CopyTo(array, arrayIndex); 
        }

        public IEnumerator<T> GetEnumerator()
        {
            return values.GetEnumerator(); 
        }

        public int IndexOf(T item)
        {
            return values.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            values.Insert(index, item); 
        }

        public bool Remove(T item)
        {
            return values.Remove(item); 
        }

        public void RemoveAt(int index)
        {
            values.RemoveAt(index); 
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator(); 


        }
    }
}

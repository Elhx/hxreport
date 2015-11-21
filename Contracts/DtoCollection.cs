using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contracts
{
    public class DtoCollection<T> : DtoBaseObject
    {
        private ICollection<T> list;

        public ICollection<T> List
        {
            get
            {
                if (list == null)
                {
                    list = new List<T>();
                }

                return list;
            }

            set
            {
                list = value;
            }
        }

        public DtoCollection(ICollection<T> theList)
        {
            list = theList ?? new Collection<T>();
        }

        public void Add(T item)
        {
            list.Add(item);
        }
    }
}

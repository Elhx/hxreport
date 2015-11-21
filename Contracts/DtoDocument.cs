using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class DtoDocument : DtoBaseObject
    {
        public string DocId { get; set; }

        public bool Subscribe { get; set; }
    }
}

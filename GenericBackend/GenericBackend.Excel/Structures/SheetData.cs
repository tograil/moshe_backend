using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericBackend.Excel.Structures
{
    public abstract class SheetData
    {
        public string Name { get; set; }
        public IEnumerable<int> Years { get; set; }
        public IEnumerable<int> Monthes { get; set; }

        public string SupervisorComment { get; set; }
    }
}

using System.Collections;
using System.Collections.Generic;

namespace Fas
{
    public class DbListData
    {
        public string ModelName { get; set; }

        public List<Hashtable> list { get; set; } = new List<Hashtable>();

        public DbListData(string name)
        {
            ModelName = name;
        }
    }
}

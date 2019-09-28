using System;
using System.Collections.Generic;
using System.Text;

namespace Test.query
{
    public interface HomeWorkMapper
    {
        int Insert(List<Dictionary<string,string>> data);
    }
}

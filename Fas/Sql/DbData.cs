using System.Collections.Generic;

namespace Fas.Sql
{
    public class DbData : Dictionary<string, string>
    {
        public DbData(string model)
        {
            this["model"] = model;
        }
    }
}

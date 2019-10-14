using System.Collections;

namespace Fas
{
    public class DbData : Hashtable
    {
        public DbData(string model)
        {
            this["model"] = model;
        }
    }
}

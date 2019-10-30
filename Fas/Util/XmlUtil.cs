using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Fas.Util
{
    public class XmlUtil
    {
        public static Hashtable ToHashtable(XmlNode node)
        {
            var ht = new Hashtable();
            foreach (XmlAttribute attr in node.Attributes)
            {
                if (node.Name == "field")
                {
                    ht[attr.Name] = attr.Value.Replace(" ", "");
                }
                else
                {
                    ht[attr.Name] = attr.Value;
                }
            }

            return ht;
        }
    }
}

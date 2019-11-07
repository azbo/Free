using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
                    string[] fields = attr.Value.Split(",");

                    string[] values = new string[fields.Length];
                    for (var i = 0; i < fields.Length; i++)
                    {
                        values[i] = fields[i].Trim(' ');
                    }
                    ht[attr.Name] = values;
                }
                else if (node.Name == "condition")
                {
                    List<string> list = new List<string>();
                    list.Add(Regex.Replace(attr.Value, @"@(\w+)", delegate (Match m)
                        {
                            list.Add(m.Groups[1].Value);
                            return m.Value;
                        }));
                    ht[attr.Name] = list;
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

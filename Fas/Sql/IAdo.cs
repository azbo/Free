using System.Collections;
using System.Collections.Generic;

namespace Fas.Sql
{
    public interface IAdo
    {
        int Insert(List<Hashtable> datas);

        int Delete();

        int Update();

        T Get<T>();
    }
}

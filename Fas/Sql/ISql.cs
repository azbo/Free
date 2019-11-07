using System.Collections;
using System.Collections.Generic;

namespace Fas
{
    public interface ISql
    {
        ISql Field(string field);

        ISql Inner(string id);

        ISql Where(string id);
        ISql OrderBy(string id);
        ISql GroupBy(string id);
        ISql Having(string id);
        ISql Limit(string id);
    }
}

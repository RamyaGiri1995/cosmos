using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.connection
{
    public class QueryBuilder
    {
        public string buildQuery(string containerId, string searchTerm)
        {
            var query = new StringBuilder("select * from ");
            query.Append(containerId+" c");
            if (searchTerm != null)
            {
                query.Append(" where c.id =\""+ searchTerm + "\""+ " or c.name =\"" + searchTerm + "\"");
            }
            return query.ToString();
        }
    }
}

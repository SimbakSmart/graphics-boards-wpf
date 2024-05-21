

using Infraestructure.Helpers;

namespace Infraestructure.Utils
{
    public  class QueueSql
    {
        private static string _query = string.Empty;
        public static string GetTotalByResponsable(FiltersParams filters = null)
        {
             _query = @"
                SELECT
                CASE
                WHEN Q.Name IS NULL THEN 'Asignadas a sistemas'
                ELSE
                REPLACE(Q.Name,'_',' ')
                END AS [Queue],
                COUNT(*) AS Total
                FROM SupportCall AS SC
                LEFT JOIN Queue AS Q  ON Q.QueueID = SC.AssignToQueueID
                WHERE Closed =0 AND YEAR(OpenDate) >= 2020
                GROUP BY  Q.Name ORDER BY TOTAL DESC
               ";
            return _query;
        }
    }
}

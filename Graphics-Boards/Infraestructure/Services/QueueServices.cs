
using Core.Models;
using Infraestructure.Data;
using Infraestructure.Helpers;
using Infraestructure.Interfaces;
using Infraestructure.Utils;
using System.Data;
using System.Data.Odbc;

namespace Infraestructure.Services
{
    public class QueueServices : IServices<Queues>
    {
        private OdbcConnection con = null;

        public async Task DisposeAsync()
        {
            if (con != null)
                await con.CloseAsync();
        }

        public async Task<List<Queues>> GetTotalsByResponsableAsync(FiltersParams filters = null)
        {
            List<Queues> _list = null;
            string _query = string.Empty;
            try
            {

                if (filters != null)
                {
                    _query = QueueSql.GetTotalByResponsable(filters);
                }
                else
                {
                    _query = QueueSql.GetTotalByResponsable();
                }


                using (OdbcConnection con = new OdbcConnection(DBContext.GetConnection))
                {
                    await con.OpenAsync();
                    using (OdbcCommand com = new OdbcCommand(_query, con))
                    {
                        if (filters != null)
                        {
                            com.CommandType = CommandType.Text;
                            com.Parameters.Add("@StartDate", OdbcType.DateTime).Value = filters.StartDate;
                            com.Parameters.Add("@EndDate", OdbcType.DateTime).Value = filters.EndDate;
                        }

                        using (OdbcDataReader reader = com.ExecuteReader())
                        {

                            _list = new List<Queues>();
                            while (reader.Read())
                            {
                                _list.Add(new Queues.QueuesBuilder()
                                                .WithName(reader["Queue"].ToString())
                                                .WithTotal(Convert.ToInt32(reader["Total"]))
                                               .Build()
                                               );
                            }
                            reader.Close();
                        }

                    }
                }
            }

            catch
            {
                return null;
            }
            return _list;
        }
    }
}

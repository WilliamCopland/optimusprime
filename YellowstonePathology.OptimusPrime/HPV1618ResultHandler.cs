using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
//using MySql.Data.MySqlClient;

namespace YellowstonePathology.OptimusPrime
{
    public class HPV1618ResultHandler
    {
        public async Task<object> Invoke(object input)
        {
            var payload = (IDictionary<string, object>)input;
            return await HandleResult(payload);
        }

        public async Task<string> HandleResult(IDictionary<string, object> payload)
        {
            var connectionString = "Data Source=TestSQL;Initial Catalog=YPIData;Integrated Security=True";

            string testName = (string)payload["testName"];
            string aliquotOrderId = (string)payload["aliquotOrderId"];
            string hpv16Result = (string)payload["hpv16Result"];
            string hpv18Result = (string)payload["hpv1845Result"];

            HPV1618Result hpv1618Result = HPV1618Result.GetResult(hpv16Result, hpv18Result);
            string sql = hpv1618Result.GetSqlStatement(aliquotOrderId);
            //await MySqlHelper.ExecuteNonQueryAsync(connectionString, sql, null);
            using (var cnx = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(sql, cnx))
                {
                    await cnx.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }

            return "Optimus Prime updated result: " + aliquotOrderId + " - " + testName + " on " + DateTime.Now.ToString();
        }
    }
}

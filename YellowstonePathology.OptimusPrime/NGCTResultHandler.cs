using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace YellowstonePathology.OptimusPrime
{
    public class NGCTResultHandler
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
            string ngResult = (string)payload["ngResult"];
            string ctResult = (string)payload["ctResult"];

            NGCTResult ngctResult = NGCTResult.GetResult(ngResult, ctResult);
            string sql = ngctResult.GetSqlStatement(aliquotOrderId);

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

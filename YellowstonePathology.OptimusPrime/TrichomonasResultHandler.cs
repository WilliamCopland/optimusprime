using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.OptimusPrime
{
    public class TrichomonasResultHandler
    {
        public async Task<object> Invoke(object input)
        {
            var payload = (IDictionary<string, object>)input;
            return await HandleResult(payload);
        }

        public async Task<string> HandleResult(IDictionary<string, object> payload)
        {            
            var connectionString = "Server = 10.1.2.26; Uid = sqldude; Pwd = 123Whatsup; Database = lis;";

            string testName = (string)payload["testName"];
            string aliquotOrderId = (string)payload["aliquotOrderId"];
            string result = (string)payload["result"];
            string sql = null;

            if (testName == "TRICH")
            {
                HPVResult hpvResult = null;
                if (result == "TRICH neg")
                {
                    hpvResult = new TrichomonasNegativeResult();
                    sql = @"Update tblTrichomonasTestOrder psoh "
                        + "Inner Join tblPanelSetOrder pso on psoh.ReportNo = pso.ReportNo "
                        + "set Result = '" + hpvResult.Result + "' "                        
                        + "Where pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";

                    sql += @"Update tblPanelSetOrder set ResultCode = '" + hpvResult.ResultCode + "', "
                        + "Accepted = 1, "
                        + "AcceptedBy = 'AUTOVER TESTING', "
                        + "AcceptedById = 5134, "
                        + "AcceptedDate = '" + DateTime.Today.ToString("yyyy-MM-dd") + "', "
                        + "AcceptedTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "', "
                        + "Final = 1, "
                        + "Signature = 'Optimus Prime', "
                        + "FinaledById = 5134, "
                        + "FinalDate = '" + DateTime.Today.ToString("yyyy-MM-dd") + "', "
                        + "FinalTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' "
                        + "where PanelSetId = 61 and Accepted = 0 and OrderedOnId = '" + aliquotOrderId + "';";                    
                }
                else if (result == "TRICH POS")
                {
                    hpvResult = new TrichomonasPositiveResult();
                    sql = @"Update tblTrichomonasTestOrder set Result = '" + hpvResult.Result + "' "
                        + "from tblTrichomonasTestOrder psoh, tblPanelSetOrder pso "
                        + "where psoh.ReportNo = pso.ReportNo "
                        + "and pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";

                    sql += @"Update tblPanelSetOrder set ResultCode = '" + hpvResult.ResultCode + "', "
                    + "HoldDistribution = 1, "
                    + "Accepted = 1, "
                    + "AcceptedBy = 'AUTOVER TESTING', "
                    + "AcceptedById = 5134, "
                    + "AcceptedDate = '" + DateTime.Today.ToString("yyyy-MM-dd") + "', "
                    + "AcceptedTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' "
                    + "where PanelSetId = 61 and Accepted = 0 and OrderedOnId = '" + aliquotOrderId + "';";
                }
                else if (result == "Invalid")
                {
                    hpvResult = new TrichomonasInvalidResult();
                    sql = @"Update tblTrichomonasTestOrder set Result = '" + hpvResult.Result + "' "
                        + "from tblTrichomonasTestOrder psoh, tblPanelSetOrder pso "
                        + "where psoh.ReportNo = pso.ReportNo "
                        + "and pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";

                    sql += @"Update tblPanelSetOrder set ResultCode = '" + hpvResult.ResultCode + "', "
                    + "HoldDistribution = 1 "                    
                    + "where PanelSetId = 61 and Accepted = 0 and OrderedOnId = '" + aliquotOrderId + "';";                    
                }
            }

            using (var cnx = new MySqlConnection(connectionString))
            {
                using (var cmd = new MySqlCommand(sql, cnx))
                {
                    await cnx.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }

            return "Optimus Prime updated result: " + aliquotOrderId + " - " + testName + " on: " + DateTime.Now.ToString();
        }
    }
}

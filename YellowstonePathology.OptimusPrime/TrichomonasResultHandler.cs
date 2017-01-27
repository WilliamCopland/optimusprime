using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

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
                    hpvResult = new HPVNegativeResult();
                    sql = @"Update tblTrichomonasTestOrder psoh INNER JOIN tblPanelSetOrder pso ON psoh.ReportNo = pso.ReportNo "
                        + "set psoh.Result = '" + hpvResult.Result + "' "
                        + "where pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";                        

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
                    hpvResult = new HPVPositiveResult();
                    sql = @"Update tblTrichomonasTestOrder psoh INNER JOIN tblPanelSetOrder pso ON psoh.ReportNo = pso.ReportNo "
                        + "set psoh.Result = '" + hpvResult.Result + "' "
                        + "where pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";                        

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
                    hpvResult = new HPVInvalidResult();
                    sql = @"Update tblHPVTestOrder psoh INNER JOIN tblPanelSetOrder pso ON psoh.ReportNo = pso.ReportNo "
                        + "set psoh.Result = '" + hpvResult.Result + "' "
                        + "where pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";                    

                    sql += @"Update tblPanelSetOrder set ResultCode = '" + hpvResult.ResultCode + "', "
                    + "HoldDistribution = 1 "                    
                    + "where PanelSetId = 61 and Accepted = 0 and OrderedOnId = '" + aliquotOrderId + "';";                    
                }
            }

            await MySqlHelper.ExecuteNonQueryAsync(connectionString, sql, null);

            return "Optimus Prime updated result: " + aliquotOrderId + " - " + testName + " on: " + DateTime.Now.ToString();
        }
    }
}

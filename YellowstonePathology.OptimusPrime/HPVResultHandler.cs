using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace YellowstonePathology.OptimusPrime
{
    public class HPVResultHandler
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
            string overallInterpretation = (string)payload["overallInterpretation"];
            string sql = null;

            if (testName == "HPV")
            {
                HPVResult hpvResult = null;
                if (overallInterpretation == "Negative")
                {
                    hpvResult = new HPVNegativeResult();
                    sql = @"Update tblHPVTestOrder set Result = '" + hpvResult.Result + "' "
                        + "from tblHPVTestOrder psoh, tblPanelSetOrder pso "
                        + "where psoh.ReportNo = pso.ReportNo "
                        + "and pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";                        

                    sql += @"Update tblPanelSetOrder set ResultCode = '" + hpvResult.ResultCode + "', "                    
                    + "[Accepted] = 1, "
                    + "[AcceptedBy] = 'AUTOVER TESTING', "                    
                    + "[AcceptedById] = 5134, "
                    + "[AcceptedDate] = '" + DateTime.Today.ToString("MM/dd/yyyy") + "', "
                    + "[AcceptedTime] = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "', "
                    + "[Final] = 1, "
                    + "[Signature] = 'AUTOVER TESTING', "
                    + "[FinaledById] = 5134, "
                    + "[FinalDate] = '" + DateTime.Today.ToString("MM/dd/yyyy") + "', "
                    + "[FinalTime] = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "' "
                    + "where PanelSetId = 14 and Accepted = 0 and OrderedOnId = '" + aliquotOrderId + "';";                    
                }
                else if (overallInterpretation == "POSITIVE")
                {
                    hpvResult = new HPVPositiveResult();
                    sql = @"Update tblHPVTestOrder set Result = '" + hpvResult.Result + "' "
                        + "from tblHPVTestOrder psoh, tblPanelSetOrder pso "
                        + "where psoh.ReportNo = pso.ReportNo "
                        + "and pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";                        

                    sql += @"Update tblPanelSetOrder set ResultCode = '" + hpvResult.ResultCode + "', "                    
                    + "[Accepted] = 1, "
                    + "[AcceptedBy] = 'AUTOVER TESTING', "
                    + "[AcceptedById] = 5134, "
                    + "[AcceptedDate] = '" + DateTime.Today.ToString("MM/dd/yyyy") + "', "
                    + "[AcceptedTime] = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "' "
                    + "where PanelSetId = 14 and Accepted = 0 and OrderedOnId = '" + aliquotOrderId + "';";
                }
                else if (overallInterpretation == "Invalid")
                {
                    hpvResult = new HPVInvalidResult();
                    sql = @"Update tblHPVTestOrder set Result = '" + hpvResult.Result + "' "
                        + "from tblHPVTestOrder psoh, tblPanelSetOrder pso "
                        + "where psoh.ReportNo = pso.ReportNo "
                        + "and pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";                    

                    sql += @"Update tblPanelSetOrder set ResultCode = '" + hpvResult.ResultCode + "' "                    
                    + "where PanelSetId = 14 and Accepted = 0 and OrderedOnId = '" + aliquotOrderId + "';";                    
                }
            }

            using (var cnx = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(sql, cnx))
                {
                    await cnx.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }

            return "Optimus Prime updated result: " + aliquotOrderId + " - " + testName + " on: " + DateTime.Now.ToString();
        }
    }
}

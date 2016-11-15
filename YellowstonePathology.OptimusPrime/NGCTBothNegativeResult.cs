using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.OptimusPrime
{
    public class NGCTBotNegativeResult : NGCTResult
    {                        
        public NGCTBotNegativeResult() 
        {
            this.m_CTResult = "Negative";
            this.m_CTResultCode = "CTNGTV";
            this.m_NGResult = "Negative";
            this.m_NGResultCode = "NGNGTV";                        
        }

        public override string GetSqlStatement(string aliquotOrderId)
        {
            string sql = @"Update tblNGCTTestOrder set NeisseriaGonorrhoeaeResult = '" + this.m_NGResult + "',  "
                        + "NGResultCode = '" + this.m_NGResultCode + "', "
                        + "ChlamydiaTrachomatisResult = '" + this.m_CTResult + "', "
                        + "CTResultCode = '" + this.m_CTResultCode + "' "
                        + "from tblNGCTTestOrder ngct, tblPanelSetOrder pso "
                        + "where ngct.ReportNo = pso.ReportNo "
                        + "and pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";

            sql += @"Update tblPanelSetOrder set "
            + "[Accepted] = 1, "
            + "[AcceptedBy] = 'Optimus Prime', "
            + "[AcceptedById] = 5134, "
            + "[AcceptedDate] = '" + DateTime.Today.ToString("MM/dd/yyyy") + "', "
            + "[AcceptedTime] = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "', "
            + "[Final] = 1, "
            + "[Signature] = 'Optimus Prime', "
            + "[FinaledById] = 5134, "
            + "[FinalDate] = '" + DateTime.Today.ToString("MM/dd/yyyy") + "', "
            + "[FinalTime] = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "' "
            + "where PanelSetId = 3 and Accepted = 0 and OrderedOnId = '" + aliquotOrderId + "';";

            return sql;
        }
    }
}

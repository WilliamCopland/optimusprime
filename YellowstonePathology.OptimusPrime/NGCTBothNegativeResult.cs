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
            string sql = @"Update tblNGCTTestOrder ngct "
                + "Inner Join tblPanelSetOrder pso on ngct.ReportNo = pso.ReportNo "
                + "set NeisseriaGonorrhoeaeResult = '" + this.m_NGResult + "',  "
                + "NGResultCode = '" + this.m_NGResultCode + "', "
                + "ChlamydiaTrachomatisResult = '" + this.m_CTResult + "', "
                + "CTResultCode = '" + this.m_CTResultCode + "' "                
                + "Where pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";

            sql += @"Update tblPanelSetOrder set "
            + "Accepted = 1, "
            + "AcceptedBy = 'AUTOVER TESTING', "
            + "AcceptedById = 5134, "
            + "AcceptedDate = '" + DateTime.Today.ToString("yyyy-MM-dd") + "', "
            + "AcceptedTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "', "
            + "Final = 1, "
            + "Signature = 'AUTOVER TESTING', "
            + "FinaledById = 5134, "
            + "FinalDate = '" + DateTime.Today.ToString("yyyy-MM-dd") + "', "
            + "FinalTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' "
            + "where PanelSetId = 3 and Accepted = 0 and OrderedOnId = '" + aliquotOrderId + "';";

            return sql;
        }
    }
}

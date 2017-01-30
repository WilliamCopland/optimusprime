using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.OptimusPrime
{
    public class NGCTOneOrBothPositiveResult : NGCTResult
    {                        
        public NGCTOneOrBothPositiveResult(string pantherNGResult, string pantherCTResult) 
        {
            if(pantherNGResult == NGCTResult.PantherNGPositiveResult)
            {
                this.m_NGResult = "Positive";
                this.m_NGResultCode = "NGPSTV";
            }
            else
            {
                this.m_NGResult = "Negative";
                this.m_NGResultCode = "NGNGTV";
            }

            if(pantherCTResult == NGCTResult.PantherCTPositiveResult)
            {
                this.m_CTResult = "Positive";
                this.m_CTResultCode = "CTPSTV";
            }
            else
            {
                this.m_CTResult = "Negative";
                this.m_CTResultCode = "CTNGTV";
            }                       
        }

        public override string GetSqlStatement(string aliquotOrderId)
        {
            string sql = @"Update tblNGCTTestOrder ngct INNER JOIN tblPanelSetOrder pso ON ngct.ReportNo = pso.ReportNo "
                        + "set ngct.NeisseriaGonorrhoeaeResult = '" + this.m_NGResult + "',  "
                        + "ngct.NGResultCode = '" + this.m_NGResultCode + "', "
                        + "ngct.ChlamydiaTrachomatisResult = '" + this.m_CTResult + "', "
                        + "ngct.CTResultCode = '" + this.m_CTResultCode + "' "
                        + "where pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";

            sql += @"Update tblPanelSetOrder set "
            + "Accepted = 1, "
            + "AcceptedBy = 'AUTOVER TESTING', "
            + "AcceptedById = 5134, "
            + "AcceptedDate = '" + DateTime.Today.ToString("yyyy-MM-dd") + "', "
            + "AcceptedTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' "            
            + "where PanelSetId = 3 and Accepted = 0 and OrderedOnId = '" + aliquotOrderId + "';";

            return sql;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.OptimusPrime
{
    public class NGCTInvalidResult : NGCTResult
    {                        
        public NGCTInvalidResult() 
        {
            this.m_CTResult = "Invalid";
            this.m_CTResultCode = "CTNVLD";
            this.m_NGResult = "Invalid";
            this.m_NGResultCode = "NGNVLD";                        
        }

        public override string GetSqlStatement(string aliquotOrderId)
        {
            string sql = @"Update tblNGCTTestOrder ngct INNER JOIN tblPanelSetOrder pso ON ngct.ReportNo = pso.ReportNo " 
                        + "set ngct.NeisseriaGonorrhoeaeResult = '" + this.m_NGResult + "',  "
                        + "ngct.NGResultCode = '" + this.m_NGResultCode + "', "
                        + "ngct.ChlamydiaTrachomatisResult = '" + this.m_CTResult + "', "
                        + "ngct.CTResultCode = '" + this.m_CTResultCode + "' "
                        + "where pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; "; 
            return sql;
        }
    }
}

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
            string sql = @"Update tblNGCTTestOrder set NeisseriaGonorrhoeaeResult = '" + this.m_NGResult + "',  "
                        + "NGResultCode = '" + this.m_NGResultCode + "', "
                        + "ChlamydiaTrachomatisResult = '" + this.m_CTResult + "', "
                        + "CTResultCode = '" + this.m_CTResultCode + "' "
                        + "from tblNGCTTestOrder ngct, tblPanelSetOrder pso "
                        + "where ngct.ReportNo = pso.ReportNo "
                        + "and pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";
            return sql;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.OptimusPrime
{
    public class HPV1618InvalidResult : HPV1618Result
    {                        
        public HPV1618InvalidResult() 
        {
            this.m_HPV16Result = "Invalid";
            this.m_HPV16ResultCode = "HPV1618G16NVLD";
            this.m_HPV18Result = "Invalid";
            this.m_HPV18ResultCode = "HPV1618G18NVLD";                        
        }

        public override string GetSqlStatement(string aliquotOrderId)
        {
            string sql = @"Update tblPanelSetOrderHPV1618 hpv Inner Join tblPanelSetOrder pso on hpv.ReportNo = pso.ReportNo "
                        + "set hpv.HPV16Result = '" + this.m_HPV16Result + "',  "
                        + "hpv.HPV16ResultCode = '" + this.m_HPV16ResultCode + "', "
                        + "hpv.HPV18Result = '" + this.m_HPV18Result + "', "
                        + "hpv.HPV18ResultCode = '" + this.m_HPV18ResultCode + "' "
                        + "where pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";
            return sql;
        }
    }
}

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
            string sql = @"Update tblPanelSetOrderHPV1618 set HPV16Result = '" + this.m_HPV16Result + "',  "
                        + "HPV16ResultCode = '" + this.m_HPV16ResultCode + "', "
                        + "HPV18Result = '" + this.m_HPV18Result + "', "
                        + "HPV18ResultCode = '" + this.m_HPV18ResultCode + "' "
                        + "from tblPanelSetOrderHPV1618 hpv, tblPanelSetOrder pso "
                        + "where hpv.ReportNo = pso.ReportNo "
                        + "and pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";
            return sql;
        }
    }
}

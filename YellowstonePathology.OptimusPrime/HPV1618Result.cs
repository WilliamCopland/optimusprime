using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.OptimusPrime
{
    public class HPV1618Result
    {
        public const string PantherHPV16NegativeResult = "Negative";
        public const string PantherHPV16PositiveResult = "POSITIVE";

        public const string PantherHPV1845NegativeResult = "Negative";
        public const string PantherHPV1845PositiveResult = "POSITIVE";

        public const string PantherHPV16InvalidResult = "Invalid";
        public const string PantherHPV1845InvalidResult = "Invalid";

        protected string m_HPV16ResultCode;
        protected string m_HPV16Result;
        protected string m_HPV18ResultCode;
        protected string m_HPV18Result;        

        public HPV1618Result()
        {

        } 

        public static HPV1618Result GetResult(string pantherHPV16Result, string pantherHPV1845Result)
        {
            HPV1618Result result = null;
            if (pantherHPV16Result == PantherHPV16PositiveResult || pantherHPV1845Result == PantherHPV1845PositiveResult)
            {
                result = new HPV1618OneOrBothPositiveResult(pantherHPV16Result, pantherHPV1845Result);
            }
            else if (pantherHPV16Result == PantherHPV16InvalidResult || pantherHPV1845Result == PantherHPV1845InvalidResult)
            {
                result = new HPV1618InvalidResult();
            }
            else
            {
                result = new HPV1618BothNegativeResult();
            }
            return result;
        }
        
        public string HPV16ResultCode
        {
            get { return this.m_HPV16ResultCode; }
        }

        public string HPV16Result
        {
            get { return this.m_HPV16Result; }
        }

        public string HPV18ResultCode
        {
            get { return this.m_HPV18ResultCode; }
        }

        public string HPV18Result
        {
            get { return this.m_HPV18Result; }
        }       

        public virtual string GetSqlStatement(string aliquotOrderId)
        {
            string sql = @"Update tblPanelSetOrderHPV1618 set HPV16Result = '" + this.m_HPV16Result + "',  "
                        + "HPV16ResultCode = '" + this.m_HPV16ResultCode + "', "
                        + "HPV18Result = '" + this.m_HPV18Result + "', "
                        + "HPV18ResultCode = '" + this.m_HPV18ResultCode + "' "
                        + "from tblPanelSetOrderHPV1618 hpv, tblPanelSetOrder pso "
                        + "where hpv.ReportNo = pso.ReportNo "
                        + "and pso.OrderedOnId = '" + aliquotOrderId + "' and pso.Accepted = 0; ";

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
            + "where PanelSetId = 62 and Accepted = 0 and OrderedOnId = '" + aliquotOrderId + "';";

            return sql;
        }
    }
}

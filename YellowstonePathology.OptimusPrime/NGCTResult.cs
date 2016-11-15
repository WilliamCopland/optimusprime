using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.OptimusPrime
{
    public class NGCTResult
    {
        public const string PantherNGNegativeResult = "GC neg";
        public const string PantherNGPositiveResult = "GC POS";

        public const string PantherCTNegativeResult = "CT neg";
        public const string PantherCTPositiveResult = "CT POS";

        public const string PantherNGInvalidResult = "Invalid";
        public const string PantherCTInvalidResult = "Invalid";

        protected string m_NGResultCode;
        protected string m_NGResult;
        protected string m_CTResultCode;
        protected string m_CTResult;        

        public NGCTResult()
        {

        } 

        public static NGCTResult GetResult(string pantherNGResult, string pantherCTResult)
        {
            NGCTResult result = null;
            if (pantherNGResult == PantherNGPositiveResult || pantherCTResult == PantherCTPositiveResult)
            {
                result = new NGCTOneOrBothPositiveResult(pantherNGResult, pantherCTResult);
            }
            else if (pantherNGResult == PantherNGInvalidResult || pantherCTResult == PantherCTInvalidResult)
            {
                result = new NGCTInvalidResult();
            }
            else
            {
                result = new NGCTBotNegativeResult();
            }
            return result;
        }
        
        public string NGResultCode
        {
            get { return this.m_NGResultCode; }
        }

        public string NGResult
        {
            get { return this.m_NGResult; }
        }

        public string CTResultCode
        {
            get { return this.m_CTResultCode; }
        }

        public string CTResult
        {
            get { return this.m_CTResult; }
        }       

        public virtual string GetSqlStatement(string aliquotOrderId)
        {
            throw new NotImplementedException("Not Implemented here.");
        }
    }
}

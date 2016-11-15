using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.OptimusPrime
{
    public class HPVNegativeResult : HPVResult
    {
        public HPVNegativeResult()
        {            
            this.m_ResultCode = "HPVNGTV";
            this.m_Result = HPVResult.NegativeResult;
        }
    }
}

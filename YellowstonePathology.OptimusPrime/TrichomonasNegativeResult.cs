using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.OptimusPrime
{
    public class TrichomonasNegativeResult : HPVResult
    {
        public TrichomonasNegativeResult()
        {            
            this.m_ResultCode = "TRCHMNSNEG";
            this.m_Result = HPVResult.NegativeResult;
        }
    }
}

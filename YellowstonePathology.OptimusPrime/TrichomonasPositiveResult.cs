using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.OptimusPrime
{
    public class TrichomonasPositiveResult : HPVResult
    {
        public TrichomonasPositiveResult()
        {
            this.m_ResultCode = "TRCHMNSPOS";
            this.m_Result = HPVResult.PositiveResult;
        }
    }
}

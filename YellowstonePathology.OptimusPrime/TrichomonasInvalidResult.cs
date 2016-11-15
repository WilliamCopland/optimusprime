using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.OptimusPrime
{
    public class TrichomonasInvalidResult : HPVResult
    {
        public TrichomonasInvalidResult()
        {            
            this.m_ResultCode = "TRCHMNSNVLD";
            this.m_Result = HPVResult.InvalidResult;
        }
    }
}

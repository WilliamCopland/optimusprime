using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.OptimusPrime
{
    public class HPVInvalidResult : HPVResult
    {
        public HPVInvalidResult()
        {            
            this.m_ResultCode = "HPVNVLD";
            this.m_Result = HPVResult.InvalidResult;
        }
    }
}

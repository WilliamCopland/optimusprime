using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.OptimusPrime
{
    public class HPV1618BothNegativeResult : HPV1618Result
    {                        
        public HPV1618BothNegativeResult() 
        {
            this.m_HPV16Result = "Negative";
            this.m_HPV16ResultCode = "HPV1618G16NGTV";
            this.m_HPV18Result = "Negative";
            this.m_HPV18ResultCode = "HPV1618G18NGTV";                        
        }        
    }
}

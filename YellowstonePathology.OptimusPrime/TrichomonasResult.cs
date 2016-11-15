using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.OptimusPrime
{
    public class TrichomonasResult
    {        
        public static string PositiveResult = "Positive";
        public static string NegativeResult = "Negative";
        public static string InvalidResult = "Invalid";

        protected string m_ResultCode;
        protected string m_Result;        

        public TrichomonasResult()
        {

        } 
        
        public string ResultCode
        {
            get { return this.m_ResultCode; }
        }              

        public string Result
        {
            get { return this.m_Result; }
        }
    }
}

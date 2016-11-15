using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.OptimusPrime
{
    public class PanelSetOrder
    {
        private string m_ObjectId;
        protected string m_ReportNo;
        protected int m_PanelSetId;
        protected string m_PanelSetName;
        protected string m_MasterAccessionNo;

        public PanelSetOrder()
        {

        }
                
        public string ObjectId
        {
            get { return this.m_ObjectId; }
            set
            {
                if (this.m_ObjectId != value)
                {
                    this.m_ObjectId = value;        
                }
            }
        }
        
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if (this.m_ReportNo != value)
                {
                    this.m_ReportNo = value;
                }
            }
        }
        
        public int PanelSetId
        {
            get { return this.m_PanelSetId; }
            set
            {
                if (this.m_PanelSetId != value)
                {
                    this.m_PanelSetId = value;                    
                }
            }
        }
        
        public string PanelSetName
        {
            get { return this.m_PanelSetName; }
            set
            {
                if (this.m_PanelSetName != value)
                {
                    this.m_PanelSetName = value;                    
                }
            }
        }
        
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if (this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                }
            }
        }
    }
}

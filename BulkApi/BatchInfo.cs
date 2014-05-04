using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BulkApi
{
    /// <remarks/>
    [DataContract(Namespace = "http://www.force.com/2009/06/asyncapi/dataload", Name="batchInfo")]
    public partial class BatchInfo
    {

        private string idField;

        private string jobIdField;

        private BatchState stateField;

        private System.DateTime createdDateField;

        private System.DateTime systemModstampField;

        private byte numberRecordsProcessedField;

        /// <remarks/>
        [DataMember]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string jobId
        {
            get
            {
                return this.jobIdField;
            }
            set
            {
                this.jobIdField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public BatchState state
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public System.DateTime createdDate
        {
            get
            {
                return this.createdDateField;
            }
            set
            {
                this.createdDateField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public System.DateTime systemModstamp
        {
            get
            {
                return this.systemModstampField;
            }
            set
            {
                this.systemModstampField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public byte numberRecordsProcessed
        {
            get
            {
                return this.numberRecordsProcessedField;
            }
            set
            {
                this.numberRecordsProcessedField = value;
            }
        }
    }


}

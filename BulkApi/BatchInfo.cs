using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BulkApi
{

    [DataContract(Name="batchInfo", Namespace = "http://www.force.com/2009/06/asyncapi/dataload")]
    public partial class BatchInfo
    {
        public string XML { get; set; }
        private string idField;

        private string jobIdField;

        private BatchState stateField;

        private System.DateTime createdDateField;

        private System.DateTime systemModstampField;

        private long numberRecordsProcessedField;

        private long numberRecordsFailedField;

        private long totalProcessingTimeField;

        private long apiActiveProcessingTimeField;

        private long apexProcessingTimeField;

        /// <remarks/>
        [DataMember(Order=0)]
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
        [DataMember(Order = 1)]
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
        [DataMember(Order = 2)]
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
        [DataMember(Order = 3)]
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
        [DataMember(Order = 4)]
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
        [DataMember(Order = 5)]
        public long numberRecordsProcessed
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

        /// <remarks/>
        [DataMember(Order = 6)]
        public long numberRecordsFailed
        {
            get
            {
                return this.numberRecordsFailedField;
            }
            set
            {
                this.numberRecordsFailedField = value;
            }
        }

        /// <remarks/>
        [DataMember(Order = 7)]
        public long totalProcessingTime
        {
            get
            {
                return this.totalProcessingTimeField;
            }
            set
            {
                this.totalProcessingTimeField = value;
            }
        }

        /// <remarks/>
        [DataMember(Order = 8)]
        public long apiActiveProcessingTime
        {
            get
            {
                return this.apiActiveProcessingTimeField;
            }
            set
            {
                this.apiActiveProcessingTimeField = value;
            }
        }

        /// <remarks/>
        [DataMember(Order = 9)]
        public long apexProcessingTime
        {
            get
            {
                return this.apexProcessingTimeField;
            }
            set
            {
                this.apexProcessingTimeField = value;
            }
        }
    }



}

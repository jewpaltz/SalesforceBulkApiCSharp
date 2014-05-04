using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BulkApi
{
    public enum OperationType {  
        query, 
        [Description("Insert")]
        insert,
        upsert, update, delete
    }
    public enum BulkContentType { CSV, XML }
    public enum JobState { Open, Closed, Aborted, Failed }
    public enum BatchState { Queued, InProgress, Completed, Failed,[EnumMember(Value = "Not Processed")] NotProcessed }

    [DataContract(Name = "jobInfo", Namespace = "http://www.force.com/2009/06/asyncapi/dataload")]
    public class JobCreationRequest
    {
        [DataMember(Order=0)]public OperationType operation { get; set; }
        [DataMember(Name="object", Order=1)]
        public string Object { get; set; }
        [DataMember(Order=2)]
        public string externalIdFieldName { get; set; }
        [DataMember(Order = 3)]
        public BulkContentType contentType { get; set; }
    }

    [DataContract(Name = "jobInfo", Namespace = "http://www.force.com/2009/06/asyncapi/dataload")]
    public class JobCreationResponse
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public OperationType operation { get; set; }
        [DataMember(Name="object")]
        public string Object { get; set; }
        [DataMember]
        public string createdById { get; set; }
        [DataMember]
        public DateTime createdDate { get; set; }
        [DataMember]
        public DateTime systemModstamp { get; set; }
        [DataMember]
        public JobState state { get; set; }
        [DataMember]
        public string concurrencyMode { get; set; }
        [DataMember]
        public BulkContentType contentType { get; set; }
        [DataMember]
        public int numberBatchesQueued { get; set; }
        [DataMember]
        public int numberBatchesInProgress { get; set; }
        [DataMember]
        public int numberBatchesCompleted { get; set; }
        [DataMember]
        public int numberBatchesFailed { get; set; }
        [DataMember]
        public int numberBatchesTotal { get; set; }
        [DataMember]
        public int numberRecordsProcessed { get; set; }
        [DataMember]
        public int numberRetries { get; set; }
        [DataMember]
        public string apiVersion { get; set; }
        [DataMember]
        public int numberRecordsFailed { get; set; }
        [DataMember]
        public int totalProcessingTime { get; set; }
        [DataMember]
        public int apiActiveProcessingTime { get; set; }
        [DataMember]
        public int apexProcessingTime { get; set; }
    }

    [DataContract(Name = "result-list", Namespace = "http://www.force.com/2009/06/asyncapi/dataload")]
    public class ResultList
    {
        [DataMember]
        public string result { get; set; }
    }
}

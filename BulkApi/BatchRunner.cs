using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BulkApi
{
    public class BatchRunner: INotifyPropertyChanged
    {
        public BatchRunner(string userName, string password, string securityToken, OperationType operation, string Object, BulkContentType contentType, string body, string externalIdFieldName)
        {
            Job = new JobCreationResponse(); 
            Batch = new BatchInfo();
            Task = this.Run(userName, password, securityToken, operation, Object, contentType, body, externalIdFieldName);
        }

        private async Task<string> Run(string userName, string password, string securityToken, OperationType operation, string Object, BulkContentType contentType, string body, string externalIdFieldName=null)
        {
            var api = new BulkApiContext();
            await api.Login(userName, password, securityToken);
            Job = await api.CreateJob(new JobCreationRequest { contentType = contentType, Object = Object, operation = operation, externalIdFieldName=externalIdFieldName });
            Batch = await api.AddBatch(body, Job.id);
            while (Batch.state == BatchState.Queued)
            {
                await System.Threading.Tasks.Task.Delay(5000);
                Batch = await api.GetBatchStatus(Batch);
                Job = await api.GetJobStatus(Job.id);
            }
            Results = await api.GetBatchResult(Batch.id, Batch.jobId);
            return Results;
        }

        private Task<string> _Task;
        public Task<string> Task
        {
            get { return _Task; }
            private set { _Task = value; OnPropertyChanged(); }
        }
        

        private JobCreationResponse _Job;
        public JobCreationResponse Job
        {
            get { return _Job; }
            private set { _Job = value; OnPropertyChanged(); }
        }
        private BatchInfo _Batch;
        public BatchInfo Batch
        {
            get { return _Batch; }
            private set { _Batch = value; OnPropertyChanged(); }
        }
        private string _Results;
        public string Results
        {
            get { return _Results; }
            private set { _Results = value; OnPropertyChanged(); }
        }
        
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

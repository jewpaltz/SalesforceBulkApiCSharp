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
            Login(userName, password, securityToken);
            Task = this.Run(operation, Object, contentType, body, externalIdFieldName);
        }
        public BatchRunner(BulkApiContext api, OperationType operation, string Object, BulkContentType contentType, string body, string externalIdFieldName)
        {
            _Api = api;
            Task = this.Run(operation, Object, contentType, body, externalIdFieldName);
        }

        BulkApiContext _Api;

        private void Login(string userName, string password, string securityToken)
        {
            _Api = new BulkApiContext();
            //  If await the lagin or return the task (i.e. eith way of running the login asynchronesly) I get a null reference exception
            _Api.Login(userName, password, securityToken).Wait();
        }

        private async Task<string> Run(OperationType operation, string Object, BulkContentType contentType, string body, string externalIdFieldName=null)
        {
            Job = await _Api.CreateJob(new JobCreationRequest { contentType = contentType, Object = Object, operation = operation, externalIdFieldName=externalIdFieldName });
            Batch = await _Api.AddBatch(body, Job.id);
            while (Batch.state == BatchState.Queued)
            {
                await System.Threading.Tasks.Task.Delay(5000);
                Batch = await _Api.GetBatchStatus(Batch);
                Job = await _Api.GetJobStatus(Job.id);
            }
            Results = await _Api.GetBatchResult(Batch.id, Batch.jobId);
            return Results;
        }

        private Task<string> _Task;
        public Task<string> Task
        {
            get { return _Task; }
            private set { _Task = value; OnPropertyChanged(); }
        }
        

        private JobCreationResponse _Job = new JobCreationResponse();
        public JobCreationResponse Job
        {
            get { return _Job; }
            private set { _Job = value; OnPropertyChanged(); }
        }
        private BatchInfo _Batch = new BatchInfo();
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

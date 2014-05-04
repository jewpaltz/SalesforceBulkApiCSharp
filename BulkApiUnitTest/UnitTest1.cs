using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BulkApi;
using System.Threading.Tasks;
using System.IO;

namespace BulkApiUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private string _UserName = "jewpaltz@gmail.com.uat";
        private string _Password = "";
        private string _SecurityToken = "";
        private string _SOQL = "select FirstName, LastName, Phone from Contact where LastName = 'Plotkin'";



        [TestMethod]
        public void TestUpsertContacts()
        {
            var csv = File.ReadAllText("contactsupsert.csv");
            var api = new BulkApiContext();
            api.Login(_UserName, _Password, _SecurityToken).Wait();
            var job = api.CreateJob(new JobCreationRequest { contentType = BulkContentType.CSV, Object = "Contact", operation = OperationType.query }).Result;
            var batch = api.AddBatch(csv, job.id).Result;
            while (batch.state == BatchState.Queued)
            {
                Task.Delay(5000).Wait();
                batch = api.GetBatchStatus(batch).Result;
            }
            var results = api.GetBatchResult(batch.id, batch.jobId).Result;

            File.AppendAllText("results.csv", results);
        }
        [TestMethod]
        public void TestBatchRunnerUpsert()
        {
            var csv = File.ReadAllText("contactsupsert.csv");
            var b = new BatchRunner(_UserName, _Password, _SecurityToken, OperationType.upsert, "Contact", BulkContentType.CSV, csv, "CMS_Family_ID__c").Task;
            File.AppendAllText("results.csv", b.Result);
        }
        [TestMethod]
        public void TestBatchRunnerQuery()
        {
            var b = new BatchRunner(_UserName, _Password, _SecurityToken, OperationType.query, "Contact", BulkContentType.CSV, _SOQL, "CMS_Family_ID__c").Task;
            File.AppendAllText("results.csv", b.Result);
        }
    }
}

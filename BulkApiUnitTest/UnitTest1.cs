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


        //  Lower level Unit Test
        [TestMethod]
        public void TestUpsertContactsWithoutBatchRunner()
        {
            var csv = File.ReadAllText("contactsupsert.csv");
            var api = new BulkApiContext();
            api.Login(_UserName, _Password, _SecurityToken).Wait();
            var job = api.CreateJob(new JobCreationRequest { contentType = BulkContentType.CSV, Object = "Contact", operation = OperationType.upsert, externalIdFieldName= "CMS_Family_ID__c" }).Result;
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
            var b = new BatchRunner(_UserName, _Password, _SecurityToken).Run(OperationType.upsert, "Contact", BulkContentType.CSV, csv, "CMS_Family_ID__c");
            File.AppendAllText("results.csv", b.Result);
        }

        [TestMethod]
        public void TestBatchRunnerQuery()
        {
            var b = new BatchRunner(_UserName, _Password, _SecurityToken).Run( OperationType.query, "Contact", BulkContentType.CSV, _SOQL, null);
            File.AppendAllText("results.csv", b.Result);
        }

        [TestMethod]
        public void TestBatchRunnersWithSharedLogin()
        {
            var api = new BulkApiContext();
            api.Login(_UserName, _Password, _SecurityToken).Wait();

            var csv = File.ReadAllText("contactsupsert.csv");
            var b1 = new BatchRunner(api).Run(OperationType.upsert, "Contact", BulkContentType.CSV, csv, "CMS_Family_ID__c");

            var b2 = new BatchRunner(api).Run(OperationType.query, "Contact", BulkContentType.CSV, _SOQL, null);

            File.AppendAllText("results.csv", b1.Result);
            File.AppendAllText("results.csv", b2.Result);
        }

        [TestMethod]
        public void TestBatchRunnerVM()
        {
            var vm = new Salesforce_Api_Test.BulkRunnerVM();
            vm.UserName = _UserName;
            vm.Password = _Password;
            vm.SecurityToken = _SecurityToken;
            vm.OperationType = OperationType.insert;
            vm.ObjectName = "Contact";
            vm.InputString = File.ReadAllText("contactsupsert.csv");

            vm.RunBatch().Wait();
        }

    }
}

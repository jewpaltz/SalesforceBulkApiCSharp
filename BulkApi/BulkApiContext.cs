using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BulkApi
{
    public class BulkApiContext
    {
        public BulkApiContext() 
        {
        }

        private const string SOAP_URL = "https://test.salesforce.com/services/Soap/c/29.0/0DFf000000000b7";
        private const string API_PATH = "/services/async/30.0/job/";

        private HttpClient _Client = new HttpClient();
        private SalesforceReference.SoapClient _Svc = new SalesforceReference.SoapClient(
            new System.ServiceModel.BasicHttpBinding( System.ServiceModel.BasicHttpSecurityMode.Transport),
            new System.ServiceModel.EndpointAddress(SOAP_URL)
            );
        private SalesforceReference.LoginResult _CurrentLoginResult = null;

        public async Task Login(string userName, string password, string securityToken)
        {
            var response = await _Svc.loginAsync(new SalesforceReference.LoginScopeHeader(), userName, password + securityToken);
            _CurrentLoginResult = response.result;
            var soapUri = new Uri(_CurrentLoginResult.serverUrl);
            _Client.BaseAddress = new Uri(soapUri, API_PATH);
            _Client.DefaultRequestHeaders.Add("X-SFDC-Session", _CurrentLoginResult.sessionId);
        }

        public bool IsLoggedIn { get { return _CurrentLoginResult != null; } }

        public async Task<JobCreationResponse> CreateJob(JobCreationRequest req)
        {
            if (!IsLoggedIn) throw new Exception("Not Logged In");
            var response = await _Client.PostAsXmlAsync("", req);
            var jobInfo = await response.Content.ReadAsAsync<JobCreationResponse>();
            return jobInfo;
        }
        public async Task<JobCreationResponse> GetJobStatus(string id = null)
        {
            if (!IsLoggedIn) throw new Exception("Not Logged In");
            var response = await _Client.GetAsync(string.Format("{0}/", id));
            var jobInfo = await response.Content.ReadAsAsync<JobCreationResponse>();
            return jobInfo;
        }

        public async Task<BatchInfo> AddBatch(string body, string job_id = null)
        {
            if (!IsLoggedIn) throw new Exception("Not Logged In");
            var response = await _Client.PostAsync(job_id + "/batch", new StringContent(body, Encoding.UTF8, "text/csv") );
            var batchInfo = await response.Content.ReadAsAsync<BatchInfo>();
            return batchInfo;
        }
        public async Task<BatchInfo> GetBatchStatus(string id = null, string job_id = null)
        {
            if (!IsLoggedIn) throw new Exception("Not Logged In");
            var response = await _Client.GetAsync(string.Format("{0}/batch/{1}",job_id, id));
            var batchInfo = await response.Content.ReadAsAsync<BatchInfo>();
            return batchInfo;
        }
        public Task<BatchInfo> GetBatchStatus(BatchInfo req)
        {
            return GetBatchStatus(req.id, req.jobId);
        }

        public async Task<string> GetBatchResult(string id = null, string job_id = null)
        {
            if (!IsLoggedIn) throw new Exception("Not Logged In");
            var response = await _Client.GetAsync(string.Format("{0}/batch/{1}/result", job_id, id));
            if ((await response.Content.ReadAsStringAsync()).Contains("result-list"))
            {
                var resultList = await response.Content.ReadAsAsync<ResultList>();
                response = await _Client.GetAsync(string.Format("{0}/batch/{1}/result/{2}", job_id, id, resultList.result));
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}

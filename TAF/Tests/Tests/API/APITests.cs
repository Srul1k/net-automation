using Business.Business.API.Models;
using Business.Data;
using Core.Core.API;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Tests.Utils;

namespace Tests.Tests.API
{
    [Parallelizable(scope: ParallelScope.All)]
    public class APITests
    {
        [SetUp]
        public void SetUp()
        {
            LoggerHolder.Logger.Info("Generate Client");
            ClientHolder.GenerateClient(Data.APIUrl);
        }

        [Test]
        [Category("Get")]
        public async Task GetUsers_ReturnsSuccessStatusCode()
        {
            try
            {
                LoggerHolder.Logger.Debug($"Run test {nameof(GetUsers_ReturnsSuccessStatusCode)}");
                var request = new RestRequest("users", Method.Get);

                LoggerHolder.Logger.Debug($"Request Method: {request.Method}");
                LoggerHolder.Logger.Info($"Call {ClientHolder.Client.BuildUri(request)}");
                RestResponse response = await ClientHolder.Client.ExecuteAsync(request);

                LoggerHolder.Logger.Info("Validate if this is a Success status code");
                Assert.True(response.IsSuccessStatusCode);
            }
            catch (Exception ex)
            {
                LoggerHolder.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        [Category("Invalid Get")]
        public async Task GetInvalidendpoint_ReturnsNotFoundStatusCode()
        {
            try
            {
                LoggerHolder.Logger.Debug($"Run test {nameof(GetInvalidendpoint_ReturnsNotFoundStatusCode)}");
                var request = new RestRequest("invalidendpoint", Method.Get);

                LoggerHolder.Logger.Debug($"Request Method: {request.Method}");
                LoggerHolder.Logger.Info($"Call {ClientHolder.Client.BuildUri(request)}");
                RestResponse response = await ClientHolder.Client.ExecuteAsync(request);

                LoggerHolder.Logger.Info("Validate if this is a Not Found status code");
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
            catch (Exception ex)
            {
                LoggerHolder.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        [Category("Get")]
        public async Task GetUsers_ReturnsCorrectHeaders()
        {
            try
            {
                LoggerHolder.Logger.Debug($"Run test {nameof(GetUsers_ReturnsCorrectHeaders)}");
                var request = new RestRequest("users", Method.Get);

                LoggerHolder.Logger.Debug($"Request Method: {request.Method}");
                LoggerHolder.Logger.Info($"Call {ClientHolder.Client.BuildUri(request)}");
                RestResponse response = await ClientHolder.Client.ExecuteAsync(request);

                LoggerHolder.Logger.Info("Validate if this is a correct headers");
                Assert.IsTrue(response.ContentHeaders?.Any(h => "application/json; charset=utf-8".CompareTo(h.Value) == 0));
            }
            catch (Exception ex)
            {
                LoggerHolder.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        [Category("Get")]
        public async Task GetUsers_ReturnsNotEmptyUsers()
        {
            try
            {
                LoggerHolder.Logger.Debug($"Run test {nameof(GetUsers_ReturnsNotEmptyUsers)}");
                var request = new RestRequest("users", Method.Get);

                LoggerHolder.Logger.Debug($"Request Method: {request.Method}");
                LoggerHolder.Logger.Info($"Call {ClientHolder.Client.BuildUri(request)}");
                RestResponse response = await ClientHolder.Client.ExecuteAsync(request);
                var users = JsonConvert.DeserializeObject<IEnumerable<User>>(response.Content);

                LoggerHolder.Logger.Info("Validate users");
                Assert.AreEqual(10, users.Count());
                var usedIds = new List<int>();

                foreach (var user in users)
                {
                    Assert.IsTrue(!usedIds.Contains(user.Id));
                    usedIds.Add(user.Id);
                    Assert.IsNotEmpty(user.Name);
                    Assert.IsNotEmpty(user.UserName);
                    Assert.IsNotEmpty(user.Company.Name);
                };
            }
            catch (Exception ex)
            {
                LoggerHolder.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        [Category("Post")]
        public async Task PostUser_ReturnsCreatedStatusCode_WithNotEmpyId()
        {
            try
            {
                LoggerHolder.Logger.Debug($"Run test {nameof(PostUser_ReturnsCreatedStatusCode_WithNotEmpyId)}");
                var request = new RestRequest("users", Method.Post);
                request.AddJsonBody(new { name = "John Doe", username = "JD" });

                LoggerHolder.Logger.Debug($"Request Method: {request.Method}");
                LoggerHolder.Logger.Info($"Call {ClientHolder.Client.BuildUri(request)}");
                RestResponse response = await ClientHolder.Client.ExecuteAsync(request);
                var user = JsonConvert.DeserializeObject<User>(response.Content);

                LoggerHolder.Logger.Info("Validate if this is a Created status code");
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                LoggerHolder.Logger.Info("Validate that the id is not null");
                Assert.IsNotNull(user.Id);
            }
            catch (Exception ex)
            {
                LoggerHolder.Logger.Error(ex);
                throw;
            }
        }
    }
}

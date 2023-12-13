using Assignment_3_API;
using Assignment_3_API.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3_API
{
    [TestFixture]
    internal class TypicodeAPITests : CoreCodes
    {
        [Test]
        [Order(1)]
        public void GetSingleUser()
        {
            test = extent.CreateTest("Get Single User");
            Log.Information("GetSingleUser Test Started");

            var request = new RestRequest("posts/1", Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response :{response.Content}");

                var userdata = JsonConvert.DeserializeObject<UserData>(response.Content);

                Assert.NotNull(userdata);
                Log.Information("User returned");
                Assert.That(userdata.Id, Is.EqualTo("1"));
                Log.Information("Id matches with fetch");
                Assert.That(userdata.UserId, Is.EqualTo("1"));
                Log.Information("user Id matches with fetch");
                Assert.IsNotEmpty(userdata.Title);
                Log.Information("Title is not empty");
                Assert.IsNotEmpty(userdata.Body);
                Log.Information("Body is not empty");
                Log.Information("Get Single User test passed all Asserts");

                test.Pass("GetSingleUser test passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("GetSingleUser test failed");
            }

        }
        [Test]
        [Order(2)]
        public void CreateUser()
        {
            test = extent.CreateTest("Create User");
            Log.Information("CreateUser Test Started");

            var request = new RestRequest("posts", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = "21", id = "45", title = "Sofware Developer" ,body = "Jnr Developing" });
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"API Response :{response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User created and returned");
                Assert.IsNotEmpty(user.Title);
                Log.Information("Title is not empty");
                Assert.IsNotEmpty(user.Body);
                Log.Information("Body is not empty");
                Log.Information("Create User test passed all Asserts");

                test.Pass("CreateUser test passed all Asserts.");

            }
            catch (AssertionException)
            {
                test.Fail("CreateUser test failed");
            }

        }
        [Test]
        [Order(3)]
        public void UpdateUser()
        {
            test = extent.CreateTest("Update User");
            Log.Information("UpdateUser Test Started");

            var request = new RestRequest("posts/2", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = "21", id = "45", title = "Senior Sofware Developer",body="Developing" });
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response :{response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User updated and returned");
                Assert.IsNotEmpty(user.Title);
                Log.Information("Title is not empty");
                Assert.IsNotEmpty(user.Body);
                Log.Information("Body is not empty");
                Log.Information("Update User test passed all Asserts");

                test.Pass("UpdateUser test passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("UpdateUser test failed");
            }

        }
        [Test]
        [Order(4)]
        public void DeleteUser()
        {
            test = extent.CreateTest("Delete User");
            Log.Information("DeleteUser Test Started");

            var request = new RestRequest("posts/2", Method.Delete);

            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("User Deleted");
                Log.Information("Delete User test passed all Asserts");

                test.Pass("DeleteUser test passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("DeleteUser test failed");
            }

        }
        [Test]
        [Order(5)]
        public void GetAllUsers()
        {
            test = extent.CreateTest("GetAllUsers");
            Log.Information("GetAllUsers Test Started");

            var request = new RestRequest("posts", Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response :{response.Content}");

                List<UserData> usersData = JsonConvert.DeserializeObject<List<UserData>>(response.Content);

                Assert.NotNull(usersData);
                Log.Information("Users returned");
                Log.Information("GetAllUsers test passed all Asserts");

                test.Pass("GetAllUsers test passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("GetAllUsers test failed");
            }

        }
    }
}

using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_API
{
    [TestFixture]
    internal class TypicodeAPITests
    {
        private RestClient client;
        private string baseUrl = "https://jsonplaceholder.typicode.com/";

        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }
        [Test]
        [Order(1)]
        public void GetSingleUser()
        {
            var request = new RestRequest("posts/1", Method.Get);
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var userdata = JsonConvert.DeserializeObject<UserData>(response.Content);
            //UserData? user = userdata?.Data;

            Assert.NotNull(userdata);
            Assert.That(userdata.Id, Is.EqualTo("1"));
            Assert.That(userdata.UserId, Is.EqualTo("1"));
            Assert.IsNotEmpty(userdata.Title);
            Assert.IsNotEmpty(userdata.Body);

        }
        [Test]
        [Order(2)]
        public void CreateUser()
        {
            var request = new RestRequest("posts", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = "21", id = "45", title = "Sofware Developer" ,body = "Jnr Developing" });
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));

            var user = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(user);
            Assert.IsNotEmpty(user.Title);
            Assert.IsNotEmpty(user.Body);

        }
        [Test]
        [Order(3)]
        public void UpdateUser()
        {
            var request = new RestRequest("posts/2", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = "21", id = "45", title = "Senior Sofware Developer",body="Developing" });
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var user = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(user);
            Assert.IsNotEmpty(user.Title);
            Assert.IsNotEmpty(user.Body);
        }
        [Test]
        [Order(4)]
        public void DeleteUser()
        {
            var request = new RestRequest("posts/2", Method.Delete);

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

        }
        [Test]
        [Order(5)]
        public void GetAllUsers()
        {
            var request = new RestRequest("posts", Method.Get);
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            List<UserData> usersData = JsonConvert.DeserializeObject<List<UserData>>(response.Content);

            Assert.NotNull(usersData);


        }
    }
}

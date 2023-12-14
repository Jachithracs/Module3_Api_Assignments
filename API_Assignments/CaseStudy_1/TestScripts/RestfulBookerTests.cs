using CaseStudy_1.Models;
using CaseStudy_1.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy_1.TestScripts
{
    [TestFixture]
    internal class RestfulBookerTests : CoreCodes
    {
        [Test,Order(0)]
        public void GetTokenTest()
        {
            test = extent.CreateTest("GetToken Test");
            Log.Information("GetToken Test Started");
            var request = new RestRequest("auth", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });
            try
            {
                var response = client.Execute(request);
                Console.WriteLine(response.Content);
                Assert.That(response.Content, Is.Not.Null, "Response is null");
                Log.Information("Request Intitiated");
                test.Pass("GetToken Test Passed");
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Status code doest match");
                Log.Information("Succees Response Recieved");
                test.Pass("Status code Test Successful");
            }
            catch (AssertionException ex)
            {
                test.Fail("GetToken test failed");
            }
        }
        [Test,Order(1)]
        public void GetAllBookingIdTest()
        {
            test = extent.CreateTest("Get All Booking id test");
            Log.Information("Get All Booking id Test Started");
            var request = new RestRequest("booking", Method.Get);
            try
            {
                var response = client.Execute(request);
                var GetBookingID = JsonConvert.DeserializeObject<List<GetBookingId>>(response.Content);
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Status code is not 200");
                test.Pass("Status code test pass");
                Log.Information("Status code test passed");
                Assert.That(GetBookingID[0].BookingId, Is.Not.Empty, "Booking id is Empty");
                test.Pass("Booking id data test pass");
                Log.Information("Booking id data test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Get All Booking id test failed");
            }
        }
        [Test,Order(2)]
        public void GetSingleBookingIdTest()
        {
            test = extent.CreateTest("Get Single Booking Id");
            Log.Information("GetSingleBookingId Test Started");

            var request = new RestRequest("booking/1", Method.Get);
            request.AddHeader("Accept", "application/json");
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<BookingDetails>(response.Content);

                Assert.NotNull(user);
                Log.Information("User returned");
                Assert.IsNotEmpty(user.FirstName);
                Log.Information("User FirstName matches with fetch");
                Assert.IsNotEmpty(user.LastName);
                Log.Information("User lastName matches with fetch");
                Assert.IsNotEmpty(user.DepositPaid);
                Log.Information("User DepositPaid matches with fetch");


                test.Pass("GetSingleBookingIdTest passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("GetSingleBookingId test failed");
            }
        }

        [Test,Order(3)]
        public void UpdateBookingTest()
        {
            test = extent.CreateTest("GetToken Test");
            Log.Information("GetToken Test Started");
            var request = new RestRequest("auth", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });
            Log.Information("Token Generated");
            try
            {
                var response = client.Execute(request);

                var token = JsonConvert.DeserializeObject<Cookies>(response.Content);
                Log.Information("Update Booking test started");
                var requestput = new RestRequest("booking/13", Method.Put);
                requestput.AddHeader("Content-Type", "application/json");
                requestput.AddHeader("Accept", "application/json");
                requestput.AddHeader("Cookie", "token=" + token.Token);
                Log.Information("At header added cookie with token value");

                requestput.AddJsonBody(new
                {
                    firstname = "John",
                    lastname = "Smith",
                    totalprice = 111,
                    depositpaid = true,
                    bookingdates = new
                    {
                        checkin = "2018-01-01",
                        checkout = "2019-01-01"
                    },
                    additionalneeds = "Breakfast"
                });
                var responseput = client.Execute(requestput);
                Assert.That(responseput.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Status code is not 200");
                Log.Information("Update Booking test passed");
                Console.WriteLine(responseput.Content);
            }
            catch (AssertionException) 
            {
                test.Fail("Update Booking test failed");
            }

        }


        [Test,Order(2)]
        public void CreateBookingTest()
        {
            try
            {

                var requestput = new RestRequest("booking", Method.Post);
                requestput.AddHeader("Content-Type", "application/json");
                requestput.AddHeader("Accept", "application/json");
                ;


                requestput.AddJsonBody(new
                {
                    firstname = "John",
                    lastname = "Smith",
                    totalprice = 111,
                    depositpaid = true,
                    bookingdates = new
                    {
                        checkin = "2018-01-01",
                        checkout = "2019-01-01"
                    },
                    additionalneeds = "Breakfast"
                });
                Log.Information("CreatingBooking Test Started");
                var responseput = client.Execute(requestput);
                var bookingObject = JsonConvert.DeserializeObject<Booking>(responseput.Content);
                var bookingDetailsObject = bookingObject.BookingDetails;
                var BookingDetails = bookingDetailsObject.BookingDates;
                Console.WriteLine(bookingDetailsObject.FirstName);
                Console.WriteLine(responseput.Content);
                Assert.That(responseput.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Status code is not 200");
                Log.Information($"API Response :{responseput.Content}");
                Log.Information("CreatingBooking test passed all Asserts");

                test.Pass("CreatingBooking test passed all Asserts.");

            }
            catch (AssertionException)
            {
                test.Fail("CreatingBooking test failed");
            }
        }
        [Test,Order(4)]
        public void DeleteBookingTest()
        {

            test = extent.CreateTest("Delete Booking test");
            Log.Information("DeleteBooking Test started");
            var requestAuth = new RestRequest("auth", Method.Post);
            requestAuth.AddHeader("Content-Type", "application/json");
            requestAuth.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });
            Log.Information("Token Generated");
            var request = new RestRequest("booking/11", Method.Delete);
            try
            {
                var responseAuth = client.Execute(requestAuth);
                var token = JsonConvert.DeserializeObject<Cookies>(responseAuth.Content);
                request.AddHeader("Cookie", "token=" + token.Token);
                Log.Information("At header added cookie with token value");
                var response = client.Execute(request);
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created), "Status code is not 200");
                test.Pass("Status code test pass");
                Log.Information("Status code test passed");
                test.Pass("Booking id data test pass");
                Log.Information("Booking id data test passed");
                Log.Information("DeleteBooking test passed all Asserts");

                test.Pass("DeleteBooking test passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("DeleteBooking test failed");
            }
        }


    }
}

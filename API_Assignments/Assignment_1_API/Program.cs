using Newtonsoft.Json.Linq;
using RestSharp;

string baseUrl = "https://jsonplaceholder.typicode.com/";
var client = new RestClient(baseUrl);

GetAllUsers(client);
GetSingleUser(client);
CreateUser(client);
UpdateUser(client);
DeleteUser(client);


//GET All Users
static void GetAllUsers(RestClient client)
{
    var getUserRequest = new RestRequest("posts", Method.Get);

    var getUserResponse = client.Execute(getUserRequest);
    Console.WriteLine("GET Response: \n" + getUserResponse.Content);
}
//GET Single User
static void GetSingleUser(RestClient client)
{
    var getUserRequest = new RestRequest("posts/1", Method.Get);

    var getUserResponse = client.Execute(getUserRequest);
    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        //Parse Json response content
        JObject? userJson = JObject.Parse(getUserResponse.Content);

        string? userId = userJson["userId"].ToString();
        string? id = userJson["id"].ToString();
        string? userTitle = userJson["title"].ToString();

        Console.WriteLine($"User Id: {userId} \n Id : {id}\n Title : {userTitle}");
    }
    else
    {
        Console.WriteLine($"Error: {getUserResponse.ErrorMessage}");
    }
}


//POST
static void CreateUser(RestClient client)
{
    var createUserRequest = new RestRequest("posts", Method.Post);
    createUserRequest.AddHeader("Content-Type", "application/json");
    createUserRequest.AddJsonBody(new { userId = "21",id="45", title = "Sofware Developer" });

    var createUserResponse = client.Execute(createUserRequest);
    Console.WriteLine("POST Response",createUserResponse.Content);
}

//PUT
static void UpdateUser(RestClient client)
{
    var updateUserRequest = new RestRequest("posts/2", Method.Put);
    updateUserRequest.AddHeader("Content-Type", "application/json");
    updateUserRequest.AddJsonBody(new { userId = "21",id="45", title = "Senior Sofware Developer" });

    var updateUserResponse = client.Execute(updateUserRequest);
    if (updateUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        //Parse Json response content
        JObject? userJson = JObject.Parse(updateUserResponse.Content);

        string? userId = userJson["userId"].ToString();
        string? id = userJson["id"].ToString();
        string? userTitle = userJson["title"].ToString();

        Console.WriteLine($"User Id: {userId} \n Id : {id}\n Title : {userTitle}");
    }
    else
    {
        Console.WriteLine($"Error: {updateUserResponse.ErrorMessage}");
    }
}

//DELETE
static void DeleteUser(RestClient client)
{
    var deleteUserRequest = new RestRequest("posts/2", Method.Delete);
    var deleteUserResponse = client.Execute(deleteUserRequest);
    if (deleteUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {

        Console.WriteLine("Deleted Successfully");
    }
    else
    {
        Console.WriteLine($"Error: {deleteUserResponse.ErrorMessage}");
    }
}


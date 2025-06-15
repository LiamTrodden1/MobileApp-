using System.Net.Http.Json;

namespace AlbumApp.Services;

public class AuthenticationService
{

    private HttpClient _httpClient;
    public AuthenticationService() 
    { 
        //create a http request client
        _httpClient = new HttpClient();
    }

    public async Task<string> UpdatePassword(string AuthToken, string Password)
    {
        //url to database for changing password, set post message to change password
        string url = "https://identitytoolkit.googleapis.com/v1/accounts:update?key=AIzaSyCmokn22b8UgdDShbNEBkKz61cDq-o_cuI";
        HttpRequestMessage PasswordMessage = new HttpRequestMessage(HttpMethod.Post, url);

        //model to hold token and new password, and make JSON
        var model = new
        {
            idToken = AuthToken,
            password = Password
        };
        PasswordMessage.Content = JsonContent.Create(model);

        HttpResponseMessage response = await _httpClient.SendAsync(PasswordMessage);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return "success";
        }
        else
        {
            return responseContent; // return the error message
        }

        //send response 
        //HttpResponseMessage response = await _httpClient.SendAsync(PasswordMessage);


        //retrieve and return response 
        //string PasswordResponse = await response.Content.ReadAsStringAsync();
        //return PasswordResponse;
    }
}

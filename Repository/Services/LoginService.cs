using System.Text;
using System.Text.Json;
using Repository.Entities;
using Repository.Interfaces;

namespace Repository.Services;

public class LoginService: ILoginService
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly JsonSerializerOptions _options;
    
    public LoginService(string baseUrl)
    {
        _httpClient.BaseAddress = new Uri(baseUrl);
        _httpClient.Timeout = new TimeSpan(0, 0, 30);
        
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    
    public async Task<DTO_Token> Login(DTO__Login form)
    {
        try
        {
            using (_httpClient)
            {
                string json = JsonSerializer.Serialize(form, _options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"/api/Security/signIn", content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<DTO_Token>();
            } 
        }
        catch (Exception e)
        {
            if (e.InnerException != null)
                throw e.InnerException;
           
            throw; 
        }
    }
}
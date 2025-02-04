using System.Text;
using System.Text.Json;
using Repository.Entities;
using Repository.Interfaces;

namespace Repository.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly JsonSerializerOptions _options;
    public UserService(string baseUrl)
    {
        _httpClient.BaseAddress = new Uri(baseUrl);
        _httpClient.Timeout = new TimeSpan(0, 0, 30);
        
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    
    public async Task Create(UserEntity toCreate)
    {
        try
        {
            using (_httpClient)
            {
                string json = JsonSerializer.Serialize(toCreate, _options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"/api/User", content);
                response.EnsureSuccessStatusCode();
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
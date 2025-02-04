using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Repository.Entities;
using Repository.Interfaces;

namespace Repository.Services;

public class TaskService: ITaskRepository
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly JsonSerializerOptions _options;
    public TaskService(string baseUrl)
    {
        _httpClient.BaseAddress = new Uri(baseUrl);
        _httpClient.Timeout = new TimeSpan(0, 0, 30);
        
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }


    public Task<TaskEntity> Get(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TaskEntity>> GetAllTasks(string token)
    {
        try
        {
            using (_httpClient)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await _httpClient.GetAsync("/api/Task");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<List<TaskEntity>>();
            } 
        }
        catch (Exception e)
        {
            if (e.InnerException != null)
                throw e.InnerException;
           
            throw; 
        }
    }

    public async Task Create(TaskEntity toCreate)
    {
        try
        {
            using (_httpClient)
            {
                string json = JsonSerializer.Serialize(toCreate, _options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"/api/Task/{toCreate.Title}", content);
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

    public Task Update(TaskEntity toUpdate)
    {
        throw new NotImplementedException();
    }

    public Task<TaskEntity> GetbyTitle<T>(string title)
    {
        throw new NotImplementedException();
    }

    public async Task ChangeStatus(int id, string status)
    {
        try
        {
            using (_httpClient)
            {
                string json = JsonSerializer.Serialize("toCreate", _options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PatchAsync($"/api/Task/{id}/{status}", content);
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
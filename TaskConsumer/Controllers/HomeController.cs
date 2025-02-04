using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interfaces;
using TaskConsumer.Models;

namespace TaskConsumer.Controllers;

public class HomeController(ILogger<HomeController> logger, ITaskRepository tr,ILoginService ls, IConfiguration _config) : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Login");
    }

    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginForm form)
    {
        DTO_Token token = await ls.Login(new DTO__Login(){Email = form.Email, Password = form.Password});
        HttpContext.Session.SetString("token", token.AccessToken);
        HttpContext.Session.SetString("refresh", token.RefreshToken);
        return RedirectToAction("Tasks");
    }
    
    public IActionResult SignUp()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpForm form)
    {
        if (!ModelState.IsValid)
            return View(form);
           
        
        return RedirectToAction("Login");
    }
    public IActionResult Tasks()
    {
        return View(tr.GetAllTasks(HttpContext.Session.GetString("token")).Result);
    }
    
    public IActionResult CreateTask()
    {
        return View(new TaskCreate());
    }
    
    [HttpPost]
    public IActionResult CreateTask(TaskCreate newTask)
    {
        tr.Create(new TaskEntity() { Title = newTask.Title,  });
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ChangeStatus(int id)
    {
        tr.ChangeStatus(id, "Cloturee");
        return RedirectToAction("Tasks");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
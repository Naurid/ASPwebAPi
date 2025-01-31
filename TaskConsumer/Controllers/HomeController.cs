using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interfaces;
using TaskConsumer.Models;

namespace TaskConsumer.Controllers;

public class HomeController(ILogger<HomeController> logger, ITaskRepository tr) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Tasks()
    {
        return View(tr.GetAllTasks().Result);
    }
    
    public IActionResult CreateTask()
    {
        return View(new TaskCreate());
    }
    
    [HttpPost]
    public IActionResult CreateTask(TaskCreate newTask)
    {
        tr.Create(new TaskEntity() { Title = newTask.Title });
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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WeddingPlanner.Controllers
{
  public class DashboardController : Controller
  {
    private WeddingPlannerContext dbContext; 

    public DashboardController(WeddingPlannerContext context)
    {
      dbContext = context;
    }

    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
      if (userId == null)
        return RedirectToAction("Index", "Home");
      List<Wedding> AllWeddings = dbContext.Weddings
        .Include(w => w.Guests)
        .ToList();
        ViewBag.UserId = userId; 
        return View("Dashboard", AllWeddings);
    }
  }
}

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
    private LoginContext dbContext; 

    public DashboardController(LoginContext context)
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

    [HttpGet("add/wedding")]
    public IActionResult AddWedding()
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
      if (userId == null)
        return RedirectToAction("Index", "Home");

      return View("AddWedding");
    }

    [HttpPost("create/wedding")]
    public IActionResult CreateWedding(Wedding newWedding)
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
      if (userId == null)
        return RedirectToAction("Index", "Home");

      if (ModelState.IsValid)
      {
        newWedding.UserId = (int)userId; 
        dbContext.Add(newWedding);
        dbContext.SaveChanges();
        return RedirectToAction("Dashboard");
      }

      return View("AddWedding");
    }

    [HttpGet("join/wedding/{weddingId}")]
    public IActionResult JoinWedding(int weddingId)
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
      if (userId == null)
        return RedirectToAction("Index", "Home");
      var oneWedding = dbContext.Weddings.FirstOrDefault(w => w.WeddingId == weddingId);

      Association newAssociation = new Association()
      {
        UserId = (int)userId, 
        WeddingId = weddingId
      };

      dbContext.Associations.Add(newAssociation);
      oneWedding.Guests.Add(newAssociation);
      dbContext.SaveChanges();

      return RedirectToAction("Dashboard");
    }

    [HttpGet("leave/wedding/{weddingId}")]
    public IActionResult LeaveWedding(int weddingId)
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
      if (userId == null)
        return RedirectToAction("Index", "Home");
      var oneAssociation = dbContext.Associations
        .FirstOrDefault(a => a.WeddingId == weddingId && a.UserId == userId);

      dbContext.Associations.Remove(oneAssociation);
      dbContext.SaveChanges();

      return RedirectToAction("Dashboard");
    }

    [HttpGet("view/wedding/{weddingId}")]
    public IActionResult ViewWedding(int weddingId)
    {
      Wedding oneWedding = dbContext.Weddings
        .Include(w => w.Guests)
        .ThenInclude(a => a.User)
        .FirstOrDefault(w => w.WeddingId == weddingId);
        return View("ViewWedding", oneWedding);
    }

    [HttpGet("delete/wedding/{weddingId}")]
    public IActionResult DeleteWedding(int weddingId)
    {
      Wedding deletedWedding = dbContext.Weddings.FirstOrDefault(w => w.WeddingId == weddingId);
      dbContext.Weddings.Remove(deletedWedding);
      dbContext.SaveChanges(); 

      return RedirectToAction("Dashboard");
    }
  }
}

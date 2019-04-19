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
  public class LoginController : Controller
  {
    private LoginContext dbContext;

    public LoginController(LoginContext context)
    {
      dbContext = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
      return View("Index");
    }

    [HttpPost("register")]
    public IActionResult Register(User newUser)
    {
      if (ModelState.IsValid)
      {
        var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == newUser.Email);
        if (userInDb != null)
        {
          ModelState.AddModelError("Email", "This email is already taken!");
          return View("Index");
        }
        // User newUser = new User()
        // {
        //   FirstName = form.FirstName, 
        //   LastName = form.LastName,
        //   Password = form.Password, 
        //   Email = form.Email
        // };
        PasswordHasher<User> Hasher = new PasswordHasher<User>();
        newUser.Password = Hasher.HashPassword(newUser, newUser.Password);

        dbContext.Users.Add(newUser);
        dbContext.SaveChanges();
        var userToLogIn = dbContext.Users.FirstOrDefault(u => u.Email == newUser.Email);
        HttpContext.Session.SetInt32("UserId", userToLogIn.UserId);
        return RedirectToAction("Dashboard", "Dashboard");
        // return View("Success");
      }
      else
      {
        return View("Index");
      }
    }

    [HttpPost("login")]
    public IActionResult Login(Login form)
    {
      if (ModelState.IsValid)
      {
        User userInDb = dbContext.Users.FirstOrDefault(u => u.Email == form.LoginEmail);

        if (userInDb is null)
        {
          ModelState.AddModelError("Email", "Invalid Email/Password");
          return View("Index");
        
        }
        else
        {
          //initialize hasher object
          var hasher = new PasswordHasher<Login>();

          //verify password w/ what's stored in db
          var result = hasher.VerifyHashedPassword(form, userInDb.Password, form.LoginPassword);

          if(result == 0)
          {
            ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
            return View("Index");
          }
          else 
          {
            HttpContext.Session.SetInt32("UserId", userInDb.UserId);
            return RedirectToAction("Dashboard", "Dashboard");
          }
        }
      }
      else
      {
        return View("Index");
      }
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return RedirectToAction("Index");
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;


namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            _context = context;
        }
     
        [HttpGet("")]
        public IActionResult Index()
        {

            List<Dish> AllDishes = _context.Dishes.OrderByDescending(u => u.CreatedAt).ToList();
            ViewBag.Dishes = AllDishes;
            
            return View();
        }
        [HttpGet("new")]
        public IActionResult CreateDish()
        {
            return View();
        }
        [HttpPost("/new")]
        public IActionResult AddDish(Dish newDish)
        {
            if(ModelState.IsValid)
            {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
            }
            else
            return View("CreateDish");
        }
        [HttpGet("{dishId}")]
        public IActionResult ViewDish(int dishId)
        {
            Dish DishRecieved = _context.Dishes.FirstOrDefault(a => a.DishId == dishId);
            ViewBag.DishName = DishRecieved.Name;
            ViewBag.DishId = DishRecieved.DishId;
            ViewBag.DishChef = DishRecieved.Chef;
            ViewBag.DishCalories = DishRecieved.Calories;
            ViewBag.DishTastiness = DishRecieved.Tastiness;
            ViewBag.DishDescription = DishRecieved.Description;
            return View();
        }
        [HttpGet("delete/{dishId}")]
        public IActionResult DeleteDish(int dishId)
        {
            Dish RetrievedDish = _context.Dishes.FirstOrDefault(b => b.DishId == dishId);
            _context.Dishes.Remove(RetrievedDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet("edit/{dishId}")]
        public IActionResult EditDish(int dishId)
        {
            Dish RetrievedDish = _context.Dishes.FirstOrDefault(b => b.DishId == dishId);
            ViewBag.DishName = RetrievedDish.Name;
            ViewBag.DishId = RetrievedDish.DishId;
            ViewBag.DishChef = RetrievedDish.Chef;
            ViewBag.DishCalories = RetrievedDish.Calories;
            ViewBag.DishTastiness = RetrievedDish.Tastiness;
            ViewBag.DishDescription = RetrievedDish.Description;
            return View();
        }
        [HttpPost("update/{dishId}")]
        public IActionResult UpdateUser(Dish updatedDish, int dishId)
        {
            Dish UpdatedDish = _context.Dishes.FirstOrDefault(b => b.DishId == dishId);
            if(ModelState.IsValid)
            {
                UpdatedDish.Name = updatedDish.Name;
                UpdatedDish.Chef = updatedDish.Chef;
                UpdatedDish.Tastiness = updatedDish.Tastiness;
                UpdatedDish.Calories = updatedDish.Calories;
                UpdatedDish.Description = updatedDish.Description;
                UpdatedDish.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else return View("EditDish");
        }
    

    }

}

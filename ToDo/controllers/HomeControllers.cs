using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;

namespace ToDoList.Controllers
{
  public class HomeController : Controller
  {

  [HttpGet("/items")]
  public ActionResult Index()
  {
    List<Item> allItems = Item.GetAll();
    return View(allItems);
  }

  [HttpGet("/items/new")]
  public ActionResult CreateForm();

  {

    return View();
  }

  [HttpPost("/items")]
  public ActionResult Create()
  {
    Item newItem = new Item (Request.Form["new-item"]);
    newItem.Save();
    List<Item> allItems = Item.GetAll();
    return RedirectToAction("Index");
  }

  [HttpGet("/items/{id}")]
  public ActionResult Details(int id)
  {
    Item item = Item.Find(id);
    return View(item);
  }


  [HttpGet("/items/{id}/update")]
  public ActionResult Update(int id)
  {
    Item thisItem = Item.Find(id);
    thisItem.Edit(Request.Form["newname"]);
    return RedirectToAction("Index");
  }

  [HttpPost("/items/delete")]
  public ActionResult DeleteAll()
  {
    Item.ClearAll();
    return View();
  }

}
}

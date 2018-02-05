using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;

namespace ToDoList.Controllers
{
  public class HomeController : Controller
  {
    [Route("/")]
    public ActionResult Index()
    {
      return View();
    }

    [Route("/item/list")]
    public ActionResult ItemList()
    {
      List<string> allItems = Item.GetAll();
      return View(allItems);
    }
    [HttpPost("/item/list/clear")]
    public ActionResult ItemListClear()
    {
      Item.ClearAll();
      return View();
    }
    [HttpPost("/item/create")]
    public ActionResult CreateItem()
    {
      Item newItem = new Item (Request.Form["new-item"]);
      newItem.Save();
      return View(newItem);
    }
  }
}

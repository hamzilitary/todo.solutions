using System.Collections.Generic;
using System;

namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    private static List<string> _instances = new List<string> {};

    public Item (string description)
    {
      _description = description;
    }
    public string GetDescription()
    {
      return _description;
    }
    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }
    public static List<string> GetAll()
    {
      return _instances;
    }
    public void Save()
    {
      _instances.Add(_description);
    }
    public static void ClearAll()
    {
      _instances.Clear();
    }
  }
}

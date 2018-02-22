using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ToDoList;
using System;

namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    private int _id;
    private static List<string> _instances = new List<string> {};

    // public Item(string description)
    // {
    //   _description = Description;
    // }
    public Item (string Description, int Id = 0)
    {
      _description = Description;
      _id = Id;
    }
    // public void Save()
    // {
    //
    // }
    public string GetDescription()
    {
      return _description;
    }
    public void SetDescription(string newDescription)
    {
      _description = newDescription;

    }
    // public static List<string> GetAll()
    // {
    //   return _instances;
    // }

    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherItem;
        bool descriptionEquality = (this.GetDescription() == newItem.GetDescription());
        return (descriptionEquality);
      }
    }
    // public void Save()
    // {
    //   _instances.Add(_description);
    // }
    public void Edit(string newDescription)
    {
      MySqlConnection conn = DB.connection();
      conn.Open();
      var cmd = CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE items SET description = @newDescription WHERE id = @searchid;";

      MySqlParameter searchid = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter description = new MySqlParameter();
      description.Parameter = "@newDescription";
      description.Value = newDescription;
      cmd.Parameters.Add(description);

      cmd.ExecuteNonQuery();
      _description = newDescription;

      conn.Close();
      if (conn ! = null)
      {
        conn.Dispose();
      }



    }
    public static void ClearAll()
    {
      _instances.Clear();
    }

    public static List<Item> GetAll()
       {
           List<Item> allItems = new List<Item> {};
           MySqlConnection conn = DB.Connection();
           conn.Open();
           MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT * FROM item;";
           MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
           while(rdr.Read())
           {
             int itemId = rdr.GetInt32(0);
             string itemDescription = rdr.GetString(1);
             Item newItem = new Item(itemDescription, itemId);
             allItems.Add(newItem);
           }
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
           return allItems;
       }


    //
    public void Save()
     {
         MySqlConnection conn = DB.Connection();
         conn.Open();

         var cmd = conn.CreateCommand() as MySqlCommand;
         cmd.CommandText = @"INSERT INTO items (description) VALUES (@ItemDescription);";

         MySqlParameter description = new MySqlParameter();
         description.ParameterName = "@ItemDescription";
         description.Value = _description;
         cmd.Parameters.Add(description);

         cmd.ExecuteNonQuery();
         _id = (int) cmd.LastInsertedId;  // Notice the slight update to this line of code!

          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
     }
    //
     public static void DeleteAll()
      {
          MySqlConnection conn = DB.Connection();
          conn.Open();

          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"DELETE FROM item;";

          cmd.ExecuteNonQuery();

          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
     }
    //
    public static Item Find(int id)
       {
           MySqlConnection conn = DB.Connection();
           conn.Open();

           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT * FROM `items` WHERE id = @thisId;";

           MySqlParameter thisId = new MySqlParameter();
           thisId.ParameterName = "@thisId";
           thisId.Value = id;
           cmd.Parameters.Add(thisId);

           var rdr = cmd.ExecuteReader() as MySqlDataReader;

           int itemId = 0;
           string itemDescription = "";

           while (rdr.Read())
           {
               itemId = rdr.GetInt32(0);
               itemDescription = rdr.GetString(1);
           }

           Item foundItem= new Item(itemDescription, itemId);  // This line is new!

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

           return foundItem;  // This line is new!

       }

  }
}

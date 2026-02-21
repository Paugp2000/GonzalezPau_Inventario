using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.SceneManagement;

public class DatabaseSaver : MonoBehaviour
{
    public DatabaseInicializer DatabaseInicializer;
    private int idUsuarioInvetario;
    private static Inventario inventarioActual;
    private IDbConnection dbConnection3, dbConnection4;
    public LoadInventory loader;
 
    private void Awake()
    {
        idUsuarioInvetario = DatabaseInicializer.idUsuarioInventario;
    }
    private void Start()
    {
        dbConnection3 = new SqliteConnection(DBCommons.dbUri);
    }

    public void AddItemToDatabase(Item itemAñadido)
    {
        dbConnection3.Open();
        IDbCommand cmdAdd = dbConnection3.CreateCommand();
        cmdAdd.CommandText = "INSERT INTO Objeto (idObjeto, nombreItem, tipo, numeroDeDropZone, poder) " +
                             "VALUES (@idItem, @nombreItem, \"equipable\", @numDeDropZone, @poder);";
        cmdAdd.Parameters.Add(new SqliteParameter("@idItem", itemAñadido.id_item));
        cmdAdd.Parameters.Add(new SqliteParameter("@nombreItem", itemAñadido.nombre));
        cmdAdd.Parameters.Add(new SqliteParameter("@poder", itemAñadido.fuerza));
        cmdAdd.Parameters.Add(new SqliteParameter("@numDeDropZone", itemAñadido.dropZoneGuardado));

        IDbCommand cmdAdd2 = dbConnection3.CreateCommand();
        cmdAdd2.Parameters.Add(new SqliteParameter("@idItem", itemAñadido.id_item));
        cmdAdd2.CommandText = "INSERT INTO InventarioObjeto (idObjeto, idInventario, isInInventory, cantidad) " +
                              "VALUES (@idItem, " + idUsuarioInvetario + ", TRUE, 1)";
        

        try
        {
            cmdAdd.ExecuteNonQuery();
            cmdAdd2.ExecuteNonQuery();
        }
        catch
        {
            Debug.LogError("Item no añadido correctamente");
        }
        dbConnection3.Close();
    }
    public void RemoveItemFromDatabase(Item itemABorrar)
    {
        dbConnection3.Open();
        IDbCommand cmdRemove = dbConnection3.CreateCommand();
        cmdRemove.Parameters.Add(new SqliteParameter("@idItemRemove", itemABorrar.id_item));
        cmdRemove.CommandText = "DELETE FROM Objeto WHERE idObjeto = @idItemRemove;";
        IDbCommand cmdRemove2 = dbConnection3.CreateCommand();
        cmdRemove2.Parameters.Add(new SqliteParameter("@idItemRemove", itemABorrar.id_item));
        cmdRemove2.CommandText = "UPDATE InventarioObjeto SET isInInventory = FALSE WHERE idObjeto = @idItemRemove;";
        try
        {
            cmdRemove.ExecuteNonQuery();
            cmdRemove2.ExecuteNonQuery();
        }
        catch
        {
            Debug.LogError("Item no eliminado correctamente");
        }
        dbConnection3.Close();
    }
    public Inventario loadInvenario()
    {
        if (inventarioActual == null)
        {
            inventarioActual = new Inventario(DatabaseInicializer.idUsuarioInventario);
            inventarioActual.items = new List<Item>();
        }
        else
        {
            inventarioActual = devolverInventario();
        }
        

        dbConnection4 = new SqliteConnection(DBCommons.dbUri);
        dbConnection4.Open();

        IDbCommand cmdLoad = dbConnection4.CreateCommand();
        cmdLoad.CommandText =
            "SELECT Objeto.idObjeto " +
            "FROM Objeto " +
            "JOIN InventarioObjeto ON Objeto.idObjeto = InventarioObjeto.idObjeto " +
            "WHERE InventarioObjeto.idInventario = @idUsuario " +
            "AND InventarioObjeto.isInInventory = TRUE;";
       
        cmdLoad.Parameters.Add(new SqliteParameter("@idUsuario", DatabaseInicializer.idUsuarioInventario));

        using (IDataReader reader = cmdLoad.ExecuteReader())
        {
            while (reader.Read())
            {
                GameObject itemAñadir = new GameObject();
                itemAñadir.AddComponent<Item>();
                itemAñadir.GetComponent<Item>().id_item = reader.GetInt32(0);
                Debug.Log(reader.GetInt32(0));
                inventarioActual.items.Add(itemAñadir.GetComponent<Item>());

                IDbCommand cmdLoadDrop = dbConnection4.CreateCommand();
                cmdLoadDrop.CommandText =
                    "SELECT Objeto.numeroDeDropZone FROM Objeto" +
                    " JOIN InventarioObjeto ON Objeto.idObjeto = InventarioObjeto.idObjeto " +
                    " WHERE Objeto.idObjeto = " + itemAñadir.GetComponent<Item>().id_item + ";";
                using (IDataReader reader2 = cmdLoadDrop.ExecuteReader())
                {
                    itemAñadir.GetComponent<Item>().dropZoneGuardado = reader.GetInt32(0);
                }
            }
        }
    
            
        dbConnection4.Close();
        return inventarioActual;
    }

    public List<Item> loadItems()
    {
       return inventarioActual.items;
    }
    public Inventario devolverInventario()
    {
        return inventarioActual;
    }
    public void Salir()
    {
        SceneManager.LoadScene("LoginScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;

public class DatabaseSaver : MonoBehaviour
{
    public DatabaseInicializer DatabaseInicializer;
    private int idUsuarioInvetario;
    private string dbUriInventory;
    private IDbConnection dbConnection3, dbConnection4;
    public LoadInventory loader;
    private void Awake()
    {
        idUsuarioInvetario = DatabaseInicializer.idUsuarioInventario;
        dbUriInventory = "URI=file:" + Application.dataPath + "/MyDatabase.sqlite";
    }
    private void Start()
    {
        dbConnection3 = new SqliteConnection(dbUriInventory);
    }

    public void AddItemToDatabase(Item itemAñadido)
    {
        dbConnection3.Open();
        IDbCommand cmdAdd = dbConnection3.CreateCommand();
        cmdAdd.CommandText = "INSERT INTO Objeto (idObjeto, nombreItem, tipo, fuerzaItem) " +
                             "VAULES (@idItem, @nombreItem, equipable, @fuerzaItem);";
        cmdAdd.Parameters.Add(new SqliteParameter("@idItem", itemAñadido.id_item));
        cmdAdd.Parameters.Add(new SqliteParameter("@nombreItem", itemAñadido.nombre));
        cmdAdd.Parameters.Add(new SqliteParameter("@fuerzaItem", itemAñadido.fuerza));
        try
        {
            cmdAdd.ExecuteNonQuery();
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
        cmdRemove.CommandText = "DELETE FROM Items WHERE idObjeto = @idItemRemove);";
        cmdRemove.Parameters.Add(new SqliteParameter("@idItemRemove", itemABorrar.id_item));
        try
        {
            cmdRemove.ExecuteNonQuery();
        }
        catch
        {
            Debug.LogError("Item no eliminado correctamente");
        }
        dbConnection3.Close();
    }
    public Inventario loadInvenario()
    {
        Inventario inventarioActual = new Inventario(DatabaseInicializer.idUsuarioInventario);

        dbConnection4 = new SqliteConnection(dbUriInventory);
        for (int i = 0; i < 9; i++) 
        {
            try
            {
                IDbCommand cmdLoad = dbConnection4.CreateCommand();
                cmdLoad.CommandText = "SELECT idObjeto FROM Objetos, InventarioObjeto WHERE idObjeto = @param AND InventarioObjeto.isInInventory = TRUE;";
                cmdLoad.Parameters.Add(new SqliteParameter("@param", i));
                using (IDataReader reader = cmdLoad.ExecuteReader())
                {
                    inventarioActual.items.Add(loader.allItems[int.Parse(reader.GetString(0))]);
                }
            }
            catch
            {
                Debug.Log("Item no disponible en el inventario");
            }
        }
        return inventarioActual;    

    }
}

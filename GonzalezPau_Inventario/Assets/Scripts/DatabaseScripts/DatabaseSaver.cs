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
        cmdAdd.CommandText = "INSERT INTO Objeto (idObjeto, nombreItem, tipo, poder) " +
                             "VALUES (@idItem, @nombreItem, \"equipable\", @poder);";
        cmdAdd.Parameters.Add(new SqliteParameter("@idItem", itemAñadido.id_item));
        cmdAdd.Parameters.Add(new SqliteParameter("@nombreItem", itemAñadido.nombre));
        cmdAdd.Parameters.Add(new SqliteParameter("@poder", itemAñadido.fuerza));

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
        Inventario inventarioActual = new Inventario(DatabaseInicializer.idUsuarioInventario);

        dbConnection4 = new SqliteConnection(DBCommons.dbUri);

        dbConnection4.Open();
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
        dbConnection4.Close();

        return inventarioActual;

    }
}

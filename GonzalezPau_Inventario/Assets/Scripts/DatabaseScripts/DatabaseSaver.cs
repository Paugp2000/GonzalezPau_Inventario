using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using UnityEngine;
using Mono.Data.Sqlite;

public class DatabaseSaver : MonoBehaviour
{
    public DatabaseInicializer DatabaseInicializer;
    private int idUsuarioInvetario;
    private string databasePath;
    private IDbConnection dbConnection3;
    private void Awake()
    {
        idUsuarioInvetario = DatabaseInicializer.idUsuarioInventario;
        databasePath = "URI=file:" + Application.dataPath + "/Inventory" + idUsuarioInvetario + ".sqlite";
    }
    private void Start()
    {
        dbConnection3 = new SqliteConnection(databasePath);
    }

    public void AddItemToDatabase(Item itemAñadido)
    {
        dbConnection3.Open();
        IDbCommand cmdAdd = dbConnection3.CreateCommand();
        cmdAdd.CommandText = "INSERT INTO Items (idItem, nombreItem, isInInventory, fuerzaItem) " +
                             "VAULES (@idItem, @nombreItem, TRUE, @fuerzaItem);";
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
        cmdRemove.CommandText = "DELETE FROM Items WHERE idItem = @idItemRemove);";
        cmdRemove.Parameters.Add(new SqliteParameter("@idItemRemove", itemABorrar.id_item));
    }
}

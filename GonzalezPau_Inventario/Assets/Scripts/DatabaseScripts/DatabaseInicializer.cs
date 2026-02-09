using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity.VisualScripting.Dependencies.Sqlite;
using System.Data;
using System;
using Mono.Data.Sqlite;
using System.Data.Common;

public class DatabaseInicializer : MonoBehaviour
{
    public int idUsuarioInventario;
    private IDbConnection dbConnection2;
    private string dbUriInventory, databasePath;
    public LoadInventory loading;
    private Inventario inventarioBase;
    private void Awake()
    {
        idUsuarioInventario = LoginSQLController.idUsuario;
        databasePath = Application.dataPath + "/Inventory" + idUsuarioInventario + ".sqlite";
        dbUriInventory = "URI=file:"+Application.dataPath + "/Inventory" + idUsuarioInventario+ ".sqlite";
    }
    public void Start()
    {
        if (!File.Exists(databasePath))
        {
            File.Create(databasePath);
            dbConnection2 = new SqliteConnection(dbUriInventory);
            inventarioBase = new Inventario(idUsuarioInventario);
            CreateDatabase();
            loading.loadEmpthyInventary(inventarioBase);
        }
        else
        {
            loading.LoadIfDatabaseExists();
        }
    }
    public void CreateDatabase()
    {
        dbConnection2 = new SqliteConnection(dbUriInventory);
        dbConnection2.Open();
        IDbCommand cmdInven = dbConnection2.CreateCommand();
        cmdInven.CommandText = "CREATE TABLE IF NOT EXISTS Inventario (" +
                          "idUsuario INTEGER PRIMARY KEY, " +
                          "nombreUsuario TEXT NOT NULL UNIQUE);";
        cmdInven.ExecuteNonQuery();
        IDbCommand cmdItems = dbConnection2.CreateCommand();
        cmdItems.CommandText = "CREATE TABLE IF NOT EXISTS Items (" +
                          "idItem INTERGER PRIMARY KEY,"+
                          "nombreItem TEXT NOT NULL UNIQUE, "+
                          "isInInventory BOOLEAN, " +
                          "fuerzaItem INTEGER, "+
                          "FOREIGN KEY (idUsuario) REFERENCES Inventario.idUsuario);";
        cmdItems.ExecuteNonQuery();
        IDbCommand cmdAcumItems = dbConnection2.CreateCommand();
        cmdAcumItems.CommandText = "CREATE TABLE IF NOT EXISTS AcuItems (" +
                          "idItem INTERGER PRIMARY KEY," +
                          "nombreItem TEXT NOT NULL UNIQUE, " +
                          "cantidadEquip INTEGER, "+
                          "fuerzaQueOtorga INTEGER, "+
                          "FOREIGN KEY (idUsuario) REFERENCES Inventario.idUsuario);";
        cmdAcumItems.ExecuteNonQuery();
        dbConnection2.Close();
    }
}

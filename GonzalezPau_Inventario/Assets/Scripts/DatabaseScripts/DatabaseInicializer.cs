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
        dbUriInventory = "URI=file:" + Application.dataPath + "/MyDatabase.sqlite";
    }
    public void Start()
    {
            dbConnection2 = new SqliteConnection(dbUriInventory);
            inventarioBase = new Inventario(idUsuarioInventario);
            dbConnection2.Open();
            CreateDatabase();
            loading.loadEmpthyInventary(inventarioBase);
    }
    public void CreateDatabase()
    {
        dbConnection2 = new SqliteConnection(dbUriInventory);
        dbConnection2.Open();
        IDbCommand cmdInven = dbConnection2.CreateCommand();
        cmdInven.CommandText = "CREATE TABLE IF NOT EXISTS Inventario (" +
                          "FOREIGN KEY (idUsuario) REFERENCES Users.idUsuario);";
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

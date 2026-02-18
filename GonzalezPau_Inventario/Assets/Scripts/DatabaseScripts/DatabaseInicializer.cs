using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity.VisualScripting.Dependencies.Sqlite;
using System.Data;
using System;
using Mono.Data.Sqlite;
using System.Data.Common;
using UnityEngine.WSA;

public class DatabaseInicializer : MonoBehaviour
{
    public int idUsuarioInventario;
    private IDbConnection dbConnection2;
    public LoadInventory loading;
    private Inventario inventarioBase;
    private void Awake()
    {
        idUsuarioInventario = LoginSQLController.idUsuario;
    }
    public void Start()
    {
        dbConnection2 = new SqliteConnection(DBCommons.dbUri);
        inventarioBase = new Inventario(idUsuarioInventario);
        dbConnection2.Open();
        CreateDatabase();
        loading.LoadIfDatabaseExists();
    }


    public void CreateDatabase()
    {
        dbConnection2 = new SqliteConnection(DBCommons.dbUri);
        dbConnection2.Open();
        IDbCommand cmdInven = dbConnection2.CreateCommand();
        cmdInven.CommandText = "CREATE TABLE IF NOT EXISTS Inventario (" +
                          "idInventario INT PRIMARY KEY," +
                          "idUsuario INT," +
                          "FOREIGN KEY (idUsuario) REFERENCES Users(idUsuario));";
        cmdInven.ExecuteNonQuery();
        IDbCommand cmdAcumItems = dbConnection2.CreateCommand();
        cmdAcumItems.CommandText = "CREATE TABLE IF NOT EXISTS Objeto (" +
                          "idObjeto INT PRIMARY KEY," +
                          "nombreItem TEXT NOT NULL UNIQUE, " +
                          "tipo TEXT CHECK(tipo IN ('equipable','acumulable')) NOT NULL, " +
                          "poder INTEGER);";
        cmdAcumItems.ExecuteNonQuery();
        IDbCommand cmdItems = dbConnection2.CreateCommand();
        cmdItems.CommandText = "CREATE TABLE IF NOT EXISTS InventarioObjeto (" +
                          "idInventario INT," +
                          "idObjeto INT," +
                          "isInInventory BOOLEAN, " +
                          "cantidad INTEGER, " +
                          "PRIMARY KEY (idInventario, idObjeto)," +
                          "FOREIGN KEY (idInventario) REFERENCES Inventario(idInventario)," +
                          "FOREIGN KEY (idObjeto) REFERENCES Objeto(idObjeto));";
        cmdItems.ExecuteNonQuery();
        dbConnection2.Close();
    }
}

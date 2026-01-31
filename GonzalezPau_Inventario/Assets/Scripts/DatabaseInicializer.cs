using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity.VisualScripting.Dependencies.Sqlite;
using System.Data;
using System;
using Mono.Data.Sqlite;

public class DatabaseInicializer : MonoBehaviour
{
    public int idUsuarioInventario;
    private IDbConnection dbConnection2;
    private string dbUriInventory;
    private void Awake()
    {
        idUsuarioInventario = LoginSQLController.idUsuario;
        dbUriInventory = "URI=file:" + Application.dataPath + "/Inventory" + idUsuarioInventario+ ".sqlite";
        dbConnection2 = new SqliteConnection(dbUriInventory);
    }
}

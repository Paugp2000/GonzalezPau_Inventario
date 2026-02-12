using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSQLController : MonoBehaviour
{
    private string dbUri;
    private IDbConnection dbConnection;
    public static int idUsuario;
    [SerializeField] TMP_InputField nombreUsuario;
    [SerializeField] TMP_InputField contraseña;
    [SerializeField] GameObject panelErrorUsuario;
    [SerializeField] GameObject panelErrorContraseña;
    [SerializeField] GameObject panelErrorConPequeña;

    private void Start()
    {
        dbUri = "URI=file:" + Application.dataPath + "/MyDatabase.sqlite";
        dbConnection = new SqliteConnection(dbUri);
        createDatabase();
    }

    public void createDatabase()
    {
        dbConnection.Open();
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText =
            "CREATE TABLE IF NOT EXISTS Registro (" +
            "UserID INTEGER PRIMARY KEY AUTOINCREMENT, " +
            "Username TEXT NOT NULL UNIQUE, " +
            "Password TEXT NOT NULL CHECK (LENGTH(Password) >= 8));";
        cmd.ExecuteNonQuery();
        dbConnection.Close();
    }

    public void saveUserNameAndPasswordIntoDatabase()
    {
        if (string.IsNullOrEmpty(nombreUsuario.text))
        {
            panelErrorUsuario.SetActive(true);
            return;
        }

        if (string.IsNullOrEmpty(contraseña.text))
        {
            panelErrorContraseña.SetActive(true);
            return;
        }

        if (contraseña.text.Length < 8)
        {
            panelErrorConPequeña.SetActive(true);
            return;
        }

        dbConnection.Open();
        using (IDbCommand cmd = dbConnection.CreateCommand())
        {
            cmd.CommandText = "INSERT INTO Registro (Username, Password) VALUES (@user, @pass)";
            cmd.Parameters.Add(new SqliteParameter("@user", nombreUsuario.text));
            cmd.Parameters.Add(new SqliteParameter("@pass", contraseña.text));

            try
            {
                cmd.ExecuteNonQuery();
                Debug.Log("Registration successful");
            }
            catch
            {
                panelErrorUsuario.SetActive(true); // usuario duplicado
            }
        }
        dbConnection.Close();
    }

    public void Login()
    {
        dbConnection.Open();
        string userNameBetweenDots = "\"" + nombreUsuario.text + "\"";

        using (IDbCommand cmd = dbConnection.CreateCommand())
        {
            cmd.CommandText = "SELECT password FROM Registro WHERE Username = " + userNameBetweenDots + ";";
            //cmd.Parameters.Add(new SqliteParameter("@user", nombreUsuario.text));

            using (IDataReader reader = cmd.ExecuteReader())
            {
                if (!reader.Read())
                {
                    showCanvasErrorUsuario();
                    dbConnection.Close();
                    return;
                }
                string storedPass = reader.GetString(0);

                if (storedPass == contraseña.text)
                {
                    using (IDbCommand cmr = dbConnection.CreateCommand())
                    {
                        cmr.CommandText = "SELECT UserID FROM Registro WHERE Username = @User";
                        cmr.Parameters.Add(new SqliteParameter("@user", nombreUsuario.text));

                        IDataReader reader2 = cmr.ExecuteReader();
                        while (reader2.Read())
                        {
                            Debug.Log(reader2.GetInt32(0));
                            idUsuario = reader2.GetInt32(0);
                        }

                    }
                    SceneManager.LoadScene("InventoryScene");
                }
                else
                {
                    showCanvasErrorContraseña();
                }
            }
        }
        dbConnection.Close();
    }

    private void showCanvasErrorUsuario() => panelErrorUsuario.SetActive(true);
    private void showCanvasErrorContraseña() => panelErrorContraseña.SetActive(true);

    public void DesactivarPanelError(GameObject panel) => panel.SetActive(false);

    public void SalirDelPrograma() => Application.Quit();
}
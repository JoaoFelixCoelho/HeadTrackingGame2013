using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {
	
	public string serverIP = "127.0.0.1";
    private string database = "unity";
    private string UserDataBase = "root";
    private string PassDataBase = "root";
	
	string user, puntaje;
	
	public class MySQLCS : MonoBehaviour {
		

	}
	
	/*void score() {
		
        MySqlConnection cn = new MySqlConnection("Server=" + serverIP + ";Database=" + database + ";User ID=" + UserDataBase + ";Password=" + PassDataBase + ";Pooling=false");
        cn.Open();
        Debug.Log("Conexion con la base de datos correcta");
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM cuentas WHERE user = '" + user + "';");
        cmd.Connection = cn;
        MySqlDataReader reader = cmd.ExecuteReader();
		

	}
	
	// Use this for initialization
	void Start () {
		score();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	
	// Insert new entries into the table
	void Insert()
	{
	prepData();
	string query = string.Empty;
	query = "INSERT INTO cuentas (numbre, puntaje) VALUES (?nombre, ?puntaje)";
	if (con.State != ConnectionState.Open)
	con.Open();
	using (con){
		foreach (data itm in _GameItems){
			using (cmd = new MySqlCommand(query, con)){
				MySqlParameter oParam = cmd.Parameters.Add("?ID", MySqlDbType.VarChar);
				oParam.Value = itm.ID;
				MySqlParameter oParam1 = cmd.Parameters.Add("?Name", MySqlDbType.VarChar);
				oParam1.Value = itm.Name;
			}
		}
	}
*/	
	
	
}

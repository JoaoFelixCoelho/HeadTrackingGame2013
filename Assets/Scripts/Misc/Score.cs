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
	private MySqlConnection cn;
	
	string user, puntaje;
	
	public struct data
	{
		public int puntaje;
		public string nombre;
	}
	
	public void con() {
        cn = new MySqlConnection("Server=" + serverIP + ";Database=" + database + ";User ID=" + UserDataBase + ";Password=" + PassDataBase + ";Pooling=false");
        cn.Open();
        Debug.Log("Conexion con la base de datos correcta");
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM cuentas WHERE nombre = '" + user + "';", cn);
        cmd.Connection = cn;
        MySqlDataReader reader = cmd.ExecuteReader();

	}
	
	// Use this for initialization
	void Start () {
		con();
	} 
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Insert new entries into the table
	void Insert(){
		int puntaje = PlayerBehave.score;
		string nombre = string.Empty;		
		string query = string.Empty;
		
		query = "INSERT INTO cuentas (nombre, puntaje) VALUES (?nombre, ?puntaje)";
		if (cn.State != ConnectionState.Open)
			cn.Open();
		MySqlCommand cmd = new MySqlCommand(query, cn);
		MySqlParameter oParam = cmd.Parameters.Add("?nombre", MySqlDbType.VarChar);
		oParam.Value = nombre;
		MySqlParameter oParam1 = cmd.Parameters.Add("?puntaje", MySqlDbType.Int32);
		oParam1.Value = puntaje;
		
		cmd.ExecuteNonQuery();
		
	}	
	
}

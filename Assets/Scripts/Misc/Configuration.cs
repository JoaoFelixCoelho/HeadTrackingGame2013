using UnityEngine;
using System.Collections;
using System.IO;

public static class Configuration{
	
	private static string [] configLines;
	public static int cameraWiiMote  = 1;
	public static int pointerWiiMote = 0;
	public static string difficulty = "easy";

	public static void readConfig() {
		string cfg  = ((TextAsset) Resources.Load("Data/settings")).text;	
		cfg = cfg.Replace(" ",string.Empty);
		configLines = cfg.Split(';');
		
		for (int i=0; i<configLines.Length; i++) {
			string [] lineAttr = configLines[i].ToString().Split('=');
			if (lineAttr[0].ToString() == "pointerWiiMote") {
				pointerWiiMote = int.Parse(lineAttr[1].ToString());	
			}
			if (lineAttr[0].ToString() == "cameraWiiMote") {
				cameraWiiMote = int.Parse(lineAttr[1].ToString());	
			}			
			
		}				
		
	}	
	
	
	public static void saveConfig() {
		string oldCont = ((TextAsset) Resources.Load("Data/settings")).text;
		string [] lines = oldCont.Split(';');

		lines[0] = lines[0].ToString().Replace(" ",string.Empty);
		string [] columnas = lines[0].Split('=');
		columnas[1] = pointerWiiMote.ToString();
		Debug.Log(columnas.ToString());
		
		/*lines[1] = lines[1].ToString().Replace(" ",string.Empty);
		columnas = lines[1].Split('=');
		columnas[1] = cameraWiiMote.ToString();
	
	
		lines[2] = lines[2].ToString().Replace(" ",string.Empty);
		columnas = lines[2].Split('=');
		columnas[1] = difficulty.ToString();*/
		
		//for (int i=0; i<lines.Length; i++) {
			Debug.Log(lines[0].ToString() + "pointer" + pointerWiiMote);	
		//}
		
		
			
		
	}
	

}

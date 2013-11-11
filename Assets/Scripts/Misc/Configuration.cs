using UnityEngine;
using System.Collections;
using System.IO;

public static class Configuration{
	
	private static string [] configLines;
	public static int cameraWiiMote  = 1;
	public static int pointerWiiMote = 0;

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
	
	

}

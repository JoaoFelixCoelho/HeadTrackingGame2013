using UnityEngine;
using System.Collections;
using System.Xml;

public static class Stats {
	
	static XmlDocument xmlAsset = new XmlDocument();
	
	
	
	public static void read() {
		readXML();
		Debug.Log("Reading EnemyData.xml");
	}	

	private static void readXML() {
			TextAsset temporal = (TextAsset) Resources.Load("data/enemyData");
			xmlAsset.LoadXml(temporal.text);
			ZombieStats.read(xmlAsset.ChildNodes[1]["zombie"]);	
			FlubberStats.read(xmlAsset.ChildNodes[1]["flubber"]);
			SpiderStats.read(xmlAsset.ChildNodes[1]["spider"]);
			GhostStats.read(xmlAsset.ChildNodes[1]["ghost"]);
	}
	
	
	
	
	public static class ZombieStats {
		public static int baseHP;
		public static string attType;
		public static int damage;
		public static float moveSpeed;
		
		
		public static void read (XmlNode nodo) {
			baseHP = int.Parse(nodo["baseHP"].InnerText);
			attType = nodo["attackType"].InnerText;
			damage = int.Parse(nodo["damage"].InnerText);
			moveSpeed = float.Parse(nodo["moveSpeed"].InnerText);

		}
		
	}
	
	
	public static class FlubberStats {
		public static int baseHP;
		public static string attType;
		public static int damage;
		public static float moveSpeed;
		
		
		public static void read (XmlNode nodo) {
			baseHP = int.Parse(nodo["baseHP"].InnerText);
			attType = nodo["attackType"].InnerText;
			damage = int.Parse(nodo["damage"].InnerText);
			moveSpeed = float.Parse(nodo["moveSpeed"].InnerText);			
		}
		
	}
	
	
	public static class SpiderStats {
		public static int baseHP;
		public static string attType;
		public static int damage;
		public static float moveSpeed;
		
		
		public static void read (XmlNode nodo) {
			baseHP = int.Parse(nodo["baseHP"].InnerText);
			attType = nodo["attackType"].InnerText;
			damage = int.Parse(nodo["damage"].InnerText);
			moveSpeed = float.Parse(nodo["moveSpeed"].InnerText);			
		}
		
	}
	
	
	public static class GhostStats {
		public static int baseHP;
		public static string attType;
		public static int damage;
		public static float moveSpeed;
		
		
		public static void read (XmlNode nodo) {
			baseHP = int.Parse(nodo["baseHP"].InnerText);
			attType = nodo["attackType"].InnerText;
			damage = int.Parse(nodo["damage"].InnerText);
			moveSpeed = float.Parse(nodo["moveSpeed"].InnerText);			
		}
		
	}
	
	
}

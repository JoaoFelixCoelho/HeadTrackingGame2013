using UnityEngine;
using System.Collections;
using System.Xml;

public class Stats {
	
	public int baseHP;
	public string attType;
	public int damage;
	public float moveSpeed;	
	public float interval;
	public GameObject prefab;
	
	static XmlDocument xmlAsset = new XmlDocument();
	
	
	//los prefabs de los enemigos
	
	public static GameObject zombiePrefab  = (GameObject) Resources.Load("Prefabs/Enemies/zombie");
	public static GameObject flubberPrefab = (GameObject) Resources.Load("Prefabs/Enemies/flubber");
	public static GameObject spiderPrefab  = (GameObject) Resources.Load("Prefabs/Enemies/spider");
	public static GameObject ghostPrefab   = (GameObject) Resources.Load("Prefabs/Enemies/ghost");	
	
	
	
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
	
	public Stats (string type) {
		switch (type) {
		case "zombie":
			this.constructor(ZombieStats.toString());	
			this.prefab = zombiePrefab;
			break;
			
			case "flubber":
			this.constructor(FlubberStats.toString());	
			this.prefab = flubberPrefab;
			break;
			
			case "spider":
			this.constructor(SpiderStats.toString());	
			this.prefab = spiderPrefab;
			break;
			
			case "ghost":
			this.constructor(GhostStats.toString());	
			this.prefab = ghostPrefab;
			break;
			
			
		}

	}
	
	
	public void constructor(string data) {
		string [] dataArr = data.Split('/');
		this.baseHP    = int.Parse(dataArr[0]);
		this.attType   = dataArr[1];
		this.damage    = int.Parse(dataArr[2]);
		this.moveSpeed = float.Parse(dataArr[3]);
		this.interval  = float.Parse(dataArr[4]);
		
	}
	
	
	
	public static class ZombieStats  {
		public static int baseHP;
		public static string attType;
		public static int damage;
		public static float moveSpeed;
		public static float interval;
		
		
		public static void read (XmlNode nodo) {
			baseHP    = int.Parse(nodo["baseHP"].InnerText);
			attType   = nodo["attackType"].InnerText;
			damage    = int.Parse(nodo["damage"].InnerText);
			moveSpeed = float.Parse(nodo["moveSpeed"].InnerText);
			interval  = float.Parse(nodo["baseAttInterval"].InnerText);

		}
		
		public static string toString() {
			return baseHP + "/"	+ attType + "/" + damage + "/" + moveSpeed + "/" + interval;
		}
		
	}
	
	
	public static class FlubberStats {
		public static int baseHP;
		public static string attType;
		public static int damage;
		public static float moveSpeed;
		public static float interval;
		
		
		public static void read (XmlNode nodo) {
			baseHP    = int.Parse(nodo["baseHP"].InnerText);
			attType   = nodo["attackType"].InnerText;
			damage    = int.Parse(nodo["damage"].InnerText);
			moveSpeed = float.Parse(nodo["moveSpeed"].InnerText);
			interval  = float.Parse(nodo["baseAttInterval"].InnerText);
		}
		
		public static string toString() {
			return baseHP + "/"	+ attType + "/" + damage + "/" + moveSpeed + "/" + interval;
		}		
	}
	
	
	public static class SpiderStats {
		public static int baseHP;
		public static string attType;
		public static int damage;
		public static float moveSpeed;
		public static float interval;
		
		
		public static void read (XmlNode nodo) {
			baseHP    = int.Parse(nodo["baseHP"].InnerText);
			attType   = nodo["attackType"].InnerText;
			damage    = int.Parse(nodo["damage"].InnerText);
			moveSpeed = float.Parse(nodo["moveSpeed"].InnerText);
			interval  = float.Parse(nodo["baseAttInterval"].InnerText);

		}
		
		public static string toString() {
			return baseHP + "/"	+ attType + "/" + damage + "/" + moveSpeed + "/" + interval;
		}		
	}
	
	
	public static class GhostStats {
		public static int baseHP;
		public static string attType;
		public static int damage;
		public static float moveSpeed;
		public static float interval;
		
		
		public static void read (XmlNode nodo) {
			baseHP    = int.Parse(nodo["baseHP"].InnerText);
			attType   = nodo["attackType"].InnerText;
			damage    = int.Parse(nodo["damage"].InnerText);
			moveSpeed = float.Parse(nodo["moveSpeed"].InnerText);
			interval  = float.Parse(nodo["baseAttInterval"].InnerText);

		}
		
		public static string toString() {
			return baseHP + "/"	+ attType + "/" + damage + "/" + moveSpeed + "/" + interval;
		}		
		
	}
	
	
}

using UnityEngine;
using System.Collections;
using System.Xml;

public static class Round{
	
	public static int destinyQuant;
  	public static int number = 0;
	public static int count;
	public static string type;
	private static XmlDocument xmlAsset = new XmlDocument();
	public static bool isTargetRound, weaponSpawned, weaponPicked = false;

	
	
	static Round() {
	 	TextAsset temporal = (TextAsset) Resources.Load("data/Waves");
		xmlAsset.LoadXml(temporal.text);
		Debug.Log("Reading waves.xml");
		
		Round.count = xmlAsset.ChildNodes[1].ChildNodes.Count;
		Debug.Log("Playing with " + Round.count + " waves");
	}
	
	
	public static void next(){
		/*childNodes[1] la primera vez porque el primer node del archivo es <?xml version="1.0" encoding="UTF-8"?>\
		 * despues uso el numero de ronda para agarrar al childNode
		 * */
		
		number++;
		XmlNode roundData = xmlAsset.ChildNodes[1].ChildNodes[number-1];	
		type = (string) roundData["type"].InnerText;
		isTargetRound = false;
		
		if (type == "targets") {
			isTargetRound = true;
		}
		else {
			
			if(roundData["spawnWeapon"] != null) {
				weaponSpawned = true;	
			}
			
			destinyQuant = int.Parse(roundData["destiny"].InnerText);
			RoundManager.interval = float.Parse(roundData["interval"].InnerText);	
		}
	}

}

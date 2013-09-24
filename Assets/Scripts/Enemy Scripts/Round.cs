using UnityEngine;
using System.Collections;
using System.Xml;

public static class Round{
	
	public static int destinyQuant;
  	public static int number = 1;	
	public static string type;
	public static XmlDocument xmlAsset = new XmlDocument();

	
	
	static Round() {
	 	TextAsset temporal = (TextAsset) Resources.Load("data/Waves");
		xmlAsset.LoadXml(temporal.text);
		Debug.Log("Reading waves.xml");
	}
	
	
	public static void next(){
		/*childNodes[1] la primera vez porque el primer node del archivo es <?xml version="1.0" encoding="UTF-8"?>\
		 * despues uso el numero de ronda para agarrar al childNode
		 * */
		
		number++;
		XmlNode roundData = xmlAsset.ChildNodes[1].ChildNodes[number-1];		
		destinyQuant = int.Parse(roundData["destiny"].InnerText);
		type = (string) roundData["type"].InnerText;
		RoundManager.interval = float.Parse(roundData["interval"].InnerText);	
	}

}

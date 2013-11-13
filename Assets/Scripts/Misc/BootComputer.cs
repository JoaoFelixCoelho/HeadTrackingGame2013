using UnityEngine;
using System.Collections;

public class BootComputer : MonoBehaviour {
	
	string fullText = "hola todo bien chabon?|wait5| que onda?|jump1|" +
		"el otro dia fui a comer";
	float keyStroke = 0.1f;
	float keyTimer = 0f;
	float delay = 0f;
	int cont = 0;
	int jumpLines = 0;
	int length;
	public GUIText screenText;
	
	
	// Use this for initialization
	void Start () {
		screenText.text = "";
		length = fullText.Length;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		keyTimer += Time.deltaTime;
		
		if (cont < length) {
			if (keyTimer >= delay) {
				delay = 0f;
				if (keyTimer >= keyStroke) {
					keyTimer=0;
					print(fullText.Substring(cont,1));
					if (fullText.Substring(cont,1) == "|") {
						string instruccion = fullText.Substring(cont+1,4);	
						int number =int.Parse(fullText.Substring(cont+5,1));
						cont += 4;
						if(instruccion == "wait") {
							delay = number;	
						}
						if (instruccion == "jump") {
							jumpLines = number;	
						}
						
					}
					else {
						screenText.text += fullText.Substring(cont,1);
					}
					cont++;
					
					
				}
			}
		}
		
		
		
		
	}
}

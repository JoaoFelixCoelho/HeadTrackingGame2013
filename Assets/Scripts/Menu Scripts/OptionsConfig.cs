using UnityEngine;
using System.Collections;

public class OptionsConfig : MonoBehaviour {
	
	public GameObject [] options;
	public MenuHoverController menu;
	private float keyDownTimer = 0f;
	
	
	// Use this for initialization
	void Start () {
		
		/*soy dios y martha me enseno a usar string re piolah*/
		
		string originalText = options[0].GetComponent<TextMesh>().text;
		options[0].GetComponent<TextMesh>().text = replaceStringWith(originalText, (Configuration.pointerWiiMote +1).ToString());
		
		
		originalText = options[1].GetComponent<TextMesh>().text;
		options[1].GetComponent<TextMesh>().text = replaceStringWith(originalText, (Configuration.cameraWiiMote+1).ToString());
		
		
		originalText = options[2].GetComponent<TextMesh>().text;
		options[2].GetComponent<TextMesh>().text = replaceStringWith(originalText, Configuration.difficulty);

	}
	
	// Update is called once per frame
	void Update () {
		
		bool leftKey  = Input.GetKey(KeyCode.LeftArrow);
		bool rightKey = Input.GetKey(KeyCode.RightArrow);
		
		keyDownTimer += Time.deltaTime;
		
		if (leftKey) {
			changeOptionValue(-1);	
		}
		
		
		if (rightKey) {
			changeOptionValue(+1);	
		}
		
	
	}
	
	
	private void changeOptionValue(int sign) {
		
		if (keyDownTimer > 0.2f) {
			string selectedOption = (options[menu.selectedButton-1].GetComponent<TextMesh>().text);
			int optionValue = selectedOption.IndexOf('<');
			optionValue = int.Parse(selectedOption.Substring(optionValue+1,1));
			//int controlCount = WiiMote.wiimote_count();
			int controlCount = 4;
			if(optionValue + sign <= controlCount && optionValue + sign > 0) {
				string oldTxt = options[menu.selectedButton-1].GetComponent<TextMesh>().text;
				options[menu.selectedButton-1].GetComponent<TextMesh>().text = replaceStringWith(oldTxt, (optionValue+sign).ToString());	
				keyDownTimer = 0f;
				switch(menu.selectedButton-1) {
				case 0:
					Configuration.pointerWiiMote = optionValue + sign -1;
					break;
				case 1:
					Configuration.cameraWiiMote  = optionValue + sign -1;
					break;
				case 2:
					Configuration.difficulty = "easy";
					break;
					
				}
			}
			
			
		}
		
		
		
	}
	
	
	private string replaceStringWith(string oldText, string newValue) {
		return (oldText.Split('<'))[0].ToString() + "<" + newValue + ">";		
	}
	
	
}

using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	public enum MenuEnum {play = 0, option = 1, highscores = 2, exit = 3}
	public MenuEnum buttonType  = MenuEnum.play;
	public GameObject listItem, containerToOpen;
	private bool showingScores = false;
	

	
	public Shader glowShader;
	
	
	void Start() {
		StartCoroutine(RoundManager.dbController.GetScores());	
		
	}
	
	void OnMouseEnter()
	{
		//gameObject.material.color = Color.red;
	}
	
	void OnMouseExit()
	{
		//gameObject.material.color = Color.gray;
	}
	
	

	
	public GameObject checkButton() {
		
		switch(buttonType) {
			
		case MenuEnum.play:
			Application.LoadLevel(1);
			return null;
			break;
			
		case MenuEnum.option:
			return containerToOpen;
			break;
			
		case MenuEnum.highscores:
			showScoreData();
			return containerToOpen;
			break;
			
			
		case MenuEnum.exit:
			Application.Quit();
			return null;
			break;
			
		default:
			return null;
			break;
			
			
			
		}				
	}
	
	
	void OnMouseUp() {
		checkButton();
		gameObject.renderer.material.shader = glowShader;
	}
	

	void showScoreData(){
		if (!showingScores) {
			showingScores = true;
			string array;
			array = RoundManager.dbController.data;
			
			string [] rows = array.Split(';');
			
			
			containerToOpen.GetComponent<MenuHoverController>().buttons = new GameObject[rows.Length-1];
			for (int i = 0; i < rows.Length-1; i++){
				
				GameObject tmp = (GameObject) Instantiate(listItem,
															new Vector3(listItem.transform.position.x,
																		listItem.transform.position.y - listItem.GetComponent<TextMesh>().fontSize/28 * i+1, 
																		listItem.transform.position.z),
															listItem.transform.rotation
														);
				
				tmp.transform.parent = containerToOpen.transform;
				print(rows[i].ToString());
				string [] columns = rows[i].Split('|');
				tmp.GetComponent<TextMesh>().text =  columns[0] + "  -  " + columns[1];
				containerToOpen.GetComponent<MenuHoverController>().buttons[i] = tmp;
			}
		}
		
		
		
	}

	
	void Update() {
		
	}
	
}
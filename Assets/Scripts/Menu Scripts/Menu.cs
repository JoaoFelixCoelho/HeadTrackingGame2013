using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	public enum MenuEnum {play = 0, option = 1, highscores = 2, exit = 3}
	public MenuEnum buttonType  = MenuEnum.play;
	public string [] nameScore;
	public GameObject listItem, scoreContainer;
	private bool showingScores = false;
	
	
	#region scrollSystem
	private bool  scrolling = false;
	private float scrollTimer = 0f;
	private bool scrollDown, scrollUp = false;
	int scrollIndex, scrollLimit = 0;
	#endregion
	
	
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
	
	
	private void setVisibility(bool visib) {
		for (int i=0; i< transform.parent.childCount; i++) {
			transform.parent.GetChild(i).renderer.enabled = visib;
		}
		
		scoreContainer.SetActive(!visib);
		
			
	}
	
	public void checkButton() {
		
		
		switch(buttonType) {
			
		case MenuEnum.play:
			Application.LoadLevel(1);
			break;
			
		case MenuEnum.option:
			break;
			
		case MenuEnum.highscores:
			setVisibility(false);
			showScoreData();			
			break;
			
			
		case MenuEnum.exit:
			Application.Quit();
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
			nameScore = array.Split('|');
						
			
			scrollLimit = nameScore.Length;
			for (int i = 1; i <= nameScore.Length -2; i+=2){
				GameObject tmp = (GameObject) Instantiate(listItem,
															new Vector3(listItem.transform.position.x,
																		listItem.transform.position.y - listItem.GetComponent<TextMesh>().characterSize * i *2, 
																		listItem.transform.position.z),
															listItem.transform.rotation
														);
				
				tmp.transform.parent = scoreContainer.transform;
				tmp.GetComponent<TextMesh>().text =  nameScore[i+1] + "  -  " + nameScore[i];
			}
		}
		
		
		
	}
	
	private void checkScroll() {

		
			if (scrollDown || scrollUp) {
				scrollTimer += Time.deltaTime;
				scrolling = true;
				if (scrollTimer <= 0.3f) {
					if (scrollDown) {
						scrollIndex --;
						scoreContainer.transform.position = new Vector3(scoreContainer.transform.position.x, scoreContainer.transform.position.y - 0.08f, scoreContainer.transform.position.z);
					}
					if (scrollUp) {
						scrollIndex ++;
						scoreContainer.transform.position = new Vector3(scoreContainer.transform.position.x, scoreContainer.transform.position.y + 0.08f, scoreContainer.transform.position.z);
					}
				
				}
				else  {
					scrollUp   = false;
					scrollDown = false;
					scrolling  = false;	
	
				}
			}
		
	}
	
	void Update() {
		bool downKey = Input.GetKey(KeyCode.DownArrow);
		bool upKey   = Input.GetKey(KeyCode.UpArrow);
		bool backKey = Input.GetKey(KeyCode.Backspace);
		
		checkScroll();
		
		if (downKey && buttonType == MenuEnum.highscores && !scrolling) {
			scrollTimer = 0f;
			scrollDown = true;
		}
		
		if (upKey && buttonType == MenuEnum.highscores && !scrolling) {
			scrollTimer = 0f;
			scrollUp = true;
		}
		
		if (backKey && buttonType == MenuEnum.highscores) {
			setVisibility(true);	
		}
		
	}
	
}
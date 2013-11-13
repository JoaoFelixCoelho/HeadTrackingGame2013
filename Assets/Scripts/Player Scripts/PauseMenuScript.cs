using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {
	public GameObject pauseMenu;
	
	public GUITexture background;
	public GUIText resume;
	public GUIText options;
	public GUIText saveAndExit;
	public GUIText exitToWindows;
	public GUIText exitToMenu;
	
	
	
	
	private bool menuOpen;
	private bool pauseCheck = false;
	//public enum MenuEnum {Continue = 1, Options=2, SaveAndExit=3, ExitToMenu=4, ExitToWindows=5};
	public int menuNavigator=1;
	public Color lowOpacity;
	public Color normalOpacity;
	public bool canUp=false, canDown = false, letUp, letDown;
	public float timerUpDown;
	// Use this for initialization
	
	public void pauseGame() {
		if (!menuOpen) {
			Time.timeScale = 0f; 
			resizeGui ();
			pauseMenu.SetActive(true);
			menuOpen = true;
		}
		else {
			Time.timeScale = 1f;
			pauseMenu.SetActive(false);
			menuOpen = false;
		}
	}
		
	
	public void resizeGui(){
		float spacing = Screen.height/10;
		background.pixelInset = new Rect(-Screen.width/2, -Screen.height/2, Screen.width, Screen.height);
		resume.pixelOffset = new Vector2(0,0+resume.fontSize);
		options.pixelOffset = new Vector2(0,resume.pixelOffset.y-options.fontSize-spacing);
		saveAndExit.pixelOffset = new Vector2 (0,options.pixelOffset.y-saveAndExit.fontSize-spacing);
		exitToMenu.pixelOffset = new Vector2 (0,saveAndExit.pixelOffset.y-saveAndExit.fontSize-spacing);
		exitToWindows.pixelOffset = new Vector2(0,exitToMenu.pixelOffset.y-saveAndExit.fontSize-spacing);
		
	} 
	
	public void selectOption(){
		bool isUp = Input.GetKeyDown(KeyCode.UpArrow);
		bool isDown = Input.GetKeyDown(KeyCode.DownArrow);
		bool isEnter = Input.GetKeyDown(KeyCode.Return);
		bool isWiiDown = WiiMote.wiimote_getButtonDown(Configuration.pointerWiiMote);
		bool isWiiUp = WiiMote.wiimote_getButtonUp(Configuration.pointerWiiMote);
		
		if (isWiiUp){
			letUp= true;
			letUp=false;
			canUp = true;
			if (canUp){
				timerUpDown+=Time.deltaTime;
				if (timerUpDown>0.5f){
					letUp=true;
					timerUpDown=0;
					letUp=false;
				}
			}
			else { 
				canUp = false;
			}
		}
		
		if (isEnter){
			switch (menuNavigator){
			case 1:
				pauseGame();
			break;
			case 3:
				StartCoroutine(RoundManager.dbController.PostScores(PlayerBehave.playerName, PlayerBehave.score));
				pauseGame();
				Application.LoadLevel(0);
				break;
			case 4:
				pauseGame();
				Application.LoadLevel(0);
			break;
			case 5:
				Application.Quit ();
			break;
			}
		}
		
		//navegar con flechitas
		if (isUp || letUp){
			menuNavigator-=1;
			switch (menuNavigator){
				case 0:
					menuNavigator=5;
					resume.material.color = lowOpacity;
					exitToWindows.material.color = normalOpacity;
				break;
				case 1:
					resume.material.color = normalOpacity;
					options.material.color = lowOpacity;
					break;
				case 2:
					options.material.color = normalOpacity;
					saveAndExit.material.color = lowOpacity;
				break;
				case 3:
					exitToMenu.material.color = lowOpacity;
					saveAndExit.material.color = normalOpacity;
				break;
				case 4:
					exitToWindows.material.color = lowOpacity;
					exitToMenu.material.color = normalOpacity;
				break;
				case 5:
					resume.material.color = lowOpacity;
					exitToWindows.material.color = normalOpacity;
				break;
			}	
		}	
		if (isDown){
			menuNavigator+=1;
				switch (menuNavigator){
				case 1:
					resume.material.color = normalOpacity;
					exitToWindows.material.color = lowOpacity;
					break;
				case 2:
					resume.material.color = lowOpacity;
					options.material.color = normalOpacity;
				break;
				case 3:
					options.material.color = lowOpacity;
					saveAndExit.material.color = normalOpacity;
				break;
				case 4:
					saveAndExit.material.color = lowOpacity;
					exitToMenu.material.color = normalOpacity;
				break;
				case 5:
					exitToMenu.material.color = lowOpacity;
					exitToWindows.material.color = normalOpacity;
				break;
				case 6:
				menuNavigator=1;
				resume.material.color = normalOpacity;
				exitToWindows.material.color = lowOpacity;
				break;
			}	
		}
		//navegar con flechitas
	}
	
	
	void Start () {
		lowOpacity = new Color (resume.font.material.color.r, resume.font.material.color.g, resume.font.material.color.b,0.1f);
		normalOpacity = new Color (resume.font.material.color.r, resume.font.material.color.g, resume.font.material.color.b,1f);
		resume.material.color = normalOpacity;
		options.material.color = lowOpacity;
		saveAndExit.material.color = lowOpacity;
		exitToMenu.material.color = lowOpacity;
		exitToWindows.material.color = lowOpacity;
	}
	
	// Update is called once per frame
	void Update () {
		bool isEsc = Input.GetKeyDown(KeyCode.Escape);
		bool isHome = WiiMote.wiimote_getButtonHome(Configuration.pointerWiiMote);
		bool isMinus = WiiMote.wiimote_getButtonMinus(Configuration.pointerWiiMote);
		
		if (menuOpen){
			selectOption();
			
		} 
		
		if (isEsc) {
			pauseGame();	
		}
		
		if (isMinus && !pauseCheck || isHome && !pauseCheck) {
			pauseCheck = true;
			pauseGame();
		}
	}
}
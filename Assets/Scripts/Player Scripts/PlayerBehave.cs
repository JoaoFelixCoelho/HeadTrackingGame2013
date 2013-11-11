using UnityEngine;
using System.Collections;

public class PlayerBehave : MonoBehaviour {
	
	#region playerAttributes
	public int ammo;
	public GameObject handgun;
	public GameObject laser;
	public Arma weapon;
	public int kills;
	public int killsInRound;
	public static int score;
	public static string playerName = "noName";
	#endregion
	
	#region GUIs
	public Texture aversh;
	public GUITexture background;
	public GUIText ammoGUI;
	public GUIText enemyCounter;
	public GUIText warningGUI;
	public GUIText roundWarning;
	#endregion

	
	#region imageEffectManager
	public Vignetting cameraEffect;
	public MotionBlur motionBlur;
	public bool imageEffectActive;
	private float ieTimer = 0;
	public float imageDistortTime = 1.2f;
	private float chromaticRate = 90f;
	#endregion
	
	
	#region menuAttrs
	public GameObject pauseMenu;
	private bool menuOpen;
	private bool pauseCheck = false;
	#endregion
	
	#region others
	private bool handtolaser;
	#endregion
	
	
	// Use this for initialization
	void Start () {
		this.kills=0;
		if (handgun.activeSelf) {
			weapon = handgun.GetComponent<Arma>();
		}
		else {
			weapon = laser.GetComponent<Arma>();
		}
		//this.currHp = totalHp;
	}
	
	public void addAmmo(int cant){
		if (this.ammo<540){
			this.ammo+=cant;	
		}
	}
	
	public void reloadWeapon(){
		// .reload te devuelve la cantidad de balas que van a quedar, le llegan la cantidad que tenés
		this.ammo-=(weapon.reload(this.ammo));
	}
	
	public void killPlayer () {
		sendMessageToPlayer("Game Over!", "warning");
		StartCoroutine(RoundManager.dbController.PostScores(PlayerBehave.playerName, PlayerBehave.score));	
		//transform.FindChild("Main Camera").animation.Play("playerDie");
	}
	
	public void addKill() {
		this.kills++;
		this.killsInRound++;
		//meter algoritmo de medina
		PlayerBehave.score += 10 * Round.number + Random.Range(0,Round.number+kills);
	}
	
	public void cambioArma () {
		if (Round.weaponPicked) {
			if (laser.GetComponent<Crosshair>().enabled == true){
				handtolaser = false;
				laser.gameObject.animation.Play ("LaserUp");	
				StartCoroutine (handToLaser());
				weapon = handgun.GetComponent<Arma>();
	
			}
			else {
				handgun.gameObject.animation.Play ("HandgunUp");
				handtolaser = true;
				StartCoroutine (handToLaser());
				weapon = laser.GetComponent<Arma>();
			}
		}
	}

	public IEnumerator handToLaser(){
		if (handtolaser){
			yield return new WaitForSeconds(0.7f);
			
			handgun.gameObject.GetComponent<Crosshair>().enabled = false;
			laser.gameObject.GetComponent<Crosshair>().enabled = true;
			
			laser.gameObject.animation.Play ("LaserDown");
		}
		else {
			yield return new WaitForSeconds(0.7f);
			
			laser.gameObject.GetComponent<Crosshair>().enabled = false;
			handgun.gameObject.GetComponent<Crosshair>().enabled = true;
			
			
			handgun.gameObject.animation.Play ("HandgunDown");
		}
	}
		
	
	
	private void checkCameraDistort() {
		int signo = Random.Range(0,2);
		if (signo == 0) {
			cameraEffect.chromaticAberration = Random.Range(-chromaticRate, -chromaticRate+20);
		}
		else {
			cameraEffect.chromaticAberration = Random.Range(chromaticRate-50, chromaticRate);
		}
		motionBlur.enabled = true;
		cameraEffect.blur +=0.5f;
		cameraEffect.blurSpread += 0.01f;
		ieTimer += Time.deltaTime;
		if (ieTimer >= imageDistortTime) {
			cameraEffect.chromaticAberration = 0;
			cameraEffect.blurSpread = 0.75f;
			cameraEffect.blur = 0.8f;
			imageEffectActive = false;
			motionBlur.enabled = false;
			ieTimer = 0f;
		}
	}
	
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
	
	public void sendMessageToPlayer(string msg, string guiTextToUse) {
		guiTextToUse = guiTextToUse.ToLower();
		if(guiTextToUse == "ammocount") {
			ammoGUI.text = msg;
		}
		
		if (guiTextToUse == "warning") {
			warningGUI.text = msg;	
		}
		
		if(guiTextToUse == "enemyCounter") {
			enemyCounter.text = msg;	
		}
		
		if (guiTextToUse == "roundwarning") {
			roundWarning.GetComponent<FadeGUI>().enabled = true;
			roundWarning.text = msg;	
		}
		
		
	}
	
	public void resizeGui(){
		background.pixelInset = new Rect(-Screen.width/2, -Screen.height/2, Screen.width, Screen.height);
	}
	
	void Update () {
		bool isLeft = Input.GetKey(KeyCode.Mouse0);
		bool rKey = Input.GetKeyDown(KeyCode.R);
		bool gKey = Input.GetKeyDown(KeyCode.G);
		bool isEsc = Input.GetKeyDown(KeyCode.Escape);
		
		bool isB = WiiMote.wiimote_getButtonB(Configuration.pointerWiiMote);
		bool isA = WiiMote.wiimote_getButtonA(Configuration.pointerWiiMote);
		bool isHome = WiiMote.wiimote_getButtonHome(Configuration.pointerWiiMote);
		bool isMinus = WiiMote.wiimote_getButtonMinus(Configuration.pointerWiiMote);
		bool isBtnLeft = WiiMote.wiimote_getButtonLeft(Configuration.pointerWiiMote);
		bool isBtnRight = WiiMote.wiimote_getButtonRight(Configuration.pointerWiiMote);
		
			
		
		
		
		
		if (imageEffectActive) {
			checkCameraDistort();
		}
				
		if(isLeft || isB) {
			weapon.shoot();	
		}
		
		if (rKey || isA) {
			reloadWeapon();
		}
		
		if (gKey || isBtnLeft || isBtnRight) {
			cambioArma ();
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

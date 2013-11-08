using UnityEngine;
using System.Collections;

public class PlayerBehave : MonoBehaviour {
	
	#region playerAttributes
	public int ammo;
	public GameObject handgun;
	public GameObject laser;
	private Arma weapon;
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
	}
	
	public void addKill() {
		this.kills++;
		this.killsInRound++;
		//meter algoritmo de medina
		PlayerBehave.score += 10 * Round.number + Random.Range(0,Round.number+kills);
	}
	
	public void cambioArma () {
		if (Round.weaponPicked) {
			if (laser.activeSelf==true){
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
			handgun.gameObject.SetActive(false);
			laser.gameObject.SetActive(true);
			laser.gameObject.animation.Play ("LaserDown");
		}
		else {
			yield return new WaitForSeconds(0.7f);
			laser.gameObject.SetActive(false);
			handgun.gameObject.SetActive(true);
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
			//GetComponent<MouseLook>().enabled = false;
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
		background.pixelInset.width = Screen.width;
		background.pixelInset.height = Screen.height;
	}
	
	void Update () {
		bool isLeft = Input.GetKey(KeyCode.Mouse0);
		bool rKey = Input.GetKeyDown(KeyCode.R);
		bool gKey = Input.GetKeyDown(KeyCode.G);
		bool isEsc = Input.GetKeyDown(KeyCode.Escape);
		
		if (imageEffectActive) {
			checkCameraDistort();
		}
				
		if(isLeft) {
			weapon.shoot();	
		}
		
		if (rKey) {
			reloadWeapon();
		}
		
		if (gKey) {
			cambioArma ();
		}
		
		if (isEsc) {
			pauseGame();
		}
		
		
	}
}

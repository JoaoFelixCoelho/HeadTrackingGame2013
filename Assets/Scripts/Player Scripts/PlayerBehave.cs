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
	public GUIText ammoGUI;
	public GUIText enemyCounter;
	public GUIText warningGUI;
	public GUIText roundWarning;
	public QuickMessage msgGUI;
	#endregion

	
	#region imageEffectManager
	public Vignetting cameraEffect;
	public MotionBlur motionBlur;
	public bool imageEffectActive;
	private float ieTimer = 0;
	public float imageDistortTime = 1.2f;
	private float chromaticRate = 90f;
	#endregion
	
	#region others
	private bool handtolaser;

	#endregion
	
	
	// Use this for initialization
	void Start () {
		Configuration.cameraWiiMote = 0;
		print(Configuration.cameraWiiMote);

		this.kills=0;
		if (handgun.activeSelf) {
			weapon = handgun.GetComponent<Arma>();
		}
		else {
			weapon = laser.GetComponent<Arma>();
		}
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
		imageEffectActive = true;
		gameObject.GetComponent<WiiCameraScript>().enabled = false;
		this.weapon.enabled = false;
		sendMessageToPlayer("Game Over!", "warning");
		transform.FindChild("Main Camera").animation.Play("playerDie");
		StartCoroutine(RoundManager.dbController.PostScores(PlayerBehave.playerName, PlayerBehave.score));	
	}
	
	public void addKill(string enemyType) {
		this.kills++;
		this.killsInRound++;
		int extra = 0;
		int extra2 = 0;
		if(enemyType == "zombie") {
			extra = Random.Range(10,30);	
		}
		if(enemyType == "flubber") {
			extra = Random.Range(30,40);	
		}		
		if(enemyType == "spider") {
			extra = Random.Range(50,70);	
		}
		if(enemyType == "ghost") {
			extra = Random.Range(70,100);	
		}
		
		if(GetComponent<HealthSystem>().currHp > GetComponent<HealthSystem>().totHp/2) {
			extra2 = 50;	
		}
		
		
		PlayerBehave.score += 10 * Round.number + extra + extra2;
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
	
	public void wiiMenu(){
		
	}
	
	void Update () {
		bool isLeft = Input.GetKey(KeyCode.Mouse0);
		bool rKey = Input.GetKeyDown(KeyCode.R);
		bool gKey = Input.GetKeyDown(KeyCode.G);
		
		bool isB = WiiMote.wiimote_getButtonB(Configuration.pointerWiiMote);
		bool isA = WiiMote.wiimote_getButtonA(Configuration.pointerWiiMote);
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
	}
}

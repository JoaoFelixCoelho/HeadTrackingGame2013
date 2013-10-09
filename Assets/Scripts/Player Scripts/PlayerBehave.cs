using UnityEngine;
using System.Collections;

public class PlayerBehave : MonoBehaviour {

	public int ammo;
	public GameObject handgun;
	public GameObject laser;
	private Arma weapon;
	public int kills;
	public int killsInRound;
	public int score;
	
	public GUIText ammoGUI;
	public GUIText enemyCounter;
	public GUIText warningGUI;
	private double fireRate=0.2;
	private double animationwait=0;
	private bool allowFire=false;
	private bool resetBool=true;
	public Transform testSpawn;

	
	
	// Use this for initialization
	void Start () {
		this.kills=0;
		this.score=0;
		weapon = handgun.GetComponent<Arma>();
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
		Debug.Log("El jugador se murio guacho");
		//hacer mas codigo	
	}	
	
	public void addKill() {
		this.kills++;
		this.killsInRound++;
		this.score += 10 * Round.number + Random.Range(0,Round.number+kills);
	}
	
	public void cambioArma () {
		if (laser.gameObject.activeSelf==true){
			laser.gameObject.animation.Play ("LaserUp");	
			StartCoroutine (laserToHand());

		}
		else {
			handgun.gameObject.animation.Play ("HandgunUp");
			StartCoroutine (handToLaser());
		}
	}
	public IEnumerator laserToHand(){
		yield return new WaitForSeconds(0.7f);
			laser.gameObject.SetActive(false);
			handgun.gameObject.SetActive(true);
			handgun.gameObject.animation.Play ("HandgunDown");
	}
	public IEnumerator handToLaser(){
		yield return new WaitForSeconds(0.7f);
		handgun.gameObject.SetActive(false);
		laser.gameObject.SetActive(true);
		laser.gameObject.animation.Play ("LaserDown");
	}
		
	
	void Update () {
		bool isLeft = Input.GetKey(KeyCode.Mouse0);
		bool rKey = Input.GetKeyDown(KeyCode.R);
		bool gKey = Input.GetKeyDown(KeyCode.G);
		
		if(isLeft) {
			weapon.shoot();	
		}
		
		if (rKey) {
			reloadWeapon();
		}
		
		if (gKey) {
			cambioArma ();
		}	
		
	}
}

using UnityEngine;
using System.Collections;

public class PlayerBehave : MonoBehaviour {

	public int ammo;
	public GameObject rifle;
	public GameObject laser;
	private Arma weapon;
	public int kills;
	public int killsInRound;
	public int score;
	
	public GUIText ammoGUI;
	public GUIText enemyCounter;
	public GUIText warningGUI;
	private double fireRate=0.2;
	private bool allowFire=false;
	private bool resetBool=true;
	public Transform testSpawn;

	
	
	// Use this for initialization
	void Start () {
		this.kills=0;
		this.score=0;
		rifle.gameObject.SetActive(true);
		//this.currHp = totalHp;
	}
	
	public void addAmmo(int cant){
		if (this.ammo<540){
			this.ammo+=cant;	
		}
	}
	
	public void reloadWep(){
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
		
	
	void Update () {
		bool isLeft = Input.GetKey(KeyCode.Mouse0);
		bool rKey = Input.GetKeyDown(KeyCode.R);
		bool gKey = Input.GetKeyDown(KeyCode.G);
		if(isLeft) {
			weapon.shoot();	
		}
	}
}

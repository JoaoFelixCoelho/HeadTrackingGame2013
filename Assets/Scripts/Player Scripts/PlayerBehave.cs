using UnityEngine;
using System.Collections;

public class PlayerBehave : MonoBehaviour {

	public int ammo;
	public Arma weapon;
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
		//this.currHp = totalHp;
	}
	
	public void addAmmo(int cant){
		if (this.ammo<540){
			this.ammo+=cant;	
		}
	}
	
	/*public void damageHp(int cant){
		if (this.currHp>0 && this.currHp-cant>0){
			this.currHp-=cant;
		}
			else {
				killPlayer();	
			}
		}*/

	public void reloadWep(){
		// .reload te devuelve la cantidad de balas que van a quedar, le llegan la cantidad que tenés
		this.ammo-=(weapon.reload(this.ammo));
	}
	
	
	
	/*public int addHp (int cant){
		if (this.totalHp>currHp+cant){
			this.currHp+=cant;
		}
		else {
			this.currHp=this.totalHp;
		}
		return currHp;//devuelvo la vida que queda, seguro lo termino usando o algo
	}
	 */
	
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
		if(isLeft) {
			weapon.shoot();		
		}
		
	/*	//Debug.Log (fireRate);
		bool isLeft = Input.GetKey(KeyCode.Mouse0);
	//	bool isA = wiimote_getButtonA(0);
//		bool isB = wiimote_getButtonB(0);
		bool rKey = Input.GetKeyDown(KeyCode.R);
		bool zKey = Input.GetKeyDown(KeyCode.Z);
		bool xKey = Input.GetKeyDown(KeyCode.X);
		bool cKey = Input.GetKeyDown(KeyCode.C);
		fireRate -= Time.deltaTime;
		if (fireRate < 0){
			fireRate = 0.2;
			allowFire=true;
		}
			else 
				allowFire=false;
			
		if (rKey || isA){
			this.reloadWep();
			print("municion que queda: " + this.ammo);
		}
	
				
		if (isLeft && resetBool==true){
			fireRate = 0;
			resetBool=false;
		}
		if (isLeft && allowFire==true || isB && allowFire==true) {
			weapon.shoot();	
		}
		if (isLeft=false){
			resetBool=true;
		}
		*/

		
	}
	
	
}

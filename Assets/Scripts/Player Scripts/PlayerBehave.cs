using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class PlayerBehave : MonoBehaviour {
	[DllImport ("UniWii")]
	private static extern void wiimote_start();
	[DllImport ("UniWii")]
	private static extern void wiimote_stop();
	
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonB(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonA(int which);
	public int ammo;
	public Arma weapon;
	public int kills;
	public int killsInRound;
	public int score;
	
	public GUIText ammoGUI;
	public GUIText enemyCounter;
	public GUIText warningGUI;
	
	
	public Transform testSpawn;

	
	
	// Use this for initialization
	void Start () {
		wiimote_start();
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

	private void reloadWep(){
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
		bool isLeft = Input.GetMouseButtonDown(0);
		bool isA = wiimote_getButtonA(0);
		bool isB = wiimote_getButtonB(0);
		bool rKey = Input.GetKeyDown(KeyCode.R);
		bool zKey = Input.GetKeyDown(KeyCode.Z);
		bool xKey = Input.GetKeyDown(KeyCode.X);
		bool cKey = Input.GetKeyDown(KeyCode.C);
		
		
		if (rKey || isA){
			this.reloadWep();
			print("municion que queda: " + this.ammo);
		}
		
		if (zKey){		
			Enemy.createEnemy("zombie",1, testSpawn);
		}
		
		if (xKey){		
			Enemy.createEnemy("flubber",1, testSpawn);
		}
		
		if (cKey){		
			Enemy.createEnemy("dino",1, testSpawn);
		}
				
				
		if (isLeft || isB) {
			//CAMBIAS EL isLeft por lo que tengas que hacer, y para disparar, weapon.shoot(), asi de facil
			weapon.shoot();	
		}
		

		
	}
	
	
}

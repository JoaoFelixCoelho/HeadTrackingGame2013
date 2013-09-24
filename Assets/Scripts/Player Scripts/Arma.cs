using UnityEngine;
using System.Collections;

public class Arma : MonoBehaviour {
	
	
	//atributos
	protected int bullets;
	public enum WeaponEnum {Laser = 0, Rifle = 1};
	public WeaponEnum weaponModel = 0;
	public int damage;
	public float shootInterval;
	public int capacity;
	public float laserSpeed;
	public float rifleSpeed;
	bool shooting = false;
	float muzzleDeltaTime = 0;
	private float fireRate = 0.2f;
	private float timer = 0f;
	
	//Dependencias
	public Transform spawnPos;
	public Rigidbody proPrefab,rifleFab;
	public PlayerBehave player;
	
	GUIText ammoTxt;
	GUIText warningTxt;

	public GameObject muzzle;
		

	
	public void shoot(){
		
		//falta checkear que el jugador se quedo sin balas
		
		timer += Time.deltaTime;
		if (timer >= fireRate) {
		
			if (this.bullets<=0){
				showWarning("reload");
			}
			else {
				
				
				shooting = true;
				
				if (weaponModel == WeaponEnum.Laser) 				
				{
					Rigidbody lasInstance = (Rigidbody) Instantiate(proPrefab, spawnPos.position, spawnPos.rotation);
					lasInstance.AddForce(spawnPos.forward*laserSpeed);	
					lasInstance.GetComponent<Projectile>().setDamage(damage);
				}
				
				
				else if (weaponModel == WeaponEnum.Rifle)
				{
					Vector3 fwd = spawnPos.TransformDirection(Vector3.forward);
					RaycastHit hit;
					if (Physics.Raycast(transform.position, fwd, out hit))
					{
						Enemy enemyInstance = hit.collider.gameObject.GetComponent<Enemy>();
						
						if (enemyInstance != null){
							enemyInstance.GetComponent<HealthSystem>().damageHp(this.damage);
						}
					}
				}				
				
				
				
				this.bullets-=1;
				updateAmmo();
				
				if (this.bullets==0) {
					showWarning("reload");	
				}
				
			}
			timer = 0f;
		}
	}
	
	
	private void updateAmmo() {
		ammoTxt.text = this.bullets + "/" + this.capacity;
	}
	
	private void showWarning (string msg) {
		switch (msg.ToLower()) {
		case "reload":
			warningTxt.enabled = true;
			warningTxt.text = "Reload!";
			break;
			
		case "hide":
			warningTxt.enabled = false;
			break;
			
		case "empty":
			warningTxt.enabled = true;
			warningTxt.text = "out of ammo";
			break;
			
			
		}
	}
	
	
	
	
	
	public int reload (int totalBalas) {
	
		if (totalBalas==0){
			showWarning("empty");
		}
		
		else if (totalBalas>this.capacity || (totalBalas+this.bullets)>this.capacity){
			int aux = this.capacity - this.bullets;
			this.bullets = capacity;
			showWarning("hide");
			updateAmmo();
			return aux;
		}
		
		else{
			this.bullets+= totalBalas;	
			updateAmmo();
			showWarning("hide");
		}
		
		return totalBalas;//devuelve siempre totalBlas salvo el 2do caso. El return es las balas que va a restar
	}	
	
	
	void Start () {	
		this.bullets = this.capacity;
		ammoTxt = player.ammoGUI;
		warningTxt = player.warningGUI;
		updateAmmo();
		muzzle.renderer.enabled = false;

	}
	
	void Update () {
		if (shooting == true) {
			muzzle.renderer.enabled=true;
			muzzleDeltaTime += Time.deltaTime;
			if (muzzleDeltaTime > 0.15) {
				muzzle.renderer.enabled=false;	
				muzzleDeltaTime = 0;
				muzzle.transform.Rotate(new Vector3(muzzle.transform.rotation.x, muzzle.transform.rotation.y, Random.Range(-90,90)));
				shooting= false;
				
			}
		}
		
	}

}

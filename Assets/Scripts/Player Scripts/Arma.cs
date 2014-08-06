using UnityEngine;
using System.Collections;

public class Arma : MonoBehaviour {
	
	
	//atributos
	protected int bullets;
	public enum WeaponEnum {Laser = 0, Rifle = 1};
	public WeaponEnum weaponModel = 0;
	public int damage;
	public int capacity;
	public float laserSpeed;
	public bool shooting, reloading = false;
	float muzzleDeltaTime = 0;
	public float overheatCount;
	private int overheatLimit=25;
	
	public float fireRate;
	private float timer, reloadTimer = 0f;
	private float timeroverHeat=0;
	private float reloadDelay = 0f;
	
	public AudioClip shootAudio, noBulletAudio, reloadAudio;
	
	//Dependencias
	public Transform spawnPos;
	public Rigidbody proPrefab;
	private PlayerBehave player;
	
	GUIText ammoTxt;
	GUIText warningTxt;

	public GameObject muzzle;
	public GameObject modelGameObject;
	public Material laserMaterial;
	public float redIncrRate;

	public Material myMaterial;

	
	public void shoot(){
		
		//falta checkear que el jugador se quedo sin balas
		if (!reloading) {
			if (timer >= fireRate) {
			

				if (checkAmmo()) {
					
					
					//shooting = true;  SAQUE ESTE SHOOTING = TRUE Y HICE QUE REALMENTE SOLO CUANDO ESTE DISPARANDO, CONSULTAR A COZZA SI ESTA BIEN EN CLASE.
					
		
					
					if (weaponModel == WeaponEnum.Laser && overheatCount<overheatLimit) 				
					{
						audio.clip = shootAudio;
						audio.Play ();
						modelGameObject.animation.Play();
						Rigidbody lasInstance = (Rigidbody) Instantiate(proPrefab, spawnPos.position, spawnPos.rotation);
						lasInstance.AddForce(spawnPos.forward*laserSpeed);	
						lasInstance.GetComponent<Projectile>().setDamage(damage);

						overHeat();	
						shooting = true;
						WiiMote.wiimote_rumble(Configuration.pointerWiiMote, (float)0.053);
					}			
					else if (overheatCount>overheatLimit){
						WiiMote.wiimote_rumble(Configuration.pointerWiiMote, (float)1.5);
					}
					else if (weaponModel == WeaponEnum.Rifle)
					{
						audio.clip = shootAudio;
						audio.Play ();
						modelGameObject.animation.Play();
						Vector3 fwd = spawnPos.TransformDirection(Vector3.forward);
						fwd *= 20;
						WiiMote.wiimote_rumble(Configuration.pointerWiiMote, (float)0.2);
						Debug.DrawRay(transform.position,fwd);
						
						RaycastHit hit;
						if (Physics.Raycast(transform.position, fwd, out hit, 9999, 1))
						{
							//para cuando queres levantar el arma2
							if(Round.weaponSpawned && !Round.weaponPicked) {
								if (hit.collider.GetComponent<Arma>() != null) {
									hit.collider.GetComponent<WeaponSpawn>().transitionToPlayer();
								}
							}
							GameObject particleInstance = (GameObject) Instantiate(Projectile.bulletParticle, hit.point, transform.rotation);
							particleInstance.transform.LookAt(transform.position);
							Destroy(particleInstance, particleInstance.particleSystem.duration + 0.01f);
							HealthSystem HP = hit.collider.gameObject.GetComponent<HealthSystem>();
							
							if (HP != null){
								HP.damageHp(damage);
							}
						}
						shooting = true;
					}				
					else{
						shooting = false;
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
	}

	public bool checkAmmo() {
		if (this.bullets==0){
			showWarning("reload");
			if (weaponModel == WeaponEnum.Rifle){
				audio.clip = noBulletAudio;
				audio.Play();
			}
			return false;
		}
		showWarning ("hide");
		return true;
	}
	
	public void overHeat() {
		overheatCount+=1;	
		Color newCol = myMaterial.color;
		newCol.r += redIncrRate;
		myMaterial.color = newCol;
			
	}
	
	public void decreaseOverHeat(){
		if (shooting == false && overheatCount>0){
			timeroverHeat+= Time.deltaTime;
			if (timeroverHeat>1.5f){
				overheatCount-=Time.fixedDeltaTime*4;
				Color newCol = myMaterial.color;
				newCol.r -= redIncrRate * Time.fixedDeltaTime*4;
				myMaterial.color = newCol;
				
			}
		}
		else{
			timeroverHeat=0;
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

		if(this.weaponModel == WeaponEnum.Rifle) {
			if (this.bullets < this.capacity) 
			{
				if (totalBalas == 0) {
						showWarning ("empty");
				} else {
					audio.clip = reloadAudio;
					audio.Play ();	
					if (totalBalas > this.capacity || (totalBalas + this.bullets) > this.capacity) {
							reloading = true;
							if (weaponModel == WeaponEnum.Rifle) {
									modelGameObject.animation.Play ("handgunReload");
							}			
							int aux = this.capacity - this.bullets;
							this.bullets = capacity;
							showWarning ("hide");
							updateAmmo ();
							return aux;
					} 
					else {
							this.bullets += totalBalas;	
							updateAmmo ();
							showWarning ("hide");
					}
				}
				return 0;//devuelve siempre totalBlas salvo el 2do caso. El return es las balas que va a restar
			}

		}
		return 0;//devuelve siempre totalBlas salvo el 2do caso. El return es las balas que va a restar
		
	}	
	
	
	void Start () {	
		this.bullets = this.capacity;
		player = Enemy.player.GetComponent<PlayerBehave>();
		ammoTxt = player.ammoGUI;
		warningTxt = player.warningGUI;
		updateAmmo();
		muzzle.SetActive(false);
		redIncrRate = 5f/overheatLimit;
		if (this.weaponModel == WeaponEnum.Rifle) {
			reloadDelay = modelGameObject.animation["handgunReload"].length;
		}
		else {
			reloadDelay = 2f;	
		}
		//color1 = renderer.material.color;
	}
	
	void Update () {
		
		if(reloading) {
			checkReloadTimer();	
		}
		
		
		timer += Time.deltaTime;
		decreaseOverHeat();
		
		if (shooting == true) {
			muzzle.SetActive(true);
			muzzleDeltaTime += Time.deltaTime;
			if (muzzleDeltaTime > 0.15) {
				muzzleDeltaTime = 0;
				muzzle.transform.Rotate(new Vector3(0,0, Random.Range(-90,90)));
				shooting= false;
				muzzle.SetActive(false);
				
			}
		}
		
	}
	
	private void checkReloadTimer() {
		reloadTimer += Time.deltaTime;
		if (reloadTimer >= reloadDelay) {
			reloading= false;
			reloadTimer = 0f;
		}	
		
		
	}
	
	

}

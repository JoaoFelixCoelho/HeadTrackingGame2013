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
	public bool shooting = false;
	float muzzleDeltaTime = 0;
	
	
	public float fireRate;
	private float timer = 0f;
	
	//Dependencias
	public Transform spawnPos;
	public Rigidbody proPrefab;
	public PlayerBehave player;
	
	GUIText ammoTxt;
	GUIText warningTxt;

	public GameObject muzzle;
	public GameObject handgunAudio, laserAudio, nobulletAudio;
		

	
	public void shoot(){
		
		//falta checkear que el jugador se quedo sin balas
				
		if (timer >= fireRate) {
		
			if (this.bullets<=0){
				showWarning("reload");
				if (weaponModel == WeaponEnum.Rifle){
					nobulletAudio.audio.Play ();
				}
			}
			else {
				
				
				shooting = true;
				
				if (weaponModel == WeaponEnum.Laser) 				
				{
					Rigidbody lasInstance = (Rigidbody) Instantiate(proPrefab, spawnPos.position, spawnPos.rotation);
					lasInstance.AddForce(spawnPos.forward*laserSpeed);	
					lasInstance.GetComponent<Projectile>().setDamage(damage);
					laserAudio.audio.Play ();
				}
				
				
				else if (weaponModel == WeaponEnum.Rifle)
				{
					handgunAudio.audio.Play ();
					Vector3 fwd = spawnPos.TransformDirection(Vector3.forward);
					fwd *= 20;
					
					Debug.DrawRay(transform.position,fwd);
					
					RaycastHit hit;
					if (Physics.Raycast(transform.position, fwd, out hit, 9999, 1))
					{
						GameObject particleInstance = (GameObject) Instantiate(Projectile.bulletParticle, hit.point, transform.rotation);
						particleInstance.transform.LookAt(transform.position);
						Destroy(particleInstance, particleInstance.particleSystem.duration + 0.01f);
						HealthSystem HP = hit.collider.gameObject.GetComponent<HealthSystem>();
						
						if (HP != null){
							HP.damageHp(damage);
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
		muzzle.SetActive(false);

	}
	
	void Update () {
		
		timer += Time.deltaTime;

		
		
		if (shooting == true) {
			muzzle.SetActive(true);
			muzzleDeltaTime += Time.deltaTime;
			if (muzzleDeltaTime > 0.15) {
				muzzleDeltaTime = 0;
				muzzle.transform.Rotate(new Vector3(muzzle.transform.rotation.x, muzzle.transform.rotation.y, Random.Range(-90,90)));
				shooting= false;
				muzzle.SetActive(false);
				
			}
		}
		
	}

}

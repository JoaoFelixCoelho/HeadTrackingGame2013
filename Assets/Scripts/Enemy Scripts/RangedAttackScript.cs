using UnityEngine;
using System.Collections;

public class RangedAttackScript : MonoBehaviour {
	
	Enemy enemyAttrs;
	public Rigidbody projectile;
	public float attackInterval;
	public float bulletSpeed;
	public int ammo;
	public bool hasCharge;
	//accuracy 0 es lo mas preciso
	public float accuracy;
	public Transform spawnPos;
	public bool target;
	
	public bool isAttacking, readyToAttack = false;
	public float [] animationLength;
	
	protected float attackDeltaTime = 0f;
	
	
	void Start () {
		enemyAttrs = gameObject.GetComponent<Enemy>();
		if(!target) {
			attackInterval = Random.Range(enemyAttrs.baseAttInterval - (enemyAttrs.baseAttInterval/8f), enemyAttrs.baseAttInterval + (enemyAttrs.baseAttInterval/4f));
		}
		ammo += Random.Range((-ammo/2),10);
	}
	
	private void shoot() {
		gameObject.transform.LookAt(new Vector3(Enemy.player.transform.position.x, transform.position.y, Enemy.player.transform.position.z));
		attackDeltaTime += Time.deltaTime;
		if (ammo>0) {	
			if(!target) {
				if(!enemyAttrs.anim.isPlaying) {
					enemyAttrs.anim.Play(enemyAttrs.type + "Idle");
				}
				
				if(hasCharge) {
					if(attackDeltaTime  >= attackInterval - animationLength[0]) {
						if (enemyAttrs.anim.IsPlaying(enemyAttrs.type + "Idle")) {
							enemyAttrs.anim.CrossFade(enemyAttrs.type + "AttackCharge");
							if(enemyAttrs.type=="flubber") {
								gameObject.transform.FindChild("Sphere001").gameObject.SetActive(true);
							}
						}
					}
				}
			}
					
			
			
			if (attackDeltaTime >= attackInterval) {				
				
				Rigidbody tmpProjectile;
				if(!target) {
					if (enemyAttrs.type=="flubber") {
						Transform sphere = gameObject.transform.FindChild("Sphere001");
						tmpProjectile = (Rigidbody) Instantiate(projectile, sphere.position, sphere.rotation);
						sphere.gameObject.SetActive(false);
					}
					else {
						tmpProjectile = (Rigidbody) Instantiate(projectile, spawnPos.transform.position, spawnPos.transform.rotation);
					}
				}
				
				else {
					tmpProjectile = (Rigidbody) Instantiate(projectile, spawnPos.transform.position, spawnPos.transform.rotation);
				}
				
				Vector3 velocityVector = Enemy.player.transform.position - spawnPos.transform.position;
				
				//aplicar la accuracy
				velocityVector.x += Random.Range(-accuracy,accuracy);
				velocityVector.y += Random.Range(-accuracy/4,accuracy/4);
				velocityVector.z += Random.Range(0,accuracy);
				tmpProjectile.AddForce(velocityVector.normalized * bulletSpeed);
				
				if(!target) {
					enemyAttrs.anim.CrossFade(enemyAttrs.type + "Attack");
				}
				int damage;
				if(!target) {
					damage=enemyAttrs.damage;
				}
				else {
					damage=50;
				}
				tmpProjectile.GetComponent<Projectile>().setDamage(damage);
				attackDeltaTime=0f;
				ammo--;
				
				//enemyAttrs.anim.CrossFade(enemyAttrs.type + "Idle");
				
			}
		}
		else {
			//cuando se le acaban las balas, que se haga melee
			gameObject.AddComponent<MeleeAttackScript>();
			enemyAttrs.anim.Play();
			Destroy(this);
		}	
	}
	
	/*public void setPrefab(Rigidbody prefab) {
		this.projectile = prefab;	
	}*/
	
	
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!target) {
			if (enemyAttrs.insideAttZone) {
				shoot();
			}
		}
		else {
			shoot();	
		}
	
	}
}

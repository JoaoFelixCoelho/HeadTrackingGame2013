using UnityEngine;
using System.Collections;

public class RangedAttackScript : MonoBehaviour {
	
	Enemy enemyAttrs;
	public Rigidbody projectile;
	public float attackInterval;
	public float bulletSpeed;
	public int ammo;
	//accuracy 0 es lo mas preciso
	public float accuracy;
	
	public bool isAttacking, readyToAttack = false;
	public float [] animationLength;
	
	protected float attackDeltaTime = 0f;
	
	
	void Start () {
		enemyAttrs = gameObject.GetComponent<Enemy>();
		attackInterval = Random.Range(enemyAttrs.baseAttInterval - (enemyAttrs.baseAttInterval/8f), enemyAttrs.baseAttInterval + (enemyAttrs.baseAttInterval/4f));
		ammo += Random.Range((-ammo/2),10);
	}
	
	private void shoot() {
		gameObject.transform.LookAt(new Vector3(Enemy.player.transform.position.x, transform.position.y, Enemy.player.transform.position.z));
		attackDeltaTime += Time.deltaTime;
		if (ammo>0) {	
			
			if(!enemyAttrs.anim.isPlaying) {
				enemyAttrs.anim.Play(enemyAttrs.type + "Idle");
			}
			
			if(attackDeltaTime  >= attackInterval - animationLength[0]) {
				if (enemyAttrs.anim.IsPlaying(enemyAttrs.type + "Idle")) {
					enemyAttrs.anim.CrossFade(enemyAttrs.type + "AttackCharge");
					if(enemyAttrs.type=="flubber") {
						gameObject.transform.FindChild("Sphere001").gameObject.SetActive(true);
					}
				}
			}
					
			
			
			if (attackDeltaTime >= attackInterval) {				
				
				Rigidbody tmpProjectile;
				if (enemyAttrs.type=="flubber") {
					Transform sphere = gameObject.transform.FindChild("Sphere001");
					tmpProjectile = (Rigidbody) Instantiate(projectile, sphere.position, sphere.rotation);
					sphere.gameObject.SetActive(false);
				}
				else {
					tmpProjectile = (Rigidbody) Instantiate(projectile, transform.position, transform.rotation);
				}
				
				Vector3 velocityVector = Enemy.player.transform.position - transform.position;
				
				//aplicar la accuracy
				velocityVector.x += Random.Range(-accuracy,accuracy);
				velocityVector.y += Random.Range(-accuracy/2,accuracy/2);
				velocityVector.z += Random.Range(0,accuracy);
				tmpProjectile.AddForce(velocityVector.normalized * bulletSpeed);
				
				enemyAttrs.anim.CrossFade(enemyAttrs.type + "Attack");
				
				
				tmpProjectile.GetComponent<Projectile>().setDamage(enemyAttrs.damage);
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
		if (enemyAttrs.insideAttZone) {
			shoot();
		}
	
	}
}

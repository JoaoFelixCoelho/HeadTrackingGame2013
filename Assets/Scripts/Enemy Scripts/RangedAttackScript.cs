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
			if (attackDeltaTime >= attackInterval) {
				
				if (enemyAttrs.GetComponent<Enemy>().type == "flubber") {
					
				}
				
				
				Vector3 velocityVector = Enemy.player.transform.position - transform.position;
				Rigidbody tmpProjectile = (Rigidbody) Instantiate(projectile, transform.position, transform.rotation);
				
				//aplicar la accuracy
				velocityVector.x += Random.Range(-accuracy,accuracy);
				velocityVector.y += Random.Range(-accuracy/2,accuracy);
				velocityVector.z += Random.Range(0,accuracy);
				
				tmpProjectile.AddForce(velocityVector * bulletSpeed);
				
				tmpProjectile.GetComponent<Projectile>().setDamage(enemyAttrs.damage);
				attackDeltaTime=0f;
				ammo--;
			}
		}
		else {
			//cuando se le acaban las balas, que se haga melee
			gameObject.AddComponent<MeleeAttackScript>();
			Destroy(this);
		}	
	}
	
	public void setPrefab(Rigidbody prefab) {
		this.projectile = prefab;	
	}
	
	
	
	// Update is called once per frame
	void Update () {
		if (enemyAttrs.insideAttZone) {
			shoot();
		}
	
	}
}

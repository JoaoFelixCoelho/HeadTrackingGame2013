using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	public float timeToDestroy;
	public string target;
	public int damage;
	
	// Use this for initialization
	void Start () {
	}
	
	
	public void setDamage(int dmg) {
		this.damage = dmg;
	}	
	
	void OnTriggerEnter(Collider col){
		HealthSystem colTMP = col.GetComponent<HealthSystem>();
		if (colTMP != null) {
			//para que no se auto-saque vida
			if (colTMP.isPlayer && target=="Player" || !colTMP.isPlayer && target=="Enemy" ) {
				colTMP.damageHp(damage);	
				Destroy(gameObject);
			}
		}

	}
		


	
	
	
	// Update is called once per frame
	void Update() {
		Destroy (gameObject, timeToDestroy);
	}
}

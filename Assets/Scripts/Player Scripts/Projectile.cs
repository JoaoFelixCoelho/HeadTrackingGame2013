using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	public float timeToDestroy;
	public enum TargetEnum {Player = 0, Enemy = 1};
	public TargetEnum target = 0;
	public int damage;
	public static GameObject hitParticle = (GameObject) Resources.Load("Prefabs/Particles/LaserHit");
	
	// Use this for initialization
	void Start () {
	}
	
	
	public void setDamage(int dmg) {
		this.damage = dmg;
	}	
	
	void OnTriggerEnter(Collider col){
				
		if(col.tag != "DevLayer") {
			HealthSystem colTMP = col.GetComponent<HealthSystem>();
			if (colTMP != null) {
				//para que no se auto-saque vida
				if (colTMP.type == HealthSystem.Type.Player && target == TargetEnum.Player || colTMP.type == HealthSystem.Type.Enemy && target == TargetEnum.Enemy ) {
					colTMP.damageHp(damage);
					Destroy(gameObject);
				}
				
			}
		}

	}
		


	
	
	
	// Update is called once per frame
	void Update() {
		Destroy (gameObject, timeToDestroy);
	}
}

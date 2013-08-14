using UnityEngine;
using System.Collections;

public class BehaviourCollider : MonoBehaviour {
	
	
	void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == "Enemy") {
			Enemy inst = (Enemy) col.gameObject.GetComponent("Enemy");
			inst.modifyRoute();
		}
		
	}
	
	
	void Start () {
	}
	
	void Update () {
	}
}

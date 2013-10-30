using UnityEngine;
using System.Collections;

public class CheapPhysics : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Mathf.Round(transform.position.y) == -1f) {
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}
	}
}

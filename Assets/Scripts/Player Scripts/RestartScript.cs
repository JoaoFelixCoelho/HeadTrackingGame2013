using UnityEngine;
using System.Collections;

public class RestartScript : MonoBehaviour {

	public float timeToRestart = 1f;
	public float i = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		i += Time.deltaTime;
		if (i >= timeToRestart) {
			Application.LoadLevel(0);
		}
	
	}
}

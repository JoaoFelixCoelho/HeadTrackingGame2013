using UnityEngine;
using System.Collections;

public class RestartScript : MonoBehaviour {

	public float timeToRestart = 1.5f;
	public float i = 0f;
	public bool closedConnection = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		i += Time.deltaTime;
		if (!closedConnection) {
			Enemy.player.GetComponent<TCP>().closeConnection();
		}
		if (i >= timeToRestart) {
			Application.LoadLevel(0);
		}
	
	}
}

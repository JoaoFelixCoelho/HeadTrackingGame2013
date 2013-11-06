using UnityEngine;
using System.Collections;

public class WeaponSpawn : MonoBehaviour {
	
	private Transform finalPos;
	public GameObject startPos;
	private bool translate = false;
	private Vector3 perSecondDistance;
	public float translateTime;
	// Use this for initialization
	void Start () {
		finalPos = transform;
		startPos = GameObject.Find("WeaponSpawner");
		transform.position = startPos.transform.position;
		gameObject.transform.parent = startPos.transform;
		startPos.particleSystem.Play();
		gameObject.transform.localScale *= 3;
	}
	
	public void transitionToPlayer() {
		gameObject.animation.Stop();
		startPos.particleSystem.Stop();	
		perSecondDistance = (Enemy.player.transform.position - transform.position)/translateTime;
		translate = true;
	}
	
	
	// Update is called once per frame
	void Update () {
		
		if (translate) {
			transform.position += perSecondDistance * Time.deltaTime;
			if (startPos.animation.isPlaying) {
				startPos.animation.Stop();
			}
			if (Mathf.Round(transform.position.z) == Mathf.Round(Enemy.player.transform.position.z)) {
				PlayerBehave tmpPlayer = Enemy.player.GetComponent<PlayerBehave>();
				tmpPlayer.laser = gameObject;	
				Round.weaponPicked = true;
				transform.localScale /= 3;
				gameObject.collider.enabled = false;
				gameObject.transform.parent = Enemy.player.transform.FindChild("Main Camera");
				gameObject.SetActive(false);
				tmpPlayer.cambioArma();
				this.enabled = false;
			}
		}
		
	
	}
}

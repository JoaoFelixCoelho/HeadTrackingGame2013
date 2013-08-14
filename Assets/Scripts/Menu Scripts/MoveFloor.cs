using UnityEngine;
using System.Collections;

public class MoveFloor: MonoBehaviour {
    
	public float scrollSpeed = 0.5F;
    
	void Update() {
        float offset = Time.time * scrollSpeed;
        renderer.material.mainTextureOffset = new Vector2(0, offset);
    }
}
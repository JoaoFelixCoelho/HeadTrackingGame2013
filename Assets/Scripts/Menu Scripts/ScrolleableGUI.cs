using UnityEngine;
using System.Collections;

public class ScrolleableGUI : MonoBehaviour {
	
	private bool  scrolling = false;
	private float scrollTimer = 0f;
	private bool scrollDown, scrollUp = false;
	int scrollIndex, scrollLimit = 0;
	public int noScrollTimes = 0;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool downKey = Input.GetKey(KeyCode.DownArrow);
		bool upKey   = Input.GetKey(KeyCode.UpArrow);		
		
		checkScroll();
		if (downKey && !scrolling) {
			scrollTimer = 0f;
			scrollDown = true;
		}
		
		if (upKey && !scrolling) {
			scrollTimer = 0f;
			scrollUp = true;
		}		
		
		
	}
	
		
	private void checkScroll() {

		
			if (scrollDown || scrollUp) {
				scrollTimer += Time.deltaTime;
				scrolling = true;
				if (scrollTimer <= 0.3f) {
					if (scrollDown) {
					
						scrollIndex ++;
						transform.position = new Vector3(transform.position.x, transform.position.y + 0.08f, transform.position.z);
					}
					if (scrollUp) {
						scrollIndex --;
						transform.position = new Vector3(transform.position.x, transform.position.y - 0.08f, transform.position.z);
					}
				
				}
				else  {
					scrollUp   = false;
					scrollDown = false;
					scrolling  = false;	
	
				}
			}
		
	}
	
	
	
}

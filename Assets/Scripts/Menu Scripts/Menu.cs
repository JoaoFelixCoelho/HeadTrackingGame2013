using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	public bool isQuitButton = false;
	
	public bool isOptionsButton = false;

	void OnMouseEnter()
	{
		//gameObject.material.color = Color.red;
	}
	
	void OnMouseExit()
	{
		//gameObject.material.color = Color.gray;
	}
	
	void OnMouseUp()
	{
		if (isQuitButton == true)
		{
			Application.Quit();
		}
		else if (isOptionsButton == true)
		{
			//Application.LoadLevel (3);
		}	
		else
		{
			Application.LoadLevel (1);
		}
	}
}
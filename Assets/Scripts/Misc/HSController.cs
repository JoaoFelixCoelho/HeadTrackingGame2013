using UnityEngine;
using System.Collections;
 
public class HSController : MonoBehaviour
{
    private string secretKey = "martha";
    public string addScoreURL = "http://localhost:8080/score/addscore.php?";
    public string highscoreURL = "http://localhost:8080/score/display.php";
	private bool sentScores = false;
	public string data;
 
	
	
    public IEnumerator PostScores(string name, int score)
    {
		if (!sentScores) {
			sentScores = true;
	        string hash = MD5Test.Md5Sum(name + score + secretKey);
	 
	        string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;
	 
	        WWW hs_post = new WWW(post_url);
	        yield return hs_post;
	 
	        if (hs_post.error != null)
	        {
	            print("Error: " + hs_post.error);
	        }
		}
    }
 
    public IEnumerator GetScores()
    {
        WWW hs_get = new WWW(highscoreURL);
		yield return  hs_get;
		data = hs_get.text;
        if (hs_get.error != null)
        {
			data = "error";
            print("Error: " + hs_get.error);
        }
		
		

		
        /*else
        {
            gameObject.guiText.text = hs_get.text;
        }*/
		
    }
 
}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HSController : MonoBehaviour {
	
	private string secretKey ="devAndre@!"; // Edit this value and make sure it's the same as the one stored on the server

	public Text uiText;
	[HideInInspector] public string addScoreUrl ="http://www.andregamedeveloper.com.br/games/addscore.php?"; //be sure to add a ? to your url
	[HideInInspector] public string highscoreUrl ="http://www.andregamedeveloper.com.br/games/display.php"; 

	public Button postButton;
	public Text postText;
	public GameObject txtText;
	public GameObject sucessText;
	public Text bigScoreText;

	private GameController gameController;
	private bool sent = false;

	private Dictionary<string, string> headers;

	// Use this for initialization
	void Awake () {
		GameObject controllerObj = GameObject.FindWithTag("GameController");
		if (controllerObj != null)
			gameController = controllerObj.GetComponent<GameController> ();

		if (gameController == null)
			Debug.Log ("Can't find game controller");

		// Reset
		postButton.gameObject.SetActive (true);
		txtText.SetActive (true);
		postText.text = "";
		sucessText.SetActive (false);

		bigScoreText.text = "" + gameController.getScore ();

		// Create Readers
		WWWForm form = new WWWForm();
		headers = form.headers;
		headers ["Access-Control-Allow-Credentials"] = "true";
		headers ["Access-Control-Allow-Headers"] = "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time";
		headers ["Access-Control-Allow-Methods"] = "GET, POST, OPTIONS";
		headers ["Access-Control-Allow-Origin"] = "*";

        StartCoroutine(GetScores());
		sent = false;
	}

	public void postScore() {
		if (sent == true)
			return;

		Debug.Log ("postScores!");
		StartCoroutine(PostScores(postText.text, gameController.getScore()));
	}

    // remember to use StartCoroutine when calling this function!
    IEnumerator PostScores(string name, int score)
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
		string hash = secretKey; //Md5Sum(name + score + secretKey);
 
		string post_url = addScoreUrl + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;
		Debug.Log ("URL: " + post_url);

        // Post the URL to the site and create a download object to get the result.
		WWW hs_post = new WWW(post_url, null, headers);
        yield return hs_post; // Wait until the download is done
 
		if (hs_post.error != null) {
			print ("There was an error posting the high score: " + hs_post.error);
		} else {
			sent = true;
			Debug.Log ("Score Sent!");

			postButton.gameObject.SetActive (false);
			txtText.SetActive (false);
			sucessText.SetActive (true);

			StartCoroutine(GetScores());
		}
    }
 
    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    IEnumerator GetScores()
    {
		uiText.text = "Loading Scores";

		// Post the URL to the site and create a download object to get the result.
		WWW hs_get = new WWW(highscoreUrl, null, headers);
		foreach(KeyValuePair<string, string> obj in headers) {
			Debug.Log (obj.Key + " : " + obj.Value);
		}
        yield return hs_get;
 
        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
			uiText.text = hs_get.text; // this is a GUIText that will display the scores in game.
        }
    }
	
	// Generate MD5 Hash
	public  string Md5Sum(string strToEncrypt)  {
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);
	 
		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);
	 
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
	 
		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}
	 
		return hashString.PadLeft(32, '0');
	}


}

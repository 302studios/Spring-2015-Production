using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerInfo : MonoBehaviour {

	public Image healthFade;
	public float currHealth = 0;
	float maxHealth = 100;
	public int speakersComplete;

	//AudioSource source;

	public Text name;
	public Text num;

	public bool hasMetronome;
	public bool hasLightController;
	public bool hasSonicCompressor;
	
	// Use this for initialization
	void Start () {
	
		currHealth = maxHealth;
		gadgetCheck ();

		healthFade = GameObject.Find ("healthFade").GetComponent<Image>();

	}

	
	// Update is called once per frame
	void Update () {



		if(currHealth < 100)
			currHealth += 0.1f;
		if (currHealth < 0)
			Application.LoadLevel ("Game Over");

		float trans = ((maxHealth - currHealth) / 300f);
		healthFade.color = new Color (1, 0, 0, trans);
		
	}

	public void gadgetCheck(){

		if ((PlayerPrefs.GetInt ("Metronome")) == 1) {
			hasMetronome = true;
		} else {
			hasMetronome = false;
		}

		if ((PlayerPrefs.GetInt ("Light Controller")) == 1) {
			hasLightController = true;
		} else {
			hasLightController = false;
		}


		if ((PlayerPrefs.GetInt ("Sonic Compressor")) == 1) {
			hasSonicCompressor = true;
		} else {
			hasSonicCompressor = false;
		}


	}
	
}

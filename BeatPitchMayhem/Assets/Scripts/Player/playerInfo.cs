using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerInfo : MonoBehaviour {

	public Image healthFade;
	public float currHealth = 0;
	float maxHealth = 100;
	public int keysCollected = 0;
	bool unlocked;

	Animator doorAnim;

	//AudioSource source;

	public Text name;
	public Text num;

	// Use this for initialization
	void Start () {
	
		currHealth = maxHealth;
		//doorAnim = GameObject.Find ("Doors").GetComponent<Animator> ();
		//source = GetComponent<AudioSource>;
		unlocked = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(currHealth < 100)
			currHealth += 0.1f;
		if (currHealth < 0)
			Application.LoadLevel ("Game Over");

		float trans = ((maxHealth - currHealth) / 300f);
		healthFade.color = new Color (1, 0, 0, trans);



		if (keysCollected == 2 && !unlocked) {
			// Play sound and unlock door.
			name.text = "Door Unlocked";
			num.text = "";
			doorAnim.SetTrigger("Unlocked");
			audio.Play();
			unlocked = true;
		} else if(!unlocked){
			num.text = ("| " + keysCollected);
		}

	}

	void OnTriggerEnter(Collider col){

		if (col.tag == "Key") {
			keysCollected++;
			Destroy(col.gameObject);
		}

	}
}

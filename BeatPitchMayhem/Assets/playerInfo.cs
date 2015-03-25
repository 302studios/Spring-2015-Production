using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerInfo : MonoBehaviour {

	public Image healthFade;
	public float currHealth = 0;
	float maxHealth = 100;

	// Use this for initialization
	void Start () {
	
		currHealth = maxHealth;

	}
	
	// Update is called once per frame
	void Update () {

		if(currHealth < 100)
			currHealth += 0.05f;

		float trans = ((maxHealth - currHealth) / 300f);
		healthFade.color = new Color (1, 0, 0, trans);


	}
}

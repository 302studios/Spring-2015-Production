using UnityEngine;
using System.Collections;

public class movingTexture : MonoBehaviour {

	Material theMat;
	public float moveRate;
	public worldInfo theWorld;
	Color theColor;
	float offX;
	float offY;
	float transparency;
	bool subtracting;

	// Use this for initialization
	void Start () {
	
		theWorld = GameObject.Find ("World").GetComponent<worldInfo>();
		theMat = this.gameObject.renderer.material;
		theColor = theWorld.theColor;
		transparency = .5f;
		theColor = new Color (theColor.r, theColor.g, theColor.b, transparency);
		theMat.SetColor("_TintColor", theColor);
		offX = 0f;
		offY = 0f;
		subtracting = true;
	}
	
	// Update is called once per frame
	void Update () {


		if (!subtracting) {
			if (transparency < .8f) {
				transparency += .03f;
			}
			else
				subtracting = true;
		}
		if (subtracting) {
			if (transparency > .4f) {
				transparency -= .03f;
			}
			else
				subtracting = false;
		}

		theColor = theWorld.theColor;
		theColor = new Color (theColor.r, theColor.g, theColor.b, transparency);
		theMat.SetColor("_TintColor", theColor);
	
		offX += moveRate * .001f;
		offY += moveRate * .002f;

		if (offX > 1f)
			offX = 0f;
		if (offY > 1f)
			offY = 0f;

		theMat.mainTextureOffset = new Vector2 (offX, offY);


	}
}

using UnityEngine;
using System.Collections;

public class movingTexture : MonoBehaviour {

	Material theMat;
	public float moveRate;
	public worldInfo theWorld;
	float offX;
	float offY;

	// Use this for initialization
	void Start () {
	
		theWorld = GameObject.Find ("World").GetComponent<worldInfo>();
		theMat = this.gameObject.renderer.material;
		theMat.SetColor("_TintColor", theWorld.theColor);
		offX = 0f;
		offY = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	
		offX += moveRate * .001f;
		offY += moveRate * .002f;

		if (offX > 1f)
			offX = 0f;
		if (offY > 1f)
			offY = 0f;

		theMat.mainTextureOffset = new Vector2 (offX, offY);


	}
}

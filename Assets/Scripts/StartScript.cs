using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour {

	public TextMesh tm;
	public float textAlphaStep;

	private float alpha = 0.0f;
	private bool rising = true;
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (rising) {
			alpha += textAlphaStep;
			if (alpha >= 1.0f) {
				rising = false;
			}
		} else {
			alpha -= textAlphaStep;
			if (alpha < 0.0f) {
				rising = true;
			}
		}
		Color c = tm.color;
		c.a = alpha;
		tm.color = c;
	}
}

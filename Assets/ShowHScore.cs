using UnityEngine;
using System.Collections;

public class ShowHScore : MonoBehaviour {

	// Use this for initialization
	public RocketManager3 rocket;
	TextMesh tm;
	void Start () {
		tm = GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Z)) PlayerPrefs.SetInt("hScore",0);
		tm.text=((int)Mathf.Max(rocket.transform.position.y,PlayerPrefs.GetInt("hScore",0))).ToString("D4");
	}
}

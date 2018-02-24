using UnityEngine;
using System.Collections;

public class ShowScores : MonoBehaviour {

	// Use this for initialization
	public RocketManager3 rocket;
	TextMesh tm;
	void Start () {
		tm = GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		tm.text=((int)rocket.transform.position.y).ToString("D4");
	}
}

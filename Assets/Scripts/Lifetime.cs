using UnityEngine;
using System.Collections;

public class Lifetime : MonoBehaviour {

	// Use this for initialization
	Transform rock;
	void Start () {
		rock = FindObjectOfType<RocketManager3>().transform;
	}
	// Update is called once per frame
	void Update () {
		if (rock.position.y-2.8f>transform.position.y)
			Destroy (gameObject);
	}
}

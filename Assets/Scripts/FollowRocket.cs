using UnityEngine;
using System.Collections;

public class FollowRocket : MonoBehaviour {
	public Transform rocket;


	// Use this for initialization
	float temp;
	void Start () {
		FindObjectOfType <Camera>().aspect=9f/16f;
		temp=rocket.position.y-transform.position.y;



	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = new Vector3(transform.position.x,rocket.position.y-temp,transform.position.z);



	}
}

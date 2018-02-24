using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SetAspect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		FindObjectOfType <Camera>().aspect=9f/16f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.touchCount > 0 || Input.anyKeyDown)
			SceneManager.LoadScene (1);
			
	}
}

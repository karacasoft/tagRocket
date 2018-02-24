using UnityEngine;
using System.Collections;

public class UfoBehaviour : MonoBehaviour {

	public float speed,nearOffset;
	float xMin,xMax;
	Transform rock;
	void Start () {
		// DestroyImmediate (this);
		rock = FindObjectOfType<RocketManager3>().transform;
		if (Random.Range (0, 2) == 0)
			speed *= -1;
		xMin=rock.GetComponent<RocketManager3>().worldXMin+nearOffset;
		xMax=rock.GetComponent<RocketManager3>().worldXMax-nearOffset;
	}

	void Update () {
		transform.position = transform.position + Vector3.right*speed*Time.deltaTime;
		if(speed>0 && transform.position.x>xMax)
			speed *= -1;
		if(speed<0 && transform.position.x<xMin)
			speed *= -1;
			
	}
}

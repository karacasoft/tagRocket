using UnityEngine;
using System.Collections;

public class RocketManager : MonoBehaviour {

	// Use this for initialization
	Rigidbody2D rb;
	public float force,turnSpeed,lengthPerLevel;
	public int currentLevel;
	public float[] maxVelocity;
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		ApplyDefaultForce ();
		if (Controls.InputLeft ())
			Turn (turnSpeed);
		if (Controls.InputRight ())
			Turn (-turnSpeed);
	}
	void Turn(float angle)
	{
		transform.Rotate (new Vector3(0,0,angle)*Time.deltaTime);
	}


	void ApplyDefaultForce()
	{
		rb.AddForce (transform.rotation * Vector2.up * force);
		if (rb.velocity.magnitude > maxVelocity[currentLevel])
			rb.velocity = rb.velocity/rb.velocity.magnitude*maxVelocity[currentLevel];
	}
}

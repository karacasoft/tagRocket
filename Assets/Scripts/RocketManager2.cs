using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RocketManager2 : MonoBehaviour {

	// Use this for initialization
	Rigidbody2D rb;
	public float appliedResistancePerDegrees,inputAppliedTorque,YmoveVelocity,XmoveVelocity;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.R))
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);

		ApplyInputTorque ();

		ApplyRotationResistance ();

		UpdateVelocityDirection ();
	
	}

	void ApplyInputTorque()
	{
		if(Controls.InputLeft())
			rb.AddTorque (inputAppliedTorque);
		if(Controls.InputRight())
			rb.AddTorque (-inputAppliedTorque);
	}

	void ApplyRotationResistance ()
	{
		if(GetCurrentAngle()>0)
			rb.AddTorque (-appliedResistancePerDegrees*GetCurrentAngle()*GetCurrentAngle());
		else
			rb.AddTorque (appliedResistancePerDegrees*GetCurrentAngle()*GetCurrentAngle());
	}

	void UpdateVelocityDirection ()
	{
		Vector2 Direction = (transform.rotation * Vector3.up);
		rb.velocity = new Vector2(XmoveVelocity*Direction.x,YmoveVelocity);
	}

	float GetCurrentAngle()
	{
		if (transform.rotation.eulerAngles.z < 180)
			return Mathf.Asin(transform.rotation.eulerAngles.z);
		else
			return Mathf.Asin(transform.rotation.eulerAngles.z-360);
	}
}

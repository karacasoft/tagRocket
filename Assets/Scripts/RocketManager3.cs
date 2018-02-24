using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RocketManager3 : MonoBehaviour {

	// Use this for initialization
	Rigidbody2D rb;
	Camera cam;
	public float forceX,speed,speedFormulaAdd,speedFormulaStart,speedFormula1,speedFormula2,returnForce1,returnForce2,angleMul,nearBorderLimit;
	public float worldXMin,worldXMax; // Ekran sinirlari
	float deathTime;
	bool gameOver=false/*,started=false*/;

	private PersistentData pers;

	public Transform musicOnTrigger;
	public Transform musicOffTrigger;

	private bool musicButtonPressed = false;

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D> ();
		cam = FindObjectOfType<Camera> ();
		//Debug.Log (cam.rect.xMax + " " + cam.rect.xMin);
		worldXMin=cam.ScreenToWorldPoint (new Vector3 (cam.rect.xMin*cam.pixelWidth, 0,0)).x+nearBorderLimit; // Ekran sinirlarini hesapla
		worldXMax=cam.ScreenToWorldPoint (new Vector3 (cam.rect.xMax*cam.pixelWidth, 0,0)).x-nearBorderLimit;
	}

	void Start () {
		pers = GameObject.FindGameObjectWithTag ("Persistent").GetComponent<PersistentData> ();

		Debug.Log (pers);

		pers.OnRestart ();

		if (PlayerPrefs.GetInt ("sound", 0) == 0) {
			musicOnTrigger.GetComponent<SpriteRenderer> ().enabled = false;
			musicOffTrigger.GetComponent<SpriteRenderer> ().enabled = true;
			musicOnTrigger.GetComponent<BoxCollider2D> ().enabled = false;
			musicOffTrigger.GetComponent<BoxCollider2D> ().enabled = true;
		}

	}

	bool soundButtonPressed() {
		for (var i = 0; i < Input.touchCount; ++i) 
		{
			//Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			Vector2 pos = Input.GetTouch(i).position;
			if (Input.GetTouch(i).phase == TouchPhase.Began) 
				//if(Input.GetMouseButtonDown(0))
			{
				RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

				// RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
				if(hitInfo)
				{
					Debug.Log( hitInfo.transform.gameObject.name );
					if (hitInfo.transform.gameObject.Equals (musicOnTrigger.gameObject)) {
						GameObject.Find ("Persistent").GetComponent<PersistentData> ().OnSoundPrefChange (false);
						musicOnTrigger.GetComponent<SpriteRenderer> ().enabled = false;
						musicOffTrigger.GetComponent<SpriteRenderer> ().enabled = true;
						musicOnTrigger.GetComponent<BoxCollider2D> ().enabled = false;
						musicOffTrigger.GetComponent<BoxCollider2D> ().enabled = true;
						musicButtonPressed = true;
						return true;
					} else if (hitInfo.transform.gameObject.Equals (musicOffTrigger.gameObject)) {
						GameObject.Find ("Persistent").GetComponent<PersistentData> ().OnSoundPrefChange (true);
						musicOnTrigger.GetComponent<SpriteRenderer> ().enabled = true;
						musicOffTrigger.GetComponent<SpriteRenderer> ().enabled = false;
						musicOnTrigger.GetComponent<BoxCollider2D> ().enabled = true;
						musicOffTrigger.GetComponent<BoxCollider2D> ().enabled = false;
						musicButtonPressed = true;
						return true;
					}

				}
			}
		}
		return false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.M)) {
			pers.OnSoundPrefChange (!pers.soundOn);
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
		/*if (!started)
		{
			if (Input.touchCount > 0)
			{
				started = true;

			}
			return;
		}*/
		musicButtonPressed = soundButtonPressed ();
		if (gameOver && Time.time - deathTime > 1)
		{
			gameObject.GetComponentInChildren<ParticleSystem>().Stop();
			if (Input.touchCount > 0 && !musicButtonPressed) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
		if (gameOver) return; // Oyun bittiyse hesaplamalari yapma
		UpdateDirection ();
		InputFunc ();
		rb.velocity=new Vector2(rb.velocity.x,speed);
		ApplyResistance ();
		StayInScreen ();
		speed = Mathf.Log(transform.position.y+speedFormulaStart,speedFormula1)*speedFormula2+speedFormulaAdd;
	}

	void InputFunc()
	{
		if (Input.touchCount > 0) {
			if (musicButtonPressed) {
				return;
			}
		} else {
			if (musicButtonPressed) {
				musicButtonPressed = false;
			}
		}
		if (Controls.InputLeft ())
			rb.AddForce (-forceX*Vector2.right);
		if (Controls.InputRight ())
			rb.AddForce (forceX*Vector2.right);
	}

	void StayInScreen ()
	{
	//	Debug.Log (worldXMin + " " + worldXMax);
		if (transform.position.x < worldXMin)
			transform.position =new Vector3(worldXMin,transform.position.y,transform.position.z);
		if (transform.position.x > worldXMax)
			transform.position = new Vector3(worldXMax,transform.position.y,transform.position.z);
	}

	void ApplyResistance () //GERI CEVIRIC KUVVETI HESAPLAR
	{
		
		rb.AddForce (Mathf.Pow(returnForce1,Mathf.Abs(rb.velocity.x))/*velocity degil*/*rb.velocity.x*returnForce2/speed/*!!!!!!!!!!!!!!!!!*/*Vector2.left);

	}
	void UpdateDirection ()
	{
		transform.rotation =  Quaternion.Euler(0,0,(Mathf.Atan2(rb.velocity.y,rb.velocity.x)*180/Mathf.PI-90)*angleMul);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		GameOver ();
	}

	void GameOver()
	{
		PlayerPrefs.SetInt("hScore",(int)Mathf.Max(PlayerPrefs.GetInt("hScore",0),transform.position.y));
		pers.OnDeath ();
		deathTime=Time.time;
		gameOver=true;
		Destroy (GetComponent<SpriteRenderer>());
		transform.Rotate (new Vector3(90,0,0));
		rb.isKinematic = true;
		//SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

}

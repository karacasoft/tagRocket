using UnityEngine;
using System.Collections;

public class PersistentData : MonoBehaviour {

	public bool soundOn{ get; private set;}
	private AudioSource fgSrc;
	private AudioSource bgSrc;

	void Awake() {
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {
		fgSrc = GetComponent<AudioSource> ();
		bgSrc = GameObject.Find("Musicer").GetComponent<AudioSource> ();
		soundOn = (PlayerPrefs.GetInt ("sound", 0) == 0) ? false : true;
		if (!soundOn) {
			fgSrc.Stop ();
			bgSrc.Stop ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	public void OnSoundPrefChange (bool soundOn) {
		this.soundOn = soundOn;
		PlayerPrefs.SetInt ("sound", soundOn ? 1 : 0);
		PlayerPrefs.Save ();

		if (soundOn) {
			fgSrc.Play ();
			bgSrc.Play ();
		} else {
			fgSrc.Stop ();
			bgSrc.Stop ();
		}
	}

	public void OnDeath() {
		fgSrc.volume = 0.0f;
	}

	public void OnRestart() {
		fgSrc.volume = 1.0f;
	}
}

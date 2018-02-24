using UnityEngine;
using System.Collections.Generic;

public class ObstacleSetManager : MonoBehaviour {

	// Use this for initialization
	public Transform rocket;
	public Transform[] obstacles;
	public List<ObstacleSet> obstacleSets;
	public float lengthBetweenSets;
	Queue<float> targets;
	float lastY=6;
	Dictionary<string,Transform> ObstacleLookUp;
	void Start()
	{
		DestroyEditorObstacles ();
		FillObstacleLookUp ();
		targets = new Queue<float> ();
		targets.Enqueue (0f);
		targets.Enqueue (0f);
	}
	void Update()
	{
		if (isTargetReached())
		{
			targets.Dequeue ();
			SpawnNewObstacleSet (lastY);
			targets.Enqueue(lastY);
		}
	}

	bool isTargetReached()
	{
		return rocket.position.y > targets.Peek ();
	}

	void SpawnNewObstacleSet (float refY)
	{
		int setID=Random.Range(0,obstacleSets.Count);
		bool isReverse = false;
		if (Random.Range (0, 2) == 0)
			isReverse = true;
		Debug.Log ("Spawning Set "+setID);
		ObstacleSet oSet=obstacleSets[setID];
		foreach(ObstacleData oData in oSet.obstacles)
		{
			float revX = oData.x;
			if (isReverse) revX = rocket.gameObject.GetComponent<RocketManager3> ().worldXMax + rocket.gameObject.GetComponent<RocketManager3> ().worldXMin - oData.x;
			GameObject spawnedObs = (GameObject)Instantiate (ObstacleLookUp[oData.type].gameObject,new Vector3(revX,oData.y+refY+lengthBetweenSets,0),Quaternion.identity);
			if (isReverse) spawnedObs.transform.localScale = new Vector3 (-spawnedObs.transform.localScale.x, spawnedObs.transform.localScale.y, spawnedObs.transform.localScale.z);
			spawnedObs.transform.SetParent (transform);
			spawnedObs.AddComponent<Lifetime> ();
			lastY = Mathf.Max (lastY,oData.y+refY+lengthBetweenSets);
		}
	}

	void FillObstacleLookUp()
	{
		ObstacleLookUp = new Dictionary<string, Transform> ();
		foreach (Transform tr in obstacles)
			ObstacleLookUp[tr.tag]=tr;
	}
	void DestroyEditorObstacles ()
	{
		GameObject[] objectsToDestroy = new GameObject[transform.childCount];
		int j=0;
		foreach (Transform childTr in transform)
		{
			objectsToDestroy[j]=childTr.gameObject;
			j++;
		}
		foreach (GameObject obj in objectsToDestroy)
			Destroy (obj);
	}
}

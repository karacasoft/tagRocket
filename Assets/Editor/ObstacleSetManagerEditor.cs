using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ObstacleSetManager))]
public class ObstacleSetManagerEditor : Editor
{

	public override void OnInspectorGUI()
	{
		//base.OnInspectorGUI ();
		serializedObject.Update();
		ObstacleSetManager myTarget= (ObstacleSetManager)target;
		EditorGUI.BeginChangeCheck ();
		SerializedProperty rock = serializedObject.FindProperty ("rocket");
		EditorGUILayout.PropertyField(rock,true);
		if(EditorGUI.EndChangeCheck ())
			serializedObject.ApplyModifiedProperties();
		GUILayout.Label ("Kullanilacak butun engellerin prefab'leri buraya referanslanmak zorunda.\n Ayni zamanda her engel turune farkli bir tag koymaniz gerekiyor.");
		EditorGUI.BeginChangeCheck ();
		SerializedProperty obst = serializedObject.FindProperty ("obstacles");
		EditorGUILayout.PropertyField(obst,true);

		/*if (GUILayout.Button ("Create New Prefab Slot"))
		{
			Undo.RecordObject (myTarget,"Undo Creating New Prefab Slot");
			myTarget.obstacles(ScriptableObject.CreateInstance<ObstacleSet> ());
			EditorUtility.SetDirty(target);
		}*/
		if(EditorGUI.EndChangeCheck ())
			serializedObject.ApplyModifiedProperties();

		GUILayout.Label ("Burdan setleri sahneye geri alip editleyip tekrar uzerine yazabilirsiniz.");
		EditorGUI.BeginChangeCheck ();
		if (myTarget.obstacleSets==null)
			myTarget.obstacleSets = new List<ObstacleSet> ();
		int i = 0;
		foreach (ObstacleSet obs in myTarget.obstacleSets)
		{
			i++;
			EditorGUILayout.BeginHorizontal ();
			GUILayout.Label ("Set "+i);
			if (GUILayout.Button ("SAVE"))
			{
				Undo.RecordObject (myTarget,"Undo Saving Over Set " + i); // DOESNT WORK?
				Debug.Log ("Deleting "+obs.obstacles.Count+" obstacles in Set "+i);
				obs.obstacles.Clear();
				foreach (Transform childTr in myTarget.gameObject.transform)
				{
					ObstacleData OD = ScriptableObject.CreateInstance<ObstacleData> ();
					Debug.Log ("Saving a "+childTr.tag+" on Set "+i);
					OD.SetValues (childTr.tag,childTr.position.x,childTr.position.y);
					obs.obstacles.Add(OD);
				}
				EditorUtility.SetDirty(target);
			}
			if (GUILayout.Button ("LOAD"))
			{
				Undo.RecordObject (myTarget.gameObject,"Undo Clearing Scene Obstacles");
				GameObject[] objectsToDestroy = new GameObject[myTarget.gameObject.transform.childCount];
				int j=0;
				foreach (Transform childTr in myTarget.gameObject.transform)
				{
					objectsToDestroy[j]=childTr.gameObject;
					j++;
				}
				foreach(GameObject obj in objectsToDestroy)
					Undo.DestroyObjectImmediate(obj);
				Undo.RecordObject (myTarget.gameObject,"Undo Loading Obstacles To Scene");
				foreach (ObstacleData oData in obs.obstacles)
				{
					GameObject OB=(GameObject)Instantiate (FindObstacleWithTag (myTarget.obstacles, oData.type).gameObject, new Vector3 (oData.x, oData.y, 0), Quaternion.identity);
					OB.transform.SetParent (myTarget.transform);
				}
				EditorUtility.SetDirty(target);
			}

			if (GUILayout.Button ("DELETE"))
			{
				Undo.RecordObject (myTarget,"Undo Deleting Set " + i);
				myTarget.obstacleSets.Remove (obs);
				EditorUtility.SetDirty(target);
				break;
			}
			EditorGUILayout.EndHorizontal();
		}
		
		if (GUILayout.Button ("Create New Set"))
		{
			Undo.RecordObject (myTarget,"Undo Creating New Set");
			ObstacleSet OS = ScriptableObject.CreateInstance<ObstacleSet> ();
			OS.obstacles = new List<ObstacleData> ();
			myTarget.obstacleSets.Add (OS);
			EditorUtility.SetDirty(target);
		}
		if (GUILayout.Button ("Clear Obstacles From Scene"))
		{
			Undo.RecordObject (myTarget.gameObject,"Undo Clearing Scene Obstacles");
			GameObject[] objectsToDestroy = new GameObject[myTarget.gameObject.transform.childCount];
			int j=0;
			foreach (Transform childTr in myTarget.gameObject.transform)
			{
				objectsToDestroy[j]=childTr.gameObject;
				j++;
			}
			foreach(GameObject obj in objectsToDestroy)
				Undo.DestroyObjectImmediate(obj);
			EditorUtility.SetDirty(target); //???? ONLY SCENE STUFF
		}

		if(EditorGUI.EndChangeCheck ())
			EditorUtility.SetDirty (target);


		EditorGUI.BeginChangeCheck ();
		SerializedProperty lengthProp = serializedObject.FindProperty ("lengthBetweenSets");
		EditorGUILayout.PropertyField(lengthProp,true);

		/*if (GUILayout.Button ("Create New Prefab Slot"))
		{
			Undo.RecordObject (myTarget,"Undo Creating New Prefab Slot");
			myTarget.obstacles(ScriptableObject.CreateInstance<ObstacleSet> ());
			EditorUtility.SetDirty(target);
		}*/
		if(EditorGUI.EndChangeCheck ())
			serializedObject.ApplyModifiedProperties();

	}

	Transform FindObstacleWithTag(Transform[] arr,string tg)
	{
		foreach (Transform tr in arr)
			if (tr.tag == tg)
				return tr;
		Debug.Log("Eklenmemis Obstacle var!!!!!!!!!! Tag: "+tg);
		return arr [0];
	}

}

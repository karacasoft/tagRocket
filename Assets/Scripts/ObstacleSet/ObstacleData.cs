using UnityEngine;
using System.Collections;
[System.Serializable]
public class ObstacleData : ScriptableObject
{
	public string type;
	public float x,y;
	public void SetValues(string tag,float outX,float outY)
	{
		type = tag;
		x = outX;
		y = outY;
	}
}
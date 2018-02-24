using UnityEngine;
using System.Collections;

public class Controls
{
	public static bool InputLeft()
	{
		if (Input.GetKey (KeyCode.LeftArrow))
			return true;
		if (Input.touchCount > 0)
		{
			Touch t = Input.GetTouch (0);
			if (t.position.x < Screen.width / 2)
				return true;
		}
		return false;
	}
	public static bool InputRight()
	{
		if (Input.GetKey (KeyCode.RightArrow))
			return true;
		if (Input.touchCount > 0)
		{
			Touch t = Input.GetTouch (0);
			if (t.position.x > Screen.width / 2)
				return true;
		}
		return false;
	}

}

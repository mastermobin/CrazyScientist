using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidWallBehaviour : MonoBehaviour
{

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Grenade" || coll.gameObject.tag == "Bullet")
			Destroy(coll.gameObject, 0);
	}
}

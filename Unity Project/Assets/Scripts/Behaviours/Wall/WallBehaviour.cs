using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Grenade")
			coll.gameObject.GetComponent<GrenadeBehaviour>().bounce();

		if (coll.gameObject.tag == "Bullet" && coll.gameObject.GetComponent<BulletBehaviour>().GetLife() == 0)
			Destroy(coll.gameObject, 0);
		else if (coll.gameObject.tag == "Bullet" && coll.gameObject.GetComponent<BulletBehaviour>().GetLife() >= 1)
		{
			coll.gameObject.GetComponent<BulletBehaviour>().bounce(transform.rotation.eulerAngles.z);
			coll.gameObject.GetComponent<BulletBehaviour>().life--;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detective : MonoBehaviour
{
	public GameObject door;
	public GameObject enemy;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
			door.transform.Rotate(0, 0, 90);
			Instantiate(enemy, transform.position, Quaternion.identity);
		}
	}
}

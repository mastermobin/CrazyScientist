using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

	public GameObject bullet;
	public GameObject gun;
	private GameObject help;

	public void DropGun()
	{
		help = Instantiate(gun, transform.position, transform.rotation);
	}
}

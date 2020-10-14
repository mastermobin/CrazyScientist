using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColtBehaviour : EnemyAbstractBehaviour
{
	public GameObject bullet;
	public AudioClip audioClip;
	private GameObject bulletClone;

	public override void Shoot()
	{
		AudioSource.PlayClipAtPoint(audioClip, pos, 1);
		bulletClone = Instantiate(bullet, pos + CalculateOffset(rotation), rotation);
	}
}

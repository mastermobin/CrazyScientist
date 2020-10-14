using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMachineGunBehaviour : EnemyAbstractBehaviour
{
	public GameObject bullet;
	public AudioClip audioClip;
	private GameObject machineGun;

	public override void Shoot()
	{
		AudioSource.PlayClipAtPoint(audioClip, pos, 1);
		Instantiate(bullet, pos + CalculateOffset(Quaternion.Euler(0, 0, rotation.eulerAngles.z + 10)), Quaternion.Euler(0, 0, rotation.eulerAngles.z + 10));
		Instantiate(bullet, pos + CalculateOffset(rotation), rotation);
		Instantiate(bullet, pos + CalculateOffset(Quaternion.Euler(0, 0, rotation.eulerAngles.z - 10)), Quaternion.Euler(0, 0, rotation.eulerAngles.z - 10));
	}

}

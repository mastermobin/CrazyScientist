using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrenadeBehaviour : EnemyAbstractBehaviour
{

	public GameObject grenade;
	public StateMachine stateMachine;

	public override void Shoot()
	{
		Instantiate(grenade, pos + CalculateOffset(rotation), rotation);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbstractBehaviour : MonoBehaviour
{
	public Vector3 pos;
	public Quaternion rotation;

	public abstract void Shoot();

	public Vector3 CalculateOffset(Quaternion q)
	{
		return new Vector3(1.5f * Mathf.Cos(q.eulerAngles.z * Mathf.Deg2Rad), 1.5f * Mathf.Sin(q.eulerAngles.z * Mathf.Deg2Rad));	}
}

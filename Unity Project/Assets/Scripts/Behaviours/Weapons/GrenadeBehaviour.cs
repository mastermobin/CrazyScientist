using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBehaviour : MonoBehaviour
{

	public StateMachine stateMachine;
	public float speed;
	public float rotate;
	public Quaternion quaternion;
	public float distance = 6f;

	private Animator animator;
	private float time;
	private float scale;
	private int sign = 1;
	void Start()
	{
		quaternion = transform.rotation;
		scale = transform.localScale.x;
		animator = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		if (stateMachine.gameState.timeMultiplier > 0)
		{
			transform.Translate(stateMachine.gameState.timeMultiplier * sign * speed * Time.fixedDeltaTime * Mathf.Cos(quaternion.eulerAngles.z * Mathf.Deg2Rad),
								stateMachine.gameState.timeMultiplier * speed * Time.fixedDeltaTime * Mathf.Sin(quaternion.eulerAngles.z * Mathf.Deg2Rad), 0, Space.World);
			transform.Rotate(0, 0, transform.transform.rotation.z + 5, Space.Self);
			distance -= speed * Time.fixedDeltaTime;
			if (distance <= 0)
				Pause();

		}
	}

	bool S = true;
	void Pause()
	{
		GetComponent<GrenadeBehaviour>().SetSpeed(0);
		GetComponent<GrenadeBehaviour>().SetRotate(10);

		if (Math.Abs(stateMachine.gameState.timeMultiplier) > 0.0005f)
			time += Time.fixedDeltaTime;
		if (time > 2 && S)
		{
			S = false;
			Smoke();
			transform.localScale = new Vector3(transform.localScale.x * 4f, transform.localScale.y * 4f, 1);
		}

	}

	void Smoke()
	{
		animator.speed = stateMachine.gameState.timeMultiplier;
		animator.SetInteger("AnimState", 1);

	}

	public void DestroyCaller()
	{
		Destroy(gameObject, 0);	}

	public void bounce()
	{
		sign = sign > 0 ? -sign : sign;

	}
	public void SetSpeed(float speed)
	{
		this.speed = speed;
	}

	public void SetRotate(float rotate)
	{
		this.rotate = rotate;
	}

	internal void SetSign(int v)
	{
		sign = v;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Creature")
		{
			EnemyBehaviour enemyBehaviour = coll.gameObject.GetComponent<EnemyBehaviour>();
			if (enemyBehaviour != null)
				enemyBehaviour.DropGun();
			Destroy(coll.gameObject, 0);
		}	}
}




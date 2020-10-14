using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

	public StateMachine stateMachine;
	public float radius = 1.1f;
	public ContactFilter2D contactFilter2d;
	public float findPlayerRadius = 9f;
	public ContactFilter2D findPlayer;
	public float rotateSpeed = 45;
	public float rotateNeed = 0;
	public float moveNeed = 0;
	public float moveSpeed = 5;

	private float walkTime = 0;
	private bool walking = false;
	private Animator animator;
	//public float moveSpeed = 0;

	void Start()
	{
		animator = GetComponent<Animator>();
		walking = (Random.Range(0, 1) < 0.5f);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		animator.speed = stateMachine.gameState.timeMultiplier;
		if (stateMachine.gameState.timeMultiplier < 0.1)
			return;
		if (TakeALook()) return;
		if (Mathf.Abs(rotateNeed) > 5)
		{
			if (rotateNeed > 0)
			{
				transform.Rotate(0, 0, stateMachine.gameState.timeMultiplier * rotateSpeed * Time.fixedDeltaTime);
				rotateNeed -= stateMachine.gameState.timeMultiplier * rotateSpeed * Time.fixedDeltaTime;
			}
			else
			{
				transform.Rotate(0, 0, -stateMachine.gameState.timeMultiplier * rotateSpeed * Time.fixedDeltaTime);
				rotateNeed += stateMachine.gameState.timeMultiplier * rotateSpeed * Time.fixedDeltaTime;
			}
		}
		else if (moveNeed > 0.3)
		{
			transform.Translate(stateMachine.gameState.timeMultiplier * moveSpeed * Time.fixedDeltaTime, 0, 0);
			moveNeed -= Time.fixedDeltaTime;
		}
		else
		{
			Collider2D[] colliders = new Collider2D[3];
			int hitCount = Physics2D.OverlapCircle(transform.position, radius, contactFilter2d, colliders);
			Vector2 pos = new Vector2(0, 0);
			bool state = false;
			for (int i = 0; i < Mathf.Min(hitCount, 3) - 1; i++)
			{
				state = true;
				pos = colliders[i].gameObject.transform.position;
				break;
			}
			if (state)
			{
				rotateNeed = 90;
				moveNeed = 0.6f;
			}
			else
			{
				if (walkTime < 0.5)
				{
					if (Random.Range(0, 10) < 8)
					{
						walkTime = Random.Range(2, 6);
						walking = !walking;
					}
					else
					{
						rotateNeed = Random.Range(-45, 45);
					}
					moveSpeed *= (Random.Range(0, 1) < 0.5) ? -1 : 1;
				}
				if (walking)
				{
					animator.SetInteger("AnimState", moveSpeed < 0 ? 2 : 1);
					transform.Translate(stateMachine.gameState.timeMultiplier * moveSpeed * Time.fixedDeltaTime, 0, 0);
				}
				else
				{
					animator.SetInteger("AnimState", 0);
				}
				walkTime -= Time.fixedDeltaTime;
			}
		}


	}


	public float fillGun = 0;
	public float rotateNeedChase = 0;
	public float seenTime = -1000;
	public Quaternion lastDirction;
	bool TakeALook()
	{
		fillGun -= Time.fixedDeltaTime;
		Collider2D[] colliders = new Collider2D[3];
		int hitCount = Physics2D.OverlapCircle(transform.position, findPlayerRadius, findPlayer, colliders);
		Vector2 pos = new Vector2(0, 0);
		bool state = false;
		for (int i = 0; i < Mathf.Min(hitCount, 3); i++)
		{
			state = true;
			pos = colliders[i].gameObject.transform.position;
		}
		if (state)
		{
			float Dx = transform.position.x - pos.x;
			float Dy = transform.position.y - pos.y;

			float Deg = Mathf.Atan(Dy / Dx) * Mathf.Rad2Deg;

			if (Dx > 0)
				Deg += 180;

			float z = transform.rotation.eulerAngles.z % 360;
			if (z > 180)
				z -= 360;
			rotateNeedChase = Deg - z;

			if (Mathf.Abs(rotateNeedChase) < 5)
			{
				if (fillGun < 1)
				{
					Shoot();
					fillGun = 5;
				}
			}
			else
			{
				if (rotateNeedChase > 0)
					transform.Rotate(0, 0, stateMachine.gameState.timeMultiplier * rotateSpeed * Time.fixedDeltaTime);
				else
					transform.Rotate(0, 0, -stateMachine.gameState.timeMultiplier * rotateSpeed * Time.fixedDeltaTime);
			}

			return true;
		}
		else if (Time.fixedTime - seenTime < 7)
		{

		}

		return false;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, radius);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, findPlayerRadius);
	}

	void Shoot()
	{
		EnemyAbstractBehaviour e = GetComponent<EnemyBehaviour>().bullet.GetComponent<EnemyAbstractBehaviour>();
		e.pos = transform.position;
		e.rotation = transform.rotation;
		e.Shoot();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

	public StateMachine stateMachine;
	public float rotateSpeed = 45f;
	public float moveSpeed = 5f;
	public GameObject machineGunBullet;
	public GameObject coltBullet;
	public GameObject grenadeBullet;
	public GameObject grenadeGun;
	private GameObject bulletClone;

	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (stateMachine.gameState.portalMode)
		{
			animator.SetInteger("AnimState", 0);
			return;
		}
		if (stateMachine.keyStates.moveForward != stateMachine.keyStates.moveBackward)
			if (stateMachine.keyStates.moveForward)
			{
				transform.Translate(moveSpeed * Time.fixedDeltaTime, 0, 0);
				animator.SetInteger("AnimState", 1);
			}
			else if (stateMachine.keyStates.moveBackward)
			{
				transform.Translate(-moveSpeed * Time.fixedDeltaTime, 0, 0);
				animator.SetInteger("AnimState", 2);

			}
		if (!stateMachine.keyStates.moveForward && !stateMachine.keyStates.moveBackward)
			animator.SetInteger("AnimState", 0);


		if (stateMachine.keyStates.rotateRight != stateMachine.keyStates.rotateLeft)
			if (stateMachine.keyStates.rotateRight)
				transform.Rotate(new Vector3(0, 0, Time.fixedDeltaTime * rotateSpeed));
			else if (stateMachine.keyStates.rotateLeft)
				transform.Rotate(new Vector3(0, 0, -Time.fixedDeltaTime * rotateSpeed));

		stateMachine.gameState.playerPosition.x = transform.position.x;
		stateMachine.gameState.playerPosition.y = transform.position.y;

		if (stateMachine.keyStates.shoot)
		{
			if (stateMachine.gameState.magazine > 0)
			{
				switch (stateMachine.gameState.gunType)
				{

					case GunTypes.Colt:
						GetComponent<BoxCollider2D>().enabled = false;
						bulletClone = Instantiate(coltBullet, transform.position + CalculateOffset(transform.rotation), transform.rotation);
						break;
					case GunTypes.MachineGun:
						machineGunBullet.SetActive(true);
						bulletClone = Instantiate(machineGunBullet, transform.position + CalculateOffset(transform.rotation), transform.rotation);
						break;
					case GunTypes.Grenade:
						GetComponent<BoxCollider2D>().enabled = false;
						bulletClone = Instantiate(grenadeBullet, transform.position + CalculateOffset(transform.rotation), transform.rotation);
						bulletClone.GetComponent<GrenadeBehaviour>().SetSign(1);
						break;
				}
			}
			if (stateMachine.gameState.magazine > 0)
				stateMachine.gameState.magazine--;
		}
	}

	void Back()
	{
		GetComponent<BoxCollider2D>().enabled = true;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		switch (coll.gameObject.tag)
		{
			case "Colt":
				stateMachine.gameState.gunType = GunTypes.Colt;
				Destroy(coll.gameObject, 0);
				stateMachine.gameState.magazine = Random.Range(2, 4);
				break;
			case "MachineGun":
				stateMachine.gameState.gunType = GunTypes.MachineGun;
				Destroy(coll.gameObject, 0);
				stateMachine.gameState.magazine = Random.Range(1, 2);
				break;
			case "GernadeLauncher":
				stateMachine.gameState.gunType = GunTypes.Grenade;
				Destroy(coll.gameObject, 0);
				stateMachine.gameState.magazine = Random.Range(0, 1);
				break;
		}	}

	public Vector3 CalculateOffset(Quaternion q)
	{
		return new Vector3(1.5f * Mathf.Cos(q.eulerAngles.z * Mathf.Deg2Rad), 1.5f * Mathf.Sin(q.eulerAngles.z * Mathf.Deg2Rad));	}
}

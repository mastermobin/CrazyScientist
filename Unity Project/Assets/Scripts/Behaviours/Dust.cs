using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
	public StateMachine stateMachine;
	public Vector2 offset = new Vector2(0.31f, 1f);
	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if ((stateMachine.keyStates.moveForward ^ stateMachine.keyStates.moveBackward) || (stateMachine.keyStates.rotateLeft ^ stateMachine.keyStates.rotateRight))
		{
			animator.SetInteger("AnimState", 0);

			if (stateMachine.keyStates.moveBackward || stateMachine.keyStates.rotateLeft)
				transform.localPosition = new Vector3(transform.localScale.x - offset.x, transform.localScale.y - offset.y, transform.localScale.z);
			else if (stateMachine.keyStates.moveForward || stateMachine.keyStates.rotateRight)
				transform.localPosition = new Vector3(-transform.localScale.x + offset.x, transform.localScale.y - offset.y, transform.localScale.z);
		}
		else
			animator.SetInteger("AnimState", 1);
	}
}

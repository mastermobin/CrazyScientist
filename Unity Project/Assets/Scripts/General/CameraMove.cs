using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

	public StateMachine stateMachine;
	public float minX, maxX;

	void FixedUpdate()
	{

		float delta = stateMachine.gameState.playerPosition.x - transform.position.x;
		if (delta > 3f && transform.position.x < maxX)
			transform.Translate(delta - 3, 0, 0);
		else if (delta < -3f && transform.position.x > minX)
			transform.Translate(delta + 3, 0, 0);
	}
}

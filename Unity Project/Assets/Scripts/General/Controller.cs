using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ControlKeys
{

	public KeyCode moveForwardKey = KeyCode.RightArrow;
	public KeyCode moveBackwardKey = KeyCode.LeftArrow;
	public KeyCode rotateRightKey = KeyCode.UpArrow;
	public KeyCode rotateLeftKey = KeyCode.DownArrow;
	public KeyCode portalKey = KeyCode.LeftAlt;
	public KeyCode shootKey = KeyCode.Space;

}

public class Controller : MonoBehaviour
{

	public StateMachine stateMachine;
	[Header("Movement Setting")]
	public ControlKeys controlKeys;

	void Update()
	{
		stateMachine.keyStates.moveForward = Input.GetKey(controlKeys.moveForwardKey);
		stateMachine.keyStates.moveBackward = Input.GetKey(controlKeys.moveBackwardKey);
		stateMachine.keyStates.rotateRight = Input.GetKey(controlKeys.rotateRightKey);
		stateMachine.keyStates.rotateLeft = Input.GetKey(controlKeys.rotateLeftKey);
		stateMachine.keyStates.portal = Input.GetKeyDown(controlKeys.portalKey);
		stateMachine.keyStates.shoot = Input.GetKeyDown(controlKeys.shootKey);


		if (stateMachine.keyStates.moveBackward || stateMachine.keyStates.moveForward || stateMachine.keyStates.shoot || stateMachine.keyStates.rotateRight || stateMachine.keyStates.rotateLeft)
		{
			if (!stateMachine.gameState.portalMode)
				stateMachine.gameState.timeMultiplier = 1f;
			else
				stateMachine.gameState.timeMultiplier = 0f;
		}
		else
			stateMachine.gameState.timeMultiplier = 0f;
	}
}

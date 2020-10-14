using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PortalMode : MonoBehaviour
{

	public StateMachine stateMachine;
	public PostProcessingBehaviour cameraBehave;
	public GameObject laser;
	public float rechargeTime = 0f;

	private bool cnt = false;
	private GameObject state;
	// Update is called once per frame
	void FixedUpdate()
	{
		if (stateMachine.keyStates.portal && !stateMachine.gameState.portalMode && rechargeTime < 0.5f)
		{
			cnt = false;
			rechargeTime = 5f;
			Instantiate(laser, (Vector2)cameraBehave.gameObject.transform.position, Quaternion.identity);
			stateMachine.gameState.portalMode = true;
			cameraBehave.enabled = true;
			cameraBehave.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
			cameraBehave.gameObject.GetComponentInChildren<Animator>().enabled = true;
		}
		else if (stateMachine.keyStates.portal || !stateMachine.gameState.portalMode)
		{
			cameraBehave.gameObject.GetComponentInChildren<Animator>().enabled = false;
			cameraBehave.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
			stateMachine.gameState.portalMode = false;
			cameraBehave.enabled = false;
		}
		if (stateMachine.gameState.timeMultiplier > 0)
			if (stateMachine.gameState.portalLanding)
			{
				rechargeTime -= Time.fixedDeltaTime;
				cnt = true;
				stateMachine.gameState.portalLanding = false;
			}
			else if (cnt)
			{
				rechargeTime -= Time.fixedDeltaTime;
			}
	}
}

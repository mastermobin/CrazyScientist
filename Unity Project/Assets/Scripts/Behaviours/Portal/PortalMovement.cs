using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMovement : MonoBehaviour
{

	public Sprite okSprite, errorSprite;

	public StateMachine stateMachine;
	public GameObject portal;
	public float speed = 0.35f;
	public bool firstChoice = true;
	public float radius = 0.3f;
	public float allowedDistance = 6f;

	private Vector2 firstPlace;
	private SpriteRenderer spriteRenderer;
	private GameObject firstLaser;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (stateMachine.gameState.portalMode)
		{
			if (stateMachine.keyStates.moveForward)
				transform.Translate(Vector3.right * speed);
			if (stateMachine.keyStates.moveBackward)
				transform.Translate(Vector3.left * speed);
			if (stateMachine.keyStates.rotateLeft)
				transform.Translate(Vector3.down * speed);
			if (stateMachine.keyStates.rotateRight)
				transform.Translate(Vector3.up * speed);

			bool possible;
			Collider2D[] cols = new Collider2D[3];
			int coll = Physics2D.OverlapCircle(transform.position, radius, new ContactFilter2D(), cols);
			if (coll == 0 && firstChoice)
			{
				possible = true;
				spriteRenderer.sprite = okSprite;
			}
			else if (firstChoice)
			{
				possible = false;
				spriteRenderer.sprite = errorSprite;
			}
			else if (coll == 0 && Vector2.Distance(firstPlace, transform.position) >= allowedDistance)
			{
				possible = true;				spriteRenderer.sprite = okSprite;
			}
			else
			{
				possible = false;
				spriteRenderer.sprite = errorSprite;
			}

			if (stateMachine.keyStates.shoot && firstChoice)
			{
				if (possible)
				{
					firstLaser = Instantiate(gameObject, transform.position, Quaternion.identity);
					firstLaser.GetComponent<SpriteRenderer>().sprite = okSprite;
					firstLaser.GetComponent<PortalMovement>().enabled = false;
					firstChoice = false;
					firstPlace = transform.position;
				}
			}
			else if (stateMachine.keyStates.shoot)
			{
				if (possible)
				{
					Destroy(firstLaser, 0);
					Destroy(gameObject, 0);
					stateMachine.gameState.portalMode = false;

					CreateTwoPortal(transform.position);
					stateMachine.gameState.portalLanding = true;
				}
			}
		}
		else
		{
			if (firstLaser != null) Destroy(firstLaser, 0);
			stateMachine.gameState.portalLanding = true;
			Destroy(gameObject, 0);
		}
	}

	void CreateTwoPortal(Vector2 pos)
	{
		GameObject temp1 = Instantiate(portal, pos, Quaternion.identity);
		GameObject temp2 = Instantiate(portal, firstPlace, Quaternion.identity);

		temp1.GetComponent<PortalBehaviour>().connectedPortal = temp2;
		temp2.GetComponent<PortalBehaviour>().connectedPortal = temp1;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;

		Gizmos.DrawWireSphere(transform.position, radius);
	}
}

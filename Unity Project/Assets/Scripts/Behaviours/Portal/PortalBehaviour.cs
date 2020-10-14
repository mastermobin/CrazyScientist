using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
	public float createDistance = 3;
	public GameObject connectedPortal;
	public bool transfered = false, guest = false;

	private Animator animator;
	private Animator[] animators;
	private GameObject input;

	void Start()
	{
		animator = GetComponent<Animator>();
		animators = GetComponentsInChildren<Animator>();
	}

	void Create(int dir, GameObject trans, Vector3 relativePos, Quaternion quat)
	{
		guest = true;
		if (dir == 1)
		{
			input = Instantiate(trans, transform.position + relativePos, quat);
			SpriteMask[] masks = GetComponentsInChildren<SpriteMask>();
			masks[1].enabled = true;
		}
		else
		{
			input = Instantiate(trans, transform.position + relativePos, quat);
			SpriteMask[] masks = GetComponentsInChildren<SpriteMask>();
			masks[0].enabled = true;
		}
		SpriteRenderer sr = input.GetComponent<SpriteRenderer>();
		if (sr != null)
			sr.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag != "Creature" && col.gameObject.tag != "Bullet" && col.gameObject.tag != "Grenade")
			return;
		if (!guest && !transfered)
		{
			if (col.gameObject.transform.position.x < transform.position.x)
			{
				if (col.gameObject.tag == "Grenade")
					connectedPortal.GetComponent<PortalBehaviour>().Create(1, col.gameObject, col.gameObject.transform.position - transform.position, col.gameObject.GetComponent<GrenadeBehaviour>().quaternion);
				else
					connectedPortal.GetComponent<PortalBehaviour>().Create(1, col.gameObject, col.gameObject.transform.position - transform.position, col.gameObject.transform.rotation);
				SpriteMask[] masks = GetComponentsInChildren<SpriteMask>();
				masks[0].enabled = true;
			}
			else
			{
				if (col.gameObject.tag == "Grenade")
					connectedPortal.GetComponent<PortalBehaviour>().Create(0, col.gameObject, col.gameObject.transform.position - transform.position, col.gameObject.GetComponent<GrenadeBehaviour>().quaternion);
				else
					connectedPortal.GetComponent<PortalBehaviour>().Create(0, col.gameObject, col.gameObject.transform.position - transform.position, col.gameObject.transform.rotation);
				SpriteMask[] masks = GetComponentsInChildren<SpriteMask>();
				masks[1].enabled = true;
			}
			transfered = true;
			input = col.gameObject;
			SpriteRenderer sr = input.GetComponent<SpriteRenderer>();
			if (sr != null)
				sr.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
		}

	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag != "Creature" && col.gameObject.tag != "Bullet" && col.gameObject.tag != "Grenade")
			return;
		if (transfered && col.gameObject == input)
		{
			Destroy(col.gameObject, 0);
			SpriteRenderer sr = input.GetComponent<SpriteRenderer>();
			if (sr != null)
				sr.maskInteraction = SpriteMaskInteraction.None;
			transfered = false;
			animator.SetInteger("AnimState", 1);
			foreach (Animator anim in animators)
				anim.SetInteger("AnimState", 1);
		}
		else if (guest && !transfered && col.gameObject == input)
		{
			SpriteRenderer sr = input.GetComponent<SpriteRenderer>();
			if (sr != null)
				sr.maskInteraction = SpriteMaskInteraction.None;
			animator.SetInteger("AnimState", 1);
			foreach (Animator anim in animators)
				anim.SetInteger("AnimState", 1);
		}
	}

	void DestroyObject()
	{
		Destroy(gameObject, 0);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletBehaviour : MonoBehaviour
{
	public StateMachine stateMachine;
	public AudioClip audioClip;
	public int life = 1;
	public float speed = 2;

	void Start()
	{
		life = Random.Range(0, 3);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		transform.Translate(stateMachine.gameState.timeMultiplier * speed * Time.fixedDeltaTime, 0, 0);
	}

	public int GetLife()
	{
		return life;
	}

	public void bounce(float z)
	{
		transform.Rotate(0, 0, (-(transform.rotation.eulerAngles.z - z) * 2) + 180);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Creature")
		{
			AudioSource.PlayClipAtPoint(audioClip, transform.position, 1);

			EnemyBehaviour enemyBehaviour = coll.gameObject.GetComponent<EnemyBehaviour>();
			if (enemyBehaviour != null)
				enemyBehaviour.DropGun();
			else
				SceneManager.LoadScene("MainScene");
			Destroy(coll.gameObject, 0);
			Destroy(gameObject, 0);
		}
	}
}




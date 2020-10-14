using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class AtTheEnd : MonoBehaviour
{

	void Start()
	{
		Invoke("NextScene", 36);
	}

	void NextScene()
	{
		SceneManager.LoadScene("MainScene");
	}
}

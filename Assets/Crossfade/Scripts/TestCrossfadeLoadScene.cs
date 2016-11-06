using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TestCrossfadeLoadScene : MonoBehaviour 
{
	public Crossfade crossfade;

	void Awake()
	{
		if (crossfade == null)
			crossfade = GameObject.FindObjectOfType<Crossfade> ();
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			crossfade.Execute(3f, delegate() {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
			}, true);
		}
	}
}

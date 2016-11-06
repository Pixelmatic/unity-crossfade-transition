using UnityEngine;
using System.Collections;

public class TestCrossfadeSameScene : MonoBehaviour 
{
	public Crossfade crossfade;
	public GameObject sprite1;
	public GameObject sprite2;

	void Awake()
	{
		if (crossfade == null)
			crossfade = GameObject.FindObjectOfType<Crossfade> ();
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			crossfade.Execute(0.5f, delegate() {
				sprite2.SetActive (!sprite2.activeSelf);
				sprite1.SetActive (!sprite1.activeSelf);
			});
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Crossfade : MonoBehaviour 
{
	private RenderTexture rt;
	private bool executing = false; // Is currently executing 
	private bool ready = false; // ready to start fading

	private float duration;
	private float alpha = 0f;
	private float time = 0f;

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	/// <summary>
	/// This method will execute a crossfade transition.
	/// </summary>
	/// <param name="duration">Duration of the crossfade animation.</param>
	/// <param name="callback">Callback, this is where you want to switch camera or scene.</param>
	/// <param name="loadingNewScene">If set to <c>true</c> it will start the fading animation only after the new scene is loaded.</param>
	public bool Execute(float duration, System.Action callback = null, bool loadingNewScene = false) 
	{
		if (executing)
			return false; // Busy

		// Make the render of the current camera
		Camera cam = Camera.main;
		rt = new RenderTexture (Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
		cam.targetTexture = rt;
		cam.Render ();
		cam.targetTexture = null;

		// Initialize animation parameter
		this.alpha = 1f;
		this.time = 0f;
		this.duration = duration;

		executing = true;
			
		// Callback
		if (callback != null)
			callback ();

		if (loadingNewScene) 
		{
			SceneManager.sceneLoaded += (arg0, arg1) => {
				ready = true;
			};
		}
		else 
		{
			ready = true;
		}

		return true;
	}

	void Update() 
	{
		if (executing == false)
			return;

		if (ready) 
		{
			alpha = Mathf.Lerp (1f, 0f, time / duration);
			time += Time.deltaTime;

			if (time > duration)
				executing = false;
		}
	}

	void OnGUI()
	{
		if (executing == false)
			return;

		GUI.color = new Color (1f, 1f, 1f, alpha);
		GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), rt);
	}
}

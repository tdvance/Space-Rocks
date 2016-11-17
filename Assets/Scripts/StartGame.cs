using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FlexibleMusicManager.instance.SetNewTrack(2);
        FlexibleMusicManager.instance.Play();
        FlexibleMusicManager.instance.repeat = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

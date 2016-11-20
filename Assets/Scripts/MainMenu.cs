using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    if(FlexibleMusicManager.instance.CurrentTrackNumber() > 1) {
            FlexibleMusicManager.instance.SetNewTrack(1);
            FlexibleMusicManager.instance.Play();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (FlexibleMusicManager.instance.CurrentTrackNumber() > 0) {
            FlexibleMusicManager.instance.repeat = true;
        }

    }
}

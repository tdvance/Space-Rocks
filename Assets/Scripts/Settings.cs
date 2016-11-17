using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Settings : MonoBehaviour {
    public GameObject musicVolumeSlider;

    void Start() {
        musicVolumeSlider.GetComponent<Slider>().value = FlexibleMusicManager.instance.volume;
    }

	public void SubmitMusicVolume() {
        FlexibleMusicManager.instance.volume = musicVolumeSlider.GetComponent<Slider>().value;
    }
}

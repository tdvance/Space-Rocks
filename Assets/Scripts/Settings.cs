using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Settings : MonoBehaviour {
    public GameObject musicVolumeSlider;
    public GameObject sfxVolumeSlider;

    void Start() {
        if (FlexibleMusicManager.instance.CurrentTrackNumber() > 0) {
            FlexibleMusicManager.instance.repeat = true;
        }
        if (PlayerPrefs.HasKey("Music Volume")) {
            FlexibleMusicManager.instance.volume = PlayerPrefs.GetFloat("Music Volume");
            Debug.Log("Setting music volume to " + FlexibleMusicManager.instance.volume);
        }
        if (PlayerPrefs.HasKey("SFX Volume")) {
            Game.sfxVolume = PlayerPrefs.GetFloat("SFX Volume");
            Debug.Log("Setting sfx volume to " + Game.sfxVolume);
        }

        musicVolumeSlider.GetComponent<Slider>().value = FlexibleMusicManager.instance.volume;
        sfxVolumeSlider.GetComponent<Slider>().value = Game.sfxVolume;
    }

    public void SubmitMusicVolume() {
        FlexibleMusicManager.instance.volume = musicVolumeSlider.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("Music Volume", FlexibleMusicManager.instance.volume);
        Debug.Log("Setting music volume to " + FlexibleMusicManager.instance.volume);
    }

    public void SubmitSFXVolume() {
        PlayerPrefs.SetFloat("SFX Volume", Game.sfxVolume);
        Game.sfxVolume = sfxVolumeSlider.GetComponent<Slider>().value;
        Debug.Log("Setting sfx volume to " + Game.sfxVolume);
    }
}

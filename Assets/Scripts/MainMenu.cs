using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour {	
	
    public AudioMixer audioMixer;

	public void Game () {
        SceneManager.LoadScene("Game");
	}

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}

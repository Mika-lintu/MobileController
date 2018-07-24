using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour {	
	
    public AudioMixer audioMixer;

	public void Level1 () {
        SceneManager.LoadScene("BermudaTriangle");
	}

    public void Level2()
    {
        SceneManager.LoadScene("BritishStronghold");
    }
    public void Level3()
    {
        SceneManager.LoadScene("DefenceHeadquarters");
    }
    public void Level4()
    {
        SceneManager.LoadScene("IslandFortress");
    }
    public void Level5()
    {
        SceneManager.LoadScene("RoadToEldorado");
    }
    public void Level6()
    {
        SceneManager.LoadScene("Tetrislands");
    }


    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private Slider volumeSlider;
    private TMP_Dropdown difficultyDropdown;
    public enum Dificulty
    {
        Easy,
        Medium,
        Hard,
        Nightmare
    }
    public void Start()
    {       
        volumeSlider = GameObject.FindObjectOfType<Slider>();
        volumeSlider.onValueChanged.AddListener(delegate { SetVolume(); });

        difficultyDropdown = GameObject.FindObjectOfType<TMP_Dropdown>();
        difficultyDropdown.onValueChanged.AddListener(delegate { SetDifficulty(); });
    }
    private int volume = 50;

    public void SetVolume()
    {
        //Debug.Log(volumeSlider.value);
        PlayerPrefs.SetInt("Volume", volume);
    }
    public void SetDifficulty()
    {
        int difficulty = difficultyDropdown.value;        
        PlayerPrefs.SetInt("Difficulty", difficulty);
        Debug.Log("Dificulty set to:" + difficulty);
    }
}

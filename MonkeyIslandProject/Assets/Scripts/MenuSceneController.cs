using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuSceneController : MonoBehaviour
{
    public Toggle music_toggle;
    public GameObject music_on_gameObject;
    public GameObject music_off_gameObject;
    public int music_state;
    public string music_key;
    public GameObject audio_controller;
    private void Start()
    {
        audio_controller = GameObject.FindGameObjectWithTag("AudioController");
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            audio_controller.GetComponent<AudioManager>().RaiseVolume("BGM");
        }
        else if (PlayerPrefs.GetInt("Music") == 0)
        {
            audio_controller.GetComponent<AudioManager>().ReduceVolume("BGM");
        }
    }
    private void Update()
    {
        ManageToggles(music_toggle, music_on_gameObject, music_off_gameObject, "Music", music_state);
        if(PlayerPrefs.GetInt("Music")==1)
        {
            audio_controller.GetComponent<AudioManager>().RaiseVolume("BGM");
        }
        else if(PlayerPrefs.GetInt("Music") == 0)
        {
            audio_controller.GetComponent<AudioManager>().ReduceVolume("BGM");
        }
    }
    public void OnMenuUIClicked(string ui_name)
    {
        switch (ui_name)
        {
            case "Play":
                SceneManager.LoadScene("Game");
                break;
            case "Exit":
                Application.Quit();
                break;
        }

    }
    public void ManageToggles(Toggle t,GameObject on_go,GameObject off_go,string key,int state)
    {
        if(PlayerPrefs.HasKey(key))
        {
            state=PlayerPrefs.GetInt(key);
        }
        else
        {
            state = 1;
            PlayerPrefs.SetInt(key,state);
        }
        if(PlayerPrefs.GetInt(key)==1)
        {
            t.isOn = true;
            on_go.SetActive(true);
            off_go.SetActive(false);
        }
        else if(PlayerPrefs.GetInt(key) == 0)
        {
            t.isOn =false;
            on_go.SetActive(false);
            off_go.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {

    private bool isOpen;
    private GameObject menu;
    private GameObject hud;
    private float health;
    private GameObject playerBase;
    private GameObject enemyBase;
    
    private float initHealth;

    private Image statusPlayer;
    private Image statusEnemy;
    public Slider[] volumeSliders;
    public Toggle[] resolutionToggles;
    private Toggle fullscreenToggle;
    public int[] screenWidths;
    int activeScreenResIndex;

    // Use this for initialization
    void Start () {
        hud = GameObject.Find("HUD").gameObject;                
        menu = GameObject.Find("Menu").gameObject;
        statusPlayer = hud.transform.Find("Canvas/Panel/StatusBar/ContainerPlayer/PlayerStatus").gameObject.GetComponent<Image>();
        statusEnemy = hud.transform.Find("Canvas/Panel/StatusBar/ContainerEnemy/EnemyStatus").gameObject.GetComponent<Image>();
        
        fullscreenToggle = menu.transform.Find("Canvas/Panel/Options/Screen/Fullscreen").gameObject.GetComponent<Toggle>();
        menu.transform.Find("Canvas/Panel/Options").gameObject.SetActive(false);
        menu.SetActive(false);
        isOpen = false;

        //PLAYER PREFERENCES
        activeScreenResIndex = PlayerPrefs.GetInt("screen res index");
        bool isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;

        //volumeSliders[0].value = AudioManager.instance.masterVolumePercent;
        //volumeSliders[1].value = AudioManager.instance.musicVolumePercent;
        //volumeSliders[2].value = AudioManager.instance.sfxVolumePercent;

        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].isOn = i == activeScreenResIndex;
        }

        if(fullscreenToggle != null)
            fullscreenToggle.isOn = isFullscreen;

        playerBase = GameObject.Find("PlayerBase").gameObject;
        enemyBase = GameObject.Find("EnemyBase").gameObject;
        health = 0;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
            {
                isOpen = false;
                menu.SetActive(false);
                hud.SetActive(true);
            }
            else
            {
                isOpen = true;
                menu.SetActive(true);
                hud.SetActive(false);
            }
        }

        //UPDATE STATUS BASE INDICATOR 

        health = playerBase.GetComponent<structureHealth>().health;
        initHealth = playerBase.GetComponent<structureHealth>().initHealth;
        statusPlayer.fillAmount = health / initHealth;

        health = enemyBase.GetComponent<structureHealth>().health;
        initHealth = enemyBase.GetComponent<structureHealth>().initHealth;
        statusEnemy.fillAmount = health / initHealth;
    }

    public void SetScreenResolution(int i)
    {
        if (resolutionToggles[i].isOn)
        {
            activeScreenResIndex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);
            PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
            PlayerPrefs.Save();
        }
    }

    public void SetMusicVolume(float value)
    {
        //AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }

    public void SetSFXVolume(float value)
    {
        //AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFullscreen;
        }

        if (isFullscreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResolution(activeScreenResIndex);
        }

        PlayerPrefs.SetInt("fullscreen", ((isFullscreen) ? 1 : 0));
        PlayerPrefs.Save();
    }
}

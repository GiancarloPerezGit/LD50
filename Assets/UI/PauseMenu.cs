using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    private UIDocument uiDoc;

    public bool isPaused = false;

    public Button resumeButton;
    public Button settingsButton;
    public Button creditsButton;
    public Button quitButton;

    public VisualElement overlayScreen;
    public VisualElement pauseScreen;
    public VisualElement settingsScreen;
    public VisualElement creditsScreen;

    public Button sbackButton;
    public Button cbackButton;

    public Slider masterSlider;
    public Slider songSlider;
    public Slider soundSlider;
    public Slider announcerSlider;

    private void Awake()
    {
        uiDoc = GetComponent<UIDocument>();
    }

    // Start is called before the first frame update
    void Start()
    {
        VisualElement root = uiDoc.rootVisualElement;

        resumeButton = root.Q<Button>("resumebutton");
        settingsButton = root.Q<Button>("settingsbutton");
        creditsButton = root.Q<Button>("creditsbutton");
        quitButton = root.Q<Button>("quitbutton");

        overlayScreen = root.Q<VisualElement>("OverlayScreen");
        pauseScreen = root.Q<VisualElement>("PauseScreen");
        settingsScreen = root.Q<VisualElement>("SettingsScreen");
        creditsScreen = root.Q<VisualElement>("CreditsScreen");

        sbackButton = root.Q<Button>("sback");
        cbackButton = root.Q<Button>("cback");

        masterSlider = root.Q<Slider>("masterSlider");
        songSlider = root.Q<Slider>("songSlider");
        soundSlider = root.Q<Slider>("soundSlider");
        announcerSlider = root.Q<Slider>("announcerSlider");

        VolumeSliderUpdate();

        overlayScreen.style.display = DisplayStyle.None;
        pauseScreen.style.display = DisplayStyle.Flex;
        settingsScreen.style.display = DisplayStyle.None;
        creditsScreen.style.display = DisplayStyle.None;

        resumeButton.clicked += ResumeButtonPressed;
        settingsButton.clicked += SettingsButtonPressed;
        creditsButton.clicked += CreditsButtonPressed;
        quitButton.clicked += QuitButtonPressed;

        sbackButton.clicked += BackButtonPressed;
        cbackButton.clicked += BackButtonPressed;

        masterSlider.RegisterValueChangedCallback(OnMasterSliderChange);
        songSlider.RegisterValueChangedCallback(OnSongSliderChange);
        soundSlider.RegisterValueChangedCallback(OnSoundSliderChange);
        announcerSlider.RegisterValueChangedCallback(OnAnnouncerSliderChange);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PausePressed();
        }
    }

    public void PausePressed()
    {
        if (isPaused)
        {
            overlayScreen.style.display = DisplayStyle.None;
            isPaused = false;

            Time.timeScale = 1f;

        } else
        {
            overlayScreen.style.display = DisplayStyle.Flex;
            isPaused = true;

            Time.timeScale = 0f;
        }
    }

    public void VolumeSliderUpdate()
    {
        //update the sliders
    }

    private void OnMasterSliderChange(ChangeEvent<float> evt)
    {
        Debug.Log("Master volume = " + masterSlider.value);
        //PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }
    private void OnSongSliderChange(ChangeEvent<float> evt)
    {
        Debug.Log("Song volume = " + songSlider.value);
    }
    private void OnSoundSliderChange(ChangeEvent<float> evt)
    {
        Debug.Log("Sound volume = " + soundSlider.value);
    }
    private void OnAnnouncerSliderChange(ChangeEvent<float> evt)
    {
        Debug.Log("Announcer volume = " + announcerSlider.value);
    }

    void ResumeButtonPressed()
    {
        PausePressed();
    }

    void SettingsButtonPressed()
    {
        pauseScreen.style.display = DisplayStyle.None;
        creditsScreen.style.display = DisplayStyle.None;
        settingsScreen.style.display = DisplayStyle.Flex;
    }

    void CreditsButtonPressed()
    {
        pauseScreen.style.display = DisplayStyle.None;
        settingsScreen.style.display = DisplayStyle.None;
        creditsScreen.style.display = DisplayStyle.Flex;
    }

    void BackButtonPressed()
    {
        settingsScreen.style.display = DisplayStyle.None;
        creditsScreen.style.display = DisplayStyle.None;
        pauseScreen.style.display = DisplayStyle.Flex;
    }

    void QuitButtonPressed()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("TestMainMenuAndres");
    }
}

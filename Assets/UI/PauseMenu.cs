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

    public float masterVolumeLevel; //The Actual Master Volume Variable
    public float songVolumeLevel; //The Actual Song Volume Variable
    public float soundVolumeLevel; //The Actual Sound Volume Variable
    public float announcerVolumeLevel; //The Actual Announcer Volume Variable

    private void Awake()
    {
        uiDoc = GetComponent<UIDocument>();
    }

    void Start()
    {
        //Query everything by string name to the UXML document
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

        //.None hides UI, .Flex shows off UI. overlay is the parent of the other three
        overlayScreen.style.display = DisplayStyle.None;
        pauseScreen.style.display = DisplayStyle.Flex;
        settingsScreen.style.display = DisplayStyle.None;
        creditsScreen.style.display = DisplayStyle.None;

        //Making it so whenever you click a button it calls its appropriate function
        resumeButton.clicked += ResumeButtonPressed;
        settingsButton.clicked += SettingsButtonPressed;
        creditsButton.clicked += CreditsButtonPressed;
        quitButton.clicked += QuitButtonPressed;

        sbackButton.clicked += BackButtonPressed; //Back button on the settings page
        cbackButton.clicked += BackButtonPressed; //Back button on the credits page

        //Making it so that whenever you move a slider it calls its appropriate function
        masterSlider.RegisterValueChangedCallback(OnMasterSliderChange);
        songSlider.RegisterValueChangedCallback(OnSongSliderChange);
        soundSlider.RegisterValueChangedCallback(OnSoundSliderChange);
        announcerSlider.RegisterValueChangedCallback(OnAnnouncerSliderChange);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PausePressed();
        }
    }

    // Function to call when you pause
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

    //Update the value of the sliders so that the slider knobs don't start
    //on default positions when your volume settings were changed
    public void VolumeSliderUpdate()
    {
        //Pull from save data
        masterVolumeLevel = PlayerPrefs.GetFloat("MasterVolume");
        songVolumeLevel = PlayerPrefs.GetFloat("SongVolume");
        soundVolumeLevel = PlayerPrefs.GetFloat("SoundVolume");
        announcerVolumeLevel = PlayerPrefs.GetFloat("AnnouncerVolume");

        //Sets the sliders here
        masterSlider.value = masterVolumeLevel;
        songSlider.value = songVolumeLevel;
        soundSlider.value = soundVolumeLevel;
        announcerSlider.value = announcerVolumeLevel;
    }

    private void OnMasterSliderChange(ChangeEvent<float> evt)
    {
        masterVolumeLevel = masterSlider.value;
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeLevel);

        //Adjust the game's volume here: GameVolume = masterVolumeLevel;
    }
    private void OnSongSliderChange(ChangeEvent<float> evt)
    {
        songVolumeLevel = songSlider.value;
        PlayerPrefs.SetFloat("SongVolume", songVolumeLevel);

        //Adjust the game's volume here: GameVolume = songVolumeLevel;
    }
    private void OnSoundSliderChange(ChangeEvent<float> evt)
    {
        soundVolumeLevel = soundSlider.value;
        PlayerPrefs.SetFloat("SoundVolume", soundVolumeLevel);

        //Adjust the game's volume here: GameVolume = soundVolumeLevel;
    }
    private void OnAnnouncerSliderChange(ChangeEvent<float> evt)
    {
        announcerVolumeLevel = announcerSlider.value;
        PlayerPrefs.SetFloat("AnnouncerVolume", announcerVolumeLevel);

        //Adjust the game's volume here: GameVolume = announcerVolumeLevel;
    }

    void ResumeButtonPressed()
    {
        PausePressed();
    }

    void SettingsButtonPressed()
    {
        VolumeSliderUpdate(); //Update the volume sliders as soon as you enter
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
        Time.timeScale = 1f; //Have to do this to be unpaused when you press "Play" in the Main Menu again

        SceneManager.LoadScene("TestMainMenuAndres"); //Replace with the main menu
    }
}

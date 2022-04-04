using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    private UIDocument uiDoc;
    public Button playButton;
    public Button settingsButton;
    public Button creditsButton;

    public VisualElement homeScreen;
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

    // Start is called before the first frame update
    void Start()
    {
        //Query everything by string name to the UXML document
        VisualElement root = uiDoc.rootVisualElement;

        playButton = root.Q<Button>("playbutton");
        settingsButton = root.Q<Button>("settingsbutton");
        creditsButton = root.Q<Button>("creditsbutton");

        homeScreen = root.Q<VisualElement>("HomeScreen");
        settingsScreen = root.Q<VisualElement>("SettingsScreen");
        creditsScreen = root.Q<VisualElement>("CreditsScreen");

        sbackButton = root.Q<Button>("sback");
        cbackButton = root.Q<Button>("cback");

        masterSlider = root.Q<Slider>("masterSlider");
        songSlider = root.Q<Slider>("songSlider");
        soundSlider = root.Q<Slider>("soundSlider");
        announcerSlider = root.Q<Slider>("announcerSlider");

        VolumeSliderUpdate();

        //DisplayStyle.None hides UI, DisplayStyle.Flex shows off UI.
        homeScreen.style.display = DisplayStyle.Flex;
        settingsScreen.style.display = DisplayStyle.None;
        creditsScreen.style.display = DisplayStyle.None;

        //Making it so whenever you click a button it calls its appropriate function
        playButton.clicked += PlayButtonPressed;
        settingsButton.clicked += SettingsButtonPressed;
        creditsButton.clicked += CreditsButtonPressed;

        sbackButton.clicked += BackButtonPressed;//Back button on the settings page
        cbackButton.clicked += BackButtonPressed;//Back button on the credits page

        //Making it so that whenever you move a slider it calls its appropriate function
        masterSlider.RegisterValueChangedCallback(OnMasterSliderChange);
        songSlider.RegisterValueChangedCallback(OnSongSliderChange);
        soundSlider.RegisterValueChangedCallback(OnSoundSliderChange);
        announcerSlider.RegisterValueChangedCallback(OnAnnouncerSliderChange);

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
        //masterVolumeLevel = masterSlider.value;
        //PlayerPrefs.SetFloat("MasterVolume", masterVolumeLevel);
        audioMixer.SetFloat("MasterVolume", (Mathf.Log10(((masterSlider.value + 1.0f) * 0.00000099009901f)) * 40) + 160);
        //Adjust the game's volume here: GameVolume = masterVolumeLevel;
    }
    private void OnSongSliderChange(ChangeEvent<float> evt)
    {
        //songVolumeLevel = songSlider.value;
        //PlayerPrefs.SetFloat("SongVolume", songVolumeLevel);
        audioMixer.SetFloat("MusicVolume", (Mathf.Log10(((songSlider.value + 1.0f) * 0.00000099009901f)) * 40) + 160);
        //Adjust the game's volume here: GameVolume = songVolumeLevel;
    }
    private void OnSoundSliderChange(ChangeEvent<float> evt)
    {
        //soundVolumeLevel = soundSlider.value;
       // PlayerPrefs.SetFloat("SoundVolume", soundVolumeLevel);
        audioMixer.SetFloat("SFXVolume", (Mathf.Log10(((soundSlider.value + 1.0f) * 0.00000099009901f)) * 40) + 160);
        //Adjust the game's volume here: GameVolume = soundVolumeLevel;
    }
    private void OnAnnouncerSliderChange(ChangeEvent<float> evt)
    {
        announcerVolumeLevel = announcerSlider.value;
        PlayerPrefs.SetFloat("AnnouncerVolume", announcerVolumeLevel);

        //Adjust the game's volume here: GameVolume = announcerVolumeLevel; 
    }

    void PlayButtonPressed()
    {
        SceneManager.LoadScene("SAPGIntegration"); //Replace with the actual Scene
    }

    void SettingsButtonPressed()
    {
        VolumeSliderUpdate(); //Update the sliders as soon as you enter
        homeScreen.style.display = DisplayStyle.None;
        creditsScreen.style.display = DisplayStyle.None;
        settingsScreen.style.display = DisplayStyle.Flex;
    }

    void CreditsButtonPressed()
    {
        homeScreen.style.display = DisplayStyle.None;
        settingsScreen.style.display = DisplayStyle.None;
        creditsScreen.style.display = DisplayStyle.Flex;
    }

    void BackButtonPressed()
    {
        settingsScreen.style.display = DisplayStyle.None;
        creditsScreen.style.display = DisplayStyle.None;
        homeScreen.style.display = DisplayStyle.Flex;
    }

    void Update()
    {
        
    }
}

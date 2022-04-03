using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private UIDocument uiDoc;
    public Button playButton;
    public Button settingsButton;
    public Button creditsButton;

    public VisualElement homeScreen;
    public VisualElement settingsScreen;
    public VisualElement creditsScreen;

    public Button sbackButton;
    public Button cbackButton;

    private void Awake()
    {
        uiDoc = GetComponent<UIDocument>();
    }

    // Start is called before the first frame update
    void Start()
    {
        VisualElement root = uiDoc.rootVisualElement;

        playButton = root.Q<Button>("playbutton");
        settingsButton = root.Q<Button>("settingsbutton");
        creditsButton = root.Q<Button>("creditsbutton");

        homeScreen = root.Q<VisualElement>("HomeScreen");
        settingsScreen = root.Q<VisualElement>("SettingsScreen");
        creditsScreen = root.Q<VisualElement>("CreditsScreen");

        sbackButton = root.Q<Button>("sback");
        cbackButton = root.Q<Button>("cback");

        homeScreen.style.display = DisplayStyle.Flex;
        settingsScreen.style.display = DisplayStyle.None;
        creditsScreen.style.display = DisplayStyle.None;

        playButton.clicked += PlayButtonPressed;
        settingsButton.clicked += SettingsButtonPressed;
        creditsButton.clicked += CreditsButtonPressed;

        sbackButton.clicked += BackButtonPressed;
        cbackButton.clicked += BackButtonPressed;

    }

    void PlayButtonPressed()
    {
        SceneManager.LoadScene("AndresScene");
    }

    void SettingsButtonPressed()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}

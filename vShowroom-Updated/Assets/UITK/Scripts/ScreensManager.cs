using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScreensManager : MonoBehaviour
{
    private VisualElement welcomeScreen;
    private VisualElement homeScreen;
    [SerializeField] EnableLocation cameraPositionSwoopStart;

    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        welcomeScreen = root.Q("WelcomeScreen");
        homeScreen = root.Q("HomeScreen");

        WelcomeScreenManager wsManager = new WelcomeScreenManager(welcomeScreen);
        wsManager.Start = () =>
        {
            cameraPositionSwoopStart.goToPosition(1);
            welcomeScreen.Display(false);
            homeScreen.Display(true);
        };

    }
}
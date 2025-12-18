using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BikesScreenManager : MonoBehaviour
{
    public Action Next { set => next_but.clicked += value; }
    public Action Inspect { set => inspect_but.clicked += value; }

    public GameObject bikeA;
    public GameObject bikeB;
    //public GameObject bikeC;

    private Button next_but;
    private Button inspect_but;
    private Button bikeA_but;
    private Button bikeB_but;
    //private Button bikeC_but;
    private Button shirt_but;
    private Button bike_but;
    private Button home_but;

    public BikesScreenManager (VisualElement root, EnableLocation cameraPosition)
    {
        next_but = root.Q<Button>("next");
        inspect_but = root.Q<Button>("inspect");
        shirt_but = root.Q<Button>("shirt");
        bike_but = root.Q<Button>("bike");
        home_but = root.Q<Button>("homebutton");

        shirt_but.clicked += () => cameraPosition.goToPos5();
        bike_but.clicked += () => cameraPosition.goToPos1();
        home_but.clicked += () => cameraPosition.ResetPosition();
    }

    public void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        bikeA_but = root.Q<Button>("alpha");
        bikeB_but = root.Q<Button>("delta");
        //bikeC_but = root.Q<Button>("bikeC");

        bikeA_but.clicked += () => ActivateBikeA();
        bikeB_but.clicked += () => ActivateBikeB();
        //bikeC_but.clicked += () => ActivateBikeC();
    }

    public void ActivateBikeA()
    {
        bikeA.SetActive(true);
        bikeB.SetActive(false);
        //bikeC.SetActive(false);
    }

    public void ActivateBikeB()
    {
        bikeA.SetActive(false);
        bikeB.SetActive(true);
        //bikeC.SetActive(false);
    }

   /* public void ActivateBikeC()
    {
        bikeA.SetActive(false);
        bikeB.SetActive(false);
        bikeC.SetActive(true);
    }*/
}
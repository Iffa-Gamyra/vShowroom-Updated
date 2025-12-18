using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomiseScreenManager : MonoBehaviour
{
    public Action Back { set => back_but.clicked += value; }
    //public Action Next { set => next_but.clicked += value; }
    public Action Inspect { set => inspect_but.clicked += value; }

    private Button back_but;
    //private Button next_but;
    private Button inspect_but;
    private Button shirt_but;
    private Button bike_but;
    private Button home_but;
    Button bodyWhiteBut;
    Button bodySilverBut;
    Button bodyAluminBut;
    Button bodyCarbonBut;
    Button bodyBlackBut;
    Button badgeSilverBut;
    Button badgeBlackBut;
    Button seatTanBut;
    Button seatBlackBut;

    public MeshRenderer tankMesh;
    public MeshRenderer badgeMesh;
    public MeshRenderer seatMesh;
    public Material bodyWhiteMat;
    public Material bodySilverMat;
    public Material bodyAluminMat;
    public Material bodyCarbonMat;
    public Material bodyBlackMat;
    public Material badgeSilverMat;
    public Material badgeBlackMat;
    public Material seatTanMat;
    public Material seatBlackMat;


    public CustomiseScreenManager(VisualElement root, EnableLocation cameraPosition)
    {
        back_but = root.Q<Button>("back");
        //next_but = root.Q<Button>("next");
        inspect_but = root.Q<Button>("inspect");
        shirt_but = root.Q<Button>("shirt");
        bike_but = root.Q<Button>("bike");
        home_but = root.Q<Button>("homebutton");
        bodyWhiteBut = root.Q<Button>("body_white");
        bodySilverBut = root.Q<Button>("body_silver");
        bodyAluminBut = root.Q<Button>("body_alumin");
        bodyCarbonBut = root.Q<Button>("body_carbon");
        bodyBlackBut = root.Q<Button>("body_black");
        badgeSilverBut = root.Q<Button>("badge_silver");
        badgeBlackBut = root.Q<Button>("badge_black");
        seatTanBut = root.Q<Button>("seat_tan");
        seatBlackBut = root.Q<Button>("seat_black");

        shirt_but.clicked += () => cameraPosition.goToPos5();
        bike_but.clicked += () => cameraPosition.goToPos1();
        home_but.clicked += () => cameraPosition.ResetPosition();

        bodyWhiteBut.clicked += () => cameraPosition.goToPos2();
        bodySilverBut.clicked += () => cameraPosition.goToPos2();
        bodyAluminBut.clicked += () => cameraPosition.goToPos2();
        bodyCarbonBut.clicked += () => cameraPosition.goToPos2();
        bodyBlackBut.clicked += () => cameraPosition.goToPos2();

        badgeSilverBut.clicked += () => cameraPosition.goToPos4();
        badgeBlackBut.clicked += () => cameraPosition.goToPos4();

        seatTanBut.clicked += () => cameraPosition.goToPos3();
        seatBlackBut.clicked += () => cameraPosition.goToPos3();
    }

    public void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        bodyWhiteBut = root.Q<Button>("body_white");
        bodySilverBut = root.Q<Button>("body_silver");
        bodyAluminBut = root.Q<Button>("body_alumin");
        bodyCarbonBut = root.Q<Button>("body_carbon");
        bodyBlackBut = root.Q<Button>("body_black");

        badgeSilverBut = root.Q<Button>("badge_silver");
        badgeBlackBut = root.Q<Button>("badge_black");

        seatTanBut = root.Q<Button>("seat_tan");
        seatBlackBut = root.Q<Button>("seat_black");

        bodyWhiteBut.clicked += () => tankMesh.material = bodyWhiteMat;
        bodySilverBut.clicked += () => tankMesh.material = bodySilverMat;
        bodyAluminBut.clicked += () => tankMesh.material = bodyAluminMat;
        bodyCarbonBut.clicked += () => tankMesh.material = bodyCarbonMat;
        bodyBlackBut.clicked += () => tankMesh.material = bodyBlackMat;

        badgeSilverBut.clicked += () => badgeMesh.material = badgeSilverMat;
        badgeBlackBut.clicked += () => badgeMesh.material = badgeBlackMat;

        seatTanBut.clicked += () => seatMesh.material = seatTanMat;
        seatBlackBut.clicked += () => seatMesh.material = seatBlackMat;
    }
}
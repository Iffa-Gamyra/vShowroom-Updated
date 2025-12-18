
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class BikeConfigurator : MonoBehaviour
{
    public VisualTreeAsset visualTreeAsset;  // Assign your UXML file here

    private VisualElement root;
    public EnableLocation cameraPosition;
    //private VisualElement root;
    public Material[] materials;
    private Dictionary<string, Action> buttonActions;
  //  public VisualElement root;

    public void OnEnable()
    {
        root = visualTreeAsset.CloneTree();
       // GetComponent<UIDocument>().rootVisualElement.Add(root);
     //   root = GetComponent<UIDocument>().rootVisualElement;

        // Define the button names and their corresponding actions
        buttonActions = new Dictionary<string, Action>
        {
            { "body_white", () => ChangeColorBody(0) },
            { "body_silver", () => ChangeColorBody(1) },
            { "body_alumin", () => ChangeColorBody(2) },
            { "body_carbon", () => ChangeColorBody(3) },
            { "body_black", () => ChangeColorBody(4) },
            { "badge_silver", () => ChangeColorBadge(0) },
            { "badge_black", () => ChangeColorBadge(1) },
            { "seat_tan", () => ChangeColorSeat(0) },
            { "seat_black", () => ChangeColorSeat(1) },
            { "inspect_body", () => Inspect(1) },
            { "inspect_badge", () => Inspect(3) },
            { "inspect_seat", () => Inspect(2) }

        };

        // Iterate through the dictionary and add event listeners dynamically
        foreach (var entry in buttonActions)
        {
            var button = root.Q<Button>(entry.Key);
            if (button != null)
            {
                button.clicked += entry.Value;
            }
        }
    }

    private void ChangeColorBody(int material)
    {
        GameObject.FindWithTag("body").GetComponent<MeshRenderer>().material = materials[material];
    }
    private void ChangeColorSeat(int material)
    {
        GameObject.FindWithTag("seat").GetComponent<MeshRenderer>().material = materials[material];

    }

    private void ChangeColorBadge (int material)
    {
        GameObject.FindWithTag("badge").GetComponent<MeshRenderer>().material = materials[material];
    }


    private void Inspect(int pos)
    {

        cameraPosition.goToPosition(pos);
    }
  
}

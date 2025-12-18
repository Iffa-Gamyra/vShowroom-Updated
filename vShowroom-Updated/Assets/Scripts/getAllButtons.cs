using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class getAllButtons : MonoBehaviour
{
    private VisualElement root;

    void OnEnable()
    {
        // Reference to the UI Document component
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        // Get all buttons in the UI
        List<Button> buttons = GetAllButtonsInUI();

        // For demonstration, log the name of each button
        foreach (var button in buttons)
        {
            Debug.Log(button.name);
        }
    }

    private List<Button> GetAllButtonsInUI()
    {
        // Query all buttons in the UI
        var buttons = root.Query<Button>().ToList();
        return buttons;
    }
}

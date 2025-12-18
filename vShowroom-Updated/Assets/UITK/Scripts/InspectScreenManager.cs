using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InspectScreenManager
{
    public Action Back { set => back_but.clicked += value; }

    private Button back_but;

    public InspectScreenManager(VisualElement root)
    {
        back_but = root.Q<Button>("back");
    }
}
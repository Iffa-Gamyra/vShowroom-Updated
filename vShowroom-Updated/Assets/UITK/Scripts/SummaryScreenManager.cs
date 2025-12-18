using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SummaryScreenManager
{
    public Action Back { set => back_but.clicked += value; }
    public Action Checkout { set => checkout_but.clicked += value; }

    private Button back_but;
    private Button checkout_but;

    public SummaryScreenManager(VisualElement root)
    {
        back_but = root.Q<Button>("back");
        checkout_but = root.Q<Button>("checkout");
    }
}
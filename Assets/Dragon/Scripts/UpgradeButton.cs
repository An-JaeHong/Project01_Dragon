using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public Text buttonNameText;
    public Button button;

    public void SelectButton(string buttonName,UnityAction onClick)
    {

        buttonNameText.text = buttonName;
        button.onClick.AddListener(()=>onClick());
     
    }
    
}

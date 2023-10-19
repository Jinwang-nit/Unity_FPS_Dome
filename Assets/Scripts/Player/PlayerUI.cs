using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;

    private void Start()
    {
        
    }

    public void UpadteText(string promptMessage)
    {
        promptText.text = promptMessage;
    }
}

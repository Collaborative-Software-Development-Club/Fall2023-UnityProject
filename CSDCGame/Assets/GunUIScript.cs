using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunUIScript : MonoBehaviour
{
    public GameObject gunStatus;
    public Firearm playerFirearmScript;
    private TextMeshProUGUI _text;
    void Start()
    {
        _text = gunStatus.GetComponent<TextMeshProUGUI>();
        if (_text == null) {
            Debug.LogError("ERROR: gunStatus did not contain a TextMeshPro element!");
            Application.Quit();
        }    
    }

    void Update()
    {
        _text.text = "[" + playerFirearmScript.fireType.ToString().ToUpper() + "] " + playerFirearmScript.projectilesRemaining + "/" + playerFirearmScript.globalCapacity;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUIScript : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private PlayerHeath _pHealth;
    void Start() {
        _text = gameObject.GetComponent<TextMeshProUGUI>();
        _pHealth = GameObject.Find("Player").GetComponent<PlayerHeath>();
        if (_pHealth == null) {
            Debug.LogError("ERROR: NO PLAYER OBJECT FOUND!");
        }
    }
    void Update()
    {
        _text.text = "HP: " + _pHealth.currentHealth.ToString();
    }
}

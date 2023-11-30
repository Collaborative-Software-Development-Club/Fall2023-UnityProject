using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIScript : MonoBehaviour
{
    public GameObject healthBar;
    public PlayerHealth pHealth;
    private Slider _healthSlider;
    void Start() {
        _healthSlider = healthBar.GetComponent<Slider>();
        if (_healthSlider == null) {
            Debug.LogError("ERROR: HEALTH SLIDER NOT FOUND!");
        }
    }
    void Update()
    {
        _healthSlider.value = (float) pHealth.currentHealth / pHealth.startingHealth;
    }
}

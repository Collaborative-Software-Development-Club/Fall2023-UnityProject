using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveUIScript : MonoBehaviour
{
    public GameObject waveStatus;
    public WaveSystemScript waveSystem;
    private TextMeshProUGUI _text;

    void Start() {
        _text = waveStatus.GetComponent<TextMeshProUGUI>();
        if (_text == null) {
            Debug.LogError("ERROR: waveStatus did not contain a TextMeshPro element!");
            Application.Quit();
        }
    }
    void Update() {
        _text.text = "[WAVE]: " + waveSystem.currWave;
    }
}

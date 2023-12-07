using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefeatScreenController : MonoBehaviour
{
    public GameObject defeatScreen;
    public PlayerHealth playerHealth;
    public GameObject PauseUI;
    private int killedCount = 0;
    public TextMeshProUGUI enemyKilled;
    public WaveSystemScript WaveSystem;


    // Start is called before the first frame update
    void Start()
    {
        defeatScreen.SetActive(false);
    }

    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            ShowDefeatScreen();
            PauseUI.SetActive(false);
            Time.timeScale = 0;
            killedCount = WaveSystem.enemyKilled;
            enemyKilled.SetText(killedCount.ToString());
        }
    }

    // Update is called once per frame
    private void ShowDefeatScreen()
    {
        defeatScreen.SetActive(true);
    }
}

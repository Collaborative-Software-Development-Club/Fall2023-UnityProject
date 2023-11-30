using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatScreenController : MonoBehaviour
{
    public GameObject defeatScreen;
    public PlayerHealth playerHealth;
    public GameObject PauseUI;

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
        }
    }

    // Update is called once per frame
    private void ShowDefeatScreen()
    {
        defeatScreen.SetActive(true);
    }
}

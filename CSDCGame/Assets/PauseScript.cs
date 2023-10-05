using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject pauseUI;
    public Button resumeButton;
    public Button quitButton;
    private bool _isPaused = false;

    void Start() {
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            _isPaused = !_isPaused;
            gameUI.SetActive(!_isPaused);
            pauseUI.SetActive(_isPaused);
            if (_isPaused) {
                PauseGame();
            }
            else {
                ResumeGame();
            }
        }
    }
    private void PauseGame() {
        Time.timeScale = 0;
    }
    public void ResumeGame() {
        Time.timeScale = 1.0f;
        _isPaused = false;
        pauseUI.SetActive(_isPaused);
        gameUI.SetActive(!_isPaused);
    }
    public void QuitGame() {
        //Debug.Log("Quitting!");
        Application.Quit();
    }
}

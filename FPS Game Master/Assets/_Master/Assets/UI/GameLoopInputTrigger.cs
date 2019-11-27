using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopInputTrigger : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += ShowGameOverScreen;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= ShowGameOverScreen;
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ShowPausePanel();
            FreezeTime();
        }
    }

    private void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        FreezeTime();
    }

    private void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }

    private void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    private void FreezeTime()
    {
        Cursor.visible = true;
       Cursor.lockState =  CursorLockMode.None;
        Time.timeScale = 0f;
    }

    private void UnFreezeTime()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    public void UnpauseGame()
    {
        UnFreezeTime();
        HidePausePanel();
    }
}

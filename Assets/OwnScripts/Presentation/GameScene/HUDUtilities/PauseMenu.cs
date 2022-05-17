using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject quickHelpMenu;
    [SerializeField] private GameObject resumeButton;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            PauseGame();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
    }

    public void QuickHelpMenu()
    {
        pauseMenu.SetActive(false);
        quickHelpMenu.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        pauseMenu.SetActive(true);
        quickHelpMenu.SetActive(false);
    }
}

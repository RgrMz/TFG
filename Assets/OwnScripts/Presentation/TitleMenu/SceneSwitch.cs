using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] private GameObject wantTutotiralPopUp;

    // Scene names, need to be cons due to switch-case limitations
    private const string firstScene = "TitleScene";
    private const string secondScene = "CreateOrSelectUserProfile";
    private const string thirdScene = "ChooseModeScene";
    private const string gameScene = "Game";
    private const string badgesScene = "BadgesMenu";
    private const string tutorialScene = "Tutorial";
    private const string winScene = "WinScene";
    private const string lostScene = "LostScene";

    private string nextScene;

    public void LoadNextScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        switch (scene.name)
        {
            case firstScene:
                SceneManager.LoadScene(secondScene);
                break;
            case secondScene:
                SceneManager.LoadScene(thirdScene);
                break;
            case thirdScene:
                wantTutotiralPopUp.SetActive(true);
                break;
            default:
                break;

        }
    }

    public void LoadMenuBadges()
    {
        SceneManager.LoadScene(badgesScene);
    }

    public void BackToProfileScene()
    {
        SceneManager.LoadScene(secondScene);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(tutorialScene);
    }

    public void CancelTutorial()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void LoadLastScene(bool win)
    {
        if(win)
        {
            SceneManager.LoadScene(winScene);
        }
        else
        {
            SceneManager.LoadScene(lostScene);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    // Scene names, need to be cons due to switch-case limitations
    private const string firstScene = "TitleScene";
    private const string secondScene = "CreateOrSelectUserProfile";
    private const string thirdScene = "ChooseModeScene";
    private const string gameScene = "Game";

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
                SceneManager.LoadScene(gameScene);
                break;
            default:
                break;
        }
 
    }
}

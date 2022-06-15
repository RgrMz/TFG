using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialHandler : MonoBehaviour
{
    private int tutorialStep;
    [SerializeField] private TextMeshProUGUI stepsText;
    [SerializeField] private GameObject tutorialImage;
    [SerializeField] private GameObject endTutorialConfirmation;
    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;

    private const int NUMBER_TUTORIAL_STEPS = 20;
    private const int MINIMUM_NUMBER_TUTORIAL_STEPS = 1;
    void Start()
    {
        tutorialStep = 1;
    }

    private void Update()
    {
        if (tutorialStep == MINIMUM_NUMBER_TUTORIAL_STEPS)
        {
            backButton.enabled = false;
            backButton.GetComponent<Image>().enabled = false;
        }
        else
        {
            backButton.enabled = true;
            backButton.GetComponent<Image>().enabled = true;
        }
        if (tutorialStep == NUMBER_TUTORIAL_STEPS)
        {
            nextButton.enabled = false;
            nextButton.GetComponent<Image>().enabled = false;
        }
        else
        {
            nextButton.enabled = true;
            nextButton.GetComponent<Image>().enabled = true;
        }
    }

    public void LoadStep(int offset)
    {
        tutorialStep += offset;
        Texture2D image = Resources.Load($"Tutorial/{tutorialStep}") as Texture2D;
        Sprite spriteFromImage = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
        tutorialImage.gameObject.GetComponent<Image>().sprite = spriteFromImage;
        stepsText.text = $"{tutorialStep} / 20";
    }

    public void EndTutorial()
    {
        endTutorialConfirmation.SetActive(true);
    }

    public void ConfirmEndTutorial()
    {
        SceneManager.LoadScene("Game");
    }

    public void CancelEndTutorial()
    {
        endTutorialConfirmation.SetActive(false);
    }

}

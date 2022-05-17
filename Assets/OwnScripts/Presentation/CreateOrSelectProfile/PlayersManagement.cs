using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersManagement : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private GameObject dropdownUsernames;
    [SerializeField] private GameObject selectDifficulty;
    [SerializeField] private GameObject seeBadges;
    [SerializeField] private InputField username;
    [SerializeField] private InputField age;
    void Start()
    {
        playerController = new PlayerController();
        List<string> options = playerController.getUsernames();
        Debug.Log(options[0]);
        dropdownUsernames.GetComponent<Dropdown>().AddOptions(options);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableSelectDifficulty()
    {
        selectDifficulty.GetComponent<Button>().interactable = true;
    }
    public void EnableSeeBadges()
    {
        seeBadges.GetComponent<Button>().interactable = true;
    }

    public void NewPlayerEnableSelectDifficultyAndBadges()
    {
        if (username.text.Length > 0 && age.text.Length > 0)
        {
            EnableSelectDifficulty();
            EnableSeeBadges();
        }
    }

}

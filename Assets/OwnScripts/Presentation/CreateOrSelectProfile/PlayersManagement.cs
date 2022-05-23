using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersManagement : MonoBehaviour
{
    public static PlayerController PlayerController;
    [SerializeField] private GameObject dropdownUsernames;
    [SerializeField] private GameObject selectDifficulty;
    [SerializeField] private GameObject seeBadges;
    [SerializeField] private InputField username;
    [SerializeField] private InputField age;
    void Start()
    {
        PlayerController = new PlayerController();
        List<string> options = PlayerController.getUsernames();
        Debug.Log(options[0]);
        dropdownUsernames.GetComponent<Dropdown>().AddOptions(options);
    }

    private void LoadPlayer()
    {
        if (dropdownUsernames.GetComponent<Dropdown>().interactable)
        {
            PlayerController.loadPlayer(dropdownUsernames.GetComponent<Dropdown>()
                .options[dropdownUsernames.GetComponent<Dropdown>().value].text);
        }       
    }

    public void ShouldPlayerBeSaved()
    {
        if (username.text.Length > 0 && age.text.Length > 0)
        {
            // A new player is created
            Player p = new Player(username.text, int.Parse(age.text));
            PlayerController.savePlayer(p);
            Debug.Log("Saved");
        }
    }

    public void EnableSelectDifficulty()
    {
        selectDifficulty.GetComponent<Button>().interactable = true;
        LoadPlayer();
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
            dropdownUsernames.GetComponent<Dropdown>().interactable = false;
        }

        if (username.text.Length == 0 || age.text.Length == 0)
        {
            selectDifficulty.GetComponent<Button>().interactable = false;
            seeBadges.GetComponent<Button>().interactable = false;
            dropdownUsernames.GetComponent<Dropdown>().interactable = false;
        }

        if (username.text.Length == 0 && age.text.Length == 0)
        {
            dropdownUsernames.GetComponent<Dropdown>().interactable = true;
        }

        if (age.text.Length != 0)
        {
            if (int.Parse(age.text) < 16)
            {
                age.text = "16";
            }

            if (int.Parse(age.text) > 65)
            {
                age.text = "65";
            }
        }
    }

}

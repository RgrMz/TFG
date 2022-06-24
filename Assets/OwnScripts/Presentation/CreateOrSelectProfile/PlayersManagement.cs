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
    [SerializeField] private GameObject warningPlayerExists;
    private PlayerDAO playerDao;
    void Start()
    {
        PlayerController = new PlayerController();
        List<string> options = PlayerController.getUsernames();
        dropdownUsernames.GetComponent<Dropdown>().AddOptions(options);
        playerDao = new PlayerDAO();
    }

    private void LoadPlayer()
    {
        if (dropdownUsernames.GetComponent<Dropdown>().interactable)
        {
            Player p = PlayerController.loadPlayer(dropdownUsernames.GetComponent<Dropdown>()
                .options[dropdownUsernames.GetComponent<Dropdown>().value].text);
            DataSaver.saveData(p, "player");
        }
    }

    public void HideWarning()
    {
        warningPlayerExists.SetActive(false);
    }

    public void ShouldPlayerBeSaved()
    {
        if (username.text.Length > 0 && age.text.Length > 1)
        {
            // A new player is created
            Player p = new Player(username.text, int.Parse(age.text));
            if (PlayerController.doPlayerExists(username.text))
            {
                PlayerController.savePlayer(p);
                p.UserId = playerDao.getUserId(username.text, int.Parse(age.text));
                DataSaver.saveData(p, "player");
            }
            else
            {
                warningPlayerExists.SetActive(true);
            }
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

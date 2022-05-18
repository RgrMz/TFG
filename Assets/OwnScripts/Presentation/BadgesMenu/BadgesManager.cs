using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BadgesManager : MonoBehaviour
{
    private void Start()
    {
        LoadBadges();
    }

    public void LoadBadges()
    {
        int i = 0;
        Badge badge = null;
        GameObject[] allChildren = new GameObject[gameObject.transform.childCount];
        List<Badge> playerBadges = PlayersManagement.PlayerController.getBadgesOfPlayer();

        //Find all child obj and store to that array
        foreach (Transform panelTransform in gameObject.transform)
        {
            allChildren[i] = panelTransform.gameObject;
            i += 1;
        }

        for (int j = 0; j < allChildren.Length && j < playerBadges.Count; j++)
        {
            GameObject child = allChildren[j];
            badge = playerBadges[j];

            foreach (Transform panelElement in child.transform)
            {
                switch (panelElement.gameObject.name)
                {
                    case "Title":
                        if (badge != null)
                        {
                            panelElement.gameObject.GetComponent<TextMeshProUGUI>().text = badge.Title;
                        }
                        break;
                    case "Description":
                        if (badge != null)
                        {
                            panelElement.gameObject.GetComponent<TextMeshProUGUI>().text = badge.Description;
                        }
                        break;
                    case "BadgeImage":
                        if (badge != null)
                        {
                            Texture2D image = Resources.Load($"Badges/{badge.ImagePath}") as Texture2D;
                            Debug.Log(image.ToString());
                            Sprite spriteFromImage = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
                            panelElement.gameObject.GetComponent<Image>().sprite = spriteFromImage;
                        }
                        break;
                }
            }
        }
    }

}

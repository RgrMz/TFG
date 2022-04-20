using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class InteractableItemBase : MonoBehaviour
{

    private GameObject interactionPanel = null;

    private GameObject player;

    public TextMeshProUGUI interactionText;

    private float interactionDistance = 6f;

    private Animator anim;

    void Start()
    {
        interactionPanel = GameObject.Find("InteractionTextPanel");
        player = GameObject.Find("PlayableCharacter");
        interactionText.enabled = false;
        anim = player.GetComponent<Animator>();
    }

    void Update()
    {
        // Coger la posicion del player
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Raycast for detecting the object.
        if (Physics.Raycast(ray, out hit, interactionDistance))
        { 

            if (hit.collider.tag == "Interactable")
            {
                Image panelImage = interactionPanel.gameObject.GetComponent<Image>();
                var tempColor = panelImage.color;
                tempColor.a = 0.8f;
                panelImage.color = tempColor;
                interactionText.enabled = true;

                interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    anim.SetBool("StartsInteraction", true);

                } else
                {
                    anim.SetBool("StartsInteraction", false);
                }

            }
            else
            {

                // Turns the prompt back off when you're not looking at the object.
                interactionText.gameObject.SetActive(false);
                Image panelImage = interactionPanel.gameObject.GetComponent<Image>();
                var tempColor = panelImage.color;
                tempColor.a = 0;
                panelImage.color = tempColor;
            }
        }
    }

}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class InteractableItemBase : MonoBehaviour
{
    private GameObject interactionPanel;

    private GameObject player;

    public TextMeshProUGUI interactionText;

    private string textToShow;

    private string animationParameter;

    private Animator anim;

    private KeyCode key;

    private bool collided;

    void Start()
    {
        interactionPanel = GameObject.Find("InteractionTextPanel");
        player = GameObject.Find("PlayableCharacter");
        interactionText.enabled = false;
        anim = player.GetComponent<Animator>();
        switch(gameObject.tag)
        {
            case "Interactable":
                animationParameter = "StartsInteraction";
                textToShow = "Press E to interact";
                key = KeyCode.E;
                break;
            case "OpsNPC":
                animationParameter = "StartsTalking1";
                textToShow = "Press G to talk with ops team members";
                key = KeyCode.G;
                break;
        }

    }

    void Update()
    {
        Debug.Log(collided);
        if (Input.GetKeyDown(key))
        {
            if(collided)
                anim.SetBool(animationParameter, true);
        }
        else
        {
            anim.SetBool(animationParameter, false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Show the text that indicates which key to press
        collided = true;
        if (other.CompareTag("Player"))
        {
            interactionText.enabled = true;
            interactionText.gameObject.SetActive(true);
            interactionText.text = textToShow;
            Image panelImage = interactionPanel.gameObject.GetComponent<Image>();
            var tempColor = panelImage.color;
            tempColor.a = 0.8f;
            panelImage.color = tempColor;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        collided = false;
        if (other.CompareTag("Player"))
        {
            // Turns the prompt back off when you're not looking at the object.
            interactionText.text = "";
            interactionText.enabled = false;
            interactionText.gameObject.SetActive(false);
            Image panelImage = interactionPanel.gameObject.GetComponent<Image>();
            var tempColor = panelImage.color;
            tempColor.a = 0;
            panelImage.color = tempColor;
        }
    }

}
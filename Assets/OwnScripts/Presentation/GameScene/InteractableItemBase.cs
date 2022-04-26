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

    // Based on player role, take the animator of the given opposite role
    private Animator animNPC;

    private KeyCode key;

    private bool collided;

    private GameObject pickUpParent;

    void Start()
    {
        interactionPanel = GameObject.Find("InteractionTextPanel");
        player = GameObject.Find("PlayableCharacter");
        if (interactionText != null)
            interactionText.enabled = false;
        // For prefabs being instantiated
        if (interactionText == null)
            interactionText = interactionPanel.GetComponentInChildren<TextMeshProUGUI>();
        anim = player.GetComponent<Animator>();
        collided = false;
        switch (gameObject.tag)
        {
            case "Interactable":
                animationParameter = "StartsInteraction";
                textToShow = "Press E to interact";
                key = KeyCode.E;
                break;
            case "OpsNPC":
                animationParameter = "StartsTalking1";
                // Cuando tengamos una clase player con un atributo role => Unificar este case y el de NPC para customizar el texto en base al rol con un condicional
                textToShow = "Press G to talk with ops team members";
                key = KeyCode.G;
                animNPC = GetComponent<Animator>();
                break;
            case "NPC":
                animationParameter = "StartsTalking1";
                // Cuando tengamos una clase player con un atributo role => Unificar este case y el de NPC para customizar el texto en base al rol con un condicional
                textToShow = "Press G to talk with dev team members";
                key = KeyCode.G;
                animNPC = GetComponent<Animator>();
                break;
            case "Pickable":
                pickUpParent = GameObject.Find("/PlayableCharacter/Skeleton/Hips/Spine/"
                    + "Chest/UpperChest/Right_Shoulder/Right_UpperArm/Right_LowerArm/Right_Hand/"
                    + "Right_IndexProximal/PickUpParent");
                animationParameter = "PickUp";
                textToShow = "Press T to grab the work done";
                key = KeyCode.T;
                break;
        }

    }

    void Update()
    {
        if (gameObject.tag.Equals("Pickable"))
        {
            if (Input.GetKeyDown(key))
            {
                if (collided)
                {
                    Destroy(GetComponent<Rigidbody>());
                    gameObject.transform.SetParent(pickUpParent.transform);
                    gameObject.transform.position = pickUpParent.transform.position;
                    anim.SetTrigger(animationParameter);
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (collided)
                {
                    anim.SetTrigger("Throw");
                    gameObject.transform.parent = null;
                    gameObject.AddComponent<Rigidbody>();
                    Rigidbody rbody = gameObject.GetComponent<Rigidbody>();
                    rbody.mass = 0.05f;
                    gameObject.transform.position = player.transform.position + new Vector3(1.5f, 0.5f, 0);
                    turnOffInteractionText();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(key))
            {
                if (collided)
                {
                    if (key == KeyCode.G)
                        // That's the key for interacting with NPCs => display its animations also
                        animNPC.SetTrigger(animationParameter);
                    anim.SetBool(animationParameter, true);
                }
            }
            else
            {
                anim.SetBool(animationParameter, false);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
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
    }

    private void OnTriggerExit(Collider other)
    {
        collided = false;
        if (other.CompareTag("Player"))
        {
            turnOffInteractionText();
        }
    }

    void turnOffInteractionText()
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
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class InteractableItemBase : MonoBehaviour
{
    private GameObject interactionPanel;

    private GameObject player;

    public TextMeshProUGUI interactionText;

    private string textToShow;

    private string animationParameter;

    private Animator anim;

    private GameObject indicatorsManagerGO;

    private GameObject gameManagerGO;

    // Based on player role, take the animator of the given opposite role
    private Animator animNPC;

    private KeyCode key;

    private bool collided;

    private bool picked;

    private GameObject pickUpParent;

    private void Awake()
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
        picked = false;

        indicatorsManagerGO = GameObject.Find("IndicatorsManager");
        gameManagerGO = GameObject.Find("GameManager");
    }

    void Start()
    {
        switch (gameObject.tag)
        {
            case "Interactable":
                animationParameter = "StartsInteraction";
                textToShow = "Press E to work";
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
            case "Customer":
                animationParameter = "StartsTalking1";               
                textToShow = "Press G to talk with the customer";
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
            case "ObjectiveHandler":
                textToShow = "Press F to choose this solution";
                animationParameter = "";
                key = KeyCode.F;
                break;
        }

    }

    void Update()
    {
        if (gameObject.CompareTag("ObjectiveHandler"))
        {
            GameObject solutionText = gameObject.transform.Find("SolutionCanvas").Find("SolutionText").gameObject;
            if (solutionText.GetComponent<TextMeshProUGUI>().text.Equals(""))
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }
        }

        if (gameObject.CompareTag("Pickable"))
        {
            if (Input.GetKeyDown(key))
            {
                if (collided)
                {
                    picked = true;
                    anim.SetTrigger(animationParameter);
                    StartCoroutine(AttachBall());
                    player.tag = "PickingObject";
                }
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                if (collided)
                {
                    picked = false;
                    anim.SetTrigger("Throw");
                    StartCoroutine(DetachBall());
                    player.tag = "Player";
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
                    {
                        // That's the key for interacting with NPCs => display their animation also
                        animNPC.SetTrigger(animationParameter);
                        StartCoroutine(SpawnBubbleCHat());
                    }
                        
                    anim.SetBool(animationParameter, true);
                    if (gameObject.CompareTag("ObjectiveHandler"))
                    {
                        Transform t = gameObject.transform.Find("SolutionCanvas").Find("SolutionText");
                        if (t.gameObject.GetComponent<TextMeshProUGUI>().text.Equals(""))
                        {
                            Debug.Log("NullText");
                        }
                        ExecuteEvents.Execute<ObjectiveHandler>(gameObject, null,
                            (manager, y) => manager.SendSolutionChoose());
                    }
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
        if (other.CompareTag("Player") && !picked)
        {
            // Show the text that indicates which key to press
            collided = true;

            if (gameObject.CompareTag("ObjectiveHandler"))
            {
                indicatorsManagerGO.GetComponent<IndicatorsManager>()
                    .BlinkIndicatorsAffected(gameObject.name[gameObject.name.Length - 1] - '0');
            }

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PickingObject") && picked)
        {
            collided = true;
            interactionText.text = "Press C to throw the work done";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        collided = false;
        if (other.CompareTag("Player"))
        {
            TurnOffInteractionText();
        }
    }

    void TurnOffInteractionText()
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

    IEnumerator DetachBall()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.parent = null;
        gameObject.AddComponent<Rigidbody>();
        Rigidbody rbody = gameObject.GetComponent<Rigidbody>();
        rbody.mass = 0.05f;
        gameObject.transform.position = player.transform.position + new Vector3(1.5f, 0.5f, 0);
        TurnOffInteractionText();
    }

    IEnumerator AttachBall()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(GetComponent<Rigidbody>());
        gameObject.transform.SetParent(pickUpParent.transform);
        gameObject.transform.position = pickUpParent.transform.position;
        yield break;
    }

    IEnumerator SpawnBubbleCHat()
    {
        Vector3 positionToSpawnBubbleChat = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
        GameObject bubbleChat = 
            Instantiate(Resources.Load($"OwnPrefabs/BubbleChat"), positionToSpawnBubbleChat, transform.rotation) as GameObject;
        string dialogue = gameManagerGO.GetComponent<GameManager>().projectController.SelectedProject.CurrentObjective.pickRandomDialogue();
        foreach(char letter in dialogue.ToCharArray())
        {
            bubbleChat.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text += letter;
            yield return new WaitForSeconds(0.09f);
        }
        yield return new WaitForSeconds(20f);
        Destroy(bubbleChat);
        yield break;
    }
}
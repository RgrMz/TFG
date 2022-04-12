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

    void Start()
    {
        interactionPanel = GameObject.Find("InteractionTextPanel");
        player = GameObject.Find("PlayableCharacter");
        interactionText.enabled = false;
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
                Debug.Log("Colisiojn");
                Image panelImage = interactionPanel.gameObject.GetComponent<Image>();
                var tempColor = panelImage.color;
                tempColor.a = 130;
                panelImage.color = tempColor;
                interactionText.enabled = true;

                interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Boton presionado");
                    // Do something here, like...

                    // Eg. destroy the object.
                    //Destroy(hit.transform.gameObject);
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

    /*private void OnTriggerEnter(Collider player)
    {
        /*if (player.gameObject.tag == "Player")
        {
            Image panelImage = interactionPanel.gameObject.GetComponent<Image>();
            var tempColor = panelImage.color;
            tempColor.a = 130;
            panelImage.color = tempColor;
            interactionText.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Presionaste E, hagamos las animaciones");
            }
        }*/

}
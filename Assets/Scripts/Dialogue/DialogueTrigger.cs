using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Sprite head;
    
    public Dialogue dialogue;

    public WomenProprity property;

    public bool isInRange;
    private bool isReading = false;

    void Update()
    {
        if(isInRange && Input.GetKeyDown(KeyCode.E))
        {
            isReading = true;
            TriggerDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && isReading == false)
        {
            GameObject.FindGameObjectsWithTag("InteractUIWrapper")[0].transform.GetChild(0).gameObject.SetActive(true);
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange = false;
            GameObject.FindGameObjectsWithTag("InteractUIWrapper")[0].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue, head, property);
    }
}

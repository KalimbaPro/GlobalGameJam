using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    public GameObject yesButton;
    public GameObject noButton;
    public GameObject continueButton;

    public PlayerAttack attack;

    private Queue<string> sentences;
    private Sprite womanHead;

    private WomenProprity property;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il n'y a plus d'une instance de DialogueManager dans la scène");
            return;
        }

        instance = this;

        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, Sprite head, WomenProprity proprytie)
    {
        property = proprytie;
        attack.enabled = false;
        womanHead = head;
        continueButton.SetActive(true);
        yesButton.SetActive(false);
        noButton.SetActive(false); animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.015f);
        }
    }

    void EndDialogue()
    {
        Debug.Log("EndDialogue");
        yesButton.SetActive(true);
        noButton.SetActive(true);
        continueButton.SetActive(false);
    }

    public void EndDialogueYesButton()
    {
        Debug.Log("SpeedBoost");
        animator.SetBool("isOpen", false);
        PlayerReprod.addFamilyMember(womanHead);
        RandomSkin.instance.RandomizeSkin();
        attack.enabled = true;
        property.UpgradeProperty();
    }

    public void EndDialogueNoButton()
    {
        animator.SetBool("isOpen", false);
    }
}

using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public AudioSource doorOpen;
    public AudioSource doorClose;
    public GameObject canvasUI;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private bool isOpen = false;

    void Update()
    {
        if (isOpen == true && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("IsOpen", false);
            canvasUI.SetActive(false);
            doorClose.Play();
            spriteRenderer.sortingOrder = 6;
            isOpen = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && isOpen == false)
        {
            animator.SetBool("IsOpen", true);
            canvasUI.SetActive(true);
            doorOpen.Play();
            isOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && isOpen == true)
        {
            animator.SetBool("IsOpen", false);
            canvasUI.SetActive(false);
            doorClose.Play();
            isOpen = false;
        }
    }
}

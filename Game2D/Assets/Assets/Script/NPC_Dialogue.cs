using UnityEngine;
using System.Collections.Generic; // ต้องมีบรรทัดนี้เพื่อใช้ List

public class NPC_Dialogue : MonoBehaviour
{
    public DialogueManager manager;
    public GameObject interactPrompt;

    [Header("บทสนทนา (กำหนดคนพูดได้ทีละประโยค)")]
    // เปลี่ยนจาก string[] เป็น List<DialogueLine>
    public List<DialogueLine> conversation; 

    private bool playerIsClose;

    void Update()
    {
        if (playerIsClose && Input.GetKeyDown(KeyCode.E))
        {
            if (interactPrompt != null) interactPrompt.SetActive(false);

            if (!manager.dialogueBox.activeInHierarchy)
            {
                // ส่งบทสนทนาทั้งหมดไปให้ Manager
                manager.StartDialogue(conversation);
            }
            else
            {
                manager.DisplayNextSentence();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            if (interactPrompt != null) interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            if (interactPrompt != null) interactPrompt.SetActive(false);
        }
    }
}
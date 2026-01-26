using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

// สร้างกล่องข้อมูลใหม่ สำหรับเก็บว่า "ประโยคนี้ ใครพูด"
[System.Serializable]
public class DialogueLine
{
    public string name;      // ชื่อคนพูด
    public Sprite portrait;  // รูปคนพูด
    [TextArea(3, 10)]
    public string sentence;  // บทพูด
}

public class DialogueManager : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject dialogueBox;
    public Image portraitImage;
    public TMP_Text nameText;
    public TMP_Text bodyText;

    private Queue<DialogueLine> lines; // เปลี่ยนจากเก็บ string เป็นเก็บ DialogueLine

    void Start()
    {
        lines = new Queue<DialogueLine>();
        dialogueBox.SetActive(false);
    }

    // รับค่าเป็น List ของ DialogueLine แทน
    public void StartDialogue(List<DialogueLine> dialogueLines)
    {
        Time.timeScale = 0f; // หยุดเวลา
        dialogueBox.SetActive(true);
        lines.Clear();

        foreach (DialogueLine line in dialogueLines)
        {
            lines.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        // ดึงข้อมูลทั้งก้อนออกมา (ชื่อ+รูป+คำพูด)
        DialogueLine currentLine = lines.Dequeue();

        // อัปเดตหน้าจอให้ตรงกับคนพูดคนใหม่
        nameText.text = currentLine.name;
        portraitImage.sprite = currentLine.portrait;
        bodyText.text = currentLine.sentence;
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        Time.timeScale = 1f; // เดินเวลาต่อ
    }
}
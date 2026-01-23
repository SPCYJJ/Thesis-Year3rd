using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("ตั้งค่าความเร็ว")]
    public float speed = 5f; // ความเร็วเดิน ปรับใน Unity ได้

    private Rigidbody2D rb;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // ดึงตัว Rigidbody มาเก็บไว้
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        // --- ส่วนที่แก้ใหม่ (รองรับทุกขนาด) ---
        // ถ้ากดขวา (มากกว่า 0) ให้ค่า Scale X เป็นบวก (หันขวา)
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        // ถ้ากดซ้าย (น้อยกว่า 0) ให้ค่า Scale X เป็นลบ (หันซ้าย)
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        // ------------------------------------
    }

    void FixedUpdate()
    {
        // สั่งให้เคลื่อนที่ (เปลี่ยนความเร็วแกน X, คงความเร็วแกน Y ไว้เหมือนเดิมเผื่อตกจากที่สูง)
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }
}
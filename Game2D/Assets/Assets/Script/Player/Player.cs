using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    /* ======================
     * Movement
     * ====================== */
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    private Rigidbody2D rb;
    private Vector2 input;

    /* ======================
     * Stamina
     * ====================== */
    [Header("Stamina")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDrain = 20f;
    public float staminaRecover = 15f;

    /* ======================
     * Health
     * ====================== */
    [Header("Health")]
    public int maxHP = 3;
    public int currentHP;

    /* ======================
     * State
     * ====================== */
    public bool isRunning;
    public bool isHiding;

    /* ======================
     * Inventory
     * ====================== */
    [Header("Inventory")]
    public List<Item> items = new List<Item>();

    /* ======================
     * Unity Events
     * ====================== */
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentStamina = maxStamina;
        currentHP = maxHP;
    }

    void Update()
    {
        HandleInput();
        HandleStamina();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    /* ======================
     * Input
     * ====================== */
    void HandleInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        isRunning = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && !isHiding;
    }

    /* ======================
     * Movement Logic
     * ====================== */
    void HandleMovement()
    {
        if (isHiding) return;

        float speed = isRunning ? runSpeed : walkSpeed;
        rb.velocity = input.normalized * speed;
    }

    /* ======================
     * Stamina Logic
     * ====================== */
    void HandleStamina()
    {
        if (isRunning && input.magnitude > 0)
        {
            currentStamina -= staminaDrain * Time.deltaTime;
        }
        else
        {
            currentStamina += staminaRecover * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

    /* ======================
     * Health Logic
     * ====================== */
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
            Die();
    }

    void Die()
    {
        GameManager.Instance.GameOver();
    }

    /* ======================
     * Hide System
     * ====================== */
    public void EnterHide()
    {
        isHiding = true;
        rb.velocity = Vector2.zero;
        rb.simulated = false;
    }

    public void ExitHide()
    {
        isHiding = false;
        rb.simulated = true;
    }

    /* ======================
     * Inventory
     * ====================== */
    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public bool HasItem(string itemID)
    {
        return items.Exists(i => i.itemID == itemID);
    }

    /* ======================
     * Interaction
     * ====================== */
    public void Interact(GameObject target)
    {
        target.SendMessage("OnInteract", this, SendMessageOptions.DontRequireReceiver);
    }
}

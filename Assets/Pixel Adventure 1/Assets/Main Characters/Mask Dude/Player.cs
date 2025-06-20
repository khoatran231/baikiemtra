using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    // Event: Phát đi tín hiệu khi Player chết.
    public static event Action OnPlayerDeath;

    // --- Components & Parameters ---
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    // --- State Variables ---
    private float dirX = 0;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isFalling = false;
    private bool currentIsDeath = false;
    private bool jumpRequested = false; // Biến đệm để ghi nhận yêu cầu nhảy

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Nếu nhân vật đã chết, không làm gì cả
        if (currentIsDeath) return;

        // 1. ĐỌC INPUT VÀ CẬP NHẬT TRẠNG THÁI NON-PHYSICS TRONG UPDATE
        HandleMovementInput();
        CheckGroundedStatus();
        FlipSprite();
        UpdateAnimationState();

        // Chỉ "ghi nhận" yêu cầu nhảy trong Update, không áp dụng lực ở đây
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        // Nếu nhân vật đã chết, không làm gì cả
        if (currentIsDeath) return;

        // 2. ÁP DỤNG LỰC VẬT LÝ TRONG FIXEDUPDATE
        HandleHorizontalMovement();
        HandleJump();
        UpdatePhysicsBasedStates();
    }

    // --- Core Methods ---

    private void HandleMovementInput()
    {
        dirX = Input.GetAxisRaw("Horizontal");
    }

    private void HandleHorizontalMovement()
    {
        rb.linearVelocity = new Vector2(dirX * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        // Áp dụng lực nhảy nếu có yêu cầu từ Update
        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpRequested = false; // Reset yêu cầu sau khi đã thực hiện
        }
    }

    private void CheckGroundedStatus()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
    }

    private void UpdatePhysicsBasedStates()
    {
        // Cập nhật trạng thái nhảy/rơi dựa trên vật lý thực tế
        if (isGrounded)
        {
            isJumping = false;
            isFalling = false;
        }
        else if (rb.linearVelocity.y > 0.1f) // Đang bay lên
        {
            isJumping = true;
            isFalling = false;
        }
        else if (rb.linearVelocity.y < -0.1f) // Đang rơi xuống
        {
            isJumping = false;
            isFalling = true;
        }
    }

    private void FlipSprite()
    {
        if (dirX < 0f) sprite.flipX = true;
        else if (dirX > 0f) sprite.flipX = false;
    }

    private void UpdateAnimationState()
    {
        // Cập nhật animation dựa trên các biến trạng thái
        anim.SetBool("isRunning", dirX != 0 && isGrounded);
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isFalling", isFalling);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentIsDeath) return;

        if (collision.CompareTag("trap"))
        {
            TriggerDeath();
        }
        else if (collision.CompareTag("Fruit"))
        {
            ScoreManager.Instance.AddScore(10);
            Destroy(collision.gameObject);
        }
    }

    private void TriggerDeath()
    {
        currentIsDeath = true;
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("isDeath");
        OnPlayerDeath?.Invoke();
    }

    // --- Public Getters for State ---
    public bool IsJumping() { return isJumping; }
    public bool IsDead() { return currentIsDeath; }

    // --- Gizmos for Editor ---
    private void OnDrawGizmosSelected()
    {
        if (groundCheckPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }
}
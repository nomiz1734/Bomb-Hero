using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField] private float maxHp = 100f;
    private float currentHp;
    [SerializeField] private Image hpBar;
    [SerializeField] public float pickupRange = 1.5f;
    public TextMeshProUGUI gameOverText; // TextMesh Pro để hiển thị
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }
    void Start()
    {
        currentHp = maxHp;
        UpdateHpBar();
        gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer2();
    }
    void MovePlayer()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.x = Input.GetAxisRaw("Horizontal");

        moveInput.Normalize();

        transform.position += moveInput * moveSpeed * Time.deltaTime;

        if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        if (moveInput != Vector3.zero)
        {
            animator.SetBool("IsRun", true);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }
    }
    void MovePlayer2()
    {
        //if (this == null || gameObject == null) return;

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();

        // Di chuyển sử dụng Rigidbody2D
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);

        // Flip sprite
        if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Animation
        animator.SetBool("IsRun", moveInput != Vector2.zero);
    }
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();
        if (currentHp <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
        ShowGameOver(); // Hiển thị "Game Over" khi chết
    }

    protected void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }

    void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true); // Kích hoạt Text "Game Over"
    }
}

using UnityEngine;

public class BossSpawnEffect : MonoBehaviour
{
    [SerializeField] private float spawnDuration = 1f;
    [SerializeField] private AnimationCurve appearCurve;

    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    private float timer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;

        // Bắt đầu từ mờ và nhỏ
        transform.localScale = Vector3.zero;
        Color c = spriteRenderer.color;
        c.a = 0;
        spriteRenderer.color = c;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / spawnDuration);
        float eased = appearCurve.Evaluate(t);

        // Scale lên từ nhỏ
        transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, eased);

        // Fade in
        Color c = spriteRenderer.color;
        c.a = eased;
        spriteRenderer.color = c;

        if (t >= 1f)
        {
            Destroy(this); // Xong rồi thì bỏ script đi
        }
    }
}

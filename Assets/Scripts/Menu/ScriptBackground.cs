using UnityEngine;

public class ScriptBackground : MonoBehaviour
{
    public float speed = 1f;
    public float scaleAmount = 0.05f;
    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * speed) * scaleAmount;
        transform.localScale = initialScale * scale;
    }
}

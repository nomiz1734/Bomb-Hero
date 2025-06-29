using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewMonoBehaviourScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text targetText;

    [Header("Color Settings")]
    public Color normalColor = Color.black;
    public Color hoverColor = Color.black; 

    [Header("Scale Settings")]
    public bool enableScale = true;
    public Vector3 normalScale = Vector3.one;
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.0f);
    public float scaleSpeed = 8f;

    private bool isHovered = false;

    void Start()
    {
        if (targetText == null)
            targetText = GetComponentInChildren<TMP_Text>(); // tìm cả trong con

        if (targetText != null)
        {
            targetText.color = normalColor;
        }
        else
        {
            Debug.LogWarning("⚠️ targetText chưa được gán và không tìm thấy TMP_Text trong object/con.");
        }

        transform.localScale = normalScale;
    }


    void Update()
    {
        if (enableScale)
        {
            Vector3 target = isHovered ? hoverScale : normalScale;
            transform.localScale = Vector3.Lerp(transform.localScale, target, Time.deltaTime * scaleSpeed);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        if (targetText != null)
            targetText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        if (targetText != null)
            targetText.color = normalColor;
    }

}

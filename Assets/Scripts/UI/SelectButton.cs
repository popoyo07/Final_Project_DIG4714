using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private AudioSource chooseButtonSFX;
    private TextMeshProUGUI textMeshPro;


    void Start()
    {
        button = GetComponent<Button>();
        if (button == null) // Check if button is null
        {
            Debug.Log("button NULL");
        }

        chooseButtonSFX = GetComponentInParent<AudioSource>();
        if (chooseButtonSFX == null) // Check if AudioSource is null
        {
            Debug.Log("chooseButtonSFX NULL");
        }

        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        if (textMeshPro == null) // Check if textMeshPro is null
        {
            Debug.Log("textMeshPro NULL");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null)
        {
            Color newColor = button.image.color;
            newColor.a = 0.4f; // A little bit visible
            button.image.color = newColor;
            textMeshPro.color = Color.red;
            chooseButtonSFX.Play();
            Debug.Log("YES");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button != null)
        {
            Color newColor = button.image.color;
            newColor.a = 0.0f; // fully invisible
            textMeshPro.color = Color.black;
            button.image.color = newColor;
        }
    }
}

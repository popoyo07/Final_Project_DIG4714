using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        if (button == null) // Check if button is null
        {
            Debug.Log("button = NULLLLLLL");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null)
        {
            Color newColor = button.image.color;
            newColor.a = 0.4f; // A little bit visible
            button.image.color = newColor;
            Debug.Log("YES");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button != null)
        {
            Color newColor = button.image.color;
            newColor.a = 0.0f; // fully invisible
            button.image.color = newColor;
        }
    }
}

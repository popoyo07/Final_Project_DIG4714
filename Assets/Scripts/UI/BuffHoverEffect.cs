using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BuffHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buffDescription; // Assign the corresponding text in Inspector

    void Start()
    {
        if (buffDescription != null)
        {
            buffDescription.gameObject.SetActive(false); // Hide text initially
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buffDescription != null)
        {
            buffDescription.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buffDescription != null)
        {
            buffDescription.gameObject.SetActive(false);
        }
    }
}

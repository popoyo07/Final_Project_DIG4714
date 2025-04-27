using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BuffHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Assign the corresponding text in Inspector
    public TextMeshProUGUI buffDescription;
    AudioSource audiosource;

    [Header("SFX")]
    public AudioClip SFXselectBuffs;

    void Start()
    {
        audiosource = GetComponentInParent<AudioSource>();
        // If buffDescription is assigned, initially hide the text
        if (buffDescription != null)
        {
            buffDescription.gameObject.SetActive(false);
        }
    }

    // Called when the mouse enters the GameObject's area
    public void OnPointerEnter(PointerEventData eventData)
    {
        // When mouse hovers over the object, make the buff description visible
        if (buffDescription != null)
        {
            buffDescription.gameObject.SetActive(true);
            audiosource.clip = SFXselectBuffs;
            audiosource.Play();
        }
    }

    // Called when the mouse exits the GameObject's area
    public void OnPointerExit(PointerEventData eventData)
    {
        // When mouse stops hovering, hide the buff description again
        if (buffDescription != null)
        {
            buffDescription.gameObject.SetActive(false);
        }
    }
}

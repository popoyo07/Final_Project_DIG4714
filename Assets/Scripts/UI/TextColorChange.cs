using UnityEngine;
using TMPro;

public class TextColorFloat : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private Color targetColor;
    public float colorChangeSpeed = 3f; // Speed of color transition

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // Check if textMeshPro exist or not
        if (textMeshPro != null)
        {
            // Set the first target color
            targetColor = GetRandomColor();
        }
        else
        {
            Debug.Log("No textMeshPro");
        }
    }

    void Update()
    {
        if (textMeshPro != null)
        {
            // Smoothly interpolate towards the target color
            textMeshPro.color = Color.Lerp(textMeshPro.color, targetColor, colorChangeSpeed * Time.deltaTime);

            // If we're close to the target, pick a new one
            if (Vector4.Distance(textMeshPro.color, targetColor) < 0.05f)
            {
                targetColor = GetRandomColor();
            }
        }
    }

    Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value, 1f);
    }
}

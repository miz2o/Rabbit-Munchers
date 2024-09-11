using UnityEngine;
using TMPro;

public class RandomTextDisplay : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component
    public TextMeshProUGUI textMeshPro;

    // Array of strings that the text can randomly display
    public string[] randomTexts;

    void Start()
    {
        // Ensure the TextMeshPro component is assigned
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        // Display a random piece of text from the array
        DisplayRandomText();
    }

    // Function to display a random text
    void DisplayRandomText()
    {
        if (randomTexts.Length > 0)
        {
            // Pick a random index from the array
            int randomIndex = Random.Range(0, randomTexts.Length);

            // Set the text of the TextMeshPro component
            textMeshPro.text = randomTexts[randomIndex];
        }
        else
        {
            Debug.LogWarning("Random texts array is empty!");
        }
    }
}

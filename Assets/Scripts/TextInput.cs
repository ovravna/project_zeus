using UnityEngine;
using TMPro;

public class TextInput : MonoBehaviour
{
    public TextMeshPro textMeshPro;

    void Update()
    {
        // Check for keyboard input
        if (Input.anyKeyDown)
        {
            // Get the latest key pressed
            string key = Input.inputString;
            // Handle backspace
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                // Remove the last character from the text
                if (textMeshPro.text.Length > 0)
                {
                    textMeshPro.text = textMeshPro.text.Substring(0, textMeshPro.text.Length - 1);
                }
            }
            else
            {
                // Append the pressed key to the existing text
                textMeshPro.text += key;
            }
        }
    }
}
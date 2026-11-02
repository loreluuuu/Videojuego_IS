using TMPro;
using UnityEngine;
using System.Collections;

public class TypeWriter : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    [TextArea]
    public string dialogue;

    public float textSpeed = 0.05f;

    void Start()
    {
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        textComponent.text = "";

        foreach(char letter in dialogue.ToCharArray())
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
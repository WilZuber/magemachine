using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextRevealer : MonoBehaviour {
	private TMP_Text textObject;
    public GameObject ExpositionScreen;
    public bool TextLoaded = false;

	void Awake () {
        textObject = GetComponent<TMP_Text>();
		StartCoroutine(RevealText());
	}

    void Update() {
        // user can skip directly to menu if the text is fully loaded
        CanGoToMenu();
    }
	
	IEnumerator RevealText() {
		var originalString = textObject.text;
        var textLength = originalString.Substring(0, originalString.Length - 22);
		var numCharsRevealed = 0;

        // reveals all of the text except the delayed prompt to press a key
        // after the exposition intro has been fully loaded
		while (numCharsRevealed < textLength.Length)
		{
            // Skips Spaces
            while (originalString[numCharsRevealed] == ' ') {
                ++numCharsRevealed;
            }

			++numCharsRevealed;
			textObject.text = originalString.Substring(0, numCharsRevealed);

			yield return new WaitForSeconds(0.09f);
		}
        TextLoaded = true;

        yield return new WaitForSeconds(3f);
        textLength = originalString.Substring(0, originalString.Length);

        while (numCharsRevealed < textLength.Length)
		{
            // Skips Spaces
            while (originalString[numCharsRevealed] == ' ') {
                ++numCharsRevealed;
            }

			++numCharsRevealed;
			textObject.text = originalString.Substring(0, numCharsRevealed);

			yield return new WaitForSeconds(0.09f);
		}
	}

    public void CanGoToMenu() {
        if (TextLoaded && Input.anyKeyDown) {
            ExpositionScreen.SetActive(false);
            ExpositionScreen.SetActive(false);
        }
    }
}

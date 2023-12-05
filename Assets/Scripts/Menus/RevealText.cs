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

		var numCharsRevealed = 0;
		while (numCharsRevealed < originalString.Length)
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
	}

    public void CanGoToMenu() {
        if (TextLoaded && Input.anyKeyDown) {
            ExpositionScreen.SetActive(false);
            ExpositionScreen.SetActive(false);
        }
    }
}

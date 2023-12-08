using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScreen : MonoBehaviour {
	private TMP_Text textObject;
    public GameObject DeathScreenText;
    public static bool playerDead;

	void Awake () {
        textObject = GetComponent<TMP_Text>();
        playerDead = false;
	}

    void Update() {
        if (playerDead) {
            StartCoroutine(RevealText());
        }
    }
	
	IEnumerator RevealText() {
		var originalString = textObject.text;
		var numCharsRevealed = 0;

        // reveals all of the text
		while (numCharsRevealed < originalString.Length)
		{
            // Skips Spaces
            while (originalString[numCharsRevealed] == ' ') {
                ++numCharsRevealed;
            }

			++numCharsRevealed;
			textObject.text = originalString.Substring(0, numCharsRevealed);

            // wait between each character
			yield return new WaitForSeconds(0.01f);
		}
	}
	
}

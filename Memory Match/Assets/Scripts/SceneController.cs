using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour {
	public const int gridRows = 4;
	public const int gridCols = 5;
	public const float offsetX = 2.0f;
	public const float offsetY = 2.0f;

	[SerializeField] MemoryCard originalCard;
	[SerializeField] Sprite[] images;
	[SerializeField] TMP_Text scoreLabel;
	[SerializeField] AudioClip[] audioClips;
	
	private MemoryCard firstRevealed;
	private MemoryCard secondRevealed;
	private int score = 0;

	public bool canReveal {
		get {return secondRevealed == null;}
	}

	void Start() {
		Vector3 startPos = originalCard.transform.position;

		// create shuffled list of cards
		int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9};

		numbers = ShuffleArray(numbers);

		// place cards in a grid
		for (int i = 0; i < gridCols; i++) {
			for (int j = 0; j < gridRows; j++) {
				MemoryCard card;

				// use the original for the first grid space
				if (i == 0 && j == 0) {
					card = originalCard;
				} else {
					card = Instantiate(originalCard) as MemoryCard;
				}

				// next card in the list for each grid space
				int index = j * gridCols + i;
				int id = numbers[index];
				card.SetCard(id, images[id], audioClips[id]);

				float posX = (offsetX * i) + startPos.x;
				float posY = -(offsetY * j) + startPos.y;
				card.transform.position = new Vector3(posX, posY, startPos.z);
			}
		}
	}

	// Knuth shuffle algorithm
	private int[] ShuffleArray(int[] numbers) {
		int[] newArray = numbers.Clone() as int[];
		for (int i = 0; i < newArray.Length; i++ ) {
			int tmp = newArray[i];
			int r = Random.Range(i, newArray.Length);
			newArray[i] = newArray[r];
			newArray[r] = tmp;
		}
		return newArray;
	}


    private void Update()
    {

		if(secondRevealed != null)
        {
			if (firstRevealed.Id == secondRevealed.Id)
			{
				score++;
				scoreLabel.text = $"Score: {score}";
				firstRevealed = null;
				secondRevealed = null;
			}

			else if (!secondRevealed.audioSource.isPlaying)
            {
				firstRevealed.Unreveal();
				secondRevealed.Unreveal();
				firstRevealed = null;
				secondRevealed = null;
			}

			//else let the secondRevealed play until ending on its own, or they player clicks on it
		}
    }



    public void CardRevealed(MemoryCard card) {
		if (firstRevealed == null) {
			firstRevealed = card;
		} else {
			firstRevealed.audioSource.Stop(); //change this to mute the first so that the second can be heard alone
			secondRevealed = card;
			//StartCoroutine(CheckMatch());
		}
	}

	/*
	private IEnumerator CheckMatch() {

		// increment score if the cards match
		if (firstRevealed.Id == secondRevealed.Id) {
			score++;
			scoreLabel.text = $"Score: {score}";
		}

		// otherwise turn them back over after .5s pause
		else {
			yield return new WaitForSeconds(secondRevealed.audioSource.clip.length);//change this to allow the secondRevealed audio clip to play completely
			//yield return new WaitWhile(secondRevealed.audioSource.isPlaying)
			firstRevealed.Unreveal();
			secondRevealed.Unreveal();
		}
		
		firstRevealed = null;
		secondRevealed = null;
	}
	*/

	public void Restart() {
		SceneManager.LoadScene("Scene");
	}
}

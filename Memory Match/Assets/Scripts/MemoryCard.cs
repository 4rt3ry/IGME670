using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour {
	[SerializeField] GameObject cardBack;
	[SerializeField] SceneController controller;
	public AudioSource audioSource;

	private int _id;

	public int Id {
		get {return _id;}
	}

	public void SetCard(int id, Sprite image, AudioClip audioClip) {
		_id = id;
		GetComponent<SpriteRenderer>().sprite = image;
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = audioClip;
	}

	public void OnMouseDown() {
		if (cardBack.activeSelf && controller.canReveal)
		{
			cardBack.SetActive(false);
			controller.CardRevealed(this);
			audioSource.Play();
		}
		else if (!cardBack.activeSelf) audioSource.Stop();
	}

	public void Unreveal() {
		cardBack.SetActive(true);
		audioSource.Stop();
	}
}

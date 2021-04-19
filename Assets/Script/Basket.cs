using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
public class Basket : MonoBehaviour
{
	// public GameObject score;
	public AudioClip basket;
	public Text ScoreText;
	void OnCollisionEnter()
	{
		GetComponent<AudioSource>().Play();
	}

	void OnTriggerEnter()
	{
		int currentScore = int.Parse(ScoreText.text) + 1;
		ScoreText.text = currentScore.ToString();
		AudioSource.PlayClipAtPoint(basket, transform.position);
	}
}
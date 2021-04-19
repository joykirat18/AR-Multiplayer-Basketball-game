using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonQueries : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject PlacementIndicator;
	public GameObject basketBallRing;
	public GameObject StartButton;
	public GameObject ball;
	// public static Text ScoreText;

	private PlacementIndicator placementIndicator;
	void Start()
	{

	}

	public void onStart()
	{
		placementIndicator = FindObjectOfType<PlacementIndicator>();
		GameObject obj = Instantiate(basketBallRing, placementIndicator.transform.position, placementIndicator.transform.rotation);
		GameObject b = Instantiate(ball, StartButton.transform.position, StartButton.transform.rotation);
		Destroy(PlacementIndicator);
		StartButton.SetActive(false);
	}



	// Update is called once per frame
	void Update()
	{

	}
}

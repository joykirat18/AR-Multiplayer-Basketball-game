using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class Shoot2 : MonoBehaviourPun
{
	// Start is called before the first frame update
	public GameObject ball;
	private Vector3 throwSpeed = new Vector3(0, 26, 40); //This value is a sure basket
	public Vector3 ballPos;
	private bool thrown = false;
	private GameObject ballClone;
	public GameObject spawnPosition;
	public GameObject shootbutton;

	public GameObject availableShotsGO;
	private int availableShots = 5;

	public GameObject meter;
	public GameObject arrow;
	private float arrowSpeed = 0.3f; //Difficulty
	private bool right = true;
	public TextMeshProUGUI playerNameText;

	private GameObject gameOver;

	// Use this for initialization
	void Start()
	{

		shootbutton.SetActive(true);
		/* Increase Gravity */
		Physics.gravity = new Vector3(0, -20, 0);
		if (photonView.IsMine)
		{
			//The player is local player. 

		}
		else
		{
			//The player is remote player
			// shootbutton.SetActive(false);
		}
		SetPlayerName();
	}
	void SetPlayerName()
	{
		if (playerNameText != null)
		{
			if (photonView.IsMine)
			{
				playerNameText.text = "YOU";
				playerNameText.color = Color.red;
			}
			else
			{
				playerNameText.text = photonView.Owner.NickName;
			}
		}
	}

	void FixedUpdate()
	{
		/* Move Meter Arrow */

		if (arrow.transform.position.x < -44f && right)
		{
			arrow.transform.position += new Vector3(arrowSpeed, 0, 0);
		}
		if (arrow.transform.position.x >= -44f)
		{
			right = false;
		}
		if (right == false)
		{
			arrow.transform.position -= new Vector3(arrowSpeed, 0, 0);
		}
		if (arrow.transform.position.x <= -62f)
		{
			right = true;
		}

		/* Shoot ball on Tap */
		ShootBall();

		// shootbutton.onClick.AddListener(ShootBall);

		/* Remove Ball when it hits the floor */

		if (ballClone != null && ballClone.transform.position.y < -16)
		{
			Destroy(ballClone);
			thrown = false;
			throwSpeed = new Vector3(0, 26, 40);//Reset perfect shot variable

			/* Check if out of shots */

			if (availableShots == 0)
			{
				arrow.GetComponent<Renderer>().enabled = false;
				Instantiate(gameOver, new Vector3(0.31f, 0.2f, 0), transform.rotation);
				Invoke("restart", 2);
			}
		}
	}
	public void ShootBall()
	{
		if (!thrown)
		{
			thrown = true;
			// availableShots--;
			// availableShotsGO.GetComponent<GUIText>().text = availableShots.ToString();

			ballClone = Instantiate(ball, spawnPosition.transform.position, transform.rotation) as GameObject;
			throwSpeed.y = throwSpeed.y + arrow.transform.position.x + 53;
			throwSpeed.z = throwSpeed.z + arrow.transform.position.x + 53;
			Debug.Log("ball created");
			ballClone.GetComponent<Rigidbody>().AddForce(throwSpeed, ForceMode.Impulse);
			// GetComponent<AudioSource>().Play();
		}
	}


	void restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}

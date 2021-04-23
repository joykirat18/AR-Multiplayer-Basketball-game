using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
public class Shoot : MonoBehaviourPun
{
	public GameObject ball;
	private Vector3 throwSpeed = new Vector3(0, 12, 2.7f); //This value is a sure basket
	public Vector3 ballPos;
	private bool thrown = false;
	private GameObject ballClone;
	public GameObject spawnPosition;
	public GameObject shootbutton;
	private int counter = 0;

	public GameObject availableShotsGO;
	public static int availableShots = 5;

	PhotonView photonView;
	public TextMeshProUGUI score;
	public GameObject meter;
	public GameObject arrow;
	private float arrowSpeed = 0.03f; //Difficulty
	private bool right = true;
	public TextMeshProUGUI playerNameText;
	public TextMeshProUGUI shotsLeft;

	public Text gameOver;

	public TextMeshProUGUI opponentPlayerName;
	public TextMeshProUGUI opponentPlayerScore;
	public TextMeshProUGUI opponentPlayerShotsLeft;
	public static int currentScore;
	public static int opponentPlayerScore_int;
	public static int opponentPlayerShotsLeft_int;


	// Use this for initialization
	void Start()
	{
		opponentPlayerScore_int = 0;
		opponentPlayerShotsLeft_int = 5;
		// opponentPlayerName.text = "";
		opponentPlayerScore.text = "";
		opponentPlayerShotsLeft.text = "";
		score.text = "0";
		// shotsLeft.text = "Player Ball Left : 5";
		photonView = GetComponent<PhotonView>();
		shotsLeft.text = "Player Ball Left " + availableShots.ToString();

		Physics.gravity = new Vector3(0, -20, 0);
		shootbutton.SetActive(true);
		/* Increase Gravity */
		if (photonView.IsMine)
		{
			//The player is local player. 

		}
		else
		{
			// opponentPlayerName.text = photonView.Owner.NickName;
			//The player is remote player
			// shootbutton.SetActive(false);
		}
		// SetPlayerName();
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
				opponentPlayerName.text = photonView.Owner.NickName;
			}
		}
	}

	void FixedUpdate()
	{
		/* Move Meter Arrow */

		if (arrow.transform.position.x < 0.25f && right)
		{
			arrow.transform.position += new Vector3(arrowSpeed, 0, 0);
		}
		if (arrow.transform.position.x >= 0.25f)
		{
			right = false;
		}
		if (right == false)
		{
			arrow.transform.position -= new Vector3(arrowSpeed, 0, 0);
		}
		if (arrow.transform.position.x <= -0.7f)
		{
			right = true;
		}

		/* Shoot ball on Tap */
		// ShootBall();

		// shootbutton.onClick.AddListener(ShootBall);

		/* Remove Ball when it hits the floor */
		// if (!photonView.IsMine)
		// {
		// 	opponentPlayerScore.text = "Opponent Score " + opponentPlayerScore_int.ToString();
		// 	opponentPlayerShotsLeft.text = "Opponent Shots Left  " + opponentPlayerShotsLeft_int.ToString();
		// }


		if (ballClone != null && ballClone.transform.position.y < -16)
		{
			Destroy(ballClone);
			thrown = false;
			throwSpeed = new Vector3(0, 12, 2.7f);//Reset perfect shot variable
			currentScore = int.Parse(score.text);

		}
		/* Check if out of shots */
		// if (ballClone == null)
		// {
		// 	StartCoroutine(ExampleCoroutine());
		// 	thrown = false;
		// 	throwSpeed = new Vector3(0, 12, 2.7f);//Reset perfect shot variable
		// 	currentScore = int.Parse(score.text);

		// }
		if (availableShots == 0)
		{
			arrow.GetComponent<Renderer>().enabled = false;
			if (currentScore > opponentPlayerScore_int)
			{
				gameOver.text = "You Won";
			}
			else if (currentScore < opponentPlayerScore_int)
			{
				gameOver.text = "You Lose";
			}
			else
			{
				gameOver.text = "Tie";

			}
			gameOver.text = "You Lose";
			Instantiate(gameOver, transform.position, transform.rotation);
			// Invoke("restart", 2);
		}

	}
	// IEnumerator ExampleCoroutine()
	// {
	// 	//Print the time of when the function is first called.
	// 	// Debug.Log("Started Coroutine at timestamp : " + Time.time);

	// 	//yield on a new YieldInstruction that waits for 5 seconds.
	// 	yield return new WaitForSeconds(2);

	// 	//After we have waited 5 seconds print the time again.
	// 	// Debug.Log("Finished Coroutine at timestamp : " + Time.time);
	// }
	public void ShootBall()
	{
		if (!thrown && availableShots > 0)
		{
			thrown = true;
			counter++;
			availableShots--;
			shotsLeft.text = "Player Ball Left " + availableShots.ToString();

			ballClone = Instantiate(ball, spawnPosition.transform.position, transform.rotation) as GameObject;
			throwSpeed.y = throwSpeed.y + arrow.transform.position.x / 2;
			// throwSpeed.y = throwSpeed.y;
			throwSpeed.z = throwSpeed.z + arrow.transform.position.x / 2;
			// throwSpeed.z = throwSpeed.z;
			Debug.Log("ball created");
			ballClone.GetComponent<Rigidbody>().AddForce(throwSpeed, ForceMode.Impulse);
			// GetComponent<AudioSource>().Play();
		}
		if (availableShots == 0)
		{
			Instantiate(gameOver, transform.position, transform.rotation);
		}
	}


	void restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	// 	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	// 	{
	// 		if (stream.IsWriting)
	// 		{
	// 			// int currentScore = int.Parse(ScoreText.text);
	// 			stream.SendNext(currentScore);
	// 			// int shots_left = int.Parse(availableShots.text);
	// 			stream.SendNext(availableShots);
	// 		}
	// 		else
	// 		{
	// 			opponentPlayerScore_int = (int)stream.ReceiveNext();
	// 			opponentPlayerShotsLeft_int = (int)stream.ReceiveNext();
	// 		}
	// 	}
}
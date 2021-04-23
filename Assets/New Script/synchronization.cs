using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class synchronization : MonoBehaviour, IPunObservable
{
	// Start is called before the first frame update
	PhotonView photonView;
	public TextMeshProUGUI opponentPlayerScore;
	public TextMeshProUGUI opponentPlayerShotsLeft;
	public TextMeshProUGUI opponentPlayerName;
	void Start()
	{
		photonView = GetComponent<PhotonView>();
		// if (!photonView.IsMine)
		// {
		//The player is local player. 
		// opponentPlayerName.text = photonView.Owner.NickName;
		// }


	}

	// Update is called once per frame
	void FixedUpdate()
	{
		// if (!photonView.IsMine)
		// {
		opponentPlayerScore.text = Shoot.opponentPlayerScore_int.ToString();
		Debug.Log(opponentPlayerScore.text);
		opponentPlayerShotsLeft.text = "Opponent Shots Left  " + Shoot.opponentPlayerShotsLeft_int.ToString();
		Debug.Log(opponentPlayerShotsLeft.text);
		// }

	}
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			// int currentScore = int.Parse(ScoreText.text);
			stream.SendNext(Shoot.currentScore);
			// int shots_left = int.Parse(availableShots.text);
			stream.SendNext(Shoot.availableShots);
		}
		else
		{
			Shoot.opponentPlayerScore_int = (int)stream.ReceiveNext();
			Shoot.opponentPlayerShotsLeft_int = (int)stream.ReceiveNext();
		}
	}
}

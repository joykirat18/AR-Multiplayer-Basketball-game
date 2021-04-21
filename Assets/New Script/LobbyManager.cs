using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class LobbyManager : MonoBehaviourPunCallbacks
{

	[Header("Login UI")]
	public InputField playerNameInputField;
	public GameObject uI_LoginGameObject;

	[Header("Lobby UI")]
	public GameObject uI_LobbyGameObject;
	public GameObject uI_3DGameObject;

	[Header("Connection Status UI")]
	public GameObject uI_ConnectionStatusGameObject;
	public Text connectionStatusText;
	public bool showConnectionStatus = false;

	// Start is called before the first frame update
	#region UNITY Methods
	void Start()
	{
		if (PhotonNetwork.IsConnected)
		{
			uI_LobbyGameObject.SetActive(true);
			uI_3DGameObject.SetActive(true);
			uI_ConnectionStatusGameObject.SetActive(false);
			uI_LoginGameObject.SetActive(false);
		}
		else
		{
			uI_LobbyGameObject.SetActive(false);
			uI_3DGameObject.SetActive(false);
			uI_ConnectionStatusGameObject.SetActive(false);
			uI_LoginGameObject.SetActive(true);
		}

	}

	// Update is called once per frame
	void Update()
	{
		if (showConnectionStatus)
		{
			connectionStatusText.text = "Connection Status : " + PhotonNetwork.NetworkClientState;

		}
	}

	#endregion

	#region UI Callback Methods
	public void onEnterGameButtonClicked()
	{
		string playerName = playerNameInputField.text;
		if (!string.IsNullOrEmpty(playerName))
		{
			uI_LobbyGameObject.SetActive(false);
			uI_3DGameObject.SetActive(false);
			showConnectionStatus = true;
			uI_ConnectionStatusGameObject.SetActive(true);
			uI_LoginGameObject.SetActive(false);
			if (!PhotonNetwork.IsConnected)
			{
				PhotonNetwork.LocalPlayer.NickName = playerName;
				PhotonNetwork.ConnectUsingSettings();
			}
		}
		else
		{
			Debug.Log("Player Name is invalid or empty!");
		}
	}

	public void OnQuickMatchButtonClicked()
	{
		// SceneManager.LoadScene("Scene_Loading");
		SceneLoader.Instance.LoadScene("Scene_PlayerSelection");
	}
	#endregion

	#region PHOTON Callback Methods
	public override void OnConnected()
	{
		Debug.Log("We connected to Internet");

	}
	public override void OnConnectedToMaster()
	{
		Debug.Log(PhotonNetwork.LocalPlayer.NickName + " is connected to the server");
		uI_LobbyGameObject.SetActive(true);
		uI_3DGameObject.SetActive(true);
		uI_ConnectionStatusGameObject.SetActive(false);
		uI_LoginGameObject.SetActive(false);
	}
	#endregion

}

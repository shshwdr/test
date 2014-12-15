using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	public GameObject StandbyCamera;
	SpawnSpot[] spawnSpots;

	// Use this for initialization
	void Start () {
		spawnSpots=GameObject.FindObjectsOfType<SpawnSpot>();
	Connect ();
	
	}
	
	void Connect(){
		PhotonNetwork.ConnectUsingSettings("1.0.0");//version
		//PhotonNetwork.offlineMode=true;
		
	}
	void OnGUI(){
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
	void OnJoinedLobby()
	{
		Debug.Log("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom();
	}
	void OnPhotonRandomJoinFailed(){
		Debug.Log("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom(null);
	}
	void OnJoinedRoom(){
		Debug.Log("OnjoinedRoom");
		SpawnMyPlayer();
	}
	void SpawnMyPlayer(){
		//Instantiate(playerPrefab);
		if(spawnSpots==null){Debug.Log("wtf?");return;}
		SpawnSpot mySpawnSpot=spawnSpots[Random.Range(0,spawnSpots.Length)];
		GameObject myPlayerGO=PhotonNetwork.Instantiate("First Person Controller",mySpawnSpot.transform.position,mySpawnSpot.transform.rotation,0);
		
		
		((MonoBehaviour)myPlayerGO.GetComponent("FPSInputController")).enabled=true;
		((MonoBehaviour)myPlayerGO.GetComponent("MouseLook")).enabled=true;
		((MonoBehaviour)myPlayerGO.GetComponent("CharacterMotor")).enabled=true;
		myPlayerGO.transform.FindChild("Main Camera").gameObject.SetActive(true);
		
		StandbyCamera.SetActive(false);
	}
}

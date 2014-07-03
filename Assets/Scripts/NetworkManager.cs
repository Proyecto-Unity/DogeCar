using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {
	public Camera NormalCamara;
	bool connecting = false;
	List<string> log;

	SpawnSpot[] spawnSpots;

	void Start(){
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
		PhotonNetwork.player.name = PlayerPrefs.GetString("Username","Doge");
		log = new List<string>();
	}

	void Connect() {
		PhotonNetwork.ConnectUsingSettings ("DogeCar v1.0.2");
	}
	
	void OnGUI() {
		GUILayout.Label( PhotonNetwork.connectionStateDetailed.ToString() );

		if (PhotonNetwork.connected == false && connecting == false ) {
			GUILayout.BeginArea( new Rect(0,0,Screen.width, Screen.height) );
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("Username: ");
			PhotonNetwork.player.name = GUILayout.TextField(PhotonNetwork.player.name );
			GUILayout.EndHorizontal();
			
			
			if( GUILayout.Button("Single Player") ) {
				connecting = true;
				PhotonNetwork.offlineMode = true;
				OnJoinedLobby ();
			}
			if ( GUILayout.Button("Multi Player") ) {
				connecting = true;
				Connect();
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}
		
		if (PhotonNetwork.connected == true && connecting == false) {
			GUILayout.BeginArea( new Rect(0,0,Screen.width, Screen.height) );
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();
			
	
			
			GUILayout.EndVertical();
			GUILayout.EndArea();
		}


	}
	void OnJoinedLobby(){
		Debug.Log ("Joined Lobby");
		PhotonNetwork.JoinRandomRoom ();
	}
	
	void OnPhotonRandomJoinFailed(){
		Debug.Log ("Random Join Failed");
		PhotonNetwork.CreateRoom (null);
	}
	
	void OnJoinedRoom(){
		Debug.Log ("Joined Room");
		SpawnPlayer();
	}


	void SpawnPlayer(){

		if(spawnSpots == null){
			Debug.Log("WTF");
			return;
		}

		SpawnSpot mySpawnSpot = spawnSpots[Random.Range(0 , spawnSpots.Length)];
		GameObject MyPlayerGO = (GameObject) PhotonNetwork.Instantiate("Auto", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
		//NormalCamara.enabled = false;
		//((MonoBehaviour) MyPlayerGO.GetComponent("")).enabled = true;
		//MyPlayerGO.transform.FindChild("Camara").gameObject.SetActive(true);
		GameObject Cam = GameObject.Find("Camara Principal");
		Cam.GetComponent<SmoothFollow>().enabled = true;
		Cam.GetComponent<SmoothFollow>().target = MyPlayerGO.transform;

	}


}

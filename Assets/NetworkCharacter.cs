using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {

	Vector3 realPosition;
	Quaternion realRotation;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//GetComponent<PhotonView>().isMine
		if(photonView.isMine){}
		else{
			transform.position=Vector3.Lerp(transform.position,realPosition,0.1f);
			transform.rotation=Quaternion.Lerp(transform.rotation,realRotation,0.1f);
		}
	}
	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
	{
		if(stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else
		{
			realPosition=(Vector3)stream.ReceiveNext();
			realRotation=(Quaternion) stream.ReceiveNext();
		}
	}
}

using UnityEngine;
using System.Collections;

public class DeleteZoneController : MonoBehaviour {

	public GameObject player;
	public float deleteLength = 10;
	
	void Update(){
		transform.position = new Vector3(player.transform.position.x-deleteLength, transform.position.y, player.transform.position.z-deleteLength);
	}
	void OnTriggerEnter (Collider other) {
		if(other.gameObject.tag != "floor"){
			Destroy(other.gameObject);
		}
	}
	
	void OnCollisionEnter(Collision collis) 
	{ 	
		if(collis.gameObject.tag != "floor"){
			Destroy(collis.gameObject);				
		}
	} 
}

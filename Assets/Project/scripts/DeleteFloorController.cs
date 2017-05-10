using UnityEngine;
using System.Collections;

public class DeleteFloorController : MonoBehaviour {

	public GameObject player;
	float x, z;
	
	void Start () {
		x = gameObject.transform.position.x;
		z = gameObject.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3(player.transform.position.x + x, transform.position.y, player.transform.position.z + z);
	}
	
	void OnTriggerEnter (Collider other) {
		if(other.gameObject.tag == "floor"){
			Destroy(other.gameObject);
		}
	}
	
	void OnCollisionEnter(Collision collis) 
	{ 	
		if(collis.gameObject.tag == "floor"){
			Destroy(collis.gameObject);				
		}
	} 
}

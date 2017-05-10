using UnityEngine;
using System.Collections;

public class PlatformsController : MonoBehaviour {

	public GameObject settings;
	
	void OnTriggerEnter (Collider other) {
		
		dropPlatform(other.gameObject);
		
	}
	
	void OnCollisionEnter(Collision collis) 
	{ 	
		dropPlatform(collis.gameObject);
	} 
	
	void dropPlatform(GameObject o){
		if(settings.GetComponent<Settings>().gameState == true){
			//o.GetComponent<Rigidbody>().useGravity = true;
			//o.GetComponent<BoxCollider>().isTrigger = true;
            if (o.GetComponent<PlatformsColor>())
            {
                o.GetComponent<PlatformsColor>().lightOff();
            }
		}
	}
}

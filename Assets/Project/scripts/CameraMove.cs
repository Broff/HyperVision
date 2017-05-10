using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public GameObject sett, player;
	public float x1,z1,x2,z2, speed;
	Rigidbody rb;
	void Start () {
		rb = player.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(sett.GetComponent<Settings>().gameState == true){
			if(rb.velocity.x > 1){
				gameObject.transform.position=Vector3.Lerp(transform.position, 
				new Vector3(player.transform.position.x+x1, transform.position.y, player.transform.position.z+z1), Time.deltaTime*speed);
			} else {
				gameObject.transform.position=Vector3.Lerp(transform.position, 
				new Vector3(player.transform.position.x+x2, transform.position.y, player.transform.position.z+z2), Time.deltaTime*speed);
			}
		}
	}

    public bool getWayToX()
    {
        return player.GetComponent<PlayerController>().getWayToX();
    }
}

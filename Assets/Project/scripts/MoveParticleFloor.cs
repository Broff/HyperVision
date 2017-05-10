using UnityEngine;
using System.Collections;

public class MoveParticleFloor : MonoBehaviour {

	public GameObject obj;
	public float dlt, x;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(obj.transform.position.z + dlt,transform.position.y, obj.transform.position.z + dlt);
	}
}

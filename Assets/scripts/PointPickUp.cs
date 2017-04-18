using UnityEngine;
using System.Collections;

public class PointPickUp : MonoBehaviour {

	public GameObject pointEff;
	Color c1 = Color.green;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void pickUp(){
		GameObject o = Instantiate(pointEff);
		o.GetComponent<ParticleSystem>().startColor =  c1;
		o.transform.Find("point_eff_pick_sub").gameObject.GetComponent<ParticleSystem>().startColor =  c1;
		o.transform.position = gameObject.transform.position;
		o.SetActive(true);		
		Destroy(gameObject);
	}
	
	public void setEffColor(Color c){
		c1 = c;
	}
}

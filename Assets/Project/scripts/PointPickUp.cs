using UnityEngine;
using System.Collections;

public class PointPickUp : MonoBehaviour {

	public GameObject pointEff;
	Color c1 = Color.green;
    AudioSource audio;
    Settings s;

	void Start () {
        audio = GetComponent<AudioSource>();
        s = FindObjectOfType<Settings>();
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
        s.GetComponent<Settings>().Coins += 1;
        audio.PlayOneShot(audio.clip);
		Destroy(gameObject, audio.clip.length + 0.5f);
	}
	
	public void setEffColor(Color c){
		c1 = c;
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundController : MonoBehaviour {
    
	public Sprite on, off;
	public GameObject mainTheme;
	
	void Start () {
		mainTheme = GameObject.Find("MainTheme");
		setImage();		
	}
	
	void setImage(){
		if(PlayerPrefs.GetInt("SoundOn") == 0){
			gameObject.GetComponent<Button>().image.sprite = on;
		} else {
			gameObject.GetComponent<Button>().image.sprite = off;
		}
	}
	
	public void click(){
		int a = PlayerPrefs.GetInt("SoundOn");
		if(a == 0){
			a = 1;
		} else {
			a = 0;
		}
		PlayerPrefs.SetInt("SoundOn", a);
		setImage();
		mainTheme.GetComponent<MainSoundController>().muteSound();
	}
}

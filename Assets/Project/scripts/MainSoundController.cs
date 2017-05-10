using UnityEngine;
using System.Collections;

public class MainSoundController : MonoBehaviour {
		
	public static MainSoundController instance = null;
	
	void Start () {
        if(instance!=null){
            Destroy(gameObject);
			Debug.Log ("One Music Destroyed");
            return;
        }
	    instance = this;
        DontDestroyOnLoad (gameObject);
		muteSound();
	}	
	
	public void muteSound(){
		if(PlayerPrefs.GetInt("SoundOn") == 1){
			AudioListener.volume = 0;
		} else {
			AudioListener.volume = 1;
		}
	}
}

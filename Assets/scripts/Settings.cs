using UnityEngine;
public class Settings : MonoBehaviour {

	public bool onPhone = false;
	public bool testBuild = false;
	public bool gameState = true;
	public bool wayToX = true;
	
	void Start(){
        Time.timeScale = 1;
		#if UNITY_EDITOR
			//onPhone = false;
		#endif
		
		#if UNITY_ANDROID
			//onPhone = true;
		#endif
	}	
	
	public void setWay(){
		wayToX = !wayToX;
	}
}

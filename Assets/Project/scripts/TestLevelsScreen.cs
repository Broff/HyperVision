using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TestLevelsScreen : MonoBehaviour {

	public GameObject levelGenerator;
	public InputField start, finish;
	static int[] patt = new int[2];
	
	public void View () {
		gameObject.SetActive (true);		
	}

	public void Skip () {
		gameObject.SetActive (false);
	}

	public void Click () {
		TestLevelsScreen.patt[0] = Convert.ToInt32(start.text);
		TestLevelsScreen.patt[1] = Convert.ToInt32(finish.text);	
		generateStart();
		Skip();
	}
	
	public void generateStart(){
		levelGenerator.GetComponent<LevelGenerator>().StartTest(TestLevelsScreen.patt);
	}
}


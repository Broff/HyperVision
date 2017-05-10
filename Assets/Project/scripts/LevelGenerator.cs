using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
    [Header("Settings")]
    [Range(1, 3)]
    public int colorCount = 2;
    [Range(0, 1)]
    public int levelsListNum = 0;
    public string[] levelsStr0;
    public string[] levelsStr1;
    [Space(10)]
    [Header("Settings")]
    public Transform platformsContainer;
	public GameObject settings;
	public GameObject ColorPoint, block, unactiveBlock, coloredBlock;
	public GameObject[] enviroments;
	public float mountDlt = 3;
	public GameObject player, colorController;
	public int lvlStartGeneration = 4;
	public float boxPosY = 0;
	public float boxSize = 2;
	public float startX, startZ;	
	public GameObject setVY, setVX;
	public float wallDlt = 0.338f;
	float posX, posZ;	
	
	int defInt = 0;
	int[] defaultLevels = new int[]{0,1,1,1,2,2,2,3,3,2,2,3,3,3,1};
	int index = 0;
	int[] pattern = new int[]{2,1,2,3,3,2,3,3,3,3,3,3,3,1,3,2,3};
	
	ArrayList arr0 = new ArrayList();
	ArrayList arr1 = new ArrayList();
	ArrayList arr2 = new ArrayList();
	ArrayList arr3 = new ArrayList();
		
	ArrayList allLevels = new ArrayList();
	bool testlevels = false;
	static int[] patternTest = new int[2];
	int startTestIndex;
	
	void Start () {
		posX = startX;
		posZ = startZ;			
		testlevels = settings.GetComponent<Settings>().testBuild;
		parseFile();
		
		if(testlevels == false){
			startGeneration();
		}
	}
    	
	public void StartTest(int[] patt){
		LevelGenerator.patternTest = patt;
		startTestIndex = pattern[0];
		startGeneration();
	}
	
	void Update () {
		
	}
	
	void startGeneration(){
		for(int i = 0; i < lvlStartGeneration; i++){
			generateLine();
		}
	}
	
	public void generateLine(){
		string s;
		if(testlevels == false){
			s = getStringLevel();			
		} else {
			s = getTestStringLevel(); 
		}
		
		string[] points = (s.Split('!')[0]).Split(',');
        int charValue = 0;		
		for(int i = 0; i <= points.Length-1; i++){
            int c1 = charValue = Convert.ToInt32(points[i]);
			
			if(c1 == -1){
				GameObject po = createWaypoint(0);		
				settings.GetComponent<Settings>().setWay();
				Vector3 ve = new Vector3(po.transform.position.x+1, player.transform.position.y, po.transform.position.z);
				player.GetComponent<PlayerController>().addPoint(ve);
			} else {
                int next = 0;
                int prev = 0;
                if (i != points.Length - 1)
                {
                    next = Convert.ToInt32(points[i + 1]);
                }

                if (i != 0)
                {
                    prev = Convert.ToInt32(points[i - 1]);
                }

                if ((charValue != 0 || charValue != -1) && prev != charValue)
                {
				    createPoint(c1,true);
                }
                else if ((charValue != 0 || charValue != -1) && next != charValue)
                {
                    createPoint(c1, true);
                }
                else
                {
                    createPoint(c1, false);
                }
			}	
		}
		
		GameObject p = createWaypoint(Convert.ToInt32(points[points.Length-1]));			
		settings.GetComponent<Settings>().setWay();
		Vector3 v = new Vector3(p.transform.position.x+1, player.transform.position.y, p.transform.position.z);
		player.GetComponent<PlayerController>().addPoint(v);	
	}
	
	void createPoint(int colorNum, bool p){
		createWaypoint(colorNum, true, p);
	}
		
	GameObject createWaypoint(int colorNum, bool crePoint = false, bool point = false){
        if (colorNum >= colorCount)
        {
            colorNum = colorCount;
        }

		GameObject o;
			if(colorNum == 0){
				o = Instantiate(unactiveBlock);
			} else {
				o = Instantiate(coloredBlock);
			}
			setColor(o,colorNum);
			switch(colorNum){
				case 1:
				o.tag = "c1";
				break;
				case 2:
				o.tag = "c2";
				break;
				case 3:
				o.tag = "c3";
				break;
				default :				
				o.tag = "block";
				break;
			}		
		
		if(settings.GetComponent<Settings>().wayToX == true){
			posX+=boxSize;
			o.transform.position = new Vector3(posX-1, boxPosY, posZ);
            if (point == true && colorNum != 0)
            {
				GameObject pointAnimated = Instantiate(ColorPoint);
                pointAnimated.transform.parent = platformsContainer;
				pointAnimated.transform.position = new Vector3(posX, pointAnimated.transform.position.y, posZ);	
				colorController.GetComponent<ColorController>().getPointColor(pointAnimated, colorNum);
				pointAnimated.SetActive(true);
			}
			if(crePoint == false){
			GameObject wall = Instantiate(setVX);
			wall.transform.position = new Vector3(posX + wallDlt, wall.transform.position.y, posZ - wallDlt);
            wall.transform.parent = platformsContainer;
			wall.SetActive(true);
			GameObject m = Instantiate(getRandomMount());
			colorController.GetComponent<ColorController>().getColor(m, UnityEngine.Random.Range(1,4));
			mountNewScale(m);
			mountNewPos(m, false);
			mountNewRotate(m);
            m.transform.parent = platformsContainer;
			m.SetActive(true);	

			GameObject m2 = Instantiate(getRandomMount());
			colorController.GetComponent<ColorController>().getColor(m2, UnityEngine.Random.Range(1,4));
			mountNewScale(m2);
			mountNewRotate(m2);
			m2.transform.position = new Vector3(posX + boxSize*UnityEngine.Random.Range(mountDlt, mountDlt+3) ,
					m2.transform.position.y, posZ);
            m2.transform.parent = platformsContainer;
			m2.SetActive(true);
			}
		} else {
			posZ+=boxSize;
			o.transform.position = new Vector3(posX-1, boxPosY, posZ);
            if (point == true && colorNum != 0)
            {
				GameObject pointAnimated = Instantiate(ColorPoint);
                pointAnimated.transform.parent = platformsContainer;
				pointAnimated.transform.position = new Vector3(posX, pointAnimated.transform.position.y, posZ);	
				colorController.GetComponent<ColorController>().getPointColor(pointAnimated, colorNum);
				pointAnimated.SetActive(true);
			}
			if(crePoint == false){
			GameObject wall = Instantiate(setVY);
			wall.transform.position = new Vector3(posX - wallDlt, wall.transform.position.y, posZ + wallDlt);
            wall.transform.parent = platformsContainer;
			wall.SetActive(true);	
			GameObject m = Instantiate(getRandomMount());
            m.transform.parent = platformsContainer;
			colorController.GetComponent<ColorController>().getColor(m, UnityEngine.Random.Range(1,4));
			mountNewScale(m);
			mountNewPos(m);
			mountNewRotate(m);
			m.SetActive(true);
			
			GameObject m2 = Instantiate(getRandomMount());
            m2.transform.parent = platformsContainer;
			colorController.GetComponent<ColorController>().getColor(m2, UnityEngine.Random.Range(1,4));
			mountNewScale(m2);
			mountNewRotate(m2);	
			m2.transform.position = new Vector3(posX ,
					m2.transform.position.y, posZ + boxSize*UnityEngine.Random.Range(mountDlt, mountDlt+3));			
			m2.SetActive(true);
			}
		}
        o.transform.parent = platformsContainer;
		o.SetActive(true);
        
		return o;		
	}
	
	void mountNewPos(GameObject m, bool a = true, int plusDlt = 3){
		if(a == true){
			m.transform.position = new Vector3(posX + boxSize*UnityEngine.Random.Range(mountDlt, mountDlt+plusDlt) ,
					m.transform.position.y, posZ - boxSize*UnityEngine.Random.Range(mountDlt, mountDlt+plusDlt));
		} else {
			m.transform.position = new Vector3(posX - boxSize*UnityEngine.Random.Range(mountDlt, mountDlt+plusDlt) ,
					m.transform.position.y, posZ + boxSize*UnityEngine.Random.Range(mountDlt, mountDlt+plusDlt));
		}
	}
	
	void mountNewRotate(GameObject m){
		m.transform.rotation = Quaternion.Euler(0,UnityEngine.Random.Range(0, 360),0);
	}
	
	void mountNewScale(GameObject m){
		int scale = UnityEngine.Random.Range(3, 6);
		m.transform.localScale = new Vector3(scale, scale, scale);
	}
	
	GameObject getRandomMount(){
		GameObject m = enviroments[UnityEngine.Random.Range(0,enviroments.Length)];
		return  m;
	}
	
	void setColor(GameObject o, int color){
		colorController.GetComponent<ColorController>().getColor(o, color);
	}
	
	string getTestStringLevel(){
		if(startTestIndex > patternTest[1]){
			startTestIndex = patternTest[0];
		}
		string s = (string)allLevels[startTestIndex];
		startTestIndex++;
		return s;
	}
	
	string getStringLevel(){
		int diffic = 0;
		if(defInt < defaultLevels.Length){
			diffic = Convert.ToInt32(defaultLevels[defInt]);
			defInt++;
		} else {
			diffic = Convert.ToInt32(pattern[index]);
			index++;
			if(index > pattern.Length-1){
				index = 0;
			}
		}
		string s;
		if(diffic==0){
			s = (string)arr0[UnityEngine.Random.Range(0, arr0.Count)];
		} else if(diffic==1){
			s = (string)arr1[UnityEngine.Random.Range(0, arr1.Count)];
		} else if(diffic==2){
			s = (string)arr2[UnityEngine.Random.Range(0, arr2.Count)];
		} else {
			s = (string)arr3[UnityEngine.Random.Range(0, arr3.Count)];
		}
		return s;
	}
	
	void parseFile(){
        string[] linesString = null;

        if (levelsListNum == 0)
        {
            linesString = levelsStr0;
        }
        else if (levelsListNum == 1){
            linesString = levelsStr1;
        }

		string[] lvl;
		for(int i = 0; i <= linesString.Length-1; i++){
			lvl = linesString[i].Split('!');
			int t = Convert.ToInt32(lvl[1]);
			if(t == 0){
				arr0.Add(linesString[i]);
			} else if(t == 1){
				arr1.Add(linesString[i]);
			} else if(t == 2){
				arr2.Add(linesString[i]);
			} else if(t == 3) {
				arr3.Add(linesString[i]);
			}	
			if(testlevels == true){			
				allLevels.Add(linesString[i]);
			}
		}
	}

    string s = "0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,-1,0,0,0,0,2,2,2,2,0,0,-1,0,0,0,3,3,3,0,0,0!0\n"+
    "0,0,3,3,0,0,0,1,1,1,0,0,0,-1,0,0,0,3,3,0,0,0,1,1,1,0!3\n"+
    "0,0,1,1,1,0,-1,0,0,2,2,2,0,0,-1,0,0,-1,0,0,3,3,3,0!3\n"+
    "0,0,3,3,0,0,0,1,1,1,0,0,-1,0,0,3,3,0,0,0,2,2,2,0!3\n"+
    "0,0,0,1,1,1,0,0,-1,0,0,3,3,0,0,-1,0,0,2,2,0,0!3\n"+
    "0,0,1,1,0,-1,0,0,2,2,0,0,-1,0,0,3,3,0,0!3\n"+
    "0,0,0,1,1,0,0,0,3,3,3,3,0!3\n"+
    "0,0,0,2,2,0,0,0,1,1,1,1,0!3\n"+
    "0,0,0,3,3,0,0,0,2,2,2,2,0!3\n"+
    "0,0,0,1,1,0,0,0,0,3,3,0!3\n"+
    "0,0,0,3,3,0,0,0,0,2,2,0!3\n"+
    "0,0,3,3,0,0,0,2,2,2,0!3\n"+
    "0,0,1,1,0,0,0,3,3,3,0!3\n"+
    "0,0,2,2,0,0,0,1,1,1,0!3\n"+
    "0,0,2,2,0,0,0,0,1,1,0!3\n"+
    "0,0,0,1,1,0,0,0,2,2,2,2,0!2\n"+
    "0,0,3,3,0,0,0,1,1,1,1,0!2\n"+
    "0,0,0,1,1,0,0,0,0,2,2,0!2\n"+
    "0,0,0,2,2,0,0,0,0,3,3,0!2\n"+
    "0,0,3,3,0,0,0,0,1,1,0!2\n"+
    "0,0,3,3,0,0,0,2,2,2,0!2\n"+
    "0,0,3,3,0,0,0,3,3,3,0!2\n"+
    "0,0,1,1,0,0,0,1,1,1,0!2\n"+
    "0,0,1,1,0,0,0,2,2,2,0!2\n"+
    "0,0,2,2,0,0,0,3,3,3,0!2\n"+
    "0,0,0,-1,0,0,0,1,1,1,-1,1,1,1,0,0!1\n"+
    "0,0,0,2,2,0,0,0,3,3,3,3,0!1\n"+
    "0,0,1,1,0,0,0,1,1,1,1,0!1\n"+
    "0,0,2,2,0,0,0,2,2,2,2,0!1\n"+
    "0,0,3,3,0,0,0,3,3,3,3,0!1\n"+
    "0,0,0,2,2,0,0,0,0,2,2,0!1\n"+
    "0,0,0,3,3,0,0,0,0,3,3,0!1\n"+
    "0,0,0,2,2,2,2,2,2,0,0!1\n"+
    "0,0,0,3,3,3,3,3,3,0,0!1\n"+
    "0,0,2,2,0,0,0,2,2,2,0!1\n"+
    "0,0,3,3,0,0,0,3,3,3,0!1\n"+
    "0,0,1,1,0,0,0,0,1,1,0!1\n"+
    "0,0,0,1,1,1,1,1,0,0!1\n"+
    "0,0,0,2,2,2,2,2,0,0!1\n"+
    "0,0,0,3,3,3,3,3,0,0!1\n"+
    "0,0,0,2,2,2,0,0!1\n"+
    "0,0,0,3,3,3,0,0!1";
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    [Header("Effects")]
    public GameObject particle;
    [Space(5)]
	public GameObject settings;
	public GameObject generator;
	public GameObject colorController;
	public GameObject testLevelsMenu, mainMenu, scoreMenu, deadMenu;
    public GameObject deadPlayer;
	public float timeSwitch = 0.5f;
	public float moveSpeed = 2000;
	public float maxSpeed = 650;
	public float deltaUpSpeed = 5;
	public int frequencySpeedUp = 3;
    [Header("Sounds")]
    public AudioClip switchColor;
	float playerSpeed;
	public float angleMoveSpeed = 700;
	LinkedList<Vector3> points = new LinkedList<Vector3>();
	int score = 0;	
	int colorNum = 1;
	Rigidbody rb;
	bool wayToX = true;
	static bool firstLaunch = true;
    private AudioSource audio;
	
	float startPos = 30;
	public GameObject nextFloor;
	public float nextFloorPos = 70.7f;
	
	void Start () {
        audio = GetComponent<AudioSource>();
		playerSpeed = moveSpeed;
		rb = transform.GetComponent<Rigidbody>();
		if(firstLaunch == true){
			mainMenu.GetComponent<MainScreenController> ().View ();
			firstLaunch = false;
			if(settings.GetComponent<Settings>().testBuild == true){
				testLevelsMenu.GetComponent<TestLevelsScreen> ().View();
			}
		} else {
			//setGameState(true);
			scoreMenu.GetComponent<ScoreScreenController>().View();
			scoreMenu.GetComponent<ScoreScreenController> ().TapToPlay();
			if(settings.GetComponent<Settings>().testBuild == true){
				testLevelsMenu.GetComponent<TestLevelsScreen> ().generateStart();
			}
			//playerMove();
		}		
		//scoreMenu.GetComponent<ScoreScreenController>().updateScore(score);
		//setColorPlayer();		
		//playerMove();
	}

    private Vector3 rotateValue = new Vector3();

    void RotatePlayer()
    {
        gameObject.transform.Rotate(rotateValue*Time.deltaTime);
    }

	void Update () {
		if(settings.GetComponent<Settings>().gameState == true){

			if(points.Count > 0){
				if(points.First.Value.x <= transform.position.x && points.First.Value.z <= transform.position.z)
				{
					generator.GetComponent<LevelGenerator>().generateLine();
					nextAngle();
					points.RemoveFirst();
					rb.velocity = Vector3.zero;
					rb.angularVelocity = new Vector3(0, 0, 0); 
					if(wayToX == true){
						rb.angularVelocity = new Vector3(1,0,0)*angleMoveSpeed;                       
                        rb.AddForceAtPosition(new Vector3(0, 0, 1) * playerSpeed, transform.position);                        
					} else {					
						rb.angularVelocity = new Vector3(0,0,-1)*angleMoveSpeed;                       
                        rb.AddForceAtPosition(new Vector3(1, 0, 0) * playerSpeed, transform.position);                        			
					}
					wayToX = !wayToX;
				} 
			}      
		}
	}
	
	public void setColorPlayer(){
        audio.PlayOneShot(switchColor);
        colorNum = colorController.GetComponent<ColorController>().NextColor();       
		particle.transform.Find("sphere_eff_01").gameObject.GetComponent<ParticleSystem>().startColor = colorController.GetComponent<ColorController>().getColorValue(colorNum);
		particle.transform.Find("sphere_eff_02").gameObject.GetComponent<ParticleSystem>().startColor = colorController.GetComponent<ColorController>().getColorValue(colorNum);   
        colorController.GetComponent<ColorController>().getColorPlayer(gameObject, colorNum);
        colorController.GetComponent<ColorController>().getColor(gameObject.transform.GetChild(0).gameObject, colorNum);         
	}

    public void UpdateColor(GameObject skin)
    {
        colorController.GetComponent<ColorController>().getColor(skin, colorNum);       
        particle.transform.Find("sphere_eff_01").gameObject.GetComponent<ParticleSystem>().startColor = colorController.GetComponent<ColorController>().getColorValue(colorNum);
        particle.transform.Find("sphere_eff_02").gameObject.GetComponent<ParticleSystem>().startColor = colorController.GetComponent<ColorController>().getColorValue(colorNum);  
        colorController.GetComponent<ColorController>().getColor(gameObject, colorNum);
         
    }
	
	int angleCount = 0;
	
	void nextAngle(){
		angleCount++;
		if(angleCount % frequencySpeedUp == 0){
			if(playerSpeed < maxSpeed){
				playerSpeed += deltaUpSpeed;
				Debug.Log(playerSpeed);
			}
		}
		
		if(angleCount%2==0 ){
			startPos += nextFloorPos;
			GameObject f = Instantiate(nextFloor);
			f.transform.position = new Vector3(startPos, f.transform.position.y, startPos);
			f.SetActive(true);
		}		
	}
	
	void upScore(){
		score++;
        settings.GetComponent<Settings>().Score = score;
	}
	
	public void playerMove(){
		if(wayToX == true){
			rb.angularVelocity = new Vector3(0,0,-1)*angleMoveSpeed; 
			rb.AddForceAtPosition(new Vector3(1,0,0)*playerSpeed,transform.position);
		} else {
			rb.angularVelocity = new Vector3(-1,0,0)*angleMoveSpeed; 
			rb.AddForceAtPosition(new Vector3(0,0,1)*playerSpeed,transform.position);
		}
	}

    public bool getWayToX()
    {
        return wayToX;
    }
	
	void OnTriggerEnter (Collider other) {
		if(other.gameObject.tag == "point"){
			Debug.Log("point pick");
			other.gameObject.transform.parent.gameObject.GetComponent<PointPickUp>().pickUp();
			upScore();
        }
	}

	void OnCollisionEnter(Collision other) 
	{ 
		if(other.gameObject.tag != gameObject.tag && other.gameObject.tag != "block" ){
            Debug.Log(other.gameObject.tag);
			gameOver();            
		}
	}
	
	void gameOver(){
        FindObjectOfType<AudioManager>().GameOver();
        particle.SetActive(false);
        //particle.GetComponent<Animator>().enabled = true;
        //particle.GetComponent<Animator>().Play(0);
		setGameState (false);
		destroyPlayer();
		scoreMenu.GetComponent<ScoreScreenController>().Skip();
		deadMenu.GetComponent<DeadScreenController>().View();
	}
	
	void destroyPlayer(){
		gameObject.SetActive(false);			
        Vector3 dir;
        if (wayToX == true)
        {
            dir = new Vector3(1, 0, 0);
        }
        else
        {
            dir = new Vector3(0, 1, 0);
        }
        Color c = colorController.GetComponent<ColorController>().getColorValue(colorNum-1);
        //deadPlayer.GetComponent<DeadPlayerEffect>().dead(transform.position, dir, c);
        GameObject dp = Instantiate(deadPlayer);
        dp.transform.position = transform.position;
        dp.SetActive(true);
	}
	
	public void addPoint(Vector3 p){
		points.AddLast(p);
	}
	
	public void setGameState(bool b){
		settings.GetComponent<Settings>().gameState = b;
	}
	
	public int getScore(){
		return score;
	}
}

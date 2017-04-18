using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    [Header("Effects")]
    public GameObject particle;
    public GameObject shield;
    public GameObject time;
    public GameObject color;
    [Space(5)]
	public GameObject settings;
	public GameObject generator;
	public GameObject colorController;
	public GameObject testLevelsMenu, mainMenu, scoreMenu, deadMenu;
	public GameObject helper;
    public GameObject deadPlayer;
	public float timeSwitch = 0.5f;
	public float moveSpeed = 2000;
	public float maxSpeed = 650;
	public float deltaUpSpeed = 5;
	public int frequencySpeedUp = 3;
	float playerSpeed;
	public float angleMoveSpeed = 700;
	LinkedList<Vector3> points = new LinkedList<Vector3>();
	int score = 0;	
	int colorNum = 0;
	Rigidbody rb;
	bool wayToX = true;
	static bool firstLaunch = true;
	
	float startPos = 30;
	public GameObject nextFloor;
	public float nextFloorPos = 70.7f;
	
	void Start () {	
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
		scoreMenu.GetComponent<ScoreScreenController>().updateScore(score);
		colorNum = 1;
		setColorPlayer();		
		//playerMove();
	}	
		
	void Update () {
		if(settings.GetComponent<Settings>().gameState == true){

			if(points.Count > 0){
				if(points.First.Value.x <= transform.position.x && points.First.Value.z <= transform.position.z)
				{
					generator.GetComponent<LevelGenerator>().generateLine();
					nextAngle();
					//transform.position = new Vector3(points.First.Value.x,transform.position.y,points.First.Value.z);
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
					//transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);					
				} 
			}
            
            if (godMode == true)
            {
                if (wayToX == true)
                {
                    rb.AddForceAtPosition(new Vector3(1, 0, 0) * 10, transform.position);
                }
                else
                {                    
                    rb.AddForceAtPosition(new Vector3(0, 0, 1) * 10, transform.position);
                }
            }
            
			
			if(settings.GetComponent<Settings>().onPhone == false){
				if(Input.GetButtonDown("Jump")) {
					setColorPlayer();
				}
			} else {
				if(Input.GetTouch(0).phase == TouchPhase.Began) {
					setColorPlayer();
				}
			}
		}
	}
	
	void setColorPlayer(){		
		if(colorNum > 3){
			scoreMenu.GetComponent<ScoreScreenController>().setColors(1);
		} else {
			scoreMenu.GetComponent<ScoreScreenController>().setColors(colorNum);
		}
		
		particle.transform.Find("sphere_eff_01").gameObject.GetComponent<ParticleSystem>().startColor = colorController.GetComponent<ColorController>().getColorValue(colorNum);
		particle.transform.Find("sphere_eff_02").gameObject.GetComponent<ParticleSystem>().startColor = colorController.GetComponent<ColorController>().getColorValue(colorNum);
		colorNum = colorController.GetComponent<ColorController>().getPlayerColor(gameObject, colorNum);
		colorController.GetComponent<ColorController>().getPlayerColor(helper, colorNum);
	}
	
	int angleCount = 0;
	
	void nextAngle(){
        if(godMode == true){
            gameObject.GetComponent<GodMode>().AddAngle();
        }
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
		scoreMenu.GetComponent<ScoreScreenController>().updateScore(score);
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
        else if (other.gameObject.tag == "Bonus")
        {
            BonusDestroy.BonusType t = other.gameObject.GetComponent<BonusDestroy>().Type;
            switch(t){
                case BonusDestroy.BonusType.Color:
                    GodModeOn();
                    break;
                case BonusDestroy.BonusType.Shield:
                    ShieldOn();
                    break;
                case BonusDestroy.BonusType.Time:
                    TimeBonusOn();
                    break;
            }
        }
	}


    void ShieldOn()
    {
        particle.SetActive(false);
        shield.SetActive(true);
        gameObject.GetComponent<Shield>().ActivateShield();
    }

    public void ShieldOff()
    {
        particle.SetActive(true);
        shield.SetActive(false);
    }

    bool godMode = false;

    void GodModeOn()
    {
        particle.SetActive(false);
        color.SetActive(true);
        godMode = true;
    }

    public void GodModeOff()
    {
        color.SetActive(false);
        particle.SetActive(true);
        godMode = false;
    }

    void TimeBonusOn()
    {
        particle.SetActive(false);
        time.SetActive(true);
        gameObject.GetComponent<TimeSlow>().startBonus();
        Debug.Log("Time");
    }

    public void TimeBonusOff()
    {
        time.SetActive(false);
        particle.SetActive(true);
    }
	
	void OnCollisionEnter(Collision other) 
	{ 
		if(other.gameObject.tag != gameObject.tag && other.gameObject.tag != "block" ){
			Debug.Log(other.gameObject.tag);
	        if(gameObject.GetComponent<Shield>().State == true){
                gameObject.GetComponent<Shield>().Hit();
            } else if(godMode == false ){
			    gameOver();
            }
		}
	}
	
	void gameOver(){
        particle.SetActive(false);
        shield.SetActive(false);
        color.SetActive(false);
        time.SetActive(false);
		setGameState (false);
		destroyPlayer();
		scoreMenu.GetComponent<ScoreScreenController>().Skip();
		deadMenu.GetComponent<DeadScreenController>().View();
	}
	
	void destroyPlayer(){
		gameObject.SetActive(false);	
		particle.SetActive(false);
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
        deadPlayer.GetComponent<DeadPlayerEffect>().dead(transform.position, dir, c);
	}
	
	public void addPoint(Vector3 p){
		points.AddLast(p);
		//Debug.Log(p.x.ToString()+" " +p.z.ToString()+"add point");
	}
	
	public void setGameState(bool b){
		settings.GetComponent<Settings>().gameState = b;
	}
	
	public int getScore(){
		return score;
	}
}

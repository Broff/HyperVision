using UnityEngine;

public delegate void MyDel();

public class Settings : MonoBehaviour {

	public bool onPhone = false;
	public bool testBuild = false;
	public bool gameState = true;
	public bool wayToX = true;
    private int score = 0;
    private event MyDel setCoinEvent;
    private event MyDel setScoreEvent;


	void Start(){
        Time.timeScale = 1;
        Application.targetFrameRate = 60;
	}	
	
	public void setWay(){
		wayToX = !wayToX;
	}

    public void AddSetCoinEvent(MyDel d)
    {
        setCoinEvent += d;
    }

    public int Coins
    {
        get { return PlayerPrefs.GetInt("coins"); }
        set
        {
            if (value < 0) value = 0;
            PlayerPrefs.SetInt("coins", value);
            setCoinEvent();
        }
    }

    public void AddSetScoreEvent(MyDel d)
    {
        setScoreEvent += d;
    }

    public int Score
    {
        get { return score; }
        set
        {
            if (value < 0) value = 0;
            score = value;
            setScoreEvent();
        }
    }
}

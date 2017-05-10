using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip buttonClick;
    public AudioClip gameOver;
    public AudioClip buySkin;

    AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void ButtonClick()
    {
        audio.PlayOneShot(buttonClick);
    }
    public void GameOver()
    {
        audio.PlayOneShot(gameOver);
    }
    public void BuySkin()
    {
        audio.PlayOneShot(buySkin);
    }
}

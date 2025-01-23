using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Manager : MonoBehaviour {

    public Player player;

    WaveSystem waveSystem;

    public float Score = 0;
    public Text ScoreText;
    public bool GameStarted;
    public RectTransform StartButton;


    public void Start()
    {
        waveSystem = GetComponent<WaveSystem>();
    }

    public void StartGame()
    {
        waveSystem.Begin();
        GameStarted = true;
        StartButton.gameObject.SetActive(false);
        ScoreText.gameObject.SetActive(true);
        ScoreText.text = "0";
    }

    void Update()
    {
    }

    //public void Lose()    {    }
    //public void Pause()    {        Time.timeScale = 0;   }
    //public void UnPause()    {        Time.timeScale = 1;    }
    
}
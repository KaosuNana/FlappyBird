/*
 Create By Ray : ray@raymix.net @ 极视教育
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public GameObject uiReady;

    public GameObject uiInGame;

    public GameObject uiGameOver;

    public Player player;


    public int score;

    public Text uiScore;
    public Text uiScore2;

    public int Score
    {
        get { return score; }
        set
        {
            this.score = value;
            this.uiScore.text = this.score.ToString();
            this.uiScore2.text = this.score.ToString();
        }
    }

    public PipelineManager pipelineManager;
    enum GAME_STATUS
    {
        READY,
        INGAME,
        OVER
    }

    GAME_STATUS status;

    private GAME_STATUS Status
    {
        get { return status; }
        set {
            this.status = value;
            this.UpdateUI();
        }
    }

    // Use this for initialization
    void Start () {
        uiReady.SetActive(true);
        this.Status = GAME_STATUS.READY;
        this.player.OnDeath += Player_OnDeath;
        this.player.OnScore = OnPlayerScore;

    }

    void OnPlayerScore(int score)
    {
        this.Score += score;
    }

    private void Player_OnDeath()
    {
        this.Status = GAME_STATUS.OVER;
        this.pipelineManager.Stop();
    }

    void UpdateUI()
    {
        uiReady.SetActive(this.Status == GAME_STATUS.READY);
        uiInGame.SetActive(this.Status == GAME_STATUS.INGAME);
        uiGameOver.SetActive(this.Status == GAME_STATUS.OVER);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void StartGame()
    {
        this.Status = GAME_STATUS.INGAME;
        Debug.LogFormat("StartGame:{0}", this.status);
        this.score = 0;
        pipelineManager.StartRun();
        player.Fly();
    }

    public void Restart()
    {
        this.Status = GAME_STATUS.READY;
        this.pipelineManager.Init();
        this.player.Init();
    }
}

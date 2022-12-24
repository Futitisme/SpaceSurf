using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;


    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    GameManagerState GMState;

    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    void UpdateGameManagerState()
    {
        switch(GMState)
        {
            case GameManagerState.Opening:

                GameOverGO.SetActive(false);
                playButton.SetActive(true);
                break;
            
            case GameManagerState.Gameplay:
                scoreUITextGO.GetComponent<GameScore>().Score = 0;
                playButton.SetActive(false);
                playerShip.GetComponent<PlayerControl>().Init();

                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

                break;
            
            case  GameManagerState.GameOver:
                GameOverGO.SetActive(true);
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                Invoke("ChangeToOpeningState", 5f);
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

}


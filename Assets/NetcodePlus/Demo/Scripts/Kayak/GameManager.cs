using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private int playerScore;
    [SerializeField]private GameObject[] spawnPoints;

    public int PlayerScore { get { return playerScore; } }


    public void UpdatePlayerScore(int score)
    {
        playerScore += score;
    }

    public Vector3 GetSpawnPosition(int id)
    {
        if (spawnPoints.Length >= id - 1)
        {
            return spawnPoints[id].transform.position;
        }
        else
        {
            return spawnPoints[0].transform.position;
        }
    }
}

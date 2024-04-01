using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRing : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10;

    private GameManager gm;

    private void Awake()
    {
        gm = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("Player passed through ring");
            gm.UpdatePlayerScore(scoreValue);
            Destroy(this.gameObject);
        }
    }
}

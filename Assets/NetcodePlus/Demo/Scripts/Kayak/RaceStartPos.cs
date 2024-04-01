using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetcodePlus.Demo
{
    public class RaceStartPos : MonoBehaviour
    {
    public float currentLapTime;
    public float lastLapTime;

    private UIManager ui;
    private bool lapStarted = false;

    void Awake()
    {
        ui = UIManager.Instance;
    }

    void Update()
    {
        if (lapStarted) currentLapTime += Time.deltaTime;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!lapStarted)
            {
                lapStarted = true;
            }

            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        lastLapTime = currentLapTime;
        currentLapTime = 0;
    }
}
}

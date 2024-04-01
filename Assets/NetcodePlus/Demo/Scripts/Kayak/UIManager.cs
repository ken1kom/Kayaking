using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace NetcodePlus.Demo
{
    public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text currentTime;
    [SerializeField] private Text lastTime;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private RaceStartPos raceStart;

    private GameManager gm;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        gm = GameManager.Instance;
        print("Player found by UI");
    }

    // Update is called once per frame
    void Update()
    {
        /*currentTime.text = raceStart.currentLapTime.ToString("F2");
        lastTime.text = raceStart.lastLapTime.ToString("F2");*/
    }
}
}

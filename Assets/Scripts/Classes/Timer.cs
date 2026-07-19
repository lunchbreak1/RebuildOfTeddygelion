using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float totalTime;

    public float countdown;

    TrickManager trickManager;

    TextMeshProUGUI textMeshProUGUI;

    private Coroutine timerCoroutine;

    public GameObject saveMenu;

    public WheelchairController player;
    ArcadeKart kart;
    PlayerGrind playerGrind;

    public OptionsLayout saveMenuOptions;

    // Start is called before the first frame update
    void Start()
    {
        countdown = totalTime;
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        trickManager = FindObjectOfType<TrickManager>();
        player = FindAnyObjectByType<WheelchairController>();
        kart = player.GetComponent<ArcadeKart>();
        playerGrind = player.GetComponent<PlayerGrind>();

        StartTimerCoroutine();
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown < 0)
        {
            StopTimerCoroutine();
        }
    }

    IEnumerator RailScoreCoroutine()
    {
        while (countdown > 0)
        {
            countdown-= .01f;

            int minutes = Mathf.FloorToInt(countdown / 60f);
            int seconds = Mathf.FloorToInt(countdown % 60f);
            int hundredths = Mathf.FloorToInt((countdown * 100f) % 100f);

            textMeshProUGUI.text = $"{minutes:00}:{seconds:00}.{hundredths:00}";

            yield return new WaitForSeconds(.01f);
        }

        
    }

    void StartTimerCoroutine()
    {
        if (timerCoroutine == null)
        {
            timerCoroutine = StartCoroutine(RailScoreCoroutine());
        }
    }

    void StopTimerCoroutine()
    {
        if (timerCoroutine != null)
        {
            //StopAllCoroutines();
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }


        textMeshProUGUI.text = "Time's up! Here's your score: " + trickManager.score;
        saveMenu.gameObject.SetActive(true);
        saveMenuOptions.ChangeIndex(0);
        FreezePlayer();
    }

    void FreezePlayer()
    {
        player.Animate(0, 0, 0, 0);
        player.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        player.OnRail = false;
        player.enabled = false;

        kart.enabled = false;
        
        playerGrind.StopRailCoroutine();
        playerGrind.enabled = false;

        trickManager.HideTrickCounter();
        trickManager.enabled = false;
    }

    void UnfreezePlayer()
    {
        //player.Animate(0, 0, 0, 0);
        player.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //player.OnRail = false;
        player.enabled = true;

        kart.enabled = true;

        //playerGrind.StopRailCoroutine();
        playerGrind.enabled = true;

        //trickManager.HideTrickCounter();
        trickManager.enabled = true;
    }
}

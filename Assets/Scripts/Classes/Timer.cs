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

    public GameObject pauseMenu;

    public bool paused = false;

    public OptionsLayout pauseOptions;

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
        if (countdown > 0)
        {
            if(Input.GetAxis("TriggerRight") == 1 || Input.GetAxis("Pause") == 1!)
            {
                if(!paused)
                {
                    Pause();
                }
            }
        }

        if(countdown <= 0)
        {
            StopTimerCoroutine();
        }
    }

    public void Pause()
    {
        paused = true;
        pauseMenu.SetActive(true);
        pauseOptions.ChangeIndex(0);
        //StopTimerCoroutine();

        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }

        player.Animate(0, 0, 0, 0);
        player.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        player.OnRail = false;
        player.enabled = false;

        kart.enabled = false;

        playerGrind.StopRailCoroutine();
        playerGrind.enabled = false;

        //trickManager.HideTrickCounter();
        //trickManager.enabled = false;
    }

    public void Unpause()
    {
        paused = false;
        pauseMenu.SetActive(false);
        StartTimerCoroutine();
        UnfreezePlayer();
    }

    IEnumerator TimerCoroutine()
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
            timerCoroutine = StartCoroutine(TimerCoroutine());
        }
    }

    void StopTimerCoroutine()
    {
        if (timerCoroutine != null)
        {
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
        player.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        player.enabled = true;

        kart.enabled = true;

        playerGrind.enabled = true;

        trickManager.enabled = true;
    }
}

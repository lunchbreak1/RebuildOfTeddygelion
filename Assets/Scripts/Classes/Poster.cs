using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Poster : MonoBehaviour
{
    [Tooltip("Whether the player is nearby or not.")]
    public bool playerNearby = false;

    [Tooltip("The poster to put up.")]
    public GameObject poster;

    [Tooltip("The marker indicating to place a poster.")]
    public GameObject placeholder;

    [Tooltip("The points earned by placing a poster.")]
    public float points;

    [Tooltip("The message to show when placing a poster.")]
    public string posterMessage;

    [Tooltip("The duration of the message shown by placing a poster.")]
    public float posterMessageDuration;

    [Tooltip("If the poster is set up or not.")]
    bool posterUp = false;

    [Tooltip("The object that handles the player's score")]
    TrickManager trickManager;

    [Tooltip("The object that handles the player's score")]
    PosterManager posterManager;

    private void Start()
    {
        poster.SetActive(false);
        trickManager = FindObjectOfType<TrickManager>();
        posterManager = FindObjectOfType<PosterManager>();
    }

    /// <summary>
    /// Put up a poster on a wall.
    /// </summary>
    public void PutUpPoster()
    {
        if(poster.activeSelf == false)
        { 
            poster.SetActive(true);
            placeholder.SetActive(false);

            WheelchairController wheelchairController = FindAnyObjectByType<WheelchairController>();

            if (wheelchairController != null && !posterUp)
            {
                wheelchairController.posters++;
                trickManager.AddTricksToScore(points);
                trickManager.WriteToTrickCounter(posterMessage, posterMessageDuration);
                posterUp = true;
                posterManager.SetPosterMessage();
            }
        }
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update()
    {
        if (playerNearby)
        {
            if(Input.GetAxis("Interact") > 0)
            {
                PutUpPoster();
            }
        }
    }

    /// <summary>
    /// Called when an object enters the trigger zone.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.GetComponent<WheelchairController>() != null)
        {
            playerNearby = true;
        }
    }

    /// <summary>
    /// Called when an object leaves the trigger zone.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (playerNearby)
        {
            playerNearby = false;
        }
    }
}

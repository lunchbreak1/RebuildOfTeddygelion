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

    private void Start()
    {
        poster.SetActive(false);
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

            if (wheelchairController != null)
            {
                wheelchairController.posters++;
            }
            else
            {
                print("No wheelchair controller!");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This object can be picked up by the player.
/// </summary>
public class Collectabile : MonoBehaviour
{
    [Tooltip("The points earned when the item is picked up")]
    [SerializeField]
    private int pointValue;

    [Tooltip("The message shown when the item is picked up")]
    [SerializeField]
    private string message;

    [SerializeField]
    [Tooltip("How long the message is shown when the item is picked up")]
    private int messageDuration;

    [SerializeField]
    [Tooltip("Does this object count as a collectable to the trick manager?")]
    private bool isCollectable;

    [Tooltip("The object that handles the player's score")]
    TrickManager trickManager;


    // Start is called before the first frame update
    void Start()
    {
        trickManager = FindObjectOfType<TrickManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered!");

        if (other.transform.parent.GetComponent<WheelchairController>() != null)
        {
            trickManager.AddTricksToScore(pointValue);
            trickManager.WriteToTrickCounter(message, messageDuration);
            Destroy(gameObject);

            if (isCollectable)
            {
                trickManager.collectables++;
            }
        }
    }
}

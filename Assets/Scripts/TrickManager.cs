using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class TrickManager : MonoBehaviour
{
    bool grounded = true;
    float score = 0;
    int threeSixties = 0;
    int flips = 0;
    int backFlips = 0;
    public float point360;
    public float pointFlip;
    public float pointBackFlip;
    public float turnSpeed;

    float totalPoints;

    public TextMeshProUGUI scoreCounter;
    public TextMeshProUGUI trickCounter;

    [Tooltip("The amount the player has spun around in the air in degrees.")]
    public float airXRotation = 0;

    [Tooltip("The amount the player has flipped in the air in degrees.")]
    public float airYRotation = 0;

    Vector2 moveDirection = Vector2.zero;

    // Start is called before the first frame update
    private void Start()
    {
        HideTrickCounter();
    }

    public void SetTurnSpeed(float speed)
    {
        turnSpeed = speed;
    }

    public void StartTrick()
    {
        grounded = false;
    }

    public void EndTrick()
    {
        grounded = true;
        AddTricksToScore();
        airXRotation = 0;
        airYRotation = 0;
    }

    private void Update()
    {
        if(!grounded)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");  // A/D or Left/Right arrow keys
            float vertical = Input.GetAxisRaw("Vertical");      // W/S or Up/Down arrow keys

            // Combine into a Vector2
            moveDirection = new Vector2(horizontal, vertical);

            // Normalize the direction so it's always of unit length (magnitude of 1)
            moveDirection.Normalize();
        }
    }

    private void FixedUpdate()
    {
        if(!grounded)
        {
            airXRotation += moveDirection.x * turnSpeed;

            airYRotation += moveDirection.y * turnSpeed;

            PerformTrick();
        }
    }

    void ShowTrickCounter()
    {
        trickCounter.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void HideTrickCounter()
    {
        trickCounter.text = "";
    }

    public void PerformTrick()
    {
        string message = "";

        if (Mathf.Abs(airXRotation) >= 360)
        {
            threeSixties++;
            airXRotation = 0;
        }

        if (airYRotation >= 360)
        {
            flips++;
            airYRotation = 0;
        }

        if (airYRotation <= -360)
        {
            backFlips++;
            airYRotation = 0;
        }

        float threeSixtyTotalPoints = threeSixties * point360;

        float flipTotalPoints = flips * pointFlip;

        float backflipTotalPoints = backFlips * pointBackFlip;

        if (threeSixties > 0)
        {
            message += "Outrageous 360 x" + threeSixties + " = " + threeSixtyTotalPoints + "\n";
        }

        if (flips > 0)
        {
            message += "Awesome flip x" + flips + " = " + flipTotalPoints + "\n";
        }

        if (backFlips > 0)
        {
            message += "Great back flip x" + backFlips + " = " + backflipTotalPoints + "\n";
        }

        totalPoints = threeSixtyTotalPoints + flipTotalPoints + backflipTotalPoints;

        if (totalPoints > 0)
        {
            message += "Total = " + totalPoints;
        }

        trickCounter.text = "<wave><rainb>" + message + "</rainb></wave>";
    }

    public void AddTricksToScore()
    {
        score += totalPoints;
        scoreCounter.text = "" + score;
        Invoke("HideTrickCounter", 4);
    }

    public void Wipeout()
    {
        trickCounter.text = "Wipeout!";
        Invoke("HideTrickCounter", 4);
    }
}

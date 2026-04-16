using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class TrickManager : MonoBehaviour
{
    [Tooltip("Whether the wheelchair is grounded or not.")]
    bool grounded = true;

    [Tooltip("Current player score.")]
    float score = 0;

    [Tooltip("How many 360s the player did before touching the ground.")]
    int threeSixties = 0;

    [Tooltip("How many flips the player did before touching the ground.")]
    int flips = 0;

    [Tooltip("How many backflips the player did before touching the ground.")]
    int backFlips = 0;

    [Tooltip("How many degrees are needed to do a flip.")]
    public float degreesForFlip = 360 * 2;

    [Tooltip("How many degrees are needed to do a 360.")]
    public float degreesFor360 = 360;

    [Tooltip("How many points are awarded for a 360.")]
    public float point360;

    [Tooltip("How many points are awarded for a flip.")]
    public float pointFlip;

    [Tooltip("How many points are awarded for a back flip.")]
    public float pointBackFlip;

    [Tooltip("How fast the player turns horizontally in the air.")]
    float turnSpeedX;

    [Tooltip("How fast the player turns vertically in the air.")]
    float turnSpeedY;


    public float airTurnSpeedX;
    public float airTurnSpeedY;

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
        SetTurnSpeed(airTurnSpeedX, airTurnSpeedY);
        Debug.Log("TRICK MANAGER START");
    }

    public void SetTurnSpeed(float speedX, float speedY)
    {
        turnSpeedX = speedX;
        turnSpeedY = speedY;
    }

    public void StartTrick()
    {
        grounded = false;
    }

    public void EndTrick()
    {
        threeSixties = 0;
        flips = 0;
        backFlips = 0;
        totalPoints = 0;
        grounded = true;
        AddTricksToScore();
        airXRotation = 0;
        airYRotation = 0;
        Debug.Log("END OF TRICK");
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
            airXRotation += moveDirection.x * turnSpeedX;

            airYRotation += moveDirection.y * turnSpeedY;

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
        bool newTrick = false;

        if (Mathf.Abs(airXRotation) >= degreesFor360)
        {
            threeSixties++;
            airXRotation = 0;
            newTrick = true;
        }

        if (airYRotation >= degreesForFlip)
        {
            flips++;
            airYRotation = 0;
            newTrick = true;
        }

        if (airYRotation <= -degreesForFlip)
        {
            backFlips++;
            airYRotation = 0;
            newTrick = true;
        }

        if(newTrick)
        {
            WriteScore();
        }
    }

    public void AddTricksToScore()
    {
        score += totalPoints;
        scoreCounter.text = "" + score;
        Invoke("HideTrickCounter", 4);
    }

    public void Wipeout()
    {
        if(!grounded)
        {
            trickCounter.text = "<color=red><shake>Wipeout!</color></shake>";
            grounded = true;
            CancelInvoke();
            Invoke("HideTrickCounter", 4);
        }
    }

    public void WriteScore()
    {
        string message = "";

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
}

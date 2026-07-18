using System.Collections;
using TMPro;
using UnityEngine;

public class TrickManager : MonoBehaviour
{
    [Tooltip("Whether the wheelchair is grounded or not.")]
    public bool grounded = true;

    [Tooltip("Current player score.")]
    public float score = 0;

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

    [Tooltip("This value dampens the points earned by consecutive tricks to prevent spamming.")]
    public float consecutiveComboDampenFactor = 0.75f;

    [Tooltip("How fast the player turns horizontally in the air.")]
    float turnSpeedX;

    [Tooltip("How fast the player turns vertically in the air.")]
    float turnSpeedY;

    [Tooltip("How long to show a message for")]
    public int messageDuration = 4;

    private int consecutive360Combos = 0;
    private int consecutiveFlipCombos = 0;
    private int consecutiveBackFlipCombos = 0;
    private int consecutiveRailGrinds = 0;

    public int overall360Combos = 0;
    public int overallFlipCombos = 0;
    public int overallBackFlipCombos = 0;
    public int overallRailGrinds = 0;
    public int overallWipeouts = 0;
    public int overallRailFalls = 0;
    public int collectables = 0;

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

    public bool onRail = false;

    float railPoints = 0;

    

    // Start is called before the first frame update
    private void Start()
    {
        HideTrickCounter();
        SetTurnSpeed(airTurnSpeedX, airTurnSpeedY);
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
        grounded = true;
        AddTricksToScore(totalPoints);
        CancelInvokeAndHideTrickCounter(messageDuration);
        AddConsecutiveCombos();
        ClearTrickPointCounter();
    }

    public void AddConsecutiveCombos()
    {
        if(threeSixties > 0)
        {
            if (flips == 0 && backFlips == 0)
            {
                consecutive360Combos++;
            }

            overall360Combos += threeSixties;

            consecutiveFlipCombos = 0;

            consecutiveBackFlipCombos = 0;
        }

        if (flips > 0)
        {
            if(threeSixties == 0 && backFlips == 0)
            {
                consecutiveFlipCombos++;
            }

            overallFlipCombos += flips;

            consecutive360Combos = 0;

            consecutiveBackFlipCombos = 0;

        }

        if (backFlips > 0)
        {
            if(threeSixties == 0 && flips == 0)
            {
                consecutiveBackFlipCombos++;
            }

            overallBackFlipCombos += backFlips;

            consecutive360Combos = 0;

            consecutiveFlipCombos = 0;
        }

        if(threeSixties > 0 || flips > 0 ||  backFlips > 0)
        {
            consecutiveRailGrinds = 0;
        }
    }

    public void ClearTrickPointCounter()
    {
        threeSixties = 0;
        flips = 0;
        backFlips = 0;
        totalPoints = 0;
        airXRotation = 0;
        airYRotation = 0;
    }

    private void Update()
    {
        if(!grounded && !onRail)
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
        if(!grounded && !onRail)
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
    public void HideTrickCounter()
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

    public void AddTricksToScore(float pointsToAdd)
    {
        score += pointsToAdd;
        scoreCounter.text = "" + score;
    }

    public void Wipeout()
    {
        if(!grounded)
        {
            ClearTrickPointCounter();
            WriteToTrickCounter("<color=red><shake>Wipeout!</color></shake>", messageDuration);
            grounded = true;
            overallWipeouts++;
        }
    }

    public void WriteScore()
    {
        string message = "";
        
        float threeSixtyTotalPoints = Mathf.Round(threeSixties * point360 * (Mathf.Pow(consecutiveComboDampenFactor, consecutive360Combos)));

        float flipTotalPoints = Mathf.Round(flips * pointFlip * (Mathf.Pow(consecutiveComboDampenFactor, consecutiveFlipCombos)));

        float backflipTotalPoints = Mathf.Round(backFlips * pointBackFlip * (Mathf.Pow(consecutiveComboDampenFactor, consecutiveBackFlipCombos)));

        if (threeSixties > 0)
        {
            message += "Outrageous 360! x" + threeSixties + " = " + threeSixtyTotalPoints + "\n";
        }

        if (flips > 0)
        {
            message += "Awesome flip! x" + flips + " = " + flipTotalPoints + "\n";
        }

        if (backFlips > 0)
        {
            message += "Great back flip! x" + backFlips + " = " + backflipTotalPoints + "\n";
        }

        totalPoints = threeSixtyTotalPoints + flipTotalPoints + backflipTotalPoints;

        if (totalPoints > 0)
        {
            message += "Total = " + totalPoints;
        }

        WriteToTrickCounter("<wave><rainb>" + message + "</rainb></wave>");
    }

    public void WriteToTrickCounter(string message, float duration = 0)
    {
        trickCounter.text = message;

        if(duration > 0)
        {
            CancelInvokeAndHideTrickCounter(duration);
        }
    }

    public void CancelInvokeAndHideTrickCounter(float duration = 0)
    {
        CancelInvoke("HideTrickCounter");
        Invoke("HideTrickCounter", duration);
    }

    public void ScoreRailPoints(float pointsToAdd)
    {
        railPoints += Mathf.Round(pointsToAdd * Mathf.Pow(consecutiveComboDampenFactor, consecutiveRailGrinds));
        string message = "Nice rail grind! +" + railPoints;
        WriteToTrickCounter("<wave><rainb>" + message + "</rainb></wave>");
    }

    public void AddRailPointsToScore()
    {
        score += railPoints;
        string message = "Nice rail grind! +" + railPoints;
        WriteToTrickCounter("<wave><rainb>" + message + "</rainb></wave>", messageDuration);
        railPoints = 0;    
        consecutiveRailGrinds++;
        consecutive360Combos = 0;
        consecutiveFlipCombos = 0;
        consecutiveBackFlipCombos = 0;
        overallRailGrinds++;
    }

    public void ThrowOffRail()
    {
        ClearTrickPointCounter();
        WriteToTrickCounter("<color=red><shake>Thrown off the rail!</color></shake>", messageDuration);
        railPoints = 0;
        overallRailFalls++;
    }
}

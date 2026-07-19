using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class PlayerGrind : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] bool jump;         //Inputs aren't used in the tutorial
    [SerializeField] Vector3 input;     //But they're here for rail switching

    [Header("Variables")]
    public bool onRail;
    [SerializeField] float grindSpeed;
    [SerializeField] float heightOffset;
    float timeForFullSpline;
    float elapsedTime;
    [SerializeField] float lerpSpeed = 10f;

    [Header("Scripts")]
    [SerializeField] Rail currentRailScript;
    Rigidbody playerRigidbody;
    WheelchairController charController;
    Rigidbody body;

    private Vector3 exitVelocity;

    float horizontalInput = 0f;

    public float maxRailRotation = 80f;

    [SerializeField] float rotateSpeed;

    [Tooltip("The object that manages tricks and points")]
    TrickManager trickManager;

    [Tooltip("The current balance on top of the rail. This value will shift over time.")]
    public float railBalance = 0f;

    public float slipRange = 0.5f;

    public float slipRate = 0.1f;

    public float slipMaximum = 1.5f;

    public float slipZone = 0.9f;

    public float railPoints = 1f;

    private Coroutine railScoreCoroutine;


    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        charController = GetComponent<WheelchairController>();
        body = GetComponent<Rigidbody>();
        trickManager = FindAnyObjectByType<TrickManager>();
        ThrowOffRail();
    }

    private void FixedUpdate()
    {
        if (onRail) //If on the rail, move the player along the rail
        {
            MovePlayerAlongRail();
        }
    }
    private void Update()
    {
        if (onRail) { horizontalInput = Input.GetAxisRaw("Horizontal"); }
    }
    void MovePlayerAlongRail()
    {
        if (currentRailScript != null && onRail) //This is just some additional error checking.
        {
            //Calculate a 0 to 1 normalised time value which is the progress along the rail.
            //Elapsed time divided by the full time needed to traverse the spline will give you that value.
            float progress = elapsedTime / timeForFullSpline;

            //If progress is less than 0, the player's position is before the start of the rail.
            //If greater than 1, their position is after the end of the rail.
            //In either case, the player has finished their grind.
            if (progress < 0 || progress > 1)
            {
                trickManager.AddRailPointsToScore();
                ThrowOffRail();
                Vector3 eulers = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(eulers.x, eulers.y, 0);
                return;
            }
            //The rest of this code will not execute if the player is thrown off.

            //Next Time Normalised is the player's progress value for the next update.
            //This is used for calculating the player's rotation.
            //Depending on the direction of the player on the spline, it will either add or subtract time from the
            //current elapsed time.
            float nextTimeNormalised;
            if (currentRailScript.normalDir)
                nextTimeNormalised = (elapsedTime + Time.deltaTime) / timeForFullSpline;
            else
                nextTimeNormalised = (elapsedTime - Time.deltaTime) / timeForFullSpline;

            //Calculating the local positions of the player's current position and next position
            //using current progress and the progress for the next update.
            float3 pos, tangent, up;
            float3 nextPosfloat, nextTan, nextUp;
            SplineUtility.Evaluate(currentRailScript.railSpline.Spline, progress, out pos, out tangent, out up);
            SplineUtility.Evaluate(currentRailScript.railSpline.Spline, nextTimeNormalised, out nextPosfloat, out nextTan, out nextUp);

            //Converting the local positions into world positions.
            Vector3 worldPos = currentRailScript.LocalToWorldConversion(pos);
            Vector3 nextPos = currentRailScript.LocalToWorldConversion(nextPosfloat);

            //Setting the player's position and adding a height offset so that they're sitting on top of the rail
            //instead of being in the middle of it.
            transform.position = worldPos + (transform.up * heightOffset);
            //Lerping the player's current rotation to the direction of where they are to where they're going.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(nextPos - worldPos), lerpSpeed * Time.deltaTime);
            //Lerping the player's up direction to match that of the rail, in relation to the player's current rotation.
            if(progress < 1)
            {
                Debug.Log("Progress: " + progress);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, up) * transform.rotation, lerpSpeed * Time.deltaTime);
            }

            //Finally incrementing or decrementing elapsed time for the next update based on direction.
            if (currentRailScript.normalDir)
                elapsedTime += Time.deltaTime;
            else
                elapsedTime -= Time.deltaTime;

            Vector3 grindVelocity = (nextPos - worldPos).normalized * grindSpeed;
            exitVelocity = (nextPos - worldPos).normalized * grindSpeed;

            if (progress > 0.01f && progress < slipZone)
            {
                ApplyRailSlip(worldPos);
            }
        }
    }

    /// <summary>
    /// Causes the player to slip while on the rail.
    /// </summary>
    /// <param name="worldPos"></param>
    void ApplyRailSlip(Vector3 worldPos)
    {
        if (railBalance == 0)
        {
            railBalance = UnityEngine.Random.Range(-slipRange, slipRange);
        }
        else if (railBalance > 0 && railBalance < slipMaximum)
        {
            railBalance += slipRate;
        }
        else if (railBalance < 0 && railBalance > -slipMaximum)
        {
            railBalance -= slipRate;
        }

        transform.RotateAround(worldPos, transform.forward, (railBalance - horizontalInput) * rotateSpeed);

        float roll = Mathf.DeltaAngle(0f, transform.eulerAngles.z);

        if (Mathf.Abs(roll) > maxRailRotation)
        {
            ThrowOffRail();
            transform.position = transform.position + (transform.up * 1);
            trickManager.ThrowOffRail();
            return;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Rail")
        {
            onRail = true;
            charController.OnRail = true;
            trickManager.onRail = true;
            currentRailScript = hit.gameObject.GetComponent<Rail>();
            CalculateAndSetRailPosition();
            //Message.Write("Sick rail grind!");
            StartRailCoroutine();
        }
    }

    private void OnCollisionEnter(Collision hit)
    {
        foreach (ContactPoint contact in hit.contacts)
        {
            if (contact.thisCollider.name == "KartBouncingCapsule")
            {
                return;
            }
        }

        currentRailScript = hit.gameObject.GetComponent<Rail>();

        if(currentRailScript != null)
        {
            onRail = true;
            charController.OnRail = true;
            trickManager.onRail = true;
            CalculateAndSetRailPosition();
            body.isKinematic = true;
            body.useGravity = false;
            StartRailCoroutine();
        }
    }


    void CalculateAndSetRailPosition()
    {
        //Figure out the amount of time it would take for the player to cover the rail.
        timeForFullSpline = currentRailScript.totalSplineLength / grindSpeed;

        //This is going to be the world position of where the player is going to start on the rail.
        Vector3 splinePoint;

        //The 0 to 1 value of the player's position on the spline. We also get the world position of where that
        //point is.
        float normalisedTime = Math.Abs(currentRailScript.CalculateTargetRailPoint(transform.position, out splinePoint));

        elapsedTime = timeForFullSpline * normalisedTime;
        //Multiply the full time for the spline by the normalised time to get elapsed time. This will be used in
        //the movement code.

        //Spline evaluate takes the 0 to 1 normalised time above, 
        //and uses it to give you a local position, a tangent (forward), and up
        float3 pos, forward, up;
        SplineUtility.Evaluate(currentRailScript.railSpline.Spline, normalisedTime, out pos, out forward, out up);

        Vector3 velocity = body.velocity; // or body.velocity depending on Unity version

        if (velocity.sqrMagnitude < 0.01f)
        {
            velocity = transform.forward;
        }

        //Calculate the direction the player is going down the rail
        currentRailScript.CalculateDirection(forward, transform.forward);

        //Set player's initial position on the rail before starting the movement code.
        transform.position = splinePoint + (transform.up * heightOffset);
    }
    void ThrowOffRail()
    {
        //Set onRail to false, clear the rail script, and push the player off the rail.
        //It's a little sudden, there might be a better way of doing using coroutines and looping, but this will work.
        onRail = false;
        currentRailScript = null;
        transform.position += transform.forward * 1;
        body.isKinematic = false;
        body.velocity = exitVelocity;
        body.useGravity = true;
        charController.OnRail = false;
        trickManager.onRail = false;
        railBalance = 0;
        StopRailCoroutine();
    }

    IEnumerator RailScoreCoroutine()
    {
        while (onRail)
        {
            trickManager.ScoreRailPoints(railPoints);

            yield return new WaitForSeconds(1f);
        }
    }

    public void StartRailCoroutine()
    {
        if (railScoreCoroutine == null)
        {
            railScoreCoroutine = StartCoroutine(RailScoreCoroutine());
        }
    }

    public void StopRailCoroutine()
    {
        if (railScoreCoroutine != null)
        {
            StopCoroutine(railScoreCoroutine);
            railScoreCoroutine = null;
        }
    }
}
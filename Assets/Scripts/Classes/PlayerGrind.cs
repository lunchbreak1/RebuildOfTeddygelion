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
    [SerializeField] bool jump;         
    [SerializeField] Vector3 input;     

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

    public bool paused = false;


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
        if (onRail) 
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
        if (currentRailScript != null && onRail) 
        {
            float progress = elapsedTime / timeForFullSpline;

            if (progress < 0 || progress > 1)
            {
                trickManager.AddRailPointsToScore();
                ThrowOffRail();
                Vector3 eulers = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(eulers.x, eulers.y, 0);
                return;
            }

            float nextTimeNormalised;
            if (currentRailScript.normalDir)
                nextTimeNormalised = (elapsedTime + Time.deltaTime) / timeForFullSpline;
            else
                nextTimeNormalised = (elapsedTime - Time.deltaTime) / timeForFullSpline;

            float3 pos, tangent, up;
            float3 nextPosfloat, nextTan, nextUp;
            SplineUtility.Evaluate(currentRailScript.railSpline.Spline, progress, out pos, out tangent, out up);
            SplineUtility.Evaluate(currentRailScript.railSpline.Spline, nextTimeNormalised, out nextPosfloat, out nextTan, out nextUp);

            Vector3 worldPos = currentRailScript.LocalToWorldConversion(pos);
            Vector3 nextPos = currentRailScript.LocalToWorldConversion(nextPosfloat);

            transform.position = worldPos + (transform.up * heightOffset);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(nextPos - worldPos), lerpSpeed * Time.deltaTime);

            if(progress < 1)
            {
                Debug.Log("Progress: " + progress);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, up) * transform.rotation, lerpSpeed * Time.deltaTime);
            }

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
        timeForFullSpline = currentRailScript.totalSplineLength / grindSpeed;

        Vector3 splinePoint;

        float normalisedTime = Math.Abs(currentRailScript.CalculateTargetRailPoint(transform.position, out splinePoint));

        elapsedTime = timeForFullSpline * normalisedTime;

        float3 pos, forward, up;
        SplineUtility.Evaluate(currentRailScript.railSpline.Spline, normalisedTime, out pos, out forward, out up);

        Vector3 velocity = body.velocity;

        if (velocity.sqrMagnitude < 0.01f)
        {
            velocity = transform.forward;
        }

        currentRailScript.CalculateDirection(forward, transform.forward);

        transform.position = splinePoint + (transform.up * heightOffset);
    }
    void ThrowOffRail()
    {
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
            if (!paused)
            {
                trickManager.ScoreRailPoints(railPoints);
            }           

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
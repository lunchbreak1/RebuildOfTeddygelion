using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

/// <summary>
/// This class controls the wheelchair.
/// </summary>
public class WheelchairController : MonoBehaviour
{
    [Tooltip("Whether the wheelchair is grounded or not.")]
    public bool grounded;

    [Tooltip("How fast the wheelchair turns horizontally in the air.")]
    public float turnSpeedHorizontal;

    [Tooltip("How fast the wheelchair turns vertically in the air.")]
    public float turnSpeedVertical;

    [Tooltip("Player input")]
    public Vector2 moveDirection;

    [Tooltip("The amount the player has spun around in the air in degrees.")]
    public float airXRotation = 0;

    [Tooltip("The amount the player has flipped in the air in degrees.")]
    public float airYRotation = 0;

    [Tooltip("The amount of power the player has in their jump.")]
    public float jumpPower = 0;

    [Tooltip("The amount of posters the player has put up.")]
    public int posters = 0;

    [Tooltip("The animation controller object.")]
    private Animator anim;

    [Tooltip("The object that manages tricks.")]
    private TrickManager trickManager;

    [Tooltip("Player Rigidbody")]
    private Rigidbody body;

    public bool OnRail = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        trickManager = GetComponent<TrickManager>();
    }

    /// <summary>
    /// This event is called when the player lands on the ground.
    /// </summary>
    public void OnLand()
    {
        grounded = true;
        trickManager.EndTrick();
    }

    /// <summary>
    /// This event is called when the player leaves the ground.
    /// </summary>
    public void OnJump()
    {
        grounded = false;
        trickManager.StartTrick();
    }

    public void Jump(float jumpPress)
    {
        if (grounded)
        {
            body.AddForce(Vector3.up * jumpPower * jumpPress);
        }
    }

    /// <summary>
    /// Runs once per frame.
    /// </summary>
    void Update()
    {
        // Get horizontal and vertical input
        float horizontal = Input.GetAxisRaw("Horizontal");  // A/D or Left/Right arrow keys
        float vertical = Input.GetAxisRaw("Vertical");
        float accelerate = Input.GetAxisRaw("Accelerate");
        float brake = Input.GetAxisRaw("Brake");// W/S or Up/Down arrow keys
        float jump = Input.GetAxisRaw("Jump");

        // Combine into a Vector2
        moveDirection = new Vector2(horizontal, vertical);

        // Normalize the direction so it's always of unit length (magnitude of 1)
        moveDirection.Normalize();

        if (OnRail)
        {
            Animate(0, 1, 1, 0);
        }
        else
        {
            Animate(horizontal, vertical, accelerate, brake);

            if (jump > 0)
            {
                Jump(jump);
            }
        }

    }

    /// <summary>
    /// Runs once per second.
    /// </summary>
    void FixedUpdate()
    {
        // If the player is not on the ground, they can freely rotate.
        if (!grounded && !OnRail)
        {
            transform.RotateAround(transform.position, Vector3.up, moveDirection.x * turnSpeedHorizontal);

            transform.RotateAround(transform.position, transform.right.normalized, moveDirection.y * turnSpeedVertical);
        }
    }

    /// <summary>
    /// Send information to the animation controller.
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    /// <param name="brake"></param>
    void Animate(float horizontal, float vertical, float accelerate, float brake)
    {
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Accelerate", vertical);
        anim.SetFloat("Brake", vertical);
    }
}

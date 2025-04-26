using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class controls the wheelchair.
/// </summary>
public class WheelchairController : MonoBehaviour
{
    [Tooltip("Whether the wheelchair is grounded or not.")]
    public bool grounded;

    [Tooltip("How fast the wheelchair turns in the air.")]
    public float turnSpeed;

    [Tooltip("Player input")]
    public Vector2 moveDirection;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// This event is called when the player lands on the ground.
    /// </summary>
    public void OnLand()
    {
        grounded = true;
    }

    /// <summary>
    /// This event is called when the player leaves the ground.
    /// </summary>
    public void OnJump()
    {
        grounded = false;
    }

    void Update()
    {
        // Get horizontal and vertical input
        float horizontal = Input.GetAxisRaw("Horizontal");  // A/D or Left/Right arrow keys
        float vertical = Input.GetAxisRaw("Vertical");      // W/S or Up/Down arrow keys

        // Combine into a Vector2
        moveDirection = new Vector2(horizontal, vertical);

        // Normalize the direction so it's always of unit length (magnitude of 1)
        moveDirection.Normalize();

        Animate(horizontal, vertical);
    }

    void FixedUpdate()
    {
        // If the player is not on the ground, they can freely rotate.
        if (!grounded)
        {
            transform.RotateAround(transform.position, Vector3.up, moveDirection.x * turnSpeed);

            transform.RotateAround(transform.position, transform.right, moveDirection.y * turnSpeed);
        }
    }

    void Animate(float horizontal, float vertical)
    {
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
    }


}

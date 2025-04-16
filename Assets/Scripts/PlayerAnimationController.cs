using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;

    [Header("Input Settings")]
    public string horizontalAxis = "Horizontal";  // Default Unity input for left stick X
    public string verticalAxis = "Vertical";      // Default Unity input for left stick Y

    [Header("Animator Parameters")]
    public string moveXParam = "MoveX";
    public string moveYParam = "MoveY";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxis(horizontalAxis);
        float moveY = Input.GetAxis(verticalAxis);

        // Send the input to the Animator
        animator.SetFloat(moveXParam, moveX);
        animator.SetFloat(moveYParam, moveY);
    }
}

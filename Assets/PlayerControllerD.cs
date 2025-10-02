using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    public CharacterSetting settings;
    public Transform cameraTarget; // Assign this to a camera follow target

    private Vector2 moveInput;
    private float mouseX;
    private float verticalVelocity;
    private bool isJumping;

    private void Update()
    {
        HandleMovement();
        ApplyGravity();
        RotatePlayer();
    }

    private void HandleMovement()
    {
        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;
        move *= settings.moveSpeed * Time.deltaTime;

        move.y = verticalVelocity * Time.deltaTime;

        transform.position += move;
    }

    private void ApplyGravity()
    {
        if (IsGrounded())
        {
            if (isJumping)
            {
                verticalVelocity = settings.jumpForce;
                isJumping = false;
            }
            else
            {
                verticalVelocity = -2f; // Small negative value to keep grounded
            }
        }
        else
        {
            verticalVelocity += settings.gravity * Time.deltaTime;
        }
    }

    private void RotatePlayer()
    {
        transform.Rotate(0f, mouseX * settings.rotationSpeed * Time.deltaTime, 0f);
    }

    private bool IsGrounded()
    {
        // Ground check via raycast
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 1.2f);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        Vector2 lookDelta = value.Get<Vector2>();
        mouseX = lookDelta.x;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && IsGrounded())
        {
            isJumping = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public enum CharacterTypes
    {
        Alex,
        Aura
    }
    public CharacterTypes characterType;
    public CharacterTypes currentControlling = CharacterTypes.Alex;
    public float moveSpeed;
    protected float horizontalInput;
    protected float verticalInput;
    protected Rigidbody2D rb;
    protected Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetInternalInput();
        SwitchControllingPlayer();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        UpdateAnimations();
    }

    // Get the input from the player
    private void GetInternalInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    // Move the character based on the input
    private void HandleMovement()
    {
        if (currentControlling == CharacterTypes.Alex && characterType == CharacterTypes.Alex)
        {
            Vector2 movement = new Vector2(horizontalInput, verticalInput);
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        else if (currentControlling == CharacterTypes.Aura && characterType == CharacterTypes.Aura)
        {
            Vector2 movement = new Vector2(horizontalInput, verticalInput);
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

    }

    // Update the animations based on the input
    private void UpdateAnimations()
    {
        if (currentControlling == CharacterTypes.Alex)
        {
            if (horizontalInput != 0 || verticalInput != 0)
            {
                if (horizontalInput > 0)
                {
                    ClearAlexAnimation();
                    animator.SetBool("PlayerRightSideWalking", true);
                }
                if (horizontalInput < 0)
                {
                    ClearAlexAnimation();
                    // moving left
                    animator.SetBool("PlayerSideWalking", true);
                }
                if (verticalInput > 0)
                {
                    ClearAlexAnimation();
                    // moving up
                    animator.SetBool("PlayerUpWalking", true);
                }
                if (verticalInput < 0)
                {
                    ClearAlexAnimation();
                    // moving down
                    animator.SetBool("PlayerDownWalking", true);
                }
                if (horizontalInput > 0 && verticalInput > 0)
                {
                    ClearAlexAnimation();
                    // moving top right
                    animator.SetBool("PlayerUpWalking", false);
                    animator.SetBool("PlayerRightSideWalking", true);
                }
                if (horizontalInput > 0 && verticalInput < 0)
                {
                    ClearAlexAnimation();
                    // moving bottom right
                    animator.SetBool("PlayerDownWalking", false);
                    animator.SetBool("PlayerRightSideWalking", true);
                }
                if (horizontalInput < 0 && verticalInput > 0)
                {
                    ClearAlexAnimation();
                    // moving top left
                    animator.SetBool("PlayerUpWalking", false);
                    animator.SetBool("PlayerSideWalking", true);
                }
                if (horizontalInput < 0 && verticalInput < 0)
                {
                    ClearAlexAnimation();
                    // moving bottom left
                    animator.SetBool("PlayerDownWalking", false);
                    animator.SetBool("PlayerSideWalking", true);
                }
            }
            else
            {
                ClearAlexAnimation();
            }
        }
        else
        {
            if (horizontalInput != 0 || verticalInput != 0)
            {
                if (horizontalInput > 0)
                {
                    ClearAuraAnimation();
                    animator.SetBool("AuraMoving", true);
                }
                if (horizontalInput < 0)
                {
                    ClearAuraAnimation();
                    // moving left
                    animator.SetBool("AuraMovingLeft", true);
                }

            }
            else
            {
                ClearAuraAnimation();
            }
        }
    }

    private void ClearAlexAnimation()
    {
        animator.SetBool("PlayerRightSideWalking", false);
        animator.SetBool("PlayerSideWalking", false);
        animator.SetBool("PlayerUpWalking", false);
        animator.SetBool("PlayerDownWalking", false);
    }

    private void ClearAuraAnimation()
    {
        animator.SetBool("AuraMoving", false);
        animator.SetBool("AuraMovingLeft", false);

    }

    private void SwitchControllingPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ClearAuraAnimation();
            ClearAlexAnimation();
            if (currentControlling == CharacterTypes.Alex)
            {
                currentControlling = CharacterTypes.Aura;
            }
            else
            {
                currentControlling = CharacterTypes.Alex;
            }
        }
    }
}


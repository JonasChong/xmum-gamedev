using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFollowing : MonoBehaviour
{
    public Transform alexTransform;
    public Transform auraTransform;
    public Rigidbody2D rbAlex;
    public Rigidbody2D rbAura;

    private CharacterMovement characterMovement;
    private float followRange = 1f;

    private void Start()
    {
        characterMovement = GameObject.Find("Alex").GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        AutoFollow();
    }

    private void AutoFollow()
    {
        if (characterMovement.currentControlling == CharacterMovement.CharacterTypes.Alex)
        {
            Vector3 direction = auraTransform.position - alexTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rbAura.rotation = angle;
            if (direction.magnitude > followRange)
            {
                direction.Normalize();
                Vector2 movement = direction;
                rbAura.MovePosition((Vector2)auraTransform.position - (movement * characterMovement.moveSpeed * Time.fixedDeltaTime));
            }

        }
        else if (characterMovement.currentControlling == CharacterMovement.CharacterTypes.Aura)
        {
            Vector3 direction = alexTransform.position - auraTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rbAlex.rotation = angle;
            if (direction.magnitude > followRange)
            {
                direction.Normalize();
                Vector2 movement = direction;
                rbAlex.MovePosition((Vector2)alexTransform.position - (movement * characterMovement.moveSpeed * Time.fixedDeltaTime));
            }

        }
    }
}

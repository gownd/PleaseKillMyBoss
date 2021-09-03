using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss.Core;

namespace Boss.Combat
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] float baseSpeed = 60f;
        [SerializeField] float walkSpeedModifier = 1f;
        [SerializeField] float runSpeedModifier = 2f;

        Rigidbody2D rb;
        Animator animator;

        Transform destination;
        float currentModifier;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            currentModifier = walkSpeedModifier;
        }

        private void Update()
        {
            UpdateAnimator();
        }

        private void FixedUpdate()
        {
            Move();
        }

        void Move()
        {
            if(destination != null)
            {
                // Move
                rb.velocity = new Vector2(GetMoveSpeed() * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                // Stop
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }

        float GetMoveSpeed()
        {
            float direction = 1f;
            if (destination.position.x >= transform.position.x) direction = 1f;
            else direction = -1f;

            transform.localScale = new Vector2(direction, 1f);

            float moveSpeed = baseSpeed * currentModifier * direction;
            return moveSpeed;
        }

        public void StartMoveAction(Transform target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(target);
        }

        public void MoveTo(Transform destination)
        {
            this.destination = destination;
        }

        public void Cancel()
        {
            destination = null;
            currentModifier = 0f;
        }

        public void SetMoveSpeed(float modifier)
        {
            currentModifier = modifier;
        }

        void UpdateAnimator()
        {
            animator.SetFloat("moveModifier", currentModifier);
        }
    }

}

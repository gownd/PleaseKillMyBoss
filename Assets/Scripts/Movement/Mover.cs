using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss.Core;

namespace Boss.Movement
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
        bool isWalking = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            Cancel();
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
            if(isWalking) // Walk Right
            {
                float walkSpeed = baseSpeed * currentModifier;
                rb.velocity = new Vector2(walkSpeed * Time.deltaTime, rb.velocity.y);
            }
            else if(destination != null)
            {
                // Run To
                rb.velocity = new Vector2(GetMoveToSpeed() * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                // Stop
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }

        float GetMoveToSpeed()
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
            RunTo(target);
        }

        public void RunTo(Transform destination)
        {
            isWalking = false;
            currentModifier = runSpeedModifier;
            this.destination = destination;
        }

        public void Cancel()
        {
            isWalking = false;
            destination = null;
            currentModifier = 0f;
        }

        public void SetMoveSpeed(float modifier)
        {
            
        }

        void UpdateAnimator()
        {
            animator.SetFloat("moveModifier", currentModifier);
        }

        public void WalkRight()
        {
            transform.localScale = new Vector2(1f, 1f);
            currentModifier = walkSpeedModifier;
            isWalking = true;
        }
    }

}

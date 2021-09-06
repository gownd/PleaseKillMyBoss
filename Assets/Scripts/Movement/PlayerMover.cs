using UnityEngine;

namespace Boss.Movement
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 10f;

        Rigidbody2D rb;
        Animator animator;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Update() 
        {
              
        }

        private void FixedUpdate()
        {
            Move();
            FlipSprite();  
        }

        void Move()
        {
            float controlThrow = Input.GetAxis("Horizontal");
            Vector2 playerVelocity = new Vector2(controlThrow * moveSpeed * Time.deltaTime, rb.velocity.y);
            rb.velocity = playerVelocity;

            bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
            animator.SetBool("isMoving", playerHasHorizontalSpeed);
        }

        void FlipSprite()
        {
            bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
            if (playerHasHorizontalSpeed)
            {
                transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
            }
        }
    }
}

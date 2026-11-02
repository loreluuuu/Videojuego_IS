using UnityEngine;

namespace NatureBackgroundsPixelArt
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        private Rigidbody2D rb;

        [SerializeField] private float speed = 4.0f;
        [SerializeField] private float jumpForce = 16.0f;

        private float baseSpeed;
        private float baseJumpForce;

        public bool isBig = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            baseSpeed = speed;
            baseJumpForce = jumpForce;
        }

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

            if (Input.GetButtonDown("Jump"))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }

            if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            }
        }

        // --- MÉTODOS DE POCIÓN ---
        public void ActivatePotion(float speedBoost, float jumpBoost, float duration)
        {
            StartCoroutine(PotionRoutine(speedBoost, jumpBoost, duration));
        }

        private System.Collections.IEnumerator PotionRoutine(float speedBoost, float jumpBoost, float duration)
        {
            speed += speedBoost;
            jumpForce += jumpBoost;
            yield return new WaitForSeconds(duration);
            speed -= speedBoost;
            jumpForce -= jumpBoost;
        }

        public void ActivateBigMode(float duration)
        {
            StartCoroutine(BigModeRoutine(duration));
        }

        private System.Collections.IEnumerator BigModeRoutine(float duration)
        {
            isBig = true;

            transform.localScale = new Vector3(2f, 2f, 1f);

            yield return new WaitForSeconds(duration);

            transform.localScale = new Vector3(1f, 1f, 1f);

            isBig = false;
        }
    }
}
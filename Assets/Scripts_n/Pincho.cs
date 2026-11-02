using UnityEngine;

public class Pincho : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("PINCHO TOCADO");

            if (GameManager.instance != null)
            {
                GameManager.instance.LoseLife(collision.gameObject);
            }
        }
    }
}
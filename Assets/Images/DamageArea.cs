using UnityEngine;

public class DamageArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificamos si lo que tocamos tiene la etiqueta de enemigo
        if (other.CompareTag("Enemy"))
        {
            // 🔄 BUSCAMOS EL NUEVO CEREBRO UNIFICADO DEL ENEMIGO
            EnemyController enemigo = other.GetComponent<EnemyController>();
            
            if (enemigo != null)
            {
                // Llamamos a la función unificada de recibir daño
                enemigo.TakeDamage(); 
            }
        }
    }
}
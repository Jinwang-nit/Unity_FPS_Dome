using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTranform = collision.transform;
        if (hitTranform.CompareTag("Player"))
        {
            hitTranform.GetComponent<PlayerHealth>().TakeDamage(10);
        }
        if (hitTranform.CompareTag("Enemy"))
        {
            Transform root = hitTranform.root;
            root.GetComponent<Enemy>().TakeDamage();
        }
        Destroy(gameObject);
    }
}

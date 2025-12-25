using UnityEngine;

public class Missile : MonoBehaviour
{
    [Header("Missile Settings")]
    public float speed;
    public float lifeTime;

    [Header("Explosion Settings")]
    public GameObject explosionEffect;

    [Header("Audio Settings")]
    public AudioClip explosionSfx;

    [Range(0f, 1f)]
    public float explosionVolume = 1f;


    private float Delay = 1f;
    void Start()
    {
        // Destroy missile after lifetime.
        Destroy(gameObject, lifeTime);

    }


    void Update()
    {
        // Miisile move to applied direction.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void SpawnExplosion()
    {
        // Instantiating  Explosion Effect.
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Spawning Explosion effect before Destroy Gameobject.
            SpawnExplosion();
            // Play Explosion Audio.
            if (explosionEffect != null)
            {
                AudioSource.PlayClipAtPoint(explosionSfx, transform.position, explosionVolume);
            }
            //  Missile Destroy when hit  Enemy.
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Target"))
        {
            // Play  Explosion Audio.
            if (explosionSfx != null)
            {
                AudioSource.PlayClipAtPoint(explosionSfx, transform.position, explosionVolume);
            }
            // Spawning Explosion effect before  Destroy  Gameobject.
            SpawnExplosion();
            // Missile Destroy when  hit Target.
            Destroy(collision.gameObject);
        }
        // Always Destroy Missile when it hit anything.
        Destroy(gameObject);
    }
}

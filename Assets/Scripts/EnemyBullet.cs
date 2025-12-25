using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit to player");
        }

        Destroy(gameObject);
    }
}

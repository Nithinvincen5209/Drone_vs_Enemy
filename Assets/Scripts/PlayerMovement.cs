using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Drone Movement")]
    public float speed = 10f;
    public float ascendSpeed;
    public float descendSpeed;

    [Header("Weapon Settings")]
    public GameObject missilePrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    private float nextfireTime = 0f;

    [Header("Audio Settings")]
    public AudioClip fireSfx;
    public AudioClip DroneSfx;

    [Range(0f, 1f)]
    public float droneVolume = 0.5f;
    [Range(0f, 1f)]
    public float fireVolume = 0.5f;

    private float xmov;
    private float zmov;
    private Rigidbody rb;
    public AudioSource weaponSource;
    public AudioSource droneSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Setting up drone source.
        if (droneSource != null && DroneSfx != null)
        {
            droneSource.clip = DroneSfx;
            droneSource.loop = true;
            droneSource.volume = droneVolume;
        }

    }
    private void FixedUpdate()
    {
        DroneMovement();
        AscendDescendMovement();
    }

    void Update()
    {
        HandleShooting();
    }
    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.time >= nextfireTime)
        {
            // reset next fire.
            nextfireTime = Time.time + fireRate;
            Shoot();
        }
    }
    void Shoot()
    {
        // checking missile prefab and firepoint.
        if (missilePrefab != null && firePoint != null)
        {
            // Instantiating Missile.
            Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
            weaponSource.PlayOneShot(fireSfx, fireVolume);
        }
        else
        {
            Debug.LogError("Missile or Firepoint missing");
        }
    }
    void DroneMovement()
    {
        // Drone Movement  Using W,A,S,D.
        xmov = Input.GetAxis("Horizontal");
        zmov = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(xmov, 0, zmov);
        rb.AddForce(movement * speed);

    }
    void AscendDescendMovement()
    {
        // Drone Movement for Ascending and Desceding.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rb.AddForce(Vector3.up * ascendSpeed, ForceMode.Impulse);
            droneSource.Play();

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.AddForce(Vector3.down * descendSpeed, ForceMode.Impulse);

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Dronesfx will  Stop.
            droneSource.Stop();
        }
    }
}

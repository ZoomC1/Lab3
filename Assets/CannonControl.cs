using UnityEngine;

public class CannonControl : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public GameObject cannonballPrefab; // ������ ����
    public Transform firePoint; // �����, �� ������� ����� �������� ����
    public float fireForce = 500f; // ���� ��������
    public ParticleSystem explosionEffect; // ������ �� Particle System ������� ������
    public AudioSource audioSource; // ������ �� ��������� AudioSource
    public AudioClip shootSound; // �������� ������ ��������

    private bool canShoot = true;

    void Start()
    {
        // ��������, ��� AudioSource � ��������� ������
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource component missing from this game object. Please add one.");
            }
        }

        if (shootSound == null)
        {
            Debug.LogError("Shoot sound clip not assigned. Please assign a shoot sound in the inspector.");
        }
    }

    void Update()
    {
        try
        {
            // ��������� ������� ������ AD ��� �������� ����� � ������
            float horizontal = Input.GetAxis("Horizontal"); // ��� ������ A � D
            // ��������� ������� ������ WS ��� ������� ����� � ����
            float vertical = Input.GetAxis("Vertical"); // ��� ������ W � S

            // ������������ ���� ����� ������ ������������ ���
            transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

            // ��������� ���� ����� ����� � ���� ������ ��������� ��� X
            transform.Rotate(vertical * rotationSpeed * Time.deltaTime, 0, 0, Space.Self);

            // ��������� ������� ������ ��� �������� (�� ��������� ������)
            
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error in Update method: " + ex.Message);
        }
    }

    void Shoot()
    {
        try
        {
            // ������� ���� � ����� �������� � ��� �� �����������, ��� � � ����
            GameObject cannonball = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("Cannonball instantiated at position: " + firePoint.position);

            // ��������� ������ ������
            if (explosionEffect != null)
            {
                explosionEffect.transform.position = firePoint.position;
                explosionEffect.transform.rotation = firePoint.rotation;
                explosionEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // ��������� ������� ������, ���� �� ��� ����
                explosionEffect.Play();
                Debug.Log("Explosion effect played at position: " + firePoint.position);
            }
            else
            {
                Debug.LogWarning("Explosion effect is not assigned!");
            }

            // ����������� ���� ��������
            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
                Debug.Log("Shoot sound played.");
            }

            // �������� ��������� Rigidbody ���� � ������� ��� ��������� ��������
            Rigidbody rb = cannonball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(firePoint.forward * fireForce);
                Debug.Log("Cannonball fired with force: " + fireForce);
            }
            else
            {
                Debug.LogWarning("Cannonball prefab does not have a Rigidbody component!");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error in Shoot method: " + ex.Message);
        }
    }
}
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public AudioSource audioSource; // ������ �� ��������� AudioSource
    public AudioClip collisionSound; // �������� ������ ������������

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

        if (collisionSound == null)
        {
            Debug.LogError("Collision sound clip not assigned. Please assign a collision sound in the inspector.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // ����������� ���� ������������
        if (audioSource != null && collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound);
            
        }

        // ������������� ����� �������� ������ ���������� ���� ��� ������ ��������
         
    }
}
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public AudioSource audioSource; // Ссылка на компонент AudioSource
    public AudioClip collisionSound; // Звуковой эффект столкновения

    void Start()
    {
        // Проверка, что AudioSource и аудиоклип заданы
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
        // Проигрываем звук столкновения
        if (audioSource != null && collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound);
            
        }

        // Дополнительно можно добавить логику разрушения ядра или других объектов
         
    }
}
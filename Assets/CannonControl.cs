using UnityEngine;

public class CannonControl : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public GameObject cannonballPrefab; // Префаб ядра
    public Transform firePoint; // Точка, из которой будет вылетать ядро
    public float fireForce = 500f; // Сила выстрела
    public ParticleSystem explosionEffect; // Ссылка на Particle System эффекта взрыва
    public AudioSource audioSource; // Ссылка на компонент AudioSource
    public AudioClip shootSound; // Звуковой эффект выстрела

    private bool canShoot = true;

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

        if (shootSound == null)
        {
            Debug.LogError("Shoot sound clip not assigned. Please assign a shoot sound in the inspector.");
        }
    }

    void Update()
    {
        try
        {
            // Считываем нажатия клавиш AD для поворота влево и вправо
            float horizontal = Input.GetAxis("Horizontal"); // Для клавиш A и D
            // Считываем нажатия клавиш WS для наклона вверх и вниз
            float vertical = Input.GetAxis("Vertical"); // Для клавиш W и S

            // Поворачиваем дуло пушки вокруг вертикальной оси
            transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

            // Наклоняем дуло пушки вверх и вниз вокруг локальной оси X
            transform.Rotate(vertical * rotationSpeed * Time.deltaTime, 0, 0, Space.Self);

            // Проверяем нажатие кнопки для выстрела (по умолчанию пробел)
            
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
            // Создаем ядро в точке выстрела с той же ориентацией, что и у дула
            GameObject cannonball = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("Cannonball instantiated at position: " + firePoint.position);

            // Запускаем эффект взрыва
            if (explosionEffect != null)
            {
                explosionEffect.transform.position = firePoint.position;
                explosionEffect.transform.rotation = firePoint.rotation;
                explosionEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // Остановим текущий эффект, если он еще идет
                explosionEffect.Play();
                Debug.Log("Explosion effect played at position: " + firePoint.position);
            }
            else
            {
                Debug.LogWarning("Explosion effect is not assigned!");
            }

            // Проигрываем звук выстрела
            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
                Debug.Log("Shoot sound played.");
            }

            // Получаем компонент Rigidbody ядра и придаем ему начальную скорость
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
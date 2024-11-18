using System.Collections;
using UnityEngine;

public class BirdBoss : MonoBehaviour {

    public GameObject bulletPrefab;
    private float timePassed = 0f;

    public float rotationSpeed = 10f;
    public float moveSpeed = 1.5f;
    private bool hasRotatedAt10 = false;
    private bool hasRotatedAt20 = false;

    private float totalTime = 0.0f;

    // Espiral
    public GameObject rotador;
    private float currentAngle = 0f;
    public float fireRateSpiral = 15f;

    // 6 Peaks
    public float fireRate_6Peak = 1f;
    public float bulletSpeed = 0.2f;


    private void Update() {

        timePassed += Time.deltaTime;
        totalTime += Time.deltaTime;

        // Tres modos de disparo 
        if (totalTime <= 10.0f) {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            if (timePassed > 1 / fireRateSpiral) {
                Shoot_Spiral();
                timePassed = 0.0f;
            }

        } else if (totalTime <= 20.0f) {
            if (!hasRotatedAt10) {
                transform.Rotate(0f, 180f, 0f);
                hasRotatedAt10 = true;
            }

            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            if (timePassed > fireRate_6Peak) {
                Shoot_6Peaks();
                timePassed = 0.0f;
            }

        } else if (totalTime <= 30.0f) {
            if (!hasRotatedAt20) {
                transform.Rotate(0f, 180f, 0f);
                hasRotatedAt20 = true;
            }
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            if (timePassed > 1 / fireRateSpiral) {
                Shoot_Lissajous();
                timePassed = 0.0f;
            }
        } else {
            // No se ejecuta nada
        }
    }

    private void Shoot_Spiral() {
        // Dirección de la bala --> Depende del ángulo previo
        float bulletDirX = Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        float bulletDirY = Mathf.Cos(currentAngle * Mathf.Deg2Rad); 

        Vector3 bulletVector = new Vector3(bulletDirX, bulletDirY, 0);
        Vector3 bulletMoveDirection = bulletVector.normalized; // Velocidad constante

        // Creación de la bala
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDirection(bulletMoveDirection);

        // Acumular ángulo
        currentAngle += rotationSpeed;

        // Reinicia el ángulo después de una vuelta completa
        if (currentAngle >= 360f) {
            currentAngle -= 360f;
        }
    }

    private void Shoot_6Peaks() {
        int numberOfBullets = 60; 
        float angleStep = 360f / numberOfBullets;
        float angle = 0f;
        int numberOfPeaks = 6;

        for (int i = 0; i < numberOfBullets; i++) {
            // Calcula la amplitud con base en los picos deseados
            float amplitude = 6 + Mathf.Sin(numberOfPeaks * (angle * Mathf.PI) / 180) * 3;

            // Calcula la posición de cada bala usando la amplitud calculada
            float bulletDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * amplitude;
            float bulletDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * amplitude;

            Vector3 bulletVector = new Vector3(bulletDirX, bulletDirY, transform.position.z);

            // Calcula el vector de movimiento de la bala hacia la posición deseada SIN normalizar
            Vector3 bulletMoveDirection = (bulletVector - transform.position) * bulletSpeed;

            // Crea la bala
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            bulletScript.SetDirection(bulletMoveDirection); // Envia el vector completo, no normalizado

            angle += angleStep;
        }
    }

    private void Shoot_DoubleSpiral() {
        // Primera espiral
        float bulletDirX1 = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float bulletDirY1 = Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        Vector3 bulletVector1 = new Vector3(bulletDirX1, bulletDirY1, 0);

        GameObject bullet1 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletScript1 = bullet1.GetComponent<Bullet>();
        bulletScript1.SetDirection(bulletVector1);

        // Segunda espiral (desfase de 180 grados)
        float offsetAngle = currentAngle + 180f; 

        float bulletDirX2 = Mathf.Cos(offsetAngle * Mathf.Deg2Rad);
        float bulletDirY2 = Mathf.Sin(offsetAngle * Mathf.Deg2Rad);
        Vector3 bulletVector2 = new Vector3(bulletDirX2, bulletDirY2, 0);

        GameObject bullet2 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletScript2 = bullet2.GetComponent<Bullet>();
        bulletScript2.SetDirection(bulletVector2);

        currentAngle += rotationSpeed;

        // Asegurar que el ángulo no supere los 360 grados
        if (currentAngle >= 360f) {
            currentAngle -= 360f;
        }
    }

    private void Shoot_Lissajous() {
        float a = 3; // Frecuencia en X
        float b = 2; // Frecuencia en Y
        float bulletDirX = Mathf.Sin(a * currentAngle * Mathf.Deg2Rad);
        float bulletDirY = Mathf.Sin(b * currentAngle * Mathf.Deg2Rad);

        Vector3 bulletVector = new Vector3(bulletDirX, bulletDirY, 0);

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDirection(bulletVector);

        currentAngle += rotationSpeed;

        if (currentAngle >= 360f) {
            currentAngle -= 360f;
        }
    }

}

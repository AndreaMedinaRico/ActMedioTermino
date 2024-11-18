using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 0.05f;
    private Vector3 direction;

    private float LIFETIME = 1.5f;
    private float lifetimer = 0f;

    void Start() {
        lifetimer = 0f;
        // Actualiza cuenta de balas
        BulletCounter.Instance.AddBullet();
    }

    public void SetDirection(Vector3 dir) {
        direction = dir;
    }

    private void Update() {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        lifetimer += Time.deltaTime;
        if (lifetimer >= LIFETIME) {
            // Actualiza cuenta de balas
            BulletCounter.Instance.RemoveBullet();
            Destroy(gameObject);
        }
    }
}

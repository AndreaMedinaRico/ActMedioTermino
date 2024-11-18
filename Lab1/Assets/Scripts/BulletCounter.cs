using UnityEngine;
using System;

public class BulletCounter : MonoBehaviour {

    // Notifica cambios en contador de balas
    public static Action OnBulletCountChanged;

    // Lectura desde otras clases y escritura reservada
    public static BulletCounter Instance{get; private set;}

    public int bulletCount = 0;

    // Asegura que solo exista un BulletCounter
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        OnBulletCountChanged?.Invoke();
    }

    public void AddBullet() {
        bulletCount++;
        OnBulletCountChanged?.Invoke();
    }

    public void RemoveBullet() {
        bulletCount--;
        OnBulletCountChanged?.Invoke();
    }

    public int GetBulletCount() {
        return bulletCount;
    }
}

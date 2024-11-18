using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Clase que mueve los vehículos hacia adelante
/// </summary>

public class MoveForward : MonoBehaviour {

    public int speed;

    /// <summary>
    /// Método llamado en el primer frame --> Configuraciones iniciales
    /// </summary>
    void Start() {
        
    }

    /// <summary>
    /// Actualiza cada frame
    /// </summary>
    void Update() {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}

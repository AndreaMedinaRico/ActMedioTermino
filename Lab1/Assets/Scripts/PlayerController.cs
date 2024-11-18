using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Clase que actualiza los eventos para el control del movimiento del jugador
/// </summary>

public class PlayerController : MonoBehaviour {

    // Velocidades
    public float speed = 5.0f;
    public float turnSpeed = 0.0f;

    // Controles
    public float horizontalInput;
    public float verticalInput;

    // Cámaras
    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey;

    // Multijugador
    public string inputId;

    /// <summary>
    /// Método llamado en el primer frame --> Configuraciones iniciales
    /// </summary>
    void Start() {
        
    }

    /// <summary>
    /// Actualiza cada frame
    /// </summary>
    void Update() {

        // Controles
        horizontalInput = Input.GetAxis("Horizontal" + inputId);
        verticalInput = Input.GetAxis("Vertical" + inputId);

        // Vehículo avanza hacia adelante --> velocidad y tiempo transcurrido / frames
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);

        // Giro lateral
        // transform.Translate(Vector3.right * turnSpeed * Time.deltaTime * horizontalInput);
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * horizontalInput);

        // Cambio entre cámaras
        if (Input.GetKeyDown(switchKey)) {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    }
}

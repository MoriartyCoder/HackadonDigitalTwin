using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5f; // Geschwindigkeit der Kamerabewegung
    public float verticalSpeed = 3f; // Geschwindigkeit der vertikalen Kamerabewegung
    public float rotationSpeed = 3f; // Geschwindigkeit der Kameradrehung
    public float shiftMultiplier = 2f; // Multiplikator für die Geschwindigkeit bei Umschalttaste

    void Update()
    {
        // Tastatureingaben erfassen
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Kamerabewegung berechnen
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;

        // Leertaste prüfen (nach oben bewegen)
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime);
        }

        // Linke Umschalttaste prüfen (nach unten bewegen)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.down * verticalSpeed * Time.deltaTime);
        }

        // Mausdruck prüfen (linke Maustaste)
        if (Input.GetMouseButton(0))
        {
            // Kameradrehung berechnen
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotationY = -Input.GetAxis("Mouse Y") * rotationSpeed;

            // Kamera drehen
            transform.Rotate(Vector3.up * rotationX, Space.World);
            transform.Rotate(Vector3.right * rotationY, Space.Self);
        }

        // Umschalttaste prüfen
        float multiplier = 1f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            multiplier = shiftMultiplier;
        }

        // Kamera bewegen
        transform.Translate(movement * multiplier);
    }
}
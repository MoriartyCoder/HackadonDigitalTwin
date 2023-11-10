using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5f; // Geschwindigkeit der Kamerabewegung
    public float verticalSpeed = 3f; // Geschwindigkeit der vertikalen Kamerabewegung
    public float rotationSpeed = 3f; // Geschwindigkeit der Kameradrehung
    public float shiftMultiplier = 2f; // Multiplikator für die Geschwindigkeit bei Umschalttaste
    public Transform topDownPosition; // Zielposition für die Top-Down-Ansicht

    private bool isMovingToTopDown = false; // Flag, um zu überprüfen, ob die Kamera in die Top-Down-Ansicht bewegt wird

    void Update()
    {
        // Wenn die Taste "E" gedrückt wird, startet die Coroutine für die Bewegung zur Top-Down-Ansicht
        if (Input.GetKeyDown(KeyCode.E) && !isMovingToTopDown)
        {
            StartCoroutine(MoveToTopDownView());
        }

        // Umschalttaste prüfen
        float multiplier = 1f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            multiplier = shiftMultiplier;
        }

        // Kamera bewegen
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * speed * Time.deltaTime;
        transform.Translate(movement * multiplier);

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
    }

    // Coroutine für die Bewegung zur Top-Down-Ansicht
    IEnumerator MoveToTopDownView()
    {
        isMovingToTopDown = true;

        // Nach Erreichen der Top-Down-Position, schaue nach unten
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        // Zielposition festlegen (beispielsweise über der Szene)
        Vector3 targetPosition = topDownPosition.position;

        // Kamera langsam zur Zielposition bewegen
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 2f);
            yield return null;
        }

        isMovingToTopDown = false;
    }
}

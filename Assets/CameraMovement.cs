using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5f;
    public float verticalSpeed = 3f;
    public float rotationSpeed = 3f;
    public float shiftMultiplier = 2f;
    public Transform topDownPosition;

    private bool isMovingToTopDown = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isMovingToTopDown)
        {
            StartCoroutine(MoveToTopDownView());
        }

        float multiplier = 1f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            multiplier = shiftMultiplier;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.down * verticalSpeed * Time.deltaTime);
        }

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * speed * Time.deltaTime;
        transform.Translate(movement * multiplier);

        if (Input.GetMouseButton(0))
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotationY = -Input.GetAxis("Mouse Y") * rotationSpeed;

            transform.Rotate(Vector3.up * rotationX, Space.World);
            transform.Rotate(Vector3.right * rotationY, Space.Self);
        }
    }

    IEnumerator MoveToTopDownView()
    {
        isMovingToTopDown = true;

        // Schau zuerst nach unten
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(90f, 0f, 0f);

        float elapsedTime = 0f;
        float duration = 1f; // Zeit, die für die Rotation nach unten benötigt wird

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Nach Erreichen der Top-Down-Position, schaue nach unten
        transform.rotation = targetRotation;

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
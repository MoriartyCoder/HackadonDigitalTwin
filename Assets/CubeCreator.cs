using UnityEngine;

public class CubeCreator : MonoBehaviour
{
    public GameObject cubePrefab;  // Verknüpfung mit einem Cube Prefab in Unity Inspector
    private bool isCreatingCube = false;
    private Vector3 cubeStartPosition;
    private Vector3 cubeEndPosition;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCreatingCube();
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            FinishCreatingCube();
        }

        if (isCreatingCube)
        {
            UpdateCubePosition();
        }
    }

    void StartCreatingCube()
    {
        isCreatingCube = true;
        cubeStartPosition = GetMousePositionOnFloor();
    }

    void FinishCreatingCube()
    {
        isCreatingCube = false;
        cubeEndPosition = GetMousePositionOnFloor();

        CreateCube();
    }

    void UpdateCubePosition()
    {
        cubeEndPosition = GetMousePositionOnFloor();
    }

    Vector3 GetMousePositionOnFloor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Default")))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    void CreateCube()
    {
    Vector3 cubeSize = new Vector3(cubeEndPosition.x - cubeStartPosition.x, 
                                   cubePrefab.transform.localScale.y,
                                   cubeEndPosition.z - cubeStartPosition.z);

    Vector3 cubeCenter = cubeStartPosition + 0.5f * cubeSize;

    GameObject newCube = Instantiate(cubePrefab, cubeCenter, Quaternion.identity);
    newCube.transform.localScale = cubeSize;
    }
}

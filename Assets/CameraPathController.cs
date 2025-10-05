using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraPathController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineTrackedDolly dolly;
    public Transform[] planets;
    public float moveSpeed = 0.5f;
    public float stopDuration = 2f;

    private int currentPlanetIndex = 0;
    private bool isMoving = true;
    private float pathPosition = 0f;

    private void Start()
    {
        if (virtualCamera == null)
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        dolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        StartCoroutine(MoveAlongPlanets());
    }

    private IEnumerator MoveAlongPlanets()
    {
        while (true)
        {
            if (isMoving && dolly != null)
            {
                pathPosition += moveSpeed * Time.deltaTime;
                dolly.m_PathPosition = pathPosition;
                Debug.Log("Camera moving: " + dolly.m_PathPosition);

                // Example: stop at certain path positions (e.g., per planet)
                if (Vector3.Distance(dolly.VirtualCamera.transform.position, planets[currentPlanetIndex].position) < 2f)
                {
                    Debug.Log("Reached planet: " + planets[currentPlanetIndex].name);
                    isMoving = false;

                    yield return new WaitForSeconds(stopDuration);

                    currentPlanetIndex = (currentPlanetIndex + 1) % planets.Length;
                    isMoving = true;
                }
            }

            yield return null;
        }
    }
}

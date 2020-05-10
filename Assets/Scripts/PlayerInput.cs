using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerInput : MonoBehaviour
{
    private const float zoomMax = 60;
    private const float zoomMin = 3;

    private PlatformInput input;
    private Camera cameraComponent;

    private void Start()
    {
        cameraComponent = GetComponent<Camera>();

        if (Application.isMobilePlatform)
        {
            input = new MobileInput();
        }
        else
        {
            input = new DesktopInput();
        }
    }

    private void Update()
    {
        Vector2 turnChange = input.Turn();
        if (turnChange != Vector2.zero)
        {
            transform.eulerAngles = turnChange;
        }

        float ZoomChange = input.Zoom();
        if (cameraComponent.fieldOfView + ZoomChange < zoomMax)
        {
            cameraComponent.fieldOfView += ZoomChange;
        }
        else if (cameraComponent.fieldOfView - ZoomChange > zoomMin)
        {
            cameraComponent.fieldOfView -= ZoomChange;
        }
    }

    public void ResetTurning()
    {
        input.ResetTurning();
    }
}

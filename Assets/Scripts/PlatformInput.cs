using UnityEngine;

public abstract class PlatformInput
{
    private float turnXChange = 0.0f;
    private float turnYChange = 0.0f;
    private float turningSpeed = 10.0f;

    public abstract Vector2 Turn();
    public abstract float Zoom();

    protected Vector2 ApplyTurning(float inputTurnDeltaX, float inputTurnDeltaY)
    {
        turnXChange += inputTurnDeltaX * turningSpeed * Time.deltaTime;
        turnYChange -= inputTurnDeltaY * turningSpeed * Time.deltaTime;

        return new Vector2(turnYChange, turnXChange);
    }

    public void ResetTurning()
    {
        turnXChange = 0;
        turnYChange = 0;
    }
}

public class MobileInput : PlatformInput
{
    private float lastFrameTouchesDelta = 0.0f;
    private const float zoomSpeed = 0.03f;

    public override Vector2 Turn()
    {
        if (Input.touches.Length == 1)
        {
            return ApplyTurning(Input.GetTouch(0).deltaPosition.x, Input.GetTouch(0).deltaPosition.y);
        }

        return Vector2.zero;
    }

    public override float Zoom()
    {
        if (Input.touches.Length == 2)
        {
            Vector3 TouchesDelta = Input.GetTouch(0).position - Input.GetTouch(1).position;

            float ZoomChange = TouchesDelta.magnitude * zoomSpeed * Time.deltaTime;
            if (TouchesDelta.magnitude > lastFrameTouchesDelta)
            {
                lastFrameTouchesDelta = TouchesDelta.magnitude;
                return ZoomChange;
            }
            else
            {
                lastFrameTouchesDelta = TouchesDelta.magnitude;
                return -ZoomChange;
            }
        }

        return 0.0f;
    }
}

public class DesktopInput : PlatformInput
{
    private bool turning = false;

    public override Vector2 Turn()
    {
        if (Input.GetMouseButtonDown(0))
        {
            turning = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            turning = false;
        }

        if (turning)
        {
            return ApplyTurning(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

        return Vector2.zero;
    }

    public override float Zoom()
    {
        return -Input.mouseScrollDelta.y;
    }
}

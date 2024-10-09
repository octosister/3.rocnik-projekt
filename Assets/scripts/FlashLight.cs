using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;
    public Camera playerCamera;
    private bool isFlashlightOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }

        if (isFlashlightOn)
        {
            UpdateFlashlightPosition();
        }
    }

    void ToggleFlashlight()
    {
        isFlashlightOn = !isFlashlightOn;
        flashlight.enabled = isFlashlightOn;
    }

    void UpdateFlashlightPosition()
    {
        flashlight.transform.position = playerCamera.transform.position;
        flashlight.transform.rotation = playerCamera.transform.rotation;
    }
}

using UnityEngine;

public class RandomLightFlash : MonoBehaviour
{
    public float minFlashInterval = 1.0f;
    public float maxFlashInterval = 3.0f;

    private Light lightComponent;
    private float nextFlashTime;
    private bool isLightOn = false;

    void Start()
    {
        lightComponent = GetComponent<Light>();
        ScheduleNextFlash();
    }

    void Update()
    {
        if (Time.time > nextFlashTime)
        {
            ToggleLight();
            ScheduleNextFlash();
        }
    }

    void ScheduleNextFlash()
    {
        nextFlashTime = Time.time + Random.Range(minFlashInterval, maxFlashInterval);
    }

    void ToggleLight()
    {
        isLightOn = !isLightOn;
        lightComponent.enabled = isLightOn;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class MashingBar : MonoBehaviour
{
    public Slider slider;
    public float decreaseRate = 1f;
    public float increaseAmount = 10f;
    public float smoothTime = 0.3f;

    private float currentVelocity;

    private void Start()
    {
        // Set the initial value of the slider to the maximum (100)
        slider.value = 0;
    }

    private void Update()
    {
        // Decrease the slider value over time
        if (slider.value > 0)
        {
            float targetValue = slider.value - decreaseRate * Time.deltaTime;
            slider.value = Mathf.Max(targetValue, 0f);
        }

        // Increase the slider value when the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            slider.value += increaseAmount;
        }

        // Smooth the slider movement
        SmoothSliderValue();
    }

    private void SmoothSliderValue()
    {
        // Calculate the target value based on the input and smoothing time
        float targetValue = Mathf.SmoothDamp(slider.value, slider.value, ref currentVelocity, smoothTime);

        // Apply the smoothed value to the slider
        slider.value = targetValue;

    }
    public void restartMash()
    {
        slider.value = 0;
    }

    public float getSliderValue()
    {
        return slider.value;
    }
}

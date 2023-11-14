using UnityEngine;
using UnityEngine.UI;

public class DomainEffect : MonoBehaviour
{
    public Image circleImage; // Reference to the circle Image component
    public Transform waspTransform; // Reference to the "wasp" GameObject's transform
    public float expansionSpeed = .5f; // Speed of expansion
    private bool expanding = false; // Flag to track expansion
    private Vector3 initialScale; // Store the initial scale

    private Camera mainCamera; // Reference to the main camera
    public bool isDomain = false;

    public bool waspCreatesDomain;
    public bool doDomain = false;

    void Start()
    {
        mainCamera = Camera.main;

        // Disable the circle Image initially
        circleImage.enabled = false;

        // Store the initial scale
        initialScale = circleImage.rectTransform.localScale;
    }

    void Update()
    {
        //Debug.Log(expanding);
        //Change it later
        if (doDomain && !expanding)
        {
            StartExpanding();
            isDomain = true;
        }

        if (!doDomain)
        {
            StartCollapsing();
            isDomain = false;
            circleImage.enabled = false;
        }

        if (expanding)
        {
            ExpandToScreenSize();
        }
    }

    void StartExpanding()
    {
        expanding = true;

        // Enable the circle Image
        circleImage.enabled = true;

        // Set the initial scale of the circle to the initial stored scale
        circleImage.rectTransform.localScale = initialScale;

        // Set the initial position of the circle to the "wasp" GameObject's position
        circleImage.rectTransform.position = waspTransform.position;
    }

    void StartCollapsing()
    {
        circleImage.enabled = false;
        expanding = false;
    }

  void ExpandToScreenSize()
{
    // Calculate the Canvas dimensions
    Canvas canvas = circleImage.canvas;
    float canvasWidth = canvas.pixelRect.width;
    float canvasHeight = canvas.pixelRect.height;

    // Calculate the target scale to match the screen size when the circle is enabled
    Vector3 targetScale = new Vector3(
        canvasWidth / circleImage.rectTransform.sizeDelta.x,
        canvasHeight / circleImage.rectTransform.sizeDelta.y,
        1f
    );

  
    // Check if the expansion or collapse is complete
  
    
      // Smoothly expand or collapse the circle to the screen size
    circleImage.rectTransform.localScale = Vector3.Lerp(circleImage.rectTransform.localScale, targetScale, Time.deltaTime * expansionSpeed);

}



    public bool isDomainActive()
    {
        return isDomain;
    }

    public void changeDomain()
    {
        if(doDomain)
            doDomain = false;
        else
            doDomain = true;

           
    }

}

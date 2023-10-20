using UnityEngine;
using UnityEngine.UI;

public class UIBlocker : MonoBehaviour
{
    // Reference to the Canvas that contains UI elements
    public Canvas uiCanvas;
    void Start()
    {
        BlockUI();
    }
    public void BlockUI()
    {
        // Set the "UIBlocker" panel to be fully opaque
        Image panelImage = GetComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.5f); // Adjust alpha value as needed

        // Disable all UI elements in the canvas
        if (uiCanvas != null)
        {
            GraphicRaycaster[] raycasters = uiCanvas.GetComponentsInChildren<GraphicRaycaster>();
            foreach (var raycaster in raycasters)
            {
                raycaster.enabled = false;
            }
        }
    }

    public void UnblockUI()
    {
        // Set the "UIBlocker" panel to be fully transparent
        Image panelImage = GetComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0);

        // Enable all UI elements in the canvas
        if (uiCanvas != null)
        {
            GraphicRaycaster[] raycasters = uiCanvas.GetComponentsInChildren<GraphicRaycaster>();
            foreach (var raycaster in raycasters)
            {
                raycaster.enabled = true;
            }
        }
    }
}

using UnityEngine;

public class MeshInvisible : MonoBehaviour
{
    private Renderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<Renderer>();

        // Make the mesh invisible during gameplay
        SetMeshVisibility(false);
    }

    private void Update()
    {
        // Toggle mesh visibility using a key press, for example the 'V' key
        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleVisibility();
        }
    }

    private void SetMeshVisibility(bool visible)
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = visible;
        }
    }

    public void ToggleVisibility()
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = !meshRenderer.enabled;
        }
    }
}

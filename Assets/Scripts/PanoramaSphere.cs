using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class PanoramaSphere : MonoBehaviour
{
    private MeshRenderer mesh;

    public void SetMainTexture(Texture2D newTexture)
    {
        CheckForMesh();
        mesh.material.mainTexture = newTexture;
    }

    public void SetSecondTexture(Texture2D newTexture)
    {
        CheckForMesh();
        mesh.material.SetTexture("_SecTex", newTexture);
    }

    public void SetBlend(float newBlendValue)
    {
        CheckForMesh();
        mesh.material.SetFloat("_Blend", newBlendValue);
    }

    private void CheckForMesh()
    {
        if (!mesh)
        {
            mesh = GetComponent<MeshRenderer>();
        }
    }

}

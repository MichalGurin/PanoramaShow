using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerMove player;
    [SerializeField] private PanoramaSphere firstPanoramaSphere;
    [SerializeField] private GameObject panoramaPrefab;
    [SerializeField] private Transform circle;

    private List<PanoramaSphere> panoramaSpheres;
    private int currentPanoramaIndex = 0;

    private ImageLoader imageLoader;

    private void Awake()
    {
        panoramaSpheres = new List<PanoramaSphere>();
        imageLoader = new ImageLoader(this);
        imageLoader.LoadImages();
    }

    public void GoToNextPanorama()
    {
        if (currentPanoramaIndex + 1 < imageLoader.LoadedTextures.Count)
        {
            SetSecondTextureOnAllSpheres(imageLoader.LoadedTextures[currentPanoramaIndex + 1]);
            currentPanoramaIndex++;

            Vector3 nextSpherePosition = panoramaSpheres[currentPanoramaIndex].transform.position;
            player.GetComponent<PlayerMove>().StartMovingTowardsPosition(nextSpherePosition);
        }
    }

    public void LoadingOfImagesDone()
    {
        firstPanoramaSphere.SetMainTexture(imageLoader.LoadedTextures[0]);
        if (imageLoader.LoadedTextures[1])
        {
            firstPanoramaSphere.SetSecondTexture(imageLoader.LoadedTextures[1]);
        }
        panoramaSpheres.Add(firstPanoramaSphere);

        for (int i = 1; i < imageLoader.LoadedTextures.Count; i++)
        {
            Vector3 newSphereSpawnPosition = new Vector3(firstPanoramaSphere.transform.localScale.x * i, 0, 0);
            PanoramaSphere newSphere = Instantiate<GameObject>(panoramaPrefab, newSphereSpawnPosition, Quaternion.identity).GetComponent<PanoramaSphere>();

            newSphere.SetMainTexture(imageLoader.LoadedTextures[0]);

            if (imageLoader.LoadedTextures[1])
            {
                newSphere.SetSecondTexture(imageLoader.LoadedTextures[1]);
            }

            panoramaSpheres.Add(newSphere);
        }

        FindRandomPositionForCircle();
    }

    public void MovementToSphereEnded()
    {
        SetMainTextureOnAllSpheres(imageLoader.LoadedTextures[currentPanoramaIndex]);

        FindRandomPositionForCircle();
    }

    public void SetBlendOnAllSpheres(float newBlend)
    {
        for (int i = 0; i < panoramaSpheres.Count; i++)
        {
            panoramaSpheres[i].SetBlend(newBlend);
        }
    }

    private void SetMainTextureOnAllSpheres(Texture2D newTexture)
    {
        for (int i = 0; i < panoramaSpheres.Count; i++)
        {
            panoramaSpheres[i].SetMainTexture(newTexture);
        }
    }

    private void SetSecondTextureOnAllSpheres(Texture2D newTexture)
    {
        for (int i = 0; i < panoramaSpheres.Count; i++)
        {
            panoramaSpheres[i].SetSecondTexture(newTexture);
        }
    }

    private void FindRandomPositionForCircle()
    {
        circle.position = new Vector3(player.transform.position.x + Random.Range(300, 500), Random.Range(-400, 400), Random.Range(-400, 400));
        circle.LookAt(player.transform, Vector3.up);
        circle.Rotate(new Vector3(0, 180, 0));
    }
}

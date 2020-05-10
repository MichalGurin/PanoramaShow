using System.Collections.Generic;
using UnityEngine;

public class ImageLoader
{
    private GameController gameController;

    public List<Texture2D> LoadedTextures { get; private set; }
    
    public ImageLoader(GameController gameController)
    {
        this.gameController = gameController;
        LoadedTextures = new List<Texture2D>();
    }

    public void LoadImages()
    {
        if (Application.isMobilePlatform)
        {
            LoadImagesFromGallery();
        }
        else
        {
            Texture2D lukaTexture = Resources.Load<Texture2D>("Luka");
            LoadedTextures.Add(lukaTexture);

            Texture2D cestaTexture = Resources.Load<Texture2D>("Cesta");
            LoadedTextures.Add(cestaTexture);

            Texture2D mestoTexture = Resources.Load<Texture2D>("Mesto");
            LoadedTextures.Add(mestoTexture);

            gameController.LoadingOfImagesDone();
        }
    }

    private void LoadImagesFromGallery()
    {
        NativeGallery.Permission permission;
        if (NativeGallery.CanSelectMultipleFilesFromGallery())
        {
            permission = NativeGallery.GetImagesFromGallery((paths) =>
            {
                if (paths != null)
                {
                    foreach (string path in paths)
                    {
                        Texture2D loadedTexture = LoadTextureFromGallery(path);
                        LoadedTextures.Add(loadedTexture);
                    }
                }

                if (LoadedTextures[0])
                {
                    gameController.LoadingOfImagesDone();
                }

            }, title: "Select multiple images", mime: "image/*");
        }
        else
        {
            permission = NativeGallery.GetImageFromGallery((path) =>
            {
                if (path != null)
                {
                    Texture2D loadedTexture = LoadTextureFromGallery(path);
                    LoadedTextures.Add(loadedTexture);
                }

                if (LoadedTextures[0])
                {
                    gameController.LoadingOfImagesDone();
                }

            }, title: "Select single image", mime: "image/*");
        }
    }

    private Texture2D LoadTextureFromGallery(string path)
    {
        Texture2D texture = NativeGallery.LoadImageAtPath(path);
        if (texture == null)
        {
            Debug.Log("Couldn't load texture from " + path);
            return null;
        }

        return texture;
    }
}

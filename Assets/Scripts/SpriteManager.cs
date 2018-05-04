using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEventUtils;

public class SpriteManager : Singleton<SpriteManager>
{
    private readonly string loadSpritePath = Application.dataPath + "/Texture/Sprites/bowler.png";

    private SpriteManager() {}

    Sprite sprite;
    [HideInInspector] public UEvent loadSuc = new UEvent();

    public List<Sprite> LoadsSpritesFromLocalPath()
    {
        List<Sprite> sprites = new List<Sprite>();

        WWW loadWWW = new WWW(loadSpritePath);

        return sprites;
    }

    public IEnumerator LoadSpriteFromLocalPath()
    {
        WWW loadWWW = new WWW(loadSpritePath);
        yield return loadWWW;

        Texture2D texture = loadWWW.texture;
        texture.filterMode = FilterMode.Bilinear;
        sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        if (sprite != null)
        {
            Debug.Log("LoadSpriteFromLocalPath-----------succ");
            loadSuc.Invoke();
        }
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public Sprite TestSprite()
    {
        byte[] bytes = File.ReadAllBytes(loadSpritePath);
        Texture2D texture = new Texture2D(302, 363, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Trilinear;
        texture.LoadImage(bytes);
        sprite = Sprite.Create(texture, new Rect(0, 0, 302, 363), Vector2.zero);
        return sprite;
    }
}

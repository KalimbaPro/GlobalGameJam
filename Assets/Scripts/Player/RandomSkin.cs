using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkin : MonoBehaviour
{
    public static RandomSkin instance;

    [SerializeField]
    UnityEngine.U2D.Animation.SpriteLibrary playerLib;
    public UnityEngine.UI.Image PPHud;

    public UnityEngine.U2D.Animation.SpriteLibraryAsset[] characList;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il n'y a plus d'une instance de RandomSkin dans la scène");
            return;
        }

        instance = this;
    }

    public void RandomizeSkin()
    {
        int index = UnityEngine.Random.Range(0, 4);
        UnityEngine.U2D.Animation.SpriteLibraryAsset res = characList[index];
        
        while(res == playerLib.spriteLibraryAsset)
        {
            index = UnityEngine.Random.Range(0, 4);
            res = characList[index];
        }
        PPHud.sprite = res.GetSprite("PP", "PP");
        PlayerReprod.addFamilyMember(PPHud.sprite);
        playerLib.spriteLibraryAsset = res;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfilePic : MonoBehaviour
{
    [SerializeField]
    UnityEngine.U2D.Animation.SpriteLibrary playerLib;
    public UnityEngine.UI.Image PPHud;
    public Sprite PPsprite;

    void Start()
    {
        PPsprite = CharacSelect.selectedChar.GetSprite("PP", "PP");
        playerLib.spriteLibraryAsset = CharacSelect.selectedChar;
        PPHud.sprite = PPsprite;
        PlayerReprod.addFamilyMember(PPsprite);
    }
}

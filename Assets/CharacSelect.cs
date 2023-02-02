using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacSelect : MonoBehaviour
{
    public UnityEngine.U2D.Animation.SpriteLibrary lib;

    public UnityEngine.U2D.Animation.SpriteLibraryAsset[] characList;
    public static UnityEngine.U2D.Animation.SpriteLibraryAsset selectedChar;
    public UnityEngine.U2D.Animation.SpriteLibraryAsset baseChar;
    private int curr;

    public void Awake()
    {
        selectedChar = characList[0];
    }

    public void next()
    {
        curr = (curr + 1 + characList.Length) % characList.Length;
        selectedChar = characList[curr];
        updateLib();
    }
    public void prev()
    {
        curr = (curr - 1 + characList.Length) % characList.Length;
        selectedChar = characList[curr];
        updateLib();
    }
    private void updateLib()
    {
        lib.spriteLibraryAsset = selectedChar;
    }
}

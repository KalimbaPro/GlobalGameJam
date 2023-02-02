using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReprod : MonoBehaviour
{

    public static PlayerReprod instance;

    public static List<Sprite> familyTree = new List<Sprite>();

    public GameObject player;
    public GameObject parentPanel;

    public Image Header;
    public Image Branch;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il n'y a plus d'une instance de PLayerReprod dans la sc�ne");
            return;
        }
        instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Woman")
            return;
        var type = collision.GetComponent<WomanType>().type;
        player.GetComponent<Buffs>().BuffList[type] = true;
    }

    public static void addFamilyMember(Sprite s)
    {
        familyTree.Add(s);
    }
}

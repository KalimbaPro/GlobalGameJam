using UnityEngine;

[System.Serializable]
public class Dialogue
{

    /*private int index = UnityEngine.Random.Range(0, 16);
    private string[] nameList = { "Laurie", "Laura", "Cassandra", "Barbara", "Dayana", "Camille", "Angelique", "Sage", "Aurore", "Tatiana", "Andrea", "Alexandra", "Jaina", "Ashe", "Orisa" };*/
    public string name;

    [TextArea (3, 10)]
    public string[] sentences;
}

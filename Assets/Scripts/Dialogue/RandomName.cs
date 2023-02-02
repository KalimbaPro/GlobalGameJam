using UnityEngine;

public class RandomName : MonoBehaviour
{
    private string[] nameList = { "Laurie", "Laura", "Cassandra", "Barbara", "Dayana", "Camille", "Angelique", "Sage", "Aurore", "Tatiana", "Andrea", "Alexandra", "Jaina", "Ashe", "Orisa" };

    private string characterName;

    private void Start()
    {
        characterName = getRandomName();
    }
    public string getRandomName()
    {
        int index = UnityEngine.Random.Range(0, 16);
        return nameList[index];
    }
}

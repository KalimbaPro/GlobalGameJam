using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class familyTree : MonoBehaviour
{

    public GameObject frame;
    public GameObject branch;
    public Vector3 startPos;
    public Vector2 offset;
    public Camera cam;

    private Vector3 camStartPos;
    private Vector3 camEndPos;
    private Vector3 translate;
    void Start()
    {
        camStartPos = new Vector3(startPos.x + (offset.x / 2), startPos.y, -10);
        cam.transform.position = camStartPos;
        Instantiate(frame, startPos, Quaternion.identity).GetComponentsInChildren<SpriteRenderer>()[1].sprite = PlayerReprod.familyTree[0];
        for (int i = 1; i < PlayerReprod.familyTree.Count; i+=2) {
            startPos.x += offset.x;
            startPos.z = 0;
            Instantiate(frame, startPos, Quaternion.identity).GetComponentsInChildren<SpriteRenderer>()[1].sprite = PlayerReprod.familyTree[i];
            startPos.x -= offset.x / 2;
            startPos.y -= offset.y / 2;
            startPos.z = 1;
            Instantiate(branch, startPos, Quaternion.identity);
            startPos.y -= offset.y / 2;
            startPos.z = 0;
            Instantiate(frame, startPos, Quaternion.identity).GetComponentsInChildren<SpriteRenderer>()[1].sprite = PlayerReprod.familyTree[i + 1];
        }
        camEndPos = new Vector3(startPos.x + (offset.x / 2), startPos.y, -10);
        translate = (camEndPos - camStartPos).normalized * 0.001f;
    }

    private void Update()
    {
        if (cam.transform.position.x < camEndPos.x && cam.transform.position.y > camEndPos.y)
        {
            cam.transform.Translate(translate);
        }
    }
}

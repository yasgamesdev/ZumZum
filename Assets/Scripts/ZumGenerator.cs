using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumGenerator : MonoBehaviour
{
    public GameObject zumPrefab;
    public Sprite[] zumSprites;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int num = Random.Range(0, zumSprites.Length);

            GameObject zum = Instantiate(zumPrefab, transform);
            zum.GetComponent<Zum>().Init((ZumType)num, zumSprites[num]);
        }
    }
}

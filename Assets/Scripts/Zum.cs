using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zum : MonoBehaviour
{
    public ZumType type { get; private set; }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(ZumType type, Sprite sprite)
    {
        this.type = type;
        GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
    }
}

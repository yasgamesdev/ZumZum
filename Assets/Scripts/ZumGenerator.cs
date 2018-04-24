using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumGenerator : MonoBehaviour
{
    public GameObject zumPrefab;
    public Sprite[] zumSprites;

    public List<GameObject> zumInstances = new List<GameObject>();

    public GameObject insideTop;

    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int zumNum = 50 - zumInstances.Count;

            if(zumNum > 0)
            {
                Generate(zumNum);
            }
        }

        if(Input.GetMouseButton(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitGameObject = hit.transform.gameObject;
                if(hitGameObject.tag == "Zum")
                {
                    Destroy(hitGameObject);
                    zumInstances.Remove(hitGameObject);

                    audioSource.Play();
                }
            }
        }
    }

    public void Generate(int zumNum)
    {
        for (int i = 0; i < zumNum; i++)
        {
            int num = Random.Range(0, zumSprites.Length);

            GameObject zum = Instantiate(zumPrefab, transform);
            zum.transform.localPosition = zum.transform.localPosition + new Vector3(Random.Range(-2.2f, 2.2f), Random.Range(0.0f, 5.4f), 0.0f);
            zum.GetComponent<Zum>().Init((ZumType)num, zumSprites[num]);

            zumInstances.Add(zum);
        }
    }
}

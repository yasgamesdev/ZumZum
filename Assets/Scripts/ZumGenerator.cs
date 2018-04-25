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

    bool isRemoveMode = false;
    ZumType removeZumType;
    List<GameObject> removeZumInstances = new List<GameObject>();

    public GameObject destroyParticlePrefab;

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
            GameObject hitGameObject = GetMousePosZum();

            if(hitGameObject != null)
            {
                Destroy(hitGameObject);
                zumInstances.Remove(hitGameObject);

                audioSource.Play();
            }
        }

        if(Input.GetMouseButtonDown(2) && !isRemoveMode)
        {
            GameObject hitGameObject = GetMousePosZum();

            if (hitGameObject != null)
            {
                isRemoveMode = true;
                removeZumType = hitGameObject.GetComponent<Zum>().type;
                AddRemoveZum(hitGameObject);

                audioSource.Play();
            }
        }
        else if(Input.GetMouseButton(2) && isRemoveMode)
        {
            GameObject hitGameObject = GetMousePosZum();

            if (hitGameObject != null && hitGameObject.GetComponent<Zum>().type == removeZumType)
            {
                if (AddRemoveZum(hitGameObject))
                {
                    audioSource.Play();
                }
            }
        }
        else if(Input.GetMouseButtonUp(2) && isRemoveMode)
        {
            foreach(GameObject zum in removeZumInstances)
            {
                isRemoveMode = false;
                Destroy(zum);
                zumInstances.Remove(zum);

                GameObject particle = Instantiate(destroyParticlePrefab, transform);
                particle.transform.position = zum.transform.position;
            }

            removeZumInstances.Clear();
            audioSource.Play();
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

    GameObject GetMousePosZum()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitGameObject = hit.transform.gameObject;
            if (hitGameObject.tag == "Zum")
            {
                return hitGameObject;
            }
        }

        return null;
    }

    bool AddRemoveZum(GameObject zum)
    {
        if(!removeZumInstances.Contains(zum))
        {
            removeZumInstances.Add(zum);

            return true;
        }
        else
        {
            return false;
        }
    }
}

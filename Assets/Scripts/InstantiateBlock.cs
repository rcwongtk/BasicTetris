using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBlock : MonoBehaviour
{

    public GameObject[] blockPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateNewBlock();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantiateNewBlock()
    {
        Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)], transform.position, transform.rotation);
    }

    

    
}

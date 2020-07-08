using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpawnOnClick : MonoBehaviour
{

    public GameObject[] blockPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)], transform.position, transform.rotation);
        }
    }
}

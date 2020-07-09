using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBlock : MonoBehaviour
{

    public GameObject[] blockPrefabs;
    public Sprite[] displaySprites;
    public GameObject[] displaySquares;
    public List<int> numbers;
    

    // Start is called before the first frame update
    void Start()
    {
        // Initial Population of Numbers List
        for(int i = 0; i < 3; i++)
        {
            numbers.Add(Random.Range(0, blockPrefabs.Length));
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantiateNewBlock()
    {
        // Instantiate based on number from the end of the list
        Instantiate(blockPrefabs[numbers[2]], transform.position, transform.rotation);
        // Delete number at the end of the list
        numbers.RemoveAt(2);
        // Add new number to beginng of list
        numbers.Insert(0, Random.Range(0, blockPrefabs.Length));

        UpdateDisplays();

    }

    public void UpdateDisplays()
    {
        // Display the sprite of the block that will be displayed in the future
        displaySquares[0].GetComponent<SpriteRenderer>().sprite = displaySprites[numbers[0]];
        displaySquares[1].GetComponent<SpriteRenderer>().sprite = displaySprites[numbers[1]];
        displaySquares[2].GetComponent<SpriteRenderer>().sprite = displaySprites[numbers[2]];
    }

    

    
}

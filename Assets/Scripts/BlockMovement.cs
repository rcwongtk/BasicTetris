using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockMovement : MonoBehaviour
{

    public static int height = 20;
    public static int width = 10;
    public Vector3 pivotPoint;
    private bool stopRepeating;
    private static Transform[,] grid = new Transform[width, height];
    float lastStep, timeBetweenSteps = 0.10f;
    public static GameObject playButton;

    private float previousTime;
    public static int scoreCount;
    public static bool gameEnded;

    // Start is called before the first frame update
    void Start()
    {
        // Counter that lowers the block at a consistent rate
        InvokeRepeating("BlockFall", 0.8f, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        // Detect key stroke and move at set intervals
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Timer system to ensure that block doesn't move too fast
            if (Time.time - lastStep > timeBetweenSteps)
            {
                lastStep = Time.time;
                // Move position and check if the block hits the walls. If so, backtrack.
                transform.position += new Vector3(-1, 0, 0);
                if (!ValidMove())
                {
                    transform.position += new Vector3(1, 0, 0);
                }
            }
            
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Time.time - lastStep > timeBetweenSteps)
            {
                lastStep = Time.time;
                transform.position += new Vector3(1, 0, 0);
                if (!ValidMove())
                {
                    transform.position += new Vector3(-1, 0, 0);
                }
            }
            
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Time.time - lastStep > timeBetweenSteps)
            {
                lastStep = Time.time;
                BlockFall();
            }


                
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(pivotPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(pivotPoint), new Vector3(0, 0, 1), -90);
            }
        }
    }

    // Check to see if the position of any of the blocks has left the game grid
    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }

        return true;
    }

    // Just moves the block down one.
    void BlockFall()
    {

        if(stopRepeating == false)
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(0, 1, 0);
                stopRepeating = true;
                GridPopulation();
                CheckForLines();
                if (gameEnded == false)
                {
                    FindObjectOfType<InstantiateBlock>().InstantiateNewBlock();
                }
                else if (gameEnded == true)
                {
                    GameOver();
                }
                this.enabled = false;
                
            }
        }

        
    }

    // Populates the grid with the position of the blocks. If any of the blocks have been found at the top row then game is over.
    void GridPopulation()
    {
        foreach(Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;

            if (roundedY == height-1)
            {
                gameEnded = true;
                GameOver();
            }
        }
    }

    // Check to see if there is a horizonal line filled. If so destroy blocks and shift down one.
    void CheckForLines()
    {
        for(int i = height-1; i >= 0; i--)
        {
            if(HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    // Checks one row, part of the iteration of CheckforLines
    bool HasLine(int i)
    {
        for(int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
            {
                return false;
            }
        }

        return true;
    }

    // Destroys all the gameobjects on the same row, part of CheckForLines
    void DeleteLine(int i)
    {
        for(int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
        scoreCount++;
        GameObject.Find("ScoreDisplay").GetComponent<TMP_Text>().text = "Score " + scoreCount;
    }

    // Shifts everything above the deleted row one down, part of CheckForLines
    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if(grid[j,y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);

                }
            }
        }
    }

    public void GameOver()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            for (int j = 0; j < width; j++)
            {
                if(grid[j, i] == null)
                {

                }
                else
                {
                    Destroy(grid[j, i].gameObject);
                    grid[j, i] = null;
                }  
            }
        }

        scoreCount = 0;
        GameObject.Find("ScoreDisplay").GetComponent<TMP_Text>().text = "Score " + scoreCount;
        playButton = GameObject.FindGameObjectWithTag("PlayButton");
        playButton.transform.GetChild(0).gameObject.SetActive(true);
        
    }

    public void PlayButtonClicked()
    {
        gameEnded = false;
    }

}

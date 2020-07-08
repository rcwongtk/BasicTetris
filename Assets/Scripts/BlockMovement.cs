using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{

    public static int height = 20;
    public static int width = 10;
    public Vector3 pivotPoint;
    private bool stopRepeating;
    private static Transform[,] grid = new Transform[width, height];

    private float previousTime;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("BlockFall", 0.8f, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if(!ValidMove())
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            BlockFall();
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

    void BlockFall()
    {

        if(stopRepeating == false)
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(0, 1, 0);
                FindObjectOfType<InstantiateBlock>().InstantiateNewBlock();
                stopRepeating = true;
                GridPopulation();
                CheckForLines();
                this.enabled = false;
                
            }
        }

        
    }

    void GridPopulation()
    {
        foreach(Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }

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

    void DeleteLine(int i)
    {
        for(int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

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

}

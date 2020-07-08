using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoParent : MonoBehaviour
{

    private float width;
    private float height;
    public static bool contactLeft;
    public static bool contactRight;
    public static bool contactBelow;
    public static string keyPress;
    private GameObject objectClone;
    private bool objectFalling;
    private Vector3 clonePosition;
    private Quaternion cloneRotation;
    public bool childRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    void Awake()
    {
        Invoke("Delay", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

        // Allow the delay to work, so the object can pick up falling velocity
        if(objectFalling == true)
        {

            // Check to see if object stopped falling
            float speed;
            speed = GetComponent<Rigidbody>().velocity.magnitude;
            if (speed < 0.2)
            {
                objectFalling = false;
                foreach (Transform child in transform)
                {
                    // Layer 10 is SittingTetromino
                    child.gameObject.layer = 10;
                }

                Destroy(gameObject.GetComponent<TetrominoParent>());

            }

            // Check for left arrow input
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // Stores the stroke direction you put for the switch that is triggered when the object collides
                keyPress = "Left";

                // Create a clone of the object, and shift it one to the left. If it collides with something then do not allow movement.
                clonePosition = transform.position - transform.right;
                cloneRotation = transform.rotation;
                objectClone = Instantiate(gameObject, clonePosition, cloneRotation);


                if (contactLeft == true)
                {
                    Debug.Log("Contacted Left Wall");
                    // Destroy Clone, so that it does not clog up the system.
                    Destroy(objectClone);
                }
                else
                {
                    // If no collision detected on clone, move the game object one to the left.
                    transform.position -= transform.right;
                    // Destroy Clone, so that it does not clog up the system.
                    Destroy(objectClone);
                }

                // Reset the contact boolean.
                contactLeft = false;



            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {

                keyPress = "Right";

                clonePosition = transform.position + transform.right;
                cloneRotation = transform.rotation;
                objectClone = Instantiate(gameObject, clonePosition, cloneRotation);

                if (contactRight == true)
                {
                    Debug.Log("Contacted Right Wall");
                    Destroy(objectClone);
                }
                else
                {
                    transform.position += transform.right;
                    Destroy(objectClone);
                }

                contactRight = false;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {

                keyPress = "Down";

                clonePosition = transform.position - transform.up;
                cloneRotation = transform.rotation;
                objectClone = Instantiate(gameObject, clonePosition, cloneRotation);

                if (contactBelow == true)
                {
                    Debug.Log("Contacted Floor");
                    Destroy(objectClone);
                }
                else
                {
                    transform.position -= transform.up;
                    Destroy(objectClone);
                }

                contactBelow = false;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                clonePosition = transform.position;
                cloneRotation *= Quaternion.Euler(0, 0, 90);
                objectClone = Instantiate(gameObject, clonePosition, cloneRotation);


                if (contactBelow == true)
                {
                    Debug.Log("Contacted Floor");
                    Destroy(objectClone);
                }
                else
                {
                    transform.position -= transform.up;
                    Destroy(objectClone);
                }
            }

        }

    }

    private void OnTriggerStay(Collider other)
    {

        switch (keyPress)
        {
            case "Left":
                contactLeft = true;
                contactRight = false;
                contactBelow = false;
                break;
            case "Right":
                contactLeft = false;
                contactRight = true;
                contactBelow = false;
                break;
            case "Down":
                contactLeft = false;
                contactRight = false;
                contactBelow = true;
                break;
            default:
                break;

        }

    }

    void Delay()
    {
        objectFalling = true;
    }

    
}

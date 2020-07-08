using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowupBlocks : MonoBehaviour
{

    public bool childRigidBody;
    private Rigidbody rb;
    private bool trigger;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit[] hitsLeft;
        hitsLeft = Physics.RaycastAll(transform.position, -Vector2.right, 5.0f);
        RaycastHit[] hitsRight;
        hitsRight = Physics.RaycastAll(transform.position, Vector2.right, 5.0f);

        int totalHit = hitsLeft.Length + hitsRight.Length;

        if(totalHit == 10)
        {
            for (int hitIndex = 0; hitIndex < hitsLeft.Length; hitIndex++)
            {
                Destroy(hitsLeft[hitIndex].collider.gameObject);
            }
            for (int hitIndex = 0; hitIndex < hitsRight.Length; hitIndex++)
            {
                Destroy(hitsRight[hitIndex].collider.gameObject);
            }
        }
        






        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Item Destroyed");
            Destroy(gameObject);
        }

    }
}

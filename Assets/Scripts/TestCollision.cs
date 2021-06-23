using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision! @ {collision.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger! @ {other.gameObject.name}");
    }


    void Start()
    {
        
    }

    void Update()
    {

        /*      
                ### Raycasting ###  
                Vector3 look = transform.TransformDirection(Vector3.forward);
                Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

                RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10);
                foreach (RaycastHit h in hits)
                {
                    Debug.Log($"Raycasting! @ {h.collider.gameObject.name}");
                }
                ### End of Raycasting ###
        */

        //Debug.Log(Input.mousePosition); // Screen
        //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition)); // Viewport

        // the world coordinate where clicked

        if (Input.GetMouseButtonDown(0)) // when left mouse button was pressed down
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            int MonsterMask = 1 << 8 | 1 << 9;
            LayerMask WallMask = LayerMask.GetMask("Wall"); // possible to point a LayerMask by GetMask solution 

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f, MonsterMask))
            {
                Debug.Log($"Raycasting Camera @ {hit.collider.gameObject.name}");
            }
        }

/*        if (Input.GetMouseButtonDown(0)) // when left mouse button was pressed down
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            Vector3 dir = mousePos - Camera.main.transform.position;
            dir = dir.normalized;

            Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
            {
                Debug.Log($"Raycasting Camera @ {hit.collider.gameObject.name}");
            }
        }
*/
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;

    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.5f, -4.0f); // length of btwn the character and the camera

    [SerializeField]
    GameObject _player = null;


    void Start()
    {
        
    }

    void LateUpdate()
    {
        if(_mode == Define.CameraMode.QuarterView)
        {
            // Adjusting camera view
            RaycastHit hit;
            
            // if hitting wall rather than the character,
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
            {
                // calculate the distance from the hit point where the wall is, to the character.
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f; // 0.8f 위치를 좀 더 당겨주기위해
                // change the pos of cam to (character pos + direction * distance)
                transform.position = _player.transform.position + _delta.normalized * dist + new Vector3(0f, 1f, 0);
            }
            else
            {
                // as the character moves
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }
        }
        
    }

    public void SetQuarterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}

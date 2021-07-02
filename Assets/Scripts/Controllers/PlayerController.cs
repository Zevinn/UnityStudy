using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ## Structure ##
    // GameObject (Player)
    // Transform
    // PlayerController

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 7.0f; // moving speed
    
    //float _rot = 15.0f; // rotation speed

    Vector3 _destPos;

    void Start()
    {
        //Managers.Input.KeyAction -= OnKeyboard; // 备刀秒家, avoid duplicate
        //Managers.Input.KeyAction += OnKeyboard; // 备刀脚没
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    //float Wait_Run_Ratio = 0; // to manipulate blend-tree of the animator
    public enum PlayerState
    {
        Die,
        Move,
        Idle,
    }
    PlayerState state = PlayerState.Idle;

    void UpdateDie()
    {

    }
    void UpdateMove()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            state = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            // set the value of given variable in between 0(min) to dir.magnitude(max)
            transform.position = transform.position + dir.normalized * moveDist;
            // the character's pos = current position + (direction * distance(speed*deltaTime));

            //transform.LookAt(_destPos); the below is making objects move smoother
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }

        //*** Animation ***
        Animator anim = GetComponent<Animator>();
        /* 1)   Wait_Run_Ratio = Mathf.Lerp(Wait_Run_Ratio, 1, 10.0f * Time.deltaTime);
                // set Wait_Run_Ratio close to 1 by 10.0f * Time.deltaTime at each Updadte execution 
                anim.SetFloat("Wait_Run_Ratio", Wait_Run_Ratio);
                anim.Play("WAIT_RUN");*/
        // 2)   Send information of game state
        anim.SetFloat("speed", _speed);
    }
    void UpdateIdle()
    {
        //*** Animation ***
        Animator anim = GetComponent<Animator>();
        /* 1)   Wait_Run_Ratio = Mathf.Lerp(Wait_Run_Ratio, 0, 10.0f * Time.deltaTime);
                anim.SetFloat("Wait_Run_Ratio", Wait_Run_Ratio);
                anim.Play("WAIT_RUN");*/
        anim.SetFloat("speed", 0);
    }


    void Update()
    {
        switch(state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Move:
                UpdateMove();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
        }
    }

    /*void OnKeyboard()
    {
        // Retrieve time difference to make characters/objects move in human-understandable way by deltaTime
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), Time.deltaTime * _rot);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), Time.deltaTime * _rot);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.back * Time.deltaTime * _speed; // This should be the standard as object now faces as intended
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), Time.deltaTime * _rot);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), Time.deltaTime * _rot);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
        _moveToDest = false; // as moving from keyboard commands
    }*/

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (state == PlayerState.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        //int MonsterMask = 1 << 8 | 1 << 9;
        //LayerMask WallMask = LayerMask.GetMask("Wall"); // possible to point a LayerMask by GetMask solution 

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            state = PlayerState.Move;
            //Debug.Log($"Raycasting Camera @ {hit.collider.gameObject.name}");
        }
    }
}

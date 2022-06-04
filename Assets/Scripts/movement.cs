using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    float vertical,horizontal;
    public bool canmove;
    public bool isWalking = false;
       private float xRotation = 0f;
         Vector3 moveVector;
             [SerializeField]private float _mouseSensitivity = 50f;
    [SerializeField]private float _minCameraview = -70f, _maxCameraview = 80f;

    public float _speed =2f;
        CharacterController _charController;
       public Camera _camera;

    // Start is called before the first frame update
    void Start()
    {   canmove=true;
         _charController = GetComponent<CharacterController>();
          Cursor.lockState = CursorLockMode.Locked;
    }
void FixedUpdate()
{   
     
       
}
    // Update is called once per frame
    void Update()
    {           
             if(canmove){   
                vertical = Input.GetAxis("Vertical");
             horizontal = Input.GetAxis("Horizontal");
                Vector3 movement = transform.forward * vertical + transform.right * horizontal; //changed this line.
                  //  Debug.Log(canmove);
               _charController.Move(movement * Time.deltaTime * _speed);
       }
     

             float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity; //changed this line.
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity; //changed this line.
          xRotation += mouseY;
          xRotation = Mathf.Clamp(xRotation, _minCameraview, _maxCameraview);

          _camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
          transform.Rotate(Vector3.up * mouseX * 3);
            
            moveVector = Vector3.zero;
 
         if (_charController.isGrounded == false)
         {
             moveVector += Physics.gravity;
         }
 
       if(Input.GetKeyDown(KeyCode.G)){
            transform.localPosition=new Vector3(3.54f,0.09f,3.7f);
                    if(canmove){
                        canmove=false;
                    
                    }else{
                        canmove=true;
                    }

        }
        //_charController.Move(moveVector * Time.deltaTime);
        if (horizontal != 0.000f || vertical != 0.000f)
        {
            isWalking = true;
        } else
        {
            isWalking = false;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

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

    public PlayableDirector pd;
    public GameObject wdg;
    public GameObject seatPos;
    public bool onseat;

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
            if (!onseat)
            {
                wdg.SetActive(false);

                StartCoroutine(playFadeCutScene(1f));
                StartCoroutine(OnOffSeat(seatPos.transform.position.x, seatPos.transform.position.y, seatPos.transform.position.z));
                
            } else
            {
                StartCoroutine(playFadeCutScene(1f));
                StartCoroutine(OnOffSeat(-26.978f, 0.721f, 5.033f)); 
            }

            if (canmove)
            {
                canmove = false;
            }
            else
            {
                canmove = true;
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

    public IEnumerator playFadeCutScene(float time)
    {
        yield return new WaitForSeconds(time);
        pd.Play();
    }

    IEnumerator OnOffSeat(float x, float y, float z)
    {
        yield return new WaitForSeconds(3.5f);
        //transform.position = new Vector3(seatPos.transform.position.x, seatPos.transform.position.y, seatPos.transform.position.z);
        transform.position = new Vector3(x, y, z);
    }
}

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
    public float timer;
    public float _speed =2f;
        CharacterController _charController;
       public Camera _camera;
       

    public PlayableDirector pd;
    public GameObject wdg;
    public GameObject seatPos;
    public bool onseat;
    public bool TCinstantiated;
    public bool TCSinstantiated;
    public GameObject tableCust;
    public GameObject tableCustSeated;
    
    public GameObject[] walkpositions,lookAtWhileWalk;
    public bool isMove=false;
    public bool movedtoGate=false;
    public bool inposition=false;
    public Animator water_head;
    public NPC npc;
    public GameObject chair;

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
        if(npc.rotateplayer){
            //this.transform.LookAt(chair.transform);

            // if(transform.rotation.y>136f){
            //     npc.rotateplayer=false;
            //     return;
            //  }
            // this.transform.Rotate(new Vector3(0f,137f,0f)*Time.deltaTime);
            // var byAngles=Vector3.up*30f; 
            // var fromAngle = this.transform.rotation;
            // var toAngle = Quaternion.Euler(this.transform.eulerAngles + byAngles);
             

           //this.transform.rotation=Quaternion.Lerp(fromAngle,toAngle,Time.deltaTime );

          
        }
        MoveToPositions(); 
        if(inposition){
            Quaternion currentRotation = transform.rotation;

        // Set the x component of the rotation to zero
        Quaternion targetRotation = Quaternion.Euler(0f*Time.deltaTime, currentRotation.eulerAngles.y, 0f);
        this.transform.rotation=targetRotation;
        inposition=false;

        // Set the new rotation of the GameObject
        }  
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
        
         if(timer == 0f)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                npc.rotateplayer=false;
                if (!onseat)
                {  
                    wdg.SetActive(false);
                    timer = 4.5f;
                    StartCoroutine(playFadeCutScene(1f));
                    StartCoroutine(OnOffSeat(seatPos.transform.position.x, seatPos.transform.position.y, seatPos.transform.position.z));
                    canmove = false;
                }
                else if (onseat)
                {
                    timer = 4.5f;
                    StartCoroutine(playFadeCutScene(1f));
                    StartCoroutine(OnOffSeat(-26.978f, 0.721f, 5.033f));
                    canmove = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.C) && onseat && !TCSinstantiated)
            {
                timer = 3.5f;
                if (!TCinstantiated)
                {
                    StartCoroutine(TCinstantiator());
                    TCinstantiated = true;
                }
                else
                {
                    StartCoroutine(TCdestroyer());
                    TCinstantiated = false;
                }
            }
        }

        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0f)
        {
            timer = 0f;
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
      IEnumerator RotateMe(Vector3 byAngles, float inTime) {
           var fromAngle = transform.rotation;
           var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
           for(var t = 0f; t < 1; t += Time.deltaTime/inTime) {
                transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
                yield return null;
           }
      }
    void MoveToPositions(){
        if(Input.GetKeyDown(KeyCode.Tab)){
            isMove=true;
            
        }
        if(isMove&&!movedtoGate){

            MoveTowardsWaiter(1,lookAtWhileWalk[1]);
             
            
        }
        if(movedtoGate&&isMove)
            MoveTowardsWaiter(0,lookAtWhileWalk[0]);
       
    }
    void   MoveTowardsWaiter(int i,GameObject obj){
        var offset =walkpositions[i].transform.position-this.transform.position;
        Debug.Log(offset.magnitude);
        if(offset.magnitude<=0.5f){
            

            isMove=false;
            if(i==1){
                movedtoGate=true;
                isMove=true;
            }
            if(i==0){
                
                 inposition=true;
                movedtoGate=false; 
               

                
            }
            
        }
        offset=offset.normalized*_speed;
                this.transform.LookAt(obj.transform);

        _charController.Move(offset*Time.deltaTime);
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

    public IEnumerator TCinstantiator (){
        StartCoroutine(playFadeCutScene(0f));
        yield return new WaitForSeconds(2.5f);
        tableCust.SetActive(true);
    }
    public IEnumerator TCdestroyer()
    {
        StartCoroutine(playFadeCutScene(0f));
        yield return new WaitForSeconds(2.5f);
        wdg.SetActive(false);
        tableCust.SetActive(false);
    }

    public IEnumerator TCSintantiator()
    {
        StartCoroutine(playFadeCutScene(0f));
        yield return new WaitForSeconds(2.5f);
        tableCust.SetActive(false);
        tableCustSeated.SetActive(true);
    }

    public IEnumerator TCSdestroyer()
    {
        StartCoroutine(playFadeCutScene(0f));
        yield return new WaitForSeconds(2.5f);
        tableCustSeated.SetActive(false);
    }
}

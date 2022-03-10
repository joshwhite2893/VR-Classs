using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playSphere;
    public GameObject playPlatform;
    public GameObject weight;
    public float speed = 150f;
    public bool isBall { get; set; }
    public bool isCombo { get; set; }
    public Rigidbody sphereRigid;
    private Vector3 lastPos;
    private bool weightMoving;

    private Vector3 startingSpherePos;
    private Vector3 startingPlatformPos;
    private Vector3 startingWeightPos;
    
    // Start is called before the first frame update
    void Start()
    {
        weightMoving = false;
        isBall = false;
        isCombo = false;
        sphereRigid = playSphere.GetComponent<Rigidbody>();
        startingSpherePos = playSphere.transform.position;
        startingPlatformPos = playPlatform.transform.position;
        startingWeightPos = weight.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (isBall && !isCombo)
        {
            // to avoid parenting issues, the camera will track an empty GO, corresponding to sphere location
            lastPos = playSphere.transform.position;
            UpdateSphere();
            Vector3 updateParent = new Vector3(lastPos.x, lastPos.y, transform.position.z);
            transform.position = updateParent;
        } else if(isCombo && !isBall)
        {
            // same as above, except this condition accounts for when the cube & sphere morph
            lastPos = playPlatform.transform.position;
            UpdateCombo();
            Vector3 updateParent = new Vector3(lastPos.x, lastPos.y, transform.position.z);
            transform.position = updateParent;
        }

        TouchUpdate();
        
    }

    public void UpdateSphere()
    {
        
        // init zero vec
        Vector3 dirVec = Vector3.zero;
        //Vector3 gyroVec = Vector3.zero;
        //landscape right, mapped to XZ according to documentation
        //gyroVec = Input.gyro.rotationRateUnbiased;
        dirVec = Input.acceleration;
        dirVec.x = Input.acceleration.x;

        Vector3 move = dirVec * speed * Time.deltaTime;

        sphereRigid.AddForce((dirVec * speed * Time.deltaTime)/.5f);
        
    }

    public void UpdateCombo()
    {
        Vector3 dirVec = Vector3.zero;
        //dirVec = Input.acceleration;
        dirVec.x = Input.acceleration.x;
        Vector3 move = dirVec * (speed / 4) * Time.deltaTime;
        playPlatform.transform.Translate(move);
    }

    public void CannonEngaged()
    {
        Vector3 cannonF = new Vector3(400f, 0f);
        sphereRigid.AddForce(cannonF);
    }

    public void TouchUpdate()
    {
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {

                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                  
                if (Physics.Raycast(ray, out hit, 25f))
                {
                    if (hit.collider.gameObject.CompareTag("weight"))
                    {
                        if (!weightMoving)
                        {
                            StartCoroutine(RollWeightOnce());
                        }
                    }
                }
                    
            }
        }
    }


    public IEnumerator RollWeightOnce()
    {
        weightMoving = true;
        float remainAngle = 90;
        //determine center of rotation & axis - middle of bottom-right side
        Vector3 rotCenter = weight.transform.position + Vector3.right / 2 + Vector3.down / 2;
        Vector3 rotAxis = Vector3.back;

        while(remainAngle > 0 && weightMoving)
        {
            float rotAngle = Mathf.Min(300f * Time.deltaTime, remainAngle);
            weight.transform.RotateAround(rotCenter, rotAxis, rotAngle);
            remainAngle -= rotAngle;
            yield return null;
        }

        weightMoving = false;
    }

    public void CombinePlayer()
    {
        sphereRigid.velocity = Vector3.zero;
        sphereRigid.angularVelocity = Vector3.zero;
        sphereRigid.constraints = RigidbodyConstraints.FreezeAll;
        isBall = false;
        isCombo = true;
        playSphere.transform.SetParent(playPlatform.transform);
        playSphere.transform.localPosition = new Vector3(0, 1f, 0);
        
    }

    public void RestartGame()
    {
        weightMoving = false;
        isBall = true;
        isCombo = false;
        //change sphere to have no parent
        playSphere.transform.parent = null;
        sphereRigid.constraints = RigidbodyConstraints.FreezePositionZ;
        playSphere.transform.position = startingSpherePos;
        playPlatform.transform.position = startingPlatformPos;
        weight.transform.position = startingWeightPos;
    }

}

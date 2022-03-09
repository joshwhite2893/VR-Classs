using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playSphere;
    public GameObject playPlatform;
    public float speed = 150f;
    public bool isBall { get; set; }
    public bool isCombo { get; set; }
    private Rigidbody sphereRigid;
    private Vector3 lastPos;
    
    // Start is called before the first frame update
    void Start()
    {
        isBall = false;
        isCombo = false;
        sphereRigid = playSphere.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isBall && !isCombo)
        {
            lastPos = playSphere.transform.position;
            UpdateSphere();
            Vector3 updateParent = new Vector3(lastPos.x, lastPos.y, transform.position.z);
            transform.position = updateParent;
        } else if(isCombo && !isBall)
        {
            //update combo of box & ball, will be a translation
        }
        
    }

    public void UpdateSphere()
    {
        
        // init zero vec
        Vector3 dirVec = Vector3.zero;
        Vector3 gyroVec = Vector3.zero;
        //landscape right, mapped to XZ according to documentation
        gyroVec = Input.gyro.rotationRateUnbiased;
        dirVec = Input.acceleration;
        //print("Gyro: " + gyroVec.ToString());
        //print("Accl: " + dirVec.ToString());
        //float xRot = Input.gyro.rotationRateUnbiased.y;
        dirVec.x = Input.acceleration.x;
        //dirVec.z = Input.acceleration.x;

        //Normalize unit sphere. Can add velocity scaling here.
        /*if (dirVec.sqrMagnitude > 1)
        {
            dirVec.Normalize();
        }*/

        Vector3 move = dirVec * speed * Time.deltaTime;

        sphereRigid.AddForce((dirVec * speed * Time.deltaTime)/.5f);
        
    }
}

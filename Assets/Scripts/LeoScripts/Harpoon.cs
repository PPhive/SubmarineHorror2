using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    public GameObject Rope;
    public GameObject Child;
    public Rigidbody RB;
    public SpringJoint MyJoint;
    public float Force;

    public bool Released;

    private EnemyBaby TargetHit;


    [Space]
    public float AnchorShiftX = 0.25f;
    public float AnchorShiftY = -0.25f;
    public float AnchorShiftZ = 1.2f;

    

    void Awake() 
    {
        AnchorShiftX = 0.25f;
        AnchorShiftY = -0.25f;
        AnchorShiftZ = 1.2f;
        HarpoonGun.instance.Harpoon = this.gameObject;
        MyJoint.connectedBody = CallPlayerRigidBody.instance.RB;
        MyJoint.connectedAnchor = new Vector3(AnchorShiftX, AnchorShiftY, AnchorShiftZ);
    }
    void Start()
    {
        Released = false;
        transform.position = HarpoonGun.instance.gameObject.transform.position;
        transform.rotation = HarpoonGun.instance.gameObject.transform.rotation;
        RB.AddForce(transform.forward*Force);
        MyJoint.connectedAnchor = new Vector3(AnchorShiftX, AnchorShiftY, AnchorShiftZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            Released = true;
            RB.constraints = RigidbodyConstraints.None;
        }

        if (Released && Vector3.Distance(transform.position, HarpoonGun.instance.transform.position) < 0.7f)
        {
            HarpoonGun.instance.DeleteHarpoon();
        }

        // Rope.transform.position = HarpoonGun.instance.gameObject.transform.position;
        if (Input.GetMouseButton(1) || Released)
        {
            MyJoint.maxDistance = 0f;
            Debug.Log("Retracting");
            HarpoonGun.instance.ReelInSound();
        }
        else
        {
            MyJoint.maxDistance = 25f;
            HarpoonGun.instance.StopReelInSound();
        }

        MyJoint.connectedAnchor = new Vector3(AnchorShiftX, AnchorShiftY, AnchorShiftZ);

        

    }

    private void FixedUpdate()
    {
        if (RB.constraints == RigidbodyConstraints.FreezeAll)
        {
            MyJoint.spring = 300;
            //MyJoint.connectedBody = CallPlayerRigidBody.instance.RB;
        }
        else
        {
            MyJoint.spring = 2;
        }
    }

    void Delete() //This might not be used
    {
        Child.transform.parent = null;
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") 
        {
            other.GetComponent<EnemyBaby>().enabled = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            //TargetHit.enabled = false;
            other.transform.parent = transform;
            other.transform.localPosition = new Vector3(0, 0, Random.Range(1.5f, 2.3f));
            HarpoonGun.instance.HitSound();
        }

        if (other.tag == "Wood") 
        {
            RB.constraints = RigidbodyConstraints.FreezeAll;
            HarpoonGun.instance.HitSound();
        }
    }

}

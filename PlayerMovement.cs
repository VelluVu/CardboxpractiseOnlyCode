using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class PlayerMovement : MonoBehaviour {

    public bool moveForward;
    public float moveSpeed;
    public GameObject dump;
    public float forceOfArse;
    public float explosionRadius;
    public float lookToWalkAngle;
    public GameObject wc;
    Animator wcLid;
    public GameObject scoreCounter;
    public Camera cam;
    public Transform arse;
    public bool released;
    public bool releasing;
    public bool maxForce;
    public float addedForce;
    bool move;

    private void Start()
    {
        wcLid = wc.GetComponent<Animator>();
        
    }

    void Update () {

        TakeADumpIrl();
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Escape))
        {       
            Application.Quit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == wc.GetComponent<SphereCollider>())
        {
            wcLid.SetBool("Open", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other == wc.GetComponent<SphereCollider>())
        {
            wcLid.SetBool("Open", false);
        }
    }

    public void MovePlayer()
    {
        
        if (cam.transform.eulerAngles.x > lookToWalkAngle && cam.transform.eulerAngles.x < 90f)
        {
            moveForward = true;
        } else
        {
            moveForward = false;
        }
        if (moveForward && !releasing)
        {
            Vector3 forward = cam.transform.TransformDirection(Vector3.forward);
            transform.position += new Vector3(forward.x, 0, forward.z) * moveSpeed * Time.deltaTime ;
            Debug.Log(forward);
        }

    }

    public void TakeADumpIrl()
    {
        
        if (Input.GetMouseButton(0) && !released)
        {
            releasing = true;

            if (forceOfArse >= 20)
                maxForce = true;
            if (forceOfArse <= 0.1f)
                maxForce = false;
            if (!maxForce)
            {
                forceOfArse += addedForce * Time.deltaTime;
            }
            else
            {
                forceOfArse -= addedForce * Time.deltaTime;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            releasing = false;
            released = true;
            gameObject.GetComponent<AudioSource>().Play();
            GameObject piece = Instantiate(dump, arse.position, Quaternion.identity) as GameObject;
            piece.AddComponent<Rigidbody>().AddForce(arse.forward * forceOfArse, ForceMode.Impulse);
            piece.GetComponent<Rigidbody>().mass = 0.001f;
            piece.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            piece.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;


            Destroy(piece, 15f);
            forceOfArse = 1;
            StartCoroutine(Reload());
            
        }
 
    }
    IEnumerator Reload()
    {

        yield return new WaitForSeconds(1f);
        released = false;

    }
}

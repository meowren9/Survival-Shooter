using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    Vector3 movement;
    Animator anim;
    Rigidbody rb;
    int floorMask;
    float camRayLength = 100f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        floorMask = LayerMask.GetMask("Floor");
        rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
  
    }

    private void Move(float h,float v)
    {
        
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if(Physics.Raycast(camRay,out floorHit, camRayLength, floorMask))
        {
            Debug.Log("turn");
            Vector3 playertoMouse = floorHit.point - transform.position;
            playertoMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playertoMouse);
            rb.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
       
        anim.SetBool("IsWalking", walking);
    }

}

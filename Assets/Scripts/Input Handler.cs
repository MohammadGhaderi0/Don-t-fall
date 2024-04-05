using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float RotationSpeed;
    
    public float speed = 15;
    
    Rigidbody playerRB;

    public GameObject FocalPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        FocalPoint.transform.Rotate(Vector3.up, horizontalInput * RotationSpeed * Time.deltaTime);
        
        float forwardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(FocalPoint.transform.forward * (speed * forwardInput));
    }
}

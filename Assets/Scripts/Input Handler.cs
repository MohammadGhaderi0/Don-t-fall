using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float RotationSpeed;
    
    public float speed = 15;
    
    Rigidbody playerRB;

    public GameObject FocalPoint;

    public Pause pause;


    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        if (PlayerPrefs.HasKey("sensitivity"))
        {
            RotationSpeed = 40 + PlayerPrefs.GetFloat("sensitivity");
        }
        else
        {
            RotationSpeed = 90; 
        }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        FocalPoint.transform.Rotate(Vector3.up, horizontalInput * RotationSpeed * Time.deltaTime);
        
        float forwardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(FocalPoint.transform.forward * (speed * forwardInput));

        if (Input.GetKeyDown(KeyCode.Escape) && !pause.paused)
        {
            pause.PauseAndUnpause();
        }
    }
}
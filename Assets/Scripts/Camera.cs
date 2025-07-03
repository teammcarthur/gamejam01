using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    public float smoothMult, lookMult;
    public GameObject player;
    Vector3 targetPos, smoothedPos, offset;
    Vector2 inputNormal;
    int inputX, inputY;
    void Update()
    {
        GetDirection();
    }

    void GetDirection()
    {
        inputX = (Keyboard.current.rightArrowKey.isPressed ? 1 : 0) + (Keyboard.current.leftArrowKey.isPressed ? -1 : 0);
        inputY = (Keyboard.current.upArrowKey.isPressed? 1 : 0) + (Keyboard.current.downArrowKey.isPressed? -1 : 0);
    }

    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        inputNormal = new Vector2(inputX, inputY).normalized;
        offset = new Vector3(inputNormal.x, inputNormal.y, 0) * lookMult;
        targetPos = player.transform.position + offset;
        smoothedPos = Vector3.Lerp(transform.position, targetPos, smoothMult);
        transform.position = smoothedPos + new Vector3 (0, 0, -10);
    }
}

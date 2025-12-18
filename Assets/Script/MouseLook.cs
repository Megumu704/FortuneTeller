using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // 透過 Inspector 設定滑鼠靈敏度
    public float mouseSensitivity = 300f; 

    // 新增：用於接收水平旋轉的父物件
    // 如果此欄位為空，攝影機將獨立旋轉 (目前的單獨模式)
    public Transform playerBody; 

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 1. 獲取滑鼠輸入
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // 注意：這裡使用 "Mouse Y"

        // 2. 處理垂直旋轉 (仰角 - 上下看)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 垂直旋轉應用於攝影機 (子物件)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 3. 處理水平旋轉 (左右看)
        if (playerBody != null)
        {
            // 如果指定了父物件，水平旋轉應用於父物件 (讓整個身體轉向)
            playerBody.Rotate(Vector3.up * mouseX);
        }
        else
        {
            // 如果沒有指定父物件，水平旋轉應用於攝影機本身 (目前的單獨模式)
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}
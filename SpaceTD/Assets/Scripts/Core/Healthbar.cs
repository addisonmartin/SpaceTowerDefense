using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour {

    private Texture2D greenTexture;
    private Texture2D redTexture;
    private GUIStyle redStyle;
    private GUIStyle greenStyle;
    public Vector3 size = new Vector2(16, 2);
    public float yPos = 2;

    private float health = 100f;
    private float maxHealth = 100f;

    // Start is called before the first frame update
    void Start() {
        greenTexture = new Texture2D(1, 1);
        redTexture = new Texture2D(1, 1);

        greenStyle = new GUIStyle();
        redStyle = new GUIStyle();

        greenTexture.SetPixel(0, 0, Color.green);
        redTexture.SetPixel(0, 0, Color.red);

        greenTexture.wrapMode = TextureWrapMode.Repeat;
        redTexture.wrapMode = TextureWrapMode.Repeat;

        greenTexture.Apply();
        redTexture.Apply();

        redStyle.normal.background = redTexture;
        greenStyle.normal.background = greenTexture;

        //scale healthbar to camera
        size = new Vector2(size.x * Camera.allCameras[0].pixelWidth/CameraController.TARGET_WIDTH, size.y * Camera.allCameras[0].pixelHeight / CameraController.TARGET_HEIGHT);

        //size = -(new Vector2(TARGET_SIZE * CAM_ASPECT, CameraCon))

        //size = Camera.allCameras[0].WorldToScreenPoint(-((new Vector2(TARGET_SIZE * CAM_ASPECT, Camera.allCameras[0].orthographicSize)) + size);

        //transform.localPosition = new Vector3(0, 0);
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, -transform.parent.rotation.z));
    }

    private void OnGUI() {
        Vector2 pos = Camera.allCameras[0].WorldToScreenPoint(transform.position + new Vector3(0, yPos));
        GUI.Label(new Rect(new Vector2(pos.x - size.x / 2, Camera.allCameras[0].pixelHeight - pos.y), size),
            redTexture, redStyle);
        GUI.Label(new Rect(new Vector2(pos.x - size.x / 2, Camera.allCameras[0].pixelHeight - pos.y), new Vector2((health / maxHealth) * size.x, size.y)), greenTexture, greenStyle);
    }

    public void setHealth(float health) {
        this.health = Mathf.Max(health, 0f);
    }
}

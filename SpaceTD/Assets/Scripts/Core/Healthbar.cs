using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cullen
public class Healthbar : MonoBehaviour {

    //Cullen
    private Texture2D greenTexture;
    private Texture2D redTexture;
    private GUIStyle redStyle;
    private GUIStyle greenStyle;
    public Vector3 size = new Vector2(16, 2);
    public float yPos = 2;

    private float health = 100f;
    private float maxHealth = 100f;

    //Cullen
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

        if (Core.mainCam == null) {
           Core.mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        //scale healthbar to camera
        if(Core.mainCam == null) {
            Core.mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        size = new Vector2(size.x * Core.mainCam.pixelWidth/CameraController.TARGET_WIDTH, size.y * Core.mainCam.pixelHeight / CameraController.TARGET_HEIGHT);
    }

    //Cullen
    private void OnGUI() {
        //Cullen
        Vector2 pos = Core.mainCam.WorldToScreenPoint(transform.position + new Vector3(0, yPos));
        if (Core.mainCam.pixelRect.Contains(pos)) {
            GUI.Label(new Rect(new Vector2(pos.x - size.x / 2, Core.mainCam.pixelHeight - pos.y), size),
            redTexture, redStyle);
            GUI.Label(new Rect(new Vector2(pos.x - size.x / 2, Core.mainCam.pixelHeight - pos.y), new Vector2((health / maxHealth) * size.x, size.y)), greenTexture, greenStyle);
        }
    }

    //Cullen
    public void setHealth(float health) {
        this.health = Mathf.Max(health, 0f);
    }
}

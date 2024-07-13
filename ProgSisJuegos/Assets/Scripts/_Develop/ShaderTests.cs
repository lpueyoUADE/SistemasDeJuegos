using UnityEngine;

public class ShaderTests : MonoBehaviour
{
    [Header("Camera settings")]
    public Camera cameraObject;
    public Vector3 cameraDistanceOffset = new Vector3(0, 0, -10);

    [Header("Settings")]
    public Color shipHighIntegrity = Color.green;
    public Color shipMediumIntegrity = Color.yellow;
    public Color shipLowIntegrity = Color.red;

    [Header("Set references")]
    public GameObject shipShield;

    [Header("Results")]
    public Material shipShader;

    private void Start()
    {
        shipShader = shipShield.GetComponent<Renderer>().material;
    }

    public void ShipShieldSliderChange(float amount)
    {
        cameraObject.transform.position = shipShield.transform.position + cameraDistanceOffset;

        if (amount > 0.75f)
        {
            shipShader.SetColor("_ShipIntegrityColor", shipHighIntegrity);
            return;
        }

        if (amount > 0.35f)
        {
            shipShader.SetColor("_ShipIntegrityColor", shipMediumIntegrity);
            return;
        }

        shipShader.SetColor("_ShipIntegrityColor", shipLowIntegrity);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FlameColorChanger : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public Light flameLight;
    public GameObject flame;
    public VisualEffect cartoonFlameVFX; // Reference to the Visual Effects component

    public Color initialColor = new Color(0.5f, 0.0f, 0.5f); // Purple color
    public Color menacingRedColor = new Color(1.0f, 0.0f, 1.0f); // Adjusted menacing red color
    public float detectionDistance = 5.0f; // Adjust this distance as needed

    void Start()
    {
        // Assuming the Visual Effects component is attached to the same GameObject
        cartoonFlameVFX = flame.GetComponent<VisualEffect>();
    }

    void Update()
    {
        // Check if an enemy is present and within the detection distance
        if (enemy != null && Vector3.Distance(enemy.transform.position, transform.position) < detectionDistance && enemy.activeInHierarchy)
        {
            // Calculate the distance in the x-z plane using Pythagorean theorem
            float distanceXZ = Mathf.Sqrt(Mathf.Pow(enemy.transform.position.x - player.transform.position.x, 2) +
                                          Mathf.Pow(enemy.transform.position.z - player.transform.position.z, 2));

            // Map the distance to the color difference between purple and red
            float colorFactor = (30.0f - distanceXZ)/28.0f; // 20 is the maximum distance in x and z coordinates

            // Calculate the target color based on the color factor
            Color targetColor = Color.Lerp(initialColor, menacingRedColor, colorFactor);

            // Smoothly transition the flame light color
            flameLight.color = Color.Lerp(flameLight.color, targetColor, Time.deltaTime);

            // Smoothly transition the Visual Effects component's HDR color property
            if (cartoonFlameVFX != null)
            {
                cartoonFlameVFX.SetVector4("main color", Vector4.Lerp(cartoonFlameVFX.GetVector4("main color"), new Vector4(targetColor.r, targetColor.g, targetColor.b, targetColor.a), Time.deltaTime));
            }

            // You can also apply the color to other components or materials as needed
        }
        else
        {
            // Reset to the initial color when no enemy is present or out of detection distance
            flameLight.color = Color.Lerp(flameLight.color, initialColor, Time.deltaTime);

            // Smoothly transition the Visual Effects component's HDR color property back to the initial color
            if (cartoonFlameVFX != null)
            {
                cartoonFlameVFX.SetVector4("main color", Vector4.Lerp(cartoonFlameVFX.GetVector4("main color"), new Vector4(initialColor.r, initialColor.g, initialColor.b, initialColor.a), Time.deltaTime));
            }
        }
    }
}

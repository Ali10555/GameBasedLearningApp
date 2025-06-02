using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PendulumVisuals : MonoBehaviour
{
    public PendulumSwing pendulumSwing;
    public Transform stringVisuals;
    public Transform endVisuals;

    [Header("UI")]
    public Slider lenghtSetSlider;
    public TextMeshProUGUI lenghtText;

    public Slider gravitySetSlider;
    public TextMeshProUGUI gravityText;

    public Slider angleSetSlider;
    public TextMeshProUGUI angleText;

    public TextMeshProUGUI timePeriodText;
    void Start()
    {
        lenghtSetSlider.value = pendulumSwing.length;
        gravitySetSlider.value = pendulumSwing.gravity;
        angleSetSlider.value = pendulumSwing.maxAngle;
    }

    // Update is called once per frame
    void Update()
    {
        stringVisuals.localScale = new Vector3(1, pendulumSwing.length, 1);
        endVisuals.transform.localPosition = -Vector3.up * pendulumSwing.length;

        timePeriodText.text = "Time Period: " + pendulumSwing.GetTimePeriod().ToString("f1") + "s";

        pendulumSwing.length = lenghtSetSlider.value;
        lenghtText.text = lenghtSetSlider.value.ToString("f1") + "m";

        pendulumSwing.gravity = gravitySetSlider.value;
        gravityText.text = gravitySetSlider.value.ToString("f1") + "g";

        pendulumSwing.maxAngle = angleSetSlider.value;
        angleText.text = angleSetSlider.value.ToString("f1") + "°";
    }


}

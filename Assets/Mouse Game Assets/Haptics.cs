using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Haptics : MonoBehaviour
{
    public XRBaseController leftController, rightController;
    public float defaultAmplitude = 1f;
    public float defaultDuration = 1f;
	public LineRenderer lineRenderer;

    private void Start()
    {
        leftController = GetComponent<XRController>();
        rightController = GetComponent<XRController>();
	lineRenderer = GetComponent<LineRenderer>();
    }

    //public void OnHoverEntered(HoverEnterEventArgs object)
    //{
      //  if (object.gameObject.CompareTag("Coin"))
        //{
          //  SendHaptics();
        //}
    //}

public void DisableHandCollider()
{
	foreach (var item in lineRenderer)
	{
		//item.enabled = false;
	}
}


    public void SendHaptics()
    {
        leftController.SendHapticImpulse(defaultAmplitude, defaultDuration);
        rightController.SendHapticImpulse(defaultAmplitude, defaultDuration);
    }
}
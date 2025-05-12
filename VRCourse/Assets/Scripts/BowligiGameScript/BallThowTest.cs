using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class VRBallThrow : MonoBehaviour
{
    public float throwForceMultiplier = 1f; 
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private AudioSource audioSource;

    private bool wasThrown = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        audioSource = GetComponent<AudioSource>();

        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        
        rb.velocity = args.interactorObject.GetAttachTransform(grabInteractable).forward * rb.velocity.magnitude * throwForceMultiplier;

        if (audioSource && !audioSource.isPlaying)
        {
            audioSource.Play();
            wasThrown = true;
        }
    }

    void Update()
    {
        if (wasThrown && rb.velocity.magnitude < 0.05f)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();

            wasThrown = false;
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
            grabInteractable.selectExited.RemoveListener(OnReleased);
    }
}

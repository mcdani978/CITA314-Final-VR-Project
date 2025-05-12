using System.Collections;
using UnityEngine;
using UnityEngine.XR.Management;

public class XRInitializer : MonoBehaviour
{
    private IEnumerator Start()
    {
        XRGeneralSettings xrSettings = XRGeneralSettings.Instance;

        if (xrSettings != null && xrSettings.Manager != null)
        {
            yield return xrSettings.Manager.InitializeLoader();

            if (xrSettings.Manager.activeLoader != null)
            {
                xrSettings.Manager.StartSubsystems();
            }
        }
    }

    private void OnDestroy()
    {
        XRGeneralSettings xrSettings = XRGeneralSettings.Instance;

        if (xrSettings != null && xrSettings.Manager != null)
        {
            xrSettings.Manager.StopSubsystems();
            xrSettings.Manager.DeinitializeLoader();
        }
    }
}

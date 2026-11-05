using UnityEngine;

public class SideBySideStereoSetup : MonoBehaviour
{
    public Camera leftEye;
    public Camera rightEye;

    [Tooltip("Interpupillary distance in meters")]
    public float ipd = 0.064f;

    void Start()
    {
        if (leftEye == null || rightEye == null)
        {
            Debug.LogError("Assign both leftEye and rightEye cameras.");
            return;
        }

        // Position eyes relative to the rig center
        //leftEye.transform.localPosition = new Vector3(-ipd * 0.5f, 0f, 0f);
        //rightEye.transform.localPosition = new Vector3(ipd * 0.5f, 0f, 0f);

        leftEye.transform.localRotation = Quaternion.identity;
        rightEye.transform.localRotation = Quaternion.identity;

        // Render side-by-side on the same display
        leftEye.rect = new Rect(0f, 0f, 0.5f, 1f);
        rightEye.rect = new Rect(0.5f, 0f, 0.5f, 1f);

        // Make sure both cameras match most settings
        CopySettings(leftEye, rightEye);
    }

    void CopySettings(Camera source, Camera target)
    {
        target.clearFlags = source.clearFlags;
        target.backgroundColor = source.backgroundColor;
        target.fieldOfView = source.fieldOfView;
        target.nearClipPlane = source.nearClipPlane;
        target.farClipPlane = source.farClipPlane;
        target.cullingMask = source.cullingMask;
        target.renderingPath = source.renderingPath;
        target.allowHDR = source.allowHDR;
        target.allowMSAA = source.allowMSAA;
    }
}
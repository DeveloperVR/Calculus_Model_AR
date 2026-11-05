using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultipleImagesTrackingManager : MonoBehaviour
{

    [SerializeField]
    List<GameObject> prefabsToSpawn = new List<GameObject>();

    private ARTrackedImageManager _trackedImageManager;

    private Dictionary<string, GameObject> _arObjects;

    private void Start()
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>();
        if(_trackedImageManager == null) return;
        _arObjects = new Dictionary<string, GameObject>();
        _trackedImageManager.trackablesChanged.AddListener(OnImagesTrackedChanged);
        SetUpElements();

    }

    private void ODestroy()
    {
        _trackedImageManager.trackablesChanged.RemoveListener(OnImagesTrackedChanged);
    }

    private void SetUpElements()
    {
        foreach(var prefab in prefabsToSpawn)
        {
            var arObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            arObject.name = prefab.name;
            arObject.gameObject.SetActive(false);
            _arObjects.Add(arObject.name, arObject);
        }
    }

    private void OnImagesTrackedChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach(var trackedImage in eventArgs.added)
        {
            UpdateTrackedImages(trackedImage);
        }

        foreach(var trackedImage in eventArgs.updated)
        {
            UpdateTrackedImages(trackedImage);
        }

        foreach(var trackedImage in eventArgs.removed)
        {
            UpdateTrackedImages(trackedImage.Value);
        }
    }

    private void UpdateTrackedImages(ARTrackedImage trackedImage)
    {
        if(trackedImage==null) return;
        if (trackedImage.trackingState is TrackingState.Limited or TrackingState.None)
        {
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
            return;
        }
        _arObjects[trackedImage.referenceImage.name].SetActive(true);
        _arObjects[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;
        _arObjects[trackedImage.referenceImage.name].transform.rotation = trackedImage.transform.rotation;
    }
}

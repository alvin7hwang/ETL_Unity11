using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackedImageHandler : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager _arTrackedImageManager;

    private void OnEnable()
    {
        _arTrackedImageManager.trackablesChanged.AddListener(OnTrackablesChanged);
    }

    private void OnDisable()
    {
        _arTrackedImageManager.trackablesChanged.RemoveListener(OnTrackablesChanged);
    }

    private void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        // ���� �߰��� image
        foreach (var item in args.added)
        {

        }

        // ���ŵ� image
        foreach (var item in args.updated)
        {

        }

        // ���ŵ� image
        foreach(var item in args.removed)
        {

        }
    }
}

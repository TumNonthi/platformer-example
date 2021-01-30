using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class BulletTracer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _tracerRenderer = null;
        [SerializeField] private float _tracerDuration = 0.02f;

        public void CreateTracer(Vector3 fromPosition, Vector3 targetPosition)
        {
            CoroutineUtils.RunCoroutineOnInstance(CreateTracerCoroutine(fromPosition, targetPosition));
        }

        IEnumerator CreateTracerCoroutine(Vector3 fromPosition, Vector3 targetPosition)
        {
            if (_tracerRenderer != null)
            {
                _tracerRenderer.transform.parent = null;

                _tracerRenderer.useWorldSpace = true;
                _tracerRenderer.SetPosition(0, fromPosition);
                _tracerRenderer.SetPosition(1, targetPosition);

                _tracerRenderer.enabled = true;

                yield return new WaitForSeconds(_tracerDuration);

                _tracerRenderer.enabled = false;
            }
        }
    }
}

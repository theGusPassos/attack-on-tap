using UnityEngine;

namespace AttackOnTap.Camera
{
    public class PixelPerfectCamera : MonoBehaviour
    {
        public int pixelsPerUnit = 6;

        private UnityEngine.Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<UnityEngine.Camera>();

            _camera.orthographicSize = Screen.height / 2f /pixelsPerUnit;
            print(_camera.orthographicSize );
        }
    }
}
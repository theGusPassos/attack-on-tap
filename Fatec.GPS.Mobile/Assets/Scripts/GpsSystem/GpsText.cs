using UnityEngine;
using UnityEngine.UI;

namespace GpsSystem
{
    public class GpsText : MonoBehaviour
    {
        public Text coordinates;

        private void Update()
        {
            coordinates.text = 
                "Lat: " + Gps.Instance.latitute.ToString() + 
                "Lon: " + Gps.Instance.longitute.ToString();
        }
    }
}
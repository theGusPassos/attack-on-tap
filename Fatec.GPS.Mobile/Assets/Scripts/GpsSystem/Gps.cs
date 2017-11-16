using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GpsSystem
{
    public class Gps : MonoBehaviour
    {
        public static Gps Instance { get; set; }

        public float latitute;
        public float longitute;

        private GameObject debugTextObject;
        private Text textComponent;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy (gameObject);
            }
        }

        private void Start()
        {
            debugTextObject = GameObject.FindGameObjectWithTag("debugText");
            textComponent = debugTextObject.GetComponent<Text>();
            textComponent.text = "Debug Waiting";

            StartCoroutine("StartLocationService");
        }

        private IEnumerator StartLocationService()
        {
            if (!Input.location.isEnabledByUser)
            {
                textComponent.text = "O GPS não foi ativado pelo usuário";
                yield break;
            }

            Input.location.Start(1f, .1f);
            float maxWait = 30f;

            while (Input.location.status == LocationServiceStatus.Initializing 
                && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;

                textComponent.text = "Waiting: " + maxWait;
                print ("Waiting: " + maxWait);
            }

            if (maxWait <= 0)
            {
                print ("Timed out");
                textComponent.text = "Timed out";
                yield break;
            }

            if (Input.location.status == LocationServiceStatus.Failed)
            {
                print ("Unable to determine device location");
                textComponent.text = "Unable to determine device location";

                yield break;
            }

            // acesso permitido :)
            print(
                "Location: " + 
                Input.location.lastData.latitude + " " + 
                Input.location.lastData.longitude + " " + 
                Input.location.lastData.altitude + " " + 
                Input.location.lastData.horizontalAccuracy + " " + 
                Input.location.lastData.timestamp);

            textComponent.text = "Access granted";

            latitute = Input.location.lastData.latitude;
            longitute = Input.location.lastData.longitude;

            yield break;
        }
    }
}

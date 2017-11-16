using GpsSystem;
using System;
using System.Collections;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private Settings _settings;
    [SerializeField]
    private Texture2D texture;
    [SerializeField]
    private Transform target;

    private GameObject tile;
    private float lat, lon;
    public bool comp = false;
    public float lat_comp, lon_comp;

    private void Start()
    {
        StartCoroutine(loadTiles(_settings.zoom));
    }

    private void Update()
    {
        target.position = Vector3.Lerp(target.position, new Vector3(0, 0.25f, 0), 2.0f * Time.deltaTime);

        if (!comp)
        {
            lat = Gps.Instance.latitute;
            lon = Gps.Instance.longitute;
        }
        else
        {
            lat = lat_comp;
            lon = lon_comp;
        }
    }

    private IEnumerator loadTiles(int zoom)
    {
        int size = _settings.size;
        string key = _settings.key;
        string style = _settings.style;

        string url = String.Format("https://api.mapbox.com/v4/mapbox.{5}/{0},{1},{2}/{3}x{3}@2x.png?access_token={4}", lon, lat, zoom, size, key, style);

        WWW www = new WWW(url);
        yield return www;
        texture = www.texture;

        if (tile == null)
        {
            tile = GameObject.CreatePrimitive(PrimitiveType.Plane);
            tile.name = "Tile " + lat + "" + lon;
            tile.transform.localScale = Vector3.one * _settings.scale;

            tile.GetComponent<Renderer>().material = _settings.material;
            tile.transform.parent = transform;
        }

        tile.GetComponent<Renderer>().material.mainTexture = texture;

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(loadTiles(_settings.zoom));
    }
}


[Serializable]
public class Settings
{
    [SerializeField]
    public Material material;
    [SerializeField]
    public int zoom = 18;
    [SerializeField]
    public int size = 640;

    [SerializeField]
    public float scale = 1f;
    [SerializeField]
    public string key;
    [SerializeField]
    public string style = "emerald";
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform mapParent;
    public GameObject areaPrefab;
    public RectTransform playerIcon;

    [Header("Colors")]
    public Color defaultColor = Color.grey;
    public Color currentAreaColor = Color.green;

    [Header("Map Settings")]
    public GameObject MapBounds; //Parent of areas colliders
    public PolygonCollider2D initialArea;//Initial Starting area
    public float mapScale = 10f;//adjust map size on UI

    private PolygonCollider2D[] mapAreas;
    private Dictionary<string, RectTransform> uiAreas = new Dictionary<string, RectTransform>(); 
    
    public static MapController Instance { get; set; }

    private void Start()
    {
        initialArea = GameObject.Find("StartRoom").GetComponent<PolygonCollider2D>();
        
    }

    public void populateMapBounds(PolygonCollider2D area)
    {
        area.transform.parent = MapBounds.transform;
    }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        mapAreas = MapBounds.GetComponentsInChildren<PolygonCollider2D>();
    }

    //Generate Map
    public void GenerateMap(PolygonCollider2D newCurrentArea = null)
    {
        PolygonCollider2D currentArea = newCurrentArea != null ? newCurrentArea : initialArea;

        ClearMap();

        foreach(PolygonCollider2D area in mapAreas)
        {
            CreateAreaUI(area, area == currentArea);
        }

        MovePlayerIcon(currentArea.name);

    }

    //ClearMap
    private void ClearMap()
    {
        foreach(Transform child in mapParent)
        {
            Destroy(child.gameObject);
        }

        uiAreas.Clear();
    }

    private void CreateAreaUI(PolygonCollider2D area, bool isCurrent)
    {
        //Instantiate prefab for image
        GameObject areaImage = Instantiate(areaPrefab, mapParent);
        RectTransform rectTransform = areaImage.GetComponent<RectTransform>();

        //Get Bounds
        Bounds bounds = area.bounds;

        //Scale UI Image fit map and bounds
        rectTransform.sizeDelta = new Vector2(bounds.size.x * mapScale, bounds.size.y * mapScale);
        rectTransform.anchoredPosition = bounds.center * mapScale;

        //Set color based on current or not
        areaImage.GetComponent<Image>().color = isCurrent ? currentAreaColor : defaultColor;

        //Add to dictionary
        uiAreas[area.name] = rectTransform;
    }
    //Update Current Area
    public void UpdateCurrentArea(string newCurrentArea)
    {
        foreach (KeyValuePair<string, RectTransform> area in uiAreas)
        {
            area.Value.GetComponent<Image>().color = area.Key == newCurrentArea ? currentAreaColor : defaultColor;
        }

        //move player icon
        MovePlayerIcon(newCurrentArea);
    }

    //MovePlayerIcon
    private void MovePlayerIcon(string newCurrentArea)
    {
        if (uiAreas.TryGetValue(newCurrentArea, out RectTransform areaUI))
        {
            //if current area was found, set the icon position to center of area
            playerIcon.anchoredPosition = areaUI.anchoredPosition;
        }
    }
}

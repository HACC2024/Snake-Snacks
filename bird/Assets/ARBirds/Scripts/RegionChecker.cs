using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using Mapbox.Utils;
using Mapbox.Unity.MeshGeneration.Data;
using Mapbox.Unity.MeshGeneration.Interfaces;
using Mapbox.Unity.SourceLayers;
using Mapbox.VectorTile.ExtensionMethods;

public class RegionChecker : MonoBehaviour
{
    public AbstractMap map;  // Mapbox map object
    public Transform player; // Reference to the player position
    private ILocationProvider _locationProvider;

    // Define ecosystem types
    public enum EcosystemType
    {
        Beach,
        Forest,
        Mountain,
        Urban,
        Water,
        Undefined
    }

    void Start()
    {
        _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
    }

    void Update()
    {
        // Get the player's current GPS coordinates
        Vector2d playerLatLon = _locationProvider.CurrentLocation.LatitudeLongitude;
        
        // EcosystemType ecosystem = GetEcosystemType(playerLocation);
        // Debug.Log("Current Ecosystem: " + ecosystem);
    }

    public float GetTerrainElevation(Vector2 playerLatLon)
    {
        Vector2d latLon = new Vector2d(playerLatLon.x, playerLatLon.y);
        float elevation = map.QueryElevationInMetersAt(latLon);
        return elevation;
    }

    // EcosystemType GetEcosystemType(Vector2d location)
    // {
    //     foreach (var sublayers in map.VectorData.GetAllFeatureSubLayers())
    //     {
    //         if (sublayers.("landuse"))
    //         {
    //             var properties = vectorLayer.Properties;

    //             if (properties.HasValue("class"))
    //             {
    //                 string landUseClass = properties.GetValueAsString("class");

    //                 switch (landUseClass)
    //                 {
    //                     case "forest":
    //                     case "park":
    //                         return EcosystemType.Forest;

    //                     case "mountain":
    //                         return EcosystemType.Mountain;

    //                     case "urban":
    //                         return EcosystemType.Urban;

    //                     // Add more cases for other landuse classes (farmland, etc.)
    //                 }
    //             }
    //         }
    //     }
    // }

}

using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Mapbox.Unity.MeshGeneration.Data;
using Mapbox.Unity.MeshGeneration.Enums;
using Mapbox.Unity.MeshGeneration.Components;
using Mapbox.Unity.MeshGeneration.Interfaces;
using Mapbox.Unity.SourceLayers;
using Mapbox.VectorTile.ExtensionMethods;
using Mapbox.VectorTile;
using System;
using System.Collections.Generic;
using UnityEngine;


public class RegionChecker : MonoBehaviour
{
    public AbstractMap map;  // Mapbox map object
    public Transform player;
    public Vector2d playerLatLon; // Reference to the player position
    private ILocationProvider _locationProvider;
    public float searchRadius = 5f;
    public EcosystemType currEco;
    

    // Define ecosystem types
    public enum EcosystemType
    {
        Beach,
        Park,
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
        playerLatLon = _locationProvider.CurrentLocation.LatitudeLongitude;
        if(isTileLoaded())
        {
            if(currEco != GetEcosystemType())
            {
                GetEcosystemType();
                Debug.Log(currEco);
            }
        }
    }

    public EcosystemType CheckMountain(Vector2d playerLatLon)
    {
        float elevation = map.QueryElevationInMetersAt(playerLatLon);
        // height of a mountain in meters
        if(elevation > 2500)
        {
            currEco = EcosystemType.Mountain;
            return currEco;
        }
        return EcosystemType.Undefined;
    }

    public EcosystemType GetEcosystemType()
    {
        GameObject nearest = findClosestFeature().gameObject;
        foreach(var layer in map.VectorData.GetAllFeatureSubLayers())
        {
            if(layer.SubLayerNameContains(nearest.name))
            {        
                switch(layer.presetFeatureType)
                {
                    case PresetFeatureType.Landuse: 
                        switch(layer.coreOptions.sublayerName)
                        {
                            case "landuse_park":
                                currEco = EcosystemType.Park;
                                return currEco;
                            case "landuse_residential":
                                currEco = EcosystemType.Urban;
                                return currEco;
                            case "landuse_beach":
                                currEco = EcosystemType.Beach;
                                return currEco;
                            default:
                                currEco = EcosystemType.Undefined; 
                                return currEco;
                        }
                    case PresetFeatureType.Custom:
                        currEco = EcosystemType.Water;
                        return currEco;
                    case PresetFeatureType.Buildings:
                        currEco = EcosystemType.Urban;
                        return currEco;
                    default: 
                        currEco = EcosystemType.Undefined; 
                        return currEco;
                }
            }
        }
        return currEco;
    }

    public bool isTileLoaded()
    {
        Mapbox.Map.UnwrappedTileId coordinateTileId = Conversions.LatitudeLongitudeToTileId(playerLatLon.x, playerLatLon.y, map.AbsoluteZoom);
        UnityTile tile = map.MapVisualizer.GetUnityTileFromUnwrappedTileId(coordinateTileId);
        if(tile.TileState != TilePropertyState.Unregistered)
        {
            return true;
        }
        else return false;
    }

    public Transform findClosestFeature()
    {
        Transform nearestFeature = null;
        FeatureBehaviour[] features = FindObjectsOfType<FeatureBehaviour>();
            foreach(var feature in features)
            {
                float distance = Vector3.Distance(player.position, feature.transform.position);
                if(distance <= searchRadius)
                {
                    nearestFeature = feature.transform;
                    searchRadius = distance;
                }
            }
        return nearestFeature;
    }
}

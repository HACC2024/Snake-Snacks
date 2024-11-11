using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AvidexManager : MonoBehaviour
{
    //Variables for UI
    public List<Avidex> allEntries;
    public Transform contentPanel;
    public GameObject entryPrefab;
    public GameObject detailView;
    private EcosystemType currentEcosystem;
    public GameObject AvidexUI;
    private bool toggle = true;

    //Variables for detailed bird entries
    public TMP_Text name;
    public Image image;
    public TMP_Text status;
    public TMP_Text hwnName;
    public TMP_Text description;
    public bool native;

    void Start()
    {
        //ShowBirdList(EcosystemType.Forest); // Start with a default ecosystem type
        //ToggleUI();
        //ToggleDetail();
    }
    
    public void ShowBirdList(EcosystemType ecosystemType)
    {
        currentEcosystem = ecosystemType;

        // Clear current bird entries
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Populate list with bird entries matching the selected ecosystem type
        foreach (var bird in allEntries)
        {
            if (bird.EcosystemType == ecosystemType)
            {
                GameObject entry = Instantiate(entryPrefab, contentPanel);
                AvidexDisplay entryUI = entry.GetComponent<AvidexDisplay>();
                entryUI.SetBirdData(bird);
                entryUI.OnEntrySelected += ShowBirdDetails; // Subscribe to tap/click event
            }
        }
    }

    public void ShowBirdDetails(Avidex avidex)
    {
        detailView.SetActive(true);
        name.text = avidex.Name;
        image.sprite = avidex.Photo;
        status.text = avidex.Status.ToString();
        hwnName.text = avidex.Hawaiian_Name;
        description.text = avidex.Species_Description;
        native = avidex.Native;
    }

    
    public void ShowForestBirds()
    {
        ShowBirdList(EcosystemType.Forest);
    }

    public void ShowCoastBirds()
    {
        ShowBirdList(EcosystemType.Coastline);
    }

    public void ShowMountainBirds()
    {
        ShowBirdList(EcosystemType.Mountain);
    }

    public void ShowUrbanBirds()
    {
        ShowBirdList(EcosystemType.Urban);
    }

    public void ToggleUI()
    {
        toggle = !toggle;
        AvidexUI.SetActive(toggle);
    }

    public void ToggleDetail()
    {
        detailView.SetActive(false);
    }
}

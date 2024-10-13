using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FakeCredits : MonoBehaviour
{
    private Dictionary<string, List<string>> categoryLists = new Dictionary<string, List<string>>();
    public List<string> devNames = new List<string>();
    public List<string> artNames = new List<string>();
    public List<string> musicNames = new List<string>();
    public List<string> gdNames = new List<string>();
    public List<string> uiNames = new List<string>();
    [SerializeField] TextAsset textFile;
    [SerializeField] TextMeshProUGUI devName;
    [SerializeField] TextMeshProUGUI artName;
    [SerializeField] TextMeshProUGUI musicName;
    [SerializeField] TextMeshProUGUI gdName;
    [SerializeField] TextMeshProUGUI uiName;
    // Start is called before the first frame update
    void Start()
    {
        categoryLists["[DEV]"] = devNames;
        categoryLists["[ART]"] = artNames;
        categoryLists["[MUSIC]"] = musicNames;
        categoryLists["[GD]"] = gdNames;
        categoryLists["[UI]"] = uiNames;
        LoadNamesFromFile();
        RandomNames();
    }

    private void LoadNamesFromFile()
    {

        if (textFile != null)
        {
            string[] lines = textFile.text.Split('\n');
            List<string> currentList = null;

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();

                if (string.IsNullOrEmpty(trimmedLine))
                {
                    continue; // Skip empty lines
                }

                // Check if the line is a category header and set the current list
                if (categoryLists.ContainsKey(trimmedLine.ToUpper()))
                {
                    currentList = categoryLists[trimmedLine.ToUpper()];
                }
                else if (currentList != null)
                {
                    // If it's not a header, add the name to the current list
                    currentList.Add(trimmedLine);
                }
            }

            Debug.Log("Names loaded successfully.");
            Debug.Log($"Dev Names: {devNames.Count}, Art Names: {artNames.Count}, Music Names: {musicNames.Count}, GD Names: {gdNames.Count}, UI Names: {uiNames.Count}");
        }
        else
        {
            Debug.LogError("File not found: ");
        }
    }
    
    public void RandomNames()
    {
        int randomDev = Random.Range(0,devNames.Count-1);
        Debug.Log("RANDOM DEV: " + randomDev + "// DEV COUNT" + devNames.Count);
        int randomArt = Random.Range(0, artNames.Count-1);
        int randomMusic = Random.Range(0, musicNames.Count-1);
        int randomGD = Random.Range(0, gdNames.Count-1);
        int randomUI = Random.Range(0, uiNames.Count-1);

        devName.text = devNames[randomDev];
        artName.text = artNames[randomArt];
        musicName.text = musicNames[randomMusic];
        gdName.text = gdNames[randomGD];
        uiName.text = uiNames[randomUI];
    }
}


using UnityEngine;
using System.IO;
using System.Xml.Serialization;


[System.Serializable]
public class SerializeKillTracker : MonoBehaviour
{
    private int serializeKills;
    private string path;
    private void Start()
    {

        path = Application.persistentDataPath + "/killdata.xml";
        // Save data to XML

        XmlSerializer serializer = new XmlSerializer(typeof(SerializeKillTracker));
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, serializeKills);
        }
        // Load data from XML

        if (File.Exists(path))
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                SerializeKillTracker loadedtracker = (SerializeKillTracker)serializer.Deserialize(stream);

                Debug.Log("kills counted: " + loadedtracker.serializeKills);
               
            }
        }
    }

    
}


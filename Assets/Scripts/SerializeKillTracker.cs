
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System.Xml;


[System.Serializable]
public class SerializeKillTracker : MonoBehaviour
{
   [SerializeField] private int serializeKills;
    private string path;
    private void Update()
    {
        serializeKills = KillTracker.killsCounted;
        Invoke("XMLSave", 4);
    }

    private void XMLSave()
    {
        //SerializeKillTracker tracker = new SerializeKillTracker();
        XmlDocument document = new XmlDocument();

        XmlElement root = document.CreateElement("Save");
        root.SetAttribute("FileName", "File_01");

        XmlElement killed = document.CreateElement("KillCount");
        killed.InnerText = serializeKills.ToString();
        Debug.Log("This is the value you are saving: " + killed.InnerText);
        root.AppendChild(killed);

        document.AppendChild(root);


        document.Save(Application.dataPath + "/KillTracker.text");
        if(File.Exists(Application.dataPath + "/KillTracker.text"))
        {
            Debug.Log("XML Created!");
        }
    }
    
}


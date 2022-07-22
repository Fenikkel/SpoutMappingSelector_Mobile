using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SyncManager : MonoBehaviour
{
    public GameObject m_ProjectorGrid;
    public GameObject m_VideoGrid;

    public GameObject m_ProjectorButtonPrefab;
    public GameObject m_VideoButtonPrefab;
    public GameObject m_StopProjButtonPrefab;

    int[] _ConfigArray;

    RemoteControllerTransmitter _RemoteControllerTransmitter;

    private void Start()
    {
        _RemoteControllerTransmitter = FindObjectOfType<RemoteControllerTransmitter>();
    }

    private void SyncProjectors()
    {
        //Delete previous buttons
        foreach (Transform child in m_ProjectorGrid.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in m_VideoGrid.transform)
        {
            Destroy(child.gameObject);
        }

        //Instantiate new buttons
        GameObject tempInstance;
        Button tempButton = null;

        for (int projIndex = 0; projIndex < _ConfigArray.Length; projIndex++)
        {
            tempInstance = Instantiate(m_ProjectorButtonPrefab, m_ProjectorGrid.transform);
            tempInstance.name = "ProjectorBut_" + projIndex;

            tempInstance.GetComponentInChildren<TextMeshProUGUI>().text = projIndex.ToString();

            tempButton = tempInstance.GetComponent<Button>();

            if (tempButton == null)
            {
                Debug.LogWarning(projIndex + ": No tiene el componente Button");
            }
            else
            {
                int vidCount = _ConfigArray[projIndex]; //Si no es asi no funciona
                int copyIndex = projIndex;

                tempButton.onClick.AddListener(delegate { ShowVideoBut(copyIndex, vidCount); }); //Set button behaviour
            }

            tempButton = null;
        }
    }

    public void ShowVideoBut(int projIndex, int numVideos) //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/
    {
        print(projIndex);
        //Delete previous buttons
        foreach (Transform child in m_VideoGrid.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject tempInstance;
        Button tempButton = null;

        for (int vidIndex = 0; vidIndex < numVideos; vidIndex++)
        {
            tempInstance = Instantiate(m_VideoButtonPrefab, m_VideoGrid.transform);
            tempInstance.name = "VideoBut_" + vidIndex;

            tempInstance.GetComponentInChildren<TextMeshProUGUI>().text = vidIndex.ToString();

            tempButton = tempInstance.GetComponent<Button>();

            int copyVidIndex = vidIndex; //Si no es asi no funciona
            int copyProjIndex = projIndex;

            tempButton.onClick.AddListener(delegate { _RemoteControllerTransmitter.PlayVideo(copyProjIndex, copyVidIndex); }); //Set button behaviour

            tempButton = null;
        }

        tempInstance = Instantiate(m_StopProjButtonPrefab, m_VideoGrid.transform);
        int coProjIndex = projIndex;
        tempInstance.GetComponent<Button>().onClick.AddListener(delegate { _RemoteControllerTransmitter.StopProjVideo(coProjIndex); });
        //AÑADIR BUTT DE STOP ESE PROYECTOR
    }

    public void SetConfigArray(int[] configArray)
    {
        _ConfigArray = configArray;

        if (configArray != null)
        {
            SyncProjectors();
        }
    }
}

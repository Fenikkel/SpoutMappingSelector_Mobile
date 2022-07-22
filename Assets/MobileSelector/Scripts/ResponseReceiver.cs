using extOSC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseReceiver : MonoBehaviour
{
    [Header("OSC Address")]
    public string m_SyncAddress = "/sync";

    /* Private */
    OSCReceiver _Receiver;
	SyncManager _SyncManager;

    void Start()
    {
        _Receiver = FindObjectOfType<OSCReceiver>();
        _Receiver.Bind(m_SyncAddress, Sync);

		_SyncManager = FindObjectOfType<SyncManager>();
	}

	private void Sync(OSCMessage message)
	{
		List<OSCValue> oscList;
		OSCValue[] oscArray;

		if (message.ToArray(out oscList))
		{
			if (0 == oscList.Count)
			{
				Debug.LogWarning("No data!!");
				
			}

			oscArray = oscList.ToArray();
		}
		else
		{
			Debug.LogWarning("Cant convert to a list");
			return;
		}

		int[] configArray = new int[oscArray.Length];
        for (int i = 0; i < oscArray.Length; i++)
        {
			configArray[i] = oscArray[i].IntValue;
			print(oscArray[i].IntValue);
        }

		_SyncManager.SetConfigArray(configArray);
		//_SyncManager.SyncProjectors(oscArray.Length);
	}

}

using UnityEngine;
using extOSC;

public class RemoteControllerTransmitter : MonoBehaviour
{
    [Header("OSC Settings")]
    private OSCTransmitter _Transmitter;
    public string m_SyncAddress = "/sync";

    public string m_StopAddress = "/stop";

    public string m_PlayAllAddress = "/playAll";
    public string m_PlayVideoAddress = "/play";

    private void Start()
    {
        _Transmitter = FindObjectOfType<OSCTransmitter>();

    }

    public void Sync()
    {
        OSCMessage message = new OSCMessage(m_SyncAddress);
        message.AddValue(OSCValue.Impulse());

        _Transmitter.Send(message);

        Debug.Log("Osc sended: Sync -> Imulse");
    }

    public void PlayAllVideo(bool play)
    {
        OSCMessage message = new OSCMessage(m_PlayAllAddress);

        message.AddValue(OSCValue.Bool(play));

        _Transmitter.Send(message);

        Debug.Log("Osc sended: Play videos -> " + play);
    }

    public void PlayVideo(int oscIndex, int videoIndex)
    {
        OSCMessage message = new OSCMessage(m_PlayVideoAddress);
        message.AddValue(OSCValue.Array(OSCValue.Int(oscIndex), OSCValue.Int(videoIndex)));

        _Transmitter.Send(message);

        Debug.Log("Osc sended: \nOsc index -> " + oscIndex + "\nVideo index -> " + videoIndex);
    }

    public void StopProjVideo(int projIndex)
    {
        OSCMessage message = new OSCMessage(m_StopAddress);
        message.AddValue(OSCValue.Int(projIndex));

        _Transmitter.Send(message);

        Debug.Log("Osc sended: projIndex -> " + projIndex);
    }
}

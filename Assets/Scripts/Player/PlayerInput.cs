using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public void Start()
    {
        var joys = Input.GetJoystickNames();
        Debug.Log("HELLO");
        for (var i = 0; i < joys.Length; i++)
        {
            Debug.Log(joys[i]);
        }
    }

    public static float GetAxis(string axis, Player player)
    {
        var p = player.ToString().Substring(1, 1);
        return Input.GetAxisRaw(axis + "_" + p);
    }

    public static bool GetButton(string keyName, Player player)
    {
        var p = player.ToString().Substring(1, 1);
        return Input.GetButton(keyName + "_" + p);
    }

    public static bool GetButtonUp(string keyName, Player player)
    {
        var p = player.ToString().Substring(1, 1);
        return Input.GetButtonUp(keyName + "_" + p);
    }

    public static bool GetButtonDown(string keyName, Player player)
    {
        var p = player.ToString().Substring(1, 1);
        return Input.GetButtonDown(keyName + "_" + p);
    }

}

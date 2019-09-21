using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
public class PluginTest : MonoBehaviour // <- name of script
{
    const string DLL_NAME = "A1"; // <- name of plugin
    [DllImport(DLL_NAME)]
    private static extern int SimpleFunction();

    [DllImport(DLL_NAME)]
    private static extern void savePosi(float _x, float _y, float _z, int index);
    [DllImport(DLL_NAME)]
    private static extern float getPosiX(int index);
    [DllImport(DLL_NAME)]
    private static extern float getPosiY(int index);
    [DllImport(DLL_NAME)]
    private static extern float getPosiZ(int index);
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            savePosi(1.0f, 2.0f, 3.0f, 0);
            Debug.Log(getPosiX(0));
            Debug.Log(getPosiY(0));
            Debug.Log(getPosiZ(0));
        }          
    }
}
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityPirates
{
    [Serializable]
    public class GameUIForms
    {
        [Header("UIForms")] public UIFormInfo[] Forms;
    }
}

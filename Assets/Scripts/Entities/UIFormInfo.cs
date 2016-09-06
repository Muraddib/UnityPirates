using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

namespace UnityPirates
{
    [Serializable]
    public class UIFormInfo
    {
        public UIFormIDs FormID;
        public GameObject FormPrefab;
    }
}
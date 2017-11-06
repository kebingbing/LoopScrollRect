using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace SG
{
    [RequireComponent(typeof(UnityEngine.UI.LoopScrollRect))]
    [DisallowMultipleComponent]
    public class InitOnStart : MonoBehaviour
    {
        void Start()
        {
            var rect = GetComponent<LoopScrollRect>();
            rect.RefillCells();
            
        }


    }
}
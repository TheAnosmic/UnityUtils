using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.EditorUtils
{
    [ExecuteInEditMode]
    public class SampleAnimationFrame : MonoBehaviour
    {
        #if UNITY_EDITOR

        [Range(0, 60)]
        [SerializeField] private int _frame;
        [SerializeField] private List<GameObject> _objects;
        

        private void OnValidate()
        {
            //Debug.Log(_frame);
            foreach (var o in _objects)
            {
                var clipInfo = o.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0];
                var clip = clipInfo.clip;
                var time = _frame / clip.frameRate;
                clip.SampleAnimation(o, time);
            }
        }
        
        #endif
    }
}
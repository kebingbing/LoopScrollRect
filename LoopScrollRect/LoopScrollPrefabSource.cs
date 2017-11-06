using UnityEngine;
using System.Collections;

namespace UnityEngine.UI
{
    [System.Serializable]
    public class LoopScrollPrefabSource 
    {
        public string prefabName;
        public int poolSize = 5;
        public GameObject itemprefab;

        private bool inited = false;
        public virtual GameObject GetObject()
        {
            if(!inited)
            {
                this.prefabName = itemprefab.name;
                SG.ResourceManagerExt.Instance.InitPool(this);
                GameObject pb = GameObject.Instantiate(itemprefab);
                pb.gameObject.SetActive(false);
                pb.gameObject.SetActive(true);
                
                inited = true;
            }
            return SG.ResourceManagerExt.Instance.GetObjectFromPool(this);
        }
    }
}

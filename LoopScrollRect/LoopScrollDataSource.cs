using UnityEngine;
using System.Collections;
using LuaInterface;

namespace UnityEngine.UI
{
    public abstract class LoopScrollDataSource
    {
        public LuaFunction cb;
        /// <summary>
        /// 进入item 数据刷新
        /// 隐藏item 数据刷新处理（关闭定时器 等异步操作）
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="idx"></param>
        /// <param name="isadd"></param>
        public abstract void ProvideData(Transform transform, int idx,bool isAdd);
    }

	public class LoopScrollSendIndexSource : LoopScrollDataSource
    {

		public LoopScrollSendIndexSource(){}

        public override void ProvideData(Transform transform, int idx, bool isAdd)
        {
            //transform.SendMessage("ScrollCellIndex", idx);
            if(cb != null)
            {
                cb.BeginPCall();
                cb.Push(transform.gameObject);
                cb.Push(idx);
                cb.Push(isAdd);
                cb.PCall();
                cb.EndPCall();
            }
        }
    }

	public class LoopScrollArraySource<T> : LoopScrollDataSource
    {
        T[] objectsToFill;

		public LoopScrollArraySource(T[] objectsToFill)
        {
            this.objectsToFill = objectsToFill;
        }

        public override void ProvideData(Transform transform, int idx, bool isAdd)
        {
            //transform.SendMessage("ScrollCellContent", objectsToFill[idx]);
               
        }
    }
}
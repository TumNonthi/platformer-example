using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public abstract class Pool<T> : MonoBehaviour, IPool<T> where T : IPoolable
    {
        protected Stack<T> Available = new Stack<T>();

        public abstract IFactory<T> Factory { get; set; }
        protected bool HasBeenPrewarmed { get; set; }

        private List<T> membersInUsed = new List<T>();

        protected virtual T Create()
        {
            return Factory.Create();
        }

        public virtual void Prewarm(int num)
        {
            if (HasBeenPrewarmed)
            {
                return;
            }
            for (int i = 0; i < num; i++)
            {
                Available.Push(Create());
            }
            HasBeenPrewarmed = true;
        }

        public virtual T Request()
        {
            T member = Available.Count > 0 ? Available.Pop() : Create();
            if (!membersInUsed.Contains(member))
            {
                membersInUsed.Add(member);
            }
            return member;
        }

        public virtual IEnumerable<T> Request(int num = 1)
        {
            List<T> members = new List<T>(num);
            for (int i = 0; i < num; i++)
            {
                members.Add(Request());
            }
            return members;
        }

        public virtual void Return(T member)
        {
            Available.Push(member);
        }

        public virtual void Return(IEnumerable<T> members)
        {
            foreach (T member in members)
            {
                Return(member);
            }
        }

        public virtual void Clear()
        {
            Available.Clear();
            HasBeenPrewarmed = false;
        }

        protected virtual void Update()
        {
            foreach (T member in membersInUsed)
            {
                if (member != null && member.IsDoneUsing())
                {
                    Return(member);
                }
            }
        }

        protected virtual void OnDestroy()
        {
            Clear();
        }
    }
}

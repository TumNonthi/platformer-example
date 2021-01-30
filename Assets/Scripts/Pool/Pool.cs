using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public abstract class Pool<T> : MonoBehaviour, IPool<T> where T : IPoolable
    {
        protected Queue<T> Available = new Queue<T>();

        public abstract IFactory<T> Factory { get; set; }
        protected bool HasBeenPrewarmed { get; set; }

        private List<T> membersInUse = new List<T>();
        private List<T> membersToReturn = new List<T>();

        protected virtual T Create()
        {
            return Factory.Create();
        }

        public void Prewarm(int num)
        {
            if (HasBeenPrewarmed)
            {
                return;
            }
            for (int i = 0; i < num; i++)
            {
                Available.Enqueue(Create());
            }
            HasBeenPrewarmed = true;
        }

        public virtual T Request()
        {
            T member = Available.Count > 0 ? Available.Dequeue() : Create();
            if (!membersInUse.Contains(member))
            {
                membersInUse.Add(member);
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
            membersInUse.Remove(member);
            Available.Enqueue(member);
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
            membersToReturn.Clear();

            foreach (T member in membersInUse)
            {
                if (member != null && member.IsDoneUsing())
                {
                    membersToReturn.Add(member);
                }
            }
            Return(membersToReturn);
        }

        protected virtual void OnDestroy()
        {
            Clear();
        }
    }
}

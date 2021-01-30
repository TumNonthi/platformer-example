using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public abstract class ComponentPool<T> : Pool<T> where T : Component, IPoolable
    {
        public override T Request()
        {
            T member = base.Request();
            member.gameObject.SetActive(true);
            return member;
        }

        public override void Return(T member)
        {
            member.transform.SetParent(transform);
            member.gameObject.SetActive(false);
            base.Return(member);
        }

        protected override T Create()
        {
            T newMember = base.Create();
            newMember.transform.SetParent(transform);
            newMember.gameObject.SetActive(false);
            return newMember;
        }
    }
}

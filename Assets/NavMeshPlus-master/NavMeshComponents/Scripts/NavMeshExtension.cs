using NavMeshPlus.Components;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NavMeshPlus.Extensions
{
    public abstract class NavMeshExtension: MonoBehaviour
    {
        public int Order { get; protected set; }
        public virtual void CollectSources(NavigationSurface surface, List<NavMeshBuildSource> sources, NavMeshBuilderState navNeshState) { }
        public virtual void CalculateWorldBounds(NavigationSurface surface, List<NavMeshBuildSource> sources, NavMeshBuilderState navNeshState) { }
        public virtual void PostCollectSources(NavigationSurface surface, List<NavMeshBuildSource> sources, NavMeshBuilderState navNeshState) { }
        public NavigationSurface NavMeshSurfaceOwner
        {
            get
            {
                if (m_navMeshOwner == null)
                    m_navMeshOwner = GetComponent<NavigationSurface>();
                return m_navMeshOwner;
            }
        }
        NavigationSurface m_navMeshOwner;

        protected virtual void Awake()
        {
            ConnectToVcam(true);
        }
#if UNITY_EDITOR
        [UnityEditor.Callbacks.DidReloadScripts]
        static void OnScriptReload()
        {
            var extensions = Resources.FindObjectsOfTypeAll(
                typeof(NavMeshExtension)) as NavMeshExtension[];
            foreach (var e in extensions)
                e.ConnectToVcam(true);
        }
#endif
        protected virtual void OnEnable() { }
        protected virtual void OnDestroy()
        {
            ConnectToVcam(false);
        }
        protected virtual void ConnectToVcam(bool connect)
        {
            if (connect && NavMeshSurfaceOwner == null)
                Debug.LogError("NevMeshExtension requires a NavMeshSurface component");
            if (NavMeshSurfaceOwner != null)
            {
                if (connect)
                    NavMeshSurfaceOwner.NevMeshExtensions.Add(this, Order);
                else
                    NavMeshSurfaceOwner.NevMeshExtensions.Remove(this);
            }
        }
    }
}

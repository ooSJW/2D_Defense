﻿using NavMeshPlus.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace NavMeshPlus.Extensions
{
    [ExecuteAlways]
    [AddComponentMenu("Navigation/Navigation RootSources2d", 30)]
    public class RootSources2d: NavMeshExtension
    {
        [SerializeField]
        private List<GameObject> _rootSources;

        public List<GameObject> RootSources { get => _rootSources; set => _rootSources = value; }

        protected override void Awake()
        {
            Order = -1000;
            base.Awake();
        }

        public override void CollectSources(NavigationSurface surface, List<NavMeshBuildSource> sources, NavMeshBuilderState navNeshState)
        {
            navNeshState.roots = _rootSources;
        }
    }
}

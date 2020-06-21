﻿using System;

namespace Dissonance.Engine.Core.Modules
{
	public abstract class EngineModule : IDisposable
	{
		public virtual bool AutoLoad => true;

		public Game Game { get; internal set; }
		public ModuleDependency[] Dependencies { get; internal set; }
		public int DependencyIndex { get; internal set; }

		public void Dispose() => OnDispose();

		//Init

		[VirtualMethodHook(typeof(EngineModuleHooks),nameof(EngineModuleHooks.PreInit),false,true)]
		protected virtual void PreInit() { }
		
		[VirtualMethodHook(typeof(EngineModuleHooks),nameof(EngineModuleHooks.Init),false,true)]
		protected virtual void Init() { }
		
		//Fixed Update
		
		[VirtualMethodHook(typeof(EngineModuleHooks),nameof(EngineModuleHooks.PreFixedUpdate),false,true)]
		protected virtual void PreFixedUpdate() { }

		[VirtualMethodHook(typeof(EngineModuleHooks),nameof(EngineModuleHooks.FixedUpdate),false,true)]
		protected virtual void FixedUpdate() { }

		[VirtualMethodHook(typeof(EngineModuleHooks),nameof(EngineModuleHooks.PostFixedUpdate),false,true)]
		protected virtual void PostFixedUpdate() { }
		
		//Render Update
		
		[VirtualMethodHook(typeof(EngineModuleHooks),nameof(EngineModuleHooks.PreRenderUpdate),false,true)]
		protected virtual void PreRenderUpdate() { }

		[VirtualMethodHook(typeof(EngineModuleHooks),nameof(EngineModuleHooks.RenderUpdate),false,true)]
		protected virtual void RenderUpdate() { }

		[VirtualMethodHook(typeof(EngineModuleHooks),nameof(EngineModuleHooks.PostRenderUpdate),false,true)]
		protected virtual void PostRenderUpdate() { }

		//Etc

		protected virtual void OnDispose() { }
	}
}

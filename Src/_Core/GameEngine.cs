﻿namespace Dissonance.Engine
{
	public static class GameEngine
	{
		public static bool IsInitialized { get; private set; }
		public static bool InFixedUpdate { get; private set; } // Stone age.
		public static bool InRenderUpdate { get; private set; }

		/// <summary>
		/// The <see cref="GameFlags"/> the engine was ran with.
		/// </summary>
		public static GameFlags Flags { get; private set; }

		public static void Initialize(GameFlags flags = GameFlags.None)
		{
			if (IsInitialized) {
				return;
			}

			Debug.Log("Loading engine...");

			Flags = flags;

			ModuleManagement.Initialize();

			IsInitialized = true;
		}

		public static void Terminate()
		{
			if (!IsInitialized) {
				return;
			}

			IsInitialized = false;
			InFixedUpdate = false;
			InRenderUpdate = false;

			ModuleManagement.Unload();
		}
		
		// All these hooks and their caller methods aren't elegant in the slightest...

		public static void FixedUpdate()
		{
			InFixedUpdate = true;
			InRenderUpdate = false;

			var hooks = ModuleManagement.Hooks;

			hooks.PreFixedUpdate?.Invoke();
			hooks.FixedUpdate?.Invoke();
			hooks.PostFixedUpdate?.Invoke();

			InFixedUpdate = false;
		}

		public static void RenderUpdate()
		{
			InRenderUpdate = true;
			InFixedUpdate = false;

			var hooks = ModuleManagement.Hooks;
			
			hooks.PreRenderUpdate?.Invoke();
			hooks.RenderUpdate?.Invoke();
			hooks.PostRenderUpdate?.Invoke();

			InRenderUpdate = false;
		}
	}
}

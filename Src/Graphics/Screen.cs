﻿using System;
using Dissonance.Engine.Core;
using Dissonance.Engine.Core.Modules;
using Dissonance.Engine.Structures;
using Dissonance.Framework.Windowing;

namespace Dissonance.Engine.Graphics
{
	[ModuleDependency(typeof(Windowing))]
	public sealed class Screen : EngineModule
	{
		public static bool lockCursor;

		private static CursorState cursorState;

		//Framebuffer
		public static int Width { get; private set; }
		public static int Height { get; private set; }
		public static Vector2Int Size { get; private set; }
		public static RectInt Rectangle { get; private set; }
		public static Vector2 Center { get; private set; }
		//Window
		public static int WindowX { get; internal set; }
		public static int WindowY { get; internal set; }
		public static int WindowWidth { get; internal set; }
		public static int WindowHeight { get; internal set; }
		public static Vector2Int WindowSize { get; internal set; }
		public static Vector2Int WindowLocation { get; internal set; }
		public static Vector2 WindowCenter { get; internal set; }

		/*public static bool Fullscreen {
			get => GLFW.GetWindowMonitor(Rendering.window)!=IntPtr.Zero;
			set {
				var monitor = GLFW.GetWindowMonitor(Rendering.window);
				bool isFullscreen = monitor!=IntPtr.Zero;

				if(value!=isFullscreen) {
					return;
				}

				if(value) {
					GLFW.SetWindowMonitor(Rendering.window,GLFW.GetPrimaryMonitor(),0,0,800,600,144);
				} else {
					var videoMode = GLFW.GetVideoMode(monitor);

					GLFW.SetWindowMonitor(Rendering.window,IntPtr.Zero,0,0,videoMode.width,videoMode.height,videoMode.refreshRate);
				}
			}
		}*/

		public static CursorState CursorState {
			get => cursorState;
			set => GLFW.SetInputMode(Game.Instance.GetModule<Windowing>(true).WindowHandle,InputMode.Cursor,(int)(cursorState = value));
		}

		private Windowing windowing;

		public override bool AutoLoad => !Game.NoWindow;

		protected override void Init() => UpdateValues();
		protected override void PreRenderUpdate() => UpdateValues();

		protected override void PreInit()
		{
			windowing = Game.GetModule<Windowing>(true);
		}
		protected override void OnDispose()
		{
			windowing = null;
		}

		private void UpdateValues()
		{
			var windowHandle = windowing.WindowHandle;

			//Framebuffer
			GLFW.GetFramebufferSize(windowHandle,out int framebufferWidth,out int framebufferHeight);

			Width = framebufferWidth;
			Height = framebufferHeight;

			Size = new Vector2Int(framebufferWidth,framebufferHeight);
			Center = new Vector2(framebufferWidth*0.5f,framebufferHeight*0.5f);
			Rectangle = new RectInt(0,0,framebufferWidth,framebufferHeight);

			//Window
			GLFW.GetWindowPos(windowHandle,out int windowX,out int windowY);
			GLFW.GetWindowSize(windowHandle,out int windowWidth,out int windowHeight);

			WindowX = windowX;
			WindowY = windowY;
			WindowWidth = windowWidth;
			WindowHeight = windowHeight;

			WindowSize = new Vector2Int(windowWidth,windowHeight);
			WindowLocation = new Vector2Int(windowX,windowY);

			WindowCenter = new Vector2(windowX+windowWidth*0.5f,windowY+windowHeight*0.5f);
		}
	}
}
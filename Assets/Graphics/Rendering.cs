using System;
using System.Collections.Generic;
using GameEngine.Graphics.RenderingPipelines;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace GameEngine.Graphics
{
	//TODO: Add submeshes to Mesh.cs
	//TODO: Add some way to sort objects in a way that'd let the engine skip BoxInFrustum checks for objects which are in non-visible chunks.
	public static partial class Rendering
	{
		#region PixelFormats
		//Move this
		public static Dictionary<TextureFormat,(PixelFormat formatGeneral,PixelInternalFormat formatInternal,PixelType pixelType,Type dataType)> textureFormatInfo = new Dictionary<TextureFormat,(PixelFormat,PixelInternalFormat,PixelType,Type)> {
			#region A
			{ TextureFormat.A8,					(PixelFormat.Alpha,				PixelInternalFormat.Alpha8,				PixelType.UnsignedByte,		typeof(byte)) },	//8
			{ TextureFormat.A16,				(PixelFormat.Alpha,				PixelInternalFormat.Alpha16,			PixelType.UnsignedByte,		typeof(byte)) },	//16
			#endregion
			#region R
			{ TextureFormat.R8,					(PixelFormat.Red,				PixelInternalFormat.R8,					PixelType.UnsignedByte,		typeof(byte)) },	//8
			{ TextureFormat.R8i,				(PixelFormat.RedInteger,		PixelInternalFormat.R8i,				PixelType.Byte,				typeof(sbyte)) },
			{ TextureFormat.R8ui,				(PixelFormat.RedInteger,		PixelInternalFormat.R8ui,				PixelType.UnsignedByte,		typeof(byte)) },
			{ TextureFormat.R16,				(PixelFormat.Red,				PixelInternalFormat.R16,				PixelType.UnsignedShort,	typeof(ushort)) },	//16
			{ TextureFormat.R16f,				(PixelFormat.Red,				PixelInternalFormat.R16f,				PixelType.HalfFloat,		typeof(Half)) },
			{ TextureFormat.R16i,				(PixelFormat.RedInteger,		PixelInternalFormat.R16i,				PixelType.Short,			typeof(short)) },
			{ TextureFormat.R16ui,				(PixelFormat.RedInteger,		PixelInternalFormat.R16ui,				PixelType.UnsignedShort,	typeof(ushort)) },
			{ TextureFormat.R32f,				(PixelFormat.Red,				PixelInternalFormat.R32f,				PixelType.Float,			typeof(float)) },	//32
			{ TextureFormat.R32i,				(PixelFormat.RedInteger,		PixelInternalFormat.R32i,				PixelType.Int,				typeof(int)) },
			{ TextureFormat.R32ui,				(PixelFormat.RedInteger,		PixelInternalFormat.R32ui,				PixelType.UnsignedInt,		typeof(uint)) },
			#endregion
			#region RG
			{ TextureFormat.RG8,				(PixelFormat.Rg,				PixelInternalFormat.Rg8,				PixelType.UnsignedByte,		typeof(byte)) },	//8
			{ TextureFormat.RG8i,				(PixelFormat.RgInteger,			PixelInternalFormat.Rg8i,				PixelType.Byte,				typeof(sbyte)) },
			{ TextureFormat.RG8ui,				(PixelFormat.RgInteger,			PixelInternalFormat.Rg8ui,				PixelType.UnsignedByte,		typeof(byte)) },
			{ TextureFormat.RG16,				(PixelFormat.Rg,				PixelInternalFormat.Rg16,				PixelType.UnsignedShort,	typeof(ushort)) },	//16
			{ TextureFormat.RG16f,				(PixelFormat.Rg,				PixelInternalFormat.Rg16f,				PixelType.HalfFloat,		typeof(Half)) },
			{ TextureFormat.RG16i,				(PixelFormat.RgInteger,			PixelInternalFormat.Rg16i,				PixelType.Short,			typeof(short)) },
			{ TextureFormat.RG16ui,				(PixelFormat.RgInteger,			PixelInternalFormat.Rg16ui,				PixelType.UnsignedShort,	typeof(ushort)) },
			{ TextureFormat.RG32f,				(PixelFormat.Rg,				PixelInternalFormat.Rg32f,				PixelType.Float,			typeof(float)) },	//32
			{ TextureFormat.RG32i,				(PixelFormat.RgInteger,			PixelInternalFormat.Rg32i,				PixelType.Int,				typeof(int)) },
			{ TextureFormat.RG32ui,				(PixelFormat.RgInteger,			PixelInternalFormat.Rg32ui,				PixelType.UnsignedInt,		typeof(uint)) },
			#endregion
			#region RGB
			{ TextureFormat.RGB8,				(PixelFormat.Rgb,				PixelInternalFormat.Rgb8,				PixelType.UnsignedByte,		typeof(byte)) },	//8
			{ TextureFormat.RGB8i,				(PixelFormat.RgbInteger,		PixelInternalFormat.Rgb8i,				PixelType.Byte,				typeof(sbyte)) },
			{ TextureFormat.RGB8ui,				(PixelFormat.RgbInteger,		PixelInternalFormat.Rgb8ui,				PixelType.UnsignedByte,		typeof(byte)) },
			{ TextureFormat.RGB16,				(PixelFormat.Rgb,				PixelInternalFormat.Rgb16,				PixelType.UnsignedShort,	typeof(ushort)) },	//16
			{ TextureFormat.RGB16f,				(PixelFormat.Rgb,				PixelInternalFormat.Rgb16f,				PixelType.HalfFloat,		typeof(Half)) },
			{ TextureFormat.RGB16i,				(PixelFormat.RgbInteger,		PixelInternalFormat.Rgb16,				PixelType.Short,			typeof(short)) },
			{ TextureFormat.RGB16ui,			(PixelFormat.RgbInteger,		PixelInternalFormat.Rgb16ui,			PixelType.UnsignedShort,	typeof(ushort)) },
			{ TextureFormat.RGB32f,				(PixelFormat.Rgb,				PixelInternalFormat.Rgb32f,				PixelType.Float,			typeof(float)) },	//32
			{ TextureFormat.RGB32i,				(PixelFormat.RgbInteger,		PixelInternalFormat.Rgb32i,				PixelType.Int,				typeof(int)) },
			{ TextureFormat.RGB32ui,			(PixelFormat.RgbInteger,		PixelInternalFormat.Rgb32ui,			PixelType.UnsignedInt,		typeof(uint)) },
			#endregion
			#region RGBA
			{ TextureFormat.RGBA8,				(PixelFormat.Rgba,				PixelInternalFormat.Rgba8,				PixelType.UnsignedByte,		typeof(byte)) },	//8
			{ TextureFormat.RGBA8i,				(PixelFormat.RgbaInteger,		PixelInternalFormat.Rgba8i,				PixelType.Byte,				typeof(sbyte)) },
			{ TextureFormat.RGBA8ui,			(PixelFormat.RgbaInteger,		PixelInternalFormat.Rgba8ui,			PixelType.UnsignedByte,		typeof(byte)) },
			{ TextureFormat.RGBA16,				(PixelFormat.Rgba,				PixelInternalFormat.Rgba16,				PixelType.UnsignedShort,	typeof(ushort)) },	//16
			{ TextureFormat.RGBA16f,			(PixelFormat.Rgba,				PixelInternalFormat.Rgba16f,			PixelType.HalfFloat,		typeof(Half)) },
			{ TextureFormat.RGBA16i,			(PixelFormat.RgbaInteger,		PixelInternalFormat.Rgba16,				PixelType.Short,			typeof(short)) },
			{ TextureFormat.RGBA16ui,			(PixelFormat.RgbaInteger,		PixelInternalFormat.Rgba16ui,			PixelType.UnsignedShort,	typeof(ushort)) },
			{ TextureFormat.RGBA32f,			(PixelFormat.Rgba,				PixelInternalFormat.Rgba32f,			PixelType.Float,			typeof(float)) },	//32
			{ TextureFormat.RGBA32i,			(PixelFormat.RgbaInteger,		PixelInternalFormat.Rgba32i,			PixelType.Int,				typeof(int)) },
			{ TextureFormat.RGBA32ui,			(PixelFormat.RgbaInteger,		PixelInternalFormat.Rgba32ui,			PixelType.UnsignedInt,		typeof(uint)) },
			#endregion
			#region DepthComponent
			{ TextureFormat.Depth16,			(PixelFormat.DepthComponent,	PixelInternalFormat.DepthComponent16,	PixelType.UnsignedShort,	typeof(ushort)) },	//16
			{ TextureFormat.Depth32,			(PixelFormat.DepthComponent,	PixelInternalFormat.DepthComponent32,	PixelType.UnsignedInt,		typeof(uint)) },
			{ TextureFormat.Depth32f,			(PixelFormat.DepthComponent,	PixelInternalFormat.DepthComponent32f,	PixelType.Float,			typeof(float)) },	//32
			#endregion
			#region DepthStencil
			{ TextureFormat.Depth24Stencil8,	(PixelFormat.DepthStencil,		PixelInternalFormat.Depth24Stencil8,	PixelType.UnsignedInt248,	typeof(int)) },			//16+8
			{ TextureFormat.Depth32fStencil8,	(PixelFormat.DepthStencil,		PixelInternalFormat.Depth32fStencil8,	PixelType.Float32UnsignedInt248Rev,	typeof(float)) },	//24+8
			#endregion
		};
		#endregion

		public static int drawCallsCount;
		public static Vector3 ambientColor = new Vector3(0.1f,0.1f,0.1f);

		internal static GameWindow window;
		internal static List<Camera> cameraList;
		internal static List<Renderer> rendererList;
		internal static List<Light> lightList;
		internal static List<Light2D> light2DList;
		internal static Texture whiteTexture; //TODO: Move this
		internal static Type renderingPipelineType;
		internal static BlendingFactor currentBlendFactorSrc;
		internal static BlendingFactor currentBlendFactorDst;
		internal static uint currentStencilMask;

		internal static RenderingPipeline renderingPipeline;
		public static RenderingPipeline RenderingPipeline => renderingPipeline;
		
		//TODO: To be moved
		private static Shader guiShader;
		public static Shader GUIShader => guiShader ??= Resources.Find<Shader>("GUI");

		internal static void PreInit()
		{
			renderingPipelineType = typeof(DeferredRendering);
		}
		internal static void Init()
		{
			var glVersion = GetOpenGLVersion();
			var minVersion = new Version("3.3");

			if(glVersion<minVersion) {
				throw new GraphicsException($"Please update your graphics drivers.\r\nMinimum OpenGL version required to run this application is: {minVersion}\r\nYour OpenGL version is: {glVersion}");
			}

			CheckGLErrors(); //Do not remove

			//FontImport
			//TODO: Add AssetManager for fonts and remove this hardcode
			var tex = Resources.Import<Texture>("BuiltInAssets/GUI/Fonts/DefaultFont.png");
			GUI.font = new Font(@" !""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~",tex,new Vector2(12f,16f),0) { size = 16 };

			cameraList = new List<Camera>();
			rendererList = new List<Renderer>();
			lightList = new List<Light>();
			light2DList = new List<Light2D>();
			
			GL.Enable(EnableCap.Texture2D);
			GL.CullFace(CullFaceMode.Back);
			GL.ClearDepth(1f);
			GL.DepthFunc(DepthFunction.Lequal);

			InstantiateRenderingPipeline();

			PrimitiveMeshes.GenerateDefaultMeshes();
			
			whiteTexture = new Texture(1,1);
		}

		internal static void Render()
		{
			drawCallsCount = 0;

			//Calculate view and projection matrices, culling frustums
			for(int i = 0;i<cameraList.Count;i++) {
				var camera = cameraList[i];

				var viewSize = camera.ViewPixel;
				float aspectRatio = viewSize.width/(float)viewSize.height;

				var cameraTransform = camera.Transform;
				var cameraPosition = cameraTransform.Position;

				camera.matrix_view = Matrix4x4.LookAt(cameraPosition,cameraPosition+cameraTransform.Forward,cameraTransform.Up);

				if(camera.orthographic) {
					float max = Mathf.Max(Screen.Width,Screen.Height);
					camera.matrix_proj = Matrix4x4.CreateOrthographic(Screen.Width/max*camera.orthographicSize,Screen.Height/max*camera.orthographicSize,camera.nearClip,camera.farClip);
				}else{
					camera.matrix_proj = Matrix4x4.CreatePerspectiveFOV(camera.fov*Mathf.Deg2Rad,aspectRatio,camera.nearClip,camera.farClip);
				}

				camera.matrix_viewInverse = Matrix4x4.Invert(camera.matrix_view);
				camera.matrix_projInverse = Matrix4x4.Invert(camera.matrix_proj);
				camera.CalculateFrustum(camera.matrix_view*camera.matrix_proj);
			}

			//Clear buffers
			//GL.Enable(EnableCap.StencilTest);
			GL.Viewport(0,0,Screen.Width,Screen.Height);

			if(renderingPipeline.Framebuffers!=null) {
				int length = renderingPipeline.Framebuffers.Length;

				for(int i = 0;i<=length;i++) {
					var framebuffer = i==length ? null : renderingPipeline.Framebuffers[i];
					framebuffer?.PrepareAttachments();
					Framebuffer.Bind(framebuffer);

					GL.ClearColor(0f,0f,0f,0f);
					//GL.StencilMask(~0);
					GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
				}
			}

			renderingPipeline.PreRender();

			//Render passes
			for(int i = 0;i<renderingPipeline.RenderPasses.Length;i++) {
				var pass = renderingPipeline.RenderPasses[i];
				if(pass.enabled) {
					pass.Render();
				}
			}

			Framebuffer.Bind(null);

			renderingPipeline.PostRender();

			//GL.Disable(EnableCap.StencilTest);

#region RenderTargetDebug
			if(Input.GetKey(Keys.F)) {
				//TODO: This is completely temporarily
				static bool IsDepthTexture(RenderTexture tex) => tex.name.Contains("depth");

				var framebuffers = renderingPipeline.framebuffers;

				int textureCount = 0;
				for(int i = 0;i<framebuffers.Length;i++) {
					var fb = framebuffers[i];

					for(int j = 0;j<fb.renderTextures.Length;j++) {
						var tex = fb.renderTextures[j];

						if(!IsDepthTexture(tex)) {
							textureCount++;
						}
					}
				}

				if(Input.GetKey(Keys.LShift)) {
					textureCount = Math.Min(textureCount,1);
				}

				int size = 1;
				while(size*size<textureCount) {
					size++;
				}
				
				int x = 0;
				int y = 0;
				for(int i = 0;i<framebuffers.Length;i++) {
					var framebuffer = framebuffers[i];

					Framebuffer.Bind(framebuffer,Framebuffer.Target.ReadFramebuffer);

					for(int j = 0;j<framebuffer.renderTextures.Length;j++) {
						var tex = framebuffer.renderTextures[j];

						if(IsDepthTexture(tex)) {
							continue;
						}

						GL.ReadBuffer((ReadBufferMode)((int)ReadBufferMode.ColorAttachment0+j));

						int wSize = Screen.width/size;
						int hSize = Screen.height/size;
						GL.BlitFramebuffer(
							0,0,tex.Width,tex.Height,
							x*wSize,(size-y-1)*hSize,(x+1)*wSize,(size-y)*hSize,
							ClearBufferMask.ColorBufferBit,
							BlitFramebufferFilter.Nearest
						);

						if(++x>=size) {
							x = 0;
							y++;
						}
					}
				}
			}
#endregion

			GL.Flush();
			//window.SwapBuffers(); //TODO: After upgrading to OpenTK 4.0, see if fullscreen is still bugged on multi-monitor setup when using this SwapBuffers() call instead of GL.Finish/Flush().

			Rendering.CheckGLErrors();
		}
		
		public static void SetRenderingPipeline<T>() where T : RenderingPipeline, new()
		{
			renderingPipelineType = typeof(T);

			if(renderingPipeline!=null) {
				InstantiateRenderingPipeline();
			}
		}

		internal static void InstantiateRenderingPipeline()
		{
			renderingPipeline?.Dispose();

			renderingPipeline = (RenderingPipeline)Activator.CreateInstance(renderingPipelineType);
			renderingPipeline.Init();
		}
		internal static void Resize(object sender,EventArgs e)
		{
			Screen.UpdateValues(window);

			//InstantiateRenderingPipeline();
		}
		internal static void Dispose() {}
	}
}
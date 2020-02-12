﻿using System;
using Dissonance.Framework.OpenGL;

namespace GameEngine.Graphics
{
	public class BoneWeightsAttribute : CustomVertexAttribute<BoneWeightsBuffer>
	{
		public override void Init(out string nameId,out VertexAttribPointerType pointerType,out bool isNormalized,out int size,out int offset)
		{
			nameId = "boneWeights";
			pointerType = VertexAttribPointerType.Float;
			isNormalized = false;
			size = 4;
			offset = sizeof(int)*4;
		}
	}
}
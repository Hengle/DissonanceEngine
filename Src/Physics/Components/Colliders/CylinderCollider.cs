/*using BulletSharp;
using Dissonance.Engine.Utils.Extensions;

namespace Dissonance.Engine.Physics
{
	public class CylinderCollider : Collider
	{
		protected Vector3 size = Vector3.One;
		public Vector3 Size {
			get => size;
			set {
				if(size!=value) {
					size = value;

					TryUpdateCollider();
				}
			}
		}

		internal override void UpdateCollider()
		{
			if(collShape!=null) {
				collShape.Dispose();
				collShape = null;
			}

			collShape = new CylinderShape(size.ToBulletVector3()*0.5f);

			base.UpdateCollider();
		}
	}
}*/

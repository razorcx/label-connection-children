using System.Collections.Generic;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace LabelConnectionChildren
{
	public class Program
	{
		static void Main(string[] args)
		{
			var connection = new Picker()
				.PickObject(Picker.PickObjectEnum.PICK_ONE_OBJECT) as Connection;
			if (connection == null) return;

			var children = GetModelObjectsAsList(connection.GetChildren());

			var drawer = new GraphicsDrawer();
			var color = new Color(0, 1, 1);

			children.ForEach(c =>
			{
				var part = c as Part;
				var location = part?.GetCenterLine(false)[0] as Point;
				var partMark = part?.GetPartMark();
				drawer.DrawText(location, partMark, color);
			});
		}

		private static List<ModelObject>
			GetModelObjectsAsList(ModelObjectEnumerator enumerator)
		{
			var modelObjects = new List<ModelObject>();
			while (enumerator.MoveNext())
			{
				var modelObject = enumerator.Current;
				modelObjects.Add(modelObject);
			}

			return modelObjects;
		}
	}
}

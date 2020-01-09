namespace DynamicLight2D
{
	using UnityEngine;
	using UnityEditor;
	using System.Collections;
	
	public class DynamicLightDrawGizmo{

		[DrawGizmo(GizmoType.NonSelected | GizmoType.NotInSelectionHierarchy)]
		private static void drawGizmoNow(DynamicLight dl, GizmoType gizmoType)
		{
			Gizmos.DrawIcon(dl.transform.position, "logo2DDL_gizmos.png", false);
		}

	}
}

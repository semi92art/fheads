	/*ReorderableList CreateList (SerializedObject obj, SerializedProperty prop)
	{
		ReorderableList list = new ReorderableList (obj, prop, true, true, true, true);

		list.drawHeaderCallback = rect => {
			EditorGUI.LabelField (rect, "Sprites");	
		};

		List<float> heights = new List<float> (prop.arraySize);

		list.drawElementCallback = (rect, index, active, focused) => {
			SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex (index);
			Sprite s = (element.objectReferenceValue as Sprite);

			bool foldout = active;
			float height = EditorGUIUtility.singleLineHeight * 1.25f;
			if (foldout) {
				height = EditorGUIUtility.singleLineHeight * 5;
			}

			try {
				heights [index] = height;
			} catch (ArgumentOutOfRangeException e) {
				Debug.LogWarning (e.Message);
			} finally {
				float[] floats = heights.ToArray ();
				Array.Resize (ref floats, prop.arraySize);
				heights = floats.ToList ();
			}

			float margin = height / 10;
			rect.y += margin;
			rect.height = (height / 5) * 4;
			rect.width = rect.width / 2 - margin / 2;

			if (foldout) {
				if (s) {
					EditorGUI.DrawPreviewTexture (rect, s.texture);
				}
			}
			rect.x += rect.width + margin;
			EditorGUI.ObjectField (rect, element, GUIContent.none);
		};

		list.elementHeightCallback = (index) => {
			Repaint ();
			float height = 0;

			try {
				height = heights [index];
			} catch (ArgumentOutOfRangeException e) {
				Debug.LogWarning (e.Message);
			} finally {
				float[] floats = heights.ToArray ();
				Array.Resize (ref floats, prop.arraySize);
				heights = floats.ToList ();
			}

			return height;
		};

		list.drawElementBackgroundCallback = (rect, index, active, focused) => {
			rect.height = heights [index];
			Texture2D tex = new Texture2D (1, 1);
			tex.SetPixel (0, 0, new Color (0.33f, 0.66f, 1f, 0.66f));
			tex.Apply ();
			if (active)
				GUI.DrawTexture (rect, tex as Texture);
		};

		list.onAddDropdownCallback = (rect, li) => {
			var menu = new GenericMenu ();
			menu.AddItem (new GUIContent ("Add Element"), false, () => {
				serializedObject.Update ();
				li.serializedProperty.arraySize++;
				serializedObject.ApplyModifiedProperties ();
			});

			menu.ShowAsContext ();

			float[] floats = heights.ToArray ();
			Array.Resize (ref floats, prop.arraySize);
			heights = floats.ToList ();
		};

		return list;
	}*/
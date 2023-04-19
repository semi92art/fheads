using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;

namespace SA.Android.Editor
{
    class CSharpFileCodeGen
    {
        string m_Namespace;
        readonly string m_FilePath;
        readonly List<AN_ClassCodeGen> m_Classes = new List<AN_ClassCodeGen>();

        readonly AN_CodeBuilder m_CodeBuilder = new AN_CodeBuilder();

        public CSharpFileCodeGen(string filePath)
        {
            m_FilePath = filePath;
        }

        public void SetNamespace(string @namespace)
        {
            m_Namespace = @namespace;
        }

        public void AddClass(AN_ClassCodeGen @class)
        {
            m_Classes.Add(@class);
        }

        public void Save()
        {
            m_CodeBuilder
                .AppendLine($"namespace {m_Namespace}")
                .OpenBraces();

            foreach (var generatedClass in m_Classes)
            {
                generatedClass.Build(m_CodeBuilder);
            }

            m_CodeBuilder.CloseBraces();

            var fullPath = Path.GetFullPath(m_FilePath);
            var encoding = new UTF8Encoding(true);
            File.WriteAllText(fullPath, m_CodeBuilder.ToString(), encoding);

            // Import the asset
            AssetDatabase.ImportAsset(m_FilePath);
        }
    }
}

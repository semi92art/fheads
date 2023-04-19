using System.Collections.Generic;

namespace SA.Android.Editor
{
    class AN_ClassCodeGen
    {
        readonly string m_Name;
        readonly Dictionary<string, string> m_Consts = new Dictionary<string, string>();
        readonly List<AN_ClassCodeGen> m_NestedClasses = new List<AN_ClassCodeGen>();

        public AN_ClassCodeGen(string name)
        {
            m_Name = name;
        }

        public void AddConst(string name, string value)
        {
            m_Consts.Add(name, value);
        }

        public void AddNestedClass(AN_ClassCodeGen @class)
        {
            m_NestedClasses.Add(@class);
        }

        public AN_CodeBuilder Build(AN_CodeBuilder codeBuilder)
        {
            codeBuilder
                .AppendLine($"public static class {m_Name}")
                .OpenBraces();

            foreach (var kvp in m_Consts)
            {
                codeBuilder.AppendLine($"public const string {kvp.Key} = \"{kvp.Value}\";");
            }

            foreach (var nestedClass in m_NestedClasses)
            {
                codeBuilder.AppendLine();
                nestedClass.Build(codeBuilder);
            }

            codeBuilder.CloseBraces();

            return codeBuilder;
        }
    }
}

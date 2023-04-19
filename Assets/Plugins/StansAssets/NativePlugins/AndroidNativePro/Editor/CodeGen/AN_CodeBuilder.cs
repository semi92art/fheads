using System.Text;

namespace SA.Android.Editor
{
    public class AN_CodeBuilder
    {
        const int k_IndentSpacesSize = 4;
        
        int m_Indent = 0;
        readonly StringBuilder m_StringBuilder = new StringBuilder();

        public AN_CodeBuilder AppendLine()
        {
            m_StringBuilder.AppendLine();
            return this;
        }
        public AN_CodeBuilder AppendLine(string val)
        {
            var indentStrings = new string(' ', m_Indent * k_IndentSpacesSize);
            m_StringBuilder.AppendLine(indentStrings + val);
            return this;
        }

        public AN_CodeBuilder OpenBraces()
        {
            AppendLine("{");
            m_Indent++;
            return this;
        }
        
        public AN_CodeBuilder CloseBraces()
        {
            m_Indent--;
            AppendLine("}");
            
            return this;
        }

        public AN_CodeBuilder Append(string val)
        {
            m_StringBuilder.Append(val);
            return this;
        }
        
        public override string ToString()
        {
            return m_StringBuilder.ToString();
        }
    }
}

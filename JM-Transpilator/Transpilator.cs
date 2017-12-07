using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM_Transpilator
{
    public static class Transpilator
    {
        public static string Transpilate(string incoming)
        {
            string text = "";

            string name = Split(Split(incoming, "class")[1], "Controller")[0];
            string[] methods = Split(Split(incoming, "class")[1], "[Http");

            #region Usings
            text += "using System;\r\n";
            text += "using System.Collections.Generic;\r\n";
            text += "using System.Linq;\r\n";
            text += "using Microsoft.AspNetCore.Http;\r\n";
            text += "using Microsoft.AspNetCore.Mvc;\r\n";
            text += "using netcore_star.Models;\r\n";
            text += "using netcore_star.Exceptions;\r\n";
            text += "using netcore_star.Services;\r\n\r\n";
            #endregion

            text += "namespace netcore_star.Controllers\r\n";
            text += "{\r\n";

            text += "\t///<summary>\r\n";
            text += "\t/// Controller for the " + name + "\r\n";
            text += "\t///</summary>";
            text += "\t[Route(\"api/[controller]\")]\r\n";
            text += "\tpublic class " + name + "Controller : BaseController\r\n";
            text += "\t{\r\n";

            #region Constructor
            text += "\t\tpublic " + name + "Controller(IHttpContextAccessor httpContextAccessor) : base (httpContextAccessor)\r\n";
            text += "\t\t{ }\r\n\r\n";
            #endregion

            #region Service
            text += "\t\tprotected " + name + "Service GetService()\r\n";
            text += "\t\t{\r\n";
            text += "\t\treturn new " + name + "Service(GetRequestUserHostAddress(), GetRequestUserHostName());\r\n";
            text += "\t\t}\r\n\r\n";
            #endregion

            #region Body
            for(int i = 0; i < methods.Length; i++)
            {
                string[] lines = Split(methods[i], "\r\n");

                text += SetUpMethodHeader(lines[0]);
            }
            #endregion

            text += "\t}\r\n";
            text += "}\r\n";


            StreamWriter fileGenerated = File.AppendText(name + "Controller.cs");

            fileGenerated.WriteLine(text);

            fileGenerated.Close();

            return text;
        }

        private static string[] Split(string toSplit, string separator)
        {
            return toSplit.Split(new string[] { separator }, StringSplitOptions.None);
        }

        private static string SetUpMethodHeader(string line)
        {
            string result = "";
            line = line.Replace(", EnableQuery", "");

            if (line.Contains("ResponseType"))
            {
                string type = Split(Split(line, "ResponseType(typeof(")[1], ")")[1];
                //result = 
            }

            return result;
        }
    }
}

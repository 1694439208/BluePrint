using System;
using System.Collections.Generic;
using System.Text;

namespace 蓝图重制版.BluePrint.Runtime
{
    public class CodeGenerator
    {
        public static string Generator(NodeAst nodeAst) {
            return Calculated(nodeAst);
        }
        private static string Calculated(NodeAst nodeAst)
        {
            var ret = "";
            switch (nodeAst.NodeToken)
            {
                case Token.NodeToken.Expression:
                    List<string> PrevNodes1 = new List<string>();
                    foreach (var item in nodeAst.PrevNodes)
                    {
                        PrevNodes1.Add(Calculated(item));
                    }
                    ret = string.Join(";\r\n", PrevNodes1);
                    break;
                case Token.NodeToken.Call:
                    List<string> NextNodesCode = new List<string>();
                    foreach (var item in nodeAst.NextNodes)
                    {
                        NextNodesCode.Add(Calculated(item));
                    }
                    List<string> Arguments = new List<string>();
                    foreach (var item in nodeAst.Arguments)
                    {
                        Arguments.Add($"temp_{item.NodeJoinId}");
                    }
                    List<string> Results = new List<string>();
                    foreach (var item in nodeAst.Results)
                    {
                        Results.Add($"temp_{item.NodeJoinId}");
                    }

                    List<string> PrevNodes = new List<string>();
                    foreach (var item in nodeAst.Arguments)
                    {
                        PrevNodes.Add(Calculated(item));
                    }
                    ret = nodeAst.NodeBase.CodeTemplate(NextNodesCode, PrevNodes, Arguments, Results);
                    break;
                case Token.NodeToken.Value:
                    break;
                case Token.NodeToken.None:
                    break;
                default:
                    break;
            }
            return ret;
        }
    }
}

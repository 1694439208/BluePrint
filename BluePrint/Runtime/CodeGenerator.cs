using System;
using System.Collections.Generic;
using System.Text;
using 蓝图重制版.BluePrint.IJoin;

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
                    List<ParameterAST> Arguments = new List<ParameterAST>();
                    foreach (var item in nodeAst.Arguments)
                    {
                        Arguments.Add(new ParameterAST {
                            ID = item.NodeJoinId, 
                            Join = item.Join,
                            IsThis = item.Isthis,
                        });
                    }
                    List<ParameterAST> Results = new List<ParameterAST>();
                    foreach (var item in nodeAst.Results)
                    {
                        Results.Add(new ParameterAST
                        {
                            ID = item.NodeJoinId,
                            Join = item.Join,
                            IsThis = item.Isthis,
                        });
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

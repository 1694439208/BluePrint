﻿using System;
using System.Collections.Generic;
using System.Text;
using 蓝图重制版.BluePrint.Node;

namespace 蓝图重制版.BluePrint.Runtime
{
    public class NodeParse
    {
        public BParent bParent;
        public NodeParse(BParent parent) {
            bParent = parent;
        }
        /// <summary>
        /// 实际上我们只需要遍历节点所有执行线就可以生成ast
        /// </summary>
        /// <param name="ast">ast</param>
        /// <param name="node">起始的执行节点</param>
        /// <returns></returns>
        public NodeAst Parser(INode.NodeBase node) {
            NodeAst ast = new NodeAst();
            ast.NodeJoinId = node.GetHashCode();
            ast.NodeToken = Token.NodeToken.Call;
            ast.NodeBase = node;

            foreach (var item in node._IntPutJoin)
            {
                //如果类型是可执行节点 啥也不干，因为输入的可执行被上一个节点记录了
                //如果类型是输入参数
                if (item.Item1.GetNodeType() == Token.NodeToken.Value) {
                    var line1 = bParent.bluePrint.FildIutJoin(item.Item1);
                    if (line1.Count > 0)
                    {
                        foreach (var item1 in line1)
                        {
                            var Join = (item1.GetStarJoin() as IJoinControl);
                            var Prev = Join.Get_NodeRef() as INode.NodeBase;
                            if (Prev._IntPutJoin.Exists(v => v.Item1.GetNodeType() == Token.NodeToken.Call) ||
                                Prev._OutPutJoin.Exists(v => v.Item1.GetNodeType() == Token.NodeToken.Call))
                            {
                                //如果输入的节点是有执行线，那就停止向上遍历，并且记录指针
                                ast.Arguments.Add(new NodeAst
                                {
                                    NodeToken = Token.NodeToken.Expression,
                                    NodeJoinId = Join.GetHashCode(),//当前输入引用输出的指针
                                    Join = Join,
                                    Isthis = false,
                                });
                            }
                            else
                            {
                                //如果没有执行线，那就继续向上遍历
                                ast.Arguments.Add(new NodeAst
                                {
                                    NodeToken = Token.NodeToken.Expression,
                                    NodeJoinId = Join.GetHashCode(),//当前输入引用输出的指针
                                    PrevNodes = { Parser(Prev as INode.NodeBase) },
                                    Join = Join,
                                    Isthis = false,
                                });
                            }
                        }
                    }
                    else {
                        //没有任何输入连接，那就以它自身为准
                        ast.Arguments.Add(new NodeAst
                        {
                            NodeToken = Token.NodeToken.Value,
                            NodeJoinId = item.Item1.GetHashCode(),//当前输入引用输出的指针
                            Join = item.Item1,
                            Isthis = true,
                        });
                    }
                    
                }
            }
            //遍历输出节点
            foreach (var item in node._OutPutJoin)
            {
                //如果类型是可执行节点
                if (item.Item1.GetNodeType() == Token.NodeToken.Call)
                {
                    //获取下一个可执行节点引用然后继续生成ast
                    var line = bParent.bluePrint.FildOutJoin(item.Item1);
                    foreach (var item1 in line)
                    {
                        var nextnode = (item1.GetEndJoin() as IJoinControl).Get_NodeRef();
                        ast.NextNodes.Add(Parser(nextnode as INode.NodeBase));
                    }
                    if (line.Count <= 0)
                    {
                        //如果没有那就设置为null
                        ast.NextNodes.Add(null);
                    }
                }
                //如果类型是参数
                if (item.Item1.GetNodeType() == Token.NodeToken.Value)
                {
                    //输出参数只需要把输出值绑定到上下文就ok
                    ast.Results.Add(new NodeAst { 
                        NodeToken = Token.NodeToken.Value,
                        NodeJoinId = item.Item1.GetHashCode(),
                        Join = item.Item1,
                    });
                }
            }
            return ast;
        }
        public void test() {
            NodeAst nodeAst = Parser(new INode._StartNode(null));
        }
    }
}

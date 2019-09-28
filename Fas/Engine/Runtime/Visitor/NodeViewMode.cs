// Copyright 2004-2010 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Fas.Tem.Runtime.Visitor
{
    using Fas.Tem.Runtime.Parser.Node;
    using Parser;
    using System;
    using System.Text;

    /// <summary> This class is simply a visitor implementation
    /// that traverses the AST, produced by the Velocity
    /// parsing process, and creates a visual structure
    /// of the AST. This is primarily used for
    /// debugging, but it useful for documentation
    /// as well.
    /// *
    /// </summary>
    /// <author> <a href="mailto:jvanzyl@apache.org">Jason van Zyl</a>
    /// </author>
    /// <version> $Id: NodeViewMode.cs,v 1.3 2003/10/27 13:54:11 corts Exp $
    ///
    /// </version>
    public class NodeViewMode : BaseVisitor
    {
        private int indent = 0;
        private readonly bool showTokens = true;

        /// <summary>Indent child nodes to help visually identify
        /// the structure of the AST.
        /// </summary>
        private string IndentString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < indent; ++i)
            {
                sb.Append("  ");
            }
            return sb.ToString();
        }

        /// <summary> Display the type of nodes and optionally the
        /// first token.
        /// </summary>
        private object ShowNode(INode node, object data)
        {
            string tokens = string.Empty;
            string special = string.Empty;
            Token t;

            if (showTokens)
            {
                t = node.FirstToken;

                if (t.SpecialToken != null && !t.SpecialToken.Image.StartsWith("##"))
                {
                    special = t.SpecialToken.Image;
                }

                tokens = string.Format(" -> {0}{1}", special, t.Image);
            }

            Console.Out.WriteLine(IndentString() + node + tokens);
            ++indent;
            data = node.ChildrenAccept(this, data);
            --indent;
            return data;
        }

        /// <summary>Display a SimpleNode
        /// </summary>
        public override object Visit(SimpleNode node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTprocess node
        /// </summary>
        public override object Visit(ASTprocess node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTExpression node
        /// </summary>
        public override object Visit(ASTExpression node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTAssignment node ( = )
        /// </summary>
        public override object Visit(ASTAssignment node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTOrNode ( || )
        /// </summary>
        public override object Visit(ASTOrNode node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTAndNode ( &amp;&amp; )
        /// </summary>
        public override object Visit(ASTAndNode node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTEQNode ( == )
        /// </summary>
        public override object Visit(ASTEQNode node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTNENode ( != )
        /// </summary>
        public override object Visit(ASTNENode node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTLTNode ( &lt; )
        /// </summary>
        public override object Visit(ASTLTNode node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTGTNode ( &gt; )
        /// </summary>
        public override object Visit(ASTGTNode node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTLENode ( &lt;= )
        /// </summary>
        public override object Visit(ASTLENode node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTGENode ( &gt;= )
        /// </summary>
        public override object Visit(ASTGENode node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTAddNode ( + )
        /// </summary>
        public override object Visit(ASTAddNode node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTSubtractNode ( - )
        /// </summary>
        public override object Visit(ASTSubtractNode node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTMulNode ( * )
        /// </summary>
        public override object Visit(ASTMulNode node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTDivNode ( / )
        /// </summary>
        public override object Visit(ASTDivNode node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTModNode ( % )
        /// </summary>
        public override object Visit(ASTModNode node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTNotNode ( ! )
        /// </summary>
        public override object Visit(ASTNotNode node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTNumberLiteral node
        /// </summary>
        public override object Visit(ASTNumberLiteral node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTStringLiteral node
        /// </summary>
        public override object Visit(ASTStringLiteral node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTIdentifier node
        /// </summary>
        public override object Visit(ASTIdentifier node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTMethod node
        /// </summary>
        public override object Visit(ASTMethod node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTReference node
        /// </summary>
        public override object Visit(ASTReference node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTTrue node
        /// </summary>
        public override object Visit(ASTTrue node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTFalse node
        /// </summary>
        public override object Visit(ASTFalse node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTBlock node
        /// </summary>
        public override object Visit(ASTBlock node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTText node
        /// </summary>
        public override object Visit(ASTText node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTIfStatement node
        /// </summary>
        public override object Visit(ASTIfStatement node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTElseStatement node
        /// </summary>
        public override object Visit(ASTElseStatement node, object data)
        {
            return ShowNode(node, data);
        }

        /// <summary>Display an ASTElseIfStatement node
        /// </summary>
        public override object Visit(ASTElseIfStatement node, object data)
        {
            return ShowNode(node, data);
        }

        public override object Visit(ASTObjectArray node, object data)
        {
            return ShowNode(node, data);
        }

        public override object Visit(ASTDirective node, object data)
        {
            return ShowNode(node, data);
        }

        public override object Visit(ASTWord node, object data)
        {
            return ShowNode(node, data);
        }

        public override object Visit(ASTSetDirective node, object data)
        {
            return ShowNode(node, data);
        }
    }
}
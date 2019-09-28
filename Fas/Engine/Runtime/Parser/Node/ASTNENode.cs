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

namespace Fas.Tem.Runtime.Parser.Node
{
    using Context;

    public class ASTNENode : SimpleNode
    {
        public ASTNENode(int id) : base(id)
        {
        }

        public ASTNENode(Parser p, int id) : base(p, id)
        {
        }

        /// <summary>
        /// Accept the visitor.
        /// </summary>
        public override object Accept(IParserVisitor visitor, object data)
        {
            return visitor.Visit(this, data);
        }

        public override bool Evaluate(IInternalContextAdapter context)
        {
            object left = GetChild(0).Value(context);
            object right = GetChild(1).Value(context);

            try
            {
                return ObjectComparer.CompareObjects(left, right) != 0;
            }
            catch
            {
                // Ignore, we can't compare decently by value, but we honestly don't give a sh*t
            }

            // If we can't actually compare the objects, try the operator as fallback
            // For operator overloaded types, this will not really be a reference comp, but that's ok.
            return left != right;
        }
    }
}
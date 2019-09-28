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

namespace Fas.Tem.Runtime.Directive
{
    using Context;
    using Fas.Tem.Runtime.Parser.Node;
    using System.IO;

    public class Component : Directive
    {
        public override bool Render(IInternalContextAdapter context, TextWriter writer, INode node)
        {
            return true;
        }

        public override string Name
        {
            get => "component";
            set { }
        }

        public override DirectiveType Type => DirectiveType.LINE;
    }
}
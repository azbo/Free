﻿using Fas.Tem.Context;
using Fas.Tem.Runtime.Parser.Node;
using System;
using System.IO;

namespace Fas.Tem.Runtime.Directive
{
    public class Break : Directive
    {

        public override string Name
        {
            get => "break";
            set => throw new NotSupportedException();
        }

        /**
         * Return type of this directive.
         * @return The type of this directive.
         */
        public override DirectiveType Type => DirectiveType.LINE;

        public override bool AcceptParams => false;


        /**
         * Break directive does not actually do any rendering. 
         * 
         * This directive throws a BreakException (RuntimeException) which
         * signals foreach directive to break out of the loop. Note that this
         * directive does not verify that it is being called inside a foreach
         * loop.
         * 
         * @param context
         * @param writer
         * @param node
         * @return true if the directive rendered successfully.
         * @throws IOException
         * @throws MethodInvocationException
         * @throws ResourceNotFoundException
         * @throws ParseErrorException
         */
        public override bool Render(IInternalContextAdapter context, TextWriter writer, INode node)
        {
            throw new BreakException();
        }
    }

    public class BreakException : IOException
    {

    }
}

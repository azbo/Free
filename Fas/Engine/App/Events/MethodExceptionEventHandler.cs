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

namespace Fas.Tem.App.Events
{
    using System;

    ///// <summary>
    ///// Called when a method throws an exception.  This gives the
    ///// application a chance to deal with it and either
    ///// return something nice, or throw.
    ///// 
    ///// Please return what you want rendered into the output stream.
    ///// </summary>
    //public delegate void MethodExceptionEventHandler(Object sender, MethodExceptionEventArgs e);

    public class MethodExceptionEventArgs : EventArgs
    {
        private object valueToRender;
        private readonly Exception exceptionThrown;
        private readonly Type targetClass;
        private readonly string targetMethod;

        public MethodExceptionEventArgs(Type targetClass, string targetMethod, Exception exceptionThrown)
        {
            this.targetClass = targetClass;
            this.targetMethod = targetMethod;
            this.exceptionThrown = exceptionThrown;
        }

        public object ValueToRender
        {
            get => valueToRender;
            set => valueToRender = value;
        }

        public Exception ExceptionThrown => exceptionThrown;

        public Type TargetClass => targetClass;

        public string TargetMethod => targetMethod;
    }
}
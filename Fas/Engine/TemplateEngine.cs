using Commons.Collections;
using Fas.Tem;
using Fas.Tem.App;
using Fas.Tem.Context;
using System;
using System.Collections;
using System.IO;

namespace Fas.Engine
{
    public class TemplateEngine
    {
        public void BeginInit()
        {
            vengine = new VelocityEngine();
            ExtendedProperties extendedProperties = new ExtendedProperties();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            extendedProperties.SetProperty("resource.loader", "file");
            extendedProperties.SetProperty("file.resource.loader.path", baseDirectory);
            extendedProperties.SetProperty("file.resource.loader.cache", "true");
            FileInfo fileInfo = new FileInfo(Path.Combine(baseDirectory, "nvelocity.properties"));
            if (fileInfo.Exists)
            {
                using (Stream stream = fileInfo.OpenRead())
                {
                    extendedProperties.Load(stream);
                }
            }
            vengine.Init(extendedProperties);
        }

        public bool HasTemplate(string templateName)
        {
            if (vengine == null)
            {
                throw new InvalidOperationException("Template Engine not yet initialized.");
            }
            bool result;
            try
            {
                vengine.GetTemplate(templateName);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public bool Process(IDictionary context, string templateName, TextWriter output)
        {
            if (vengine == null)
            {
                throw new InvalidOperationException("Template Engine not yet initialized.");
            }
            Template template = vengine.GetTemplate(templateName);
            template.Merge(CreateContext(context), output);
            return true;
        }

        public bool Process(IDictionary context, string templateName, TextWriter output, string inputTemplate)
        {
            return Process(context, templateName, output, new StringReader(inputTemplate));
        }

        public bool Process(IDictionary context, string templateName, TextWriter output, TextReader inputTemplate)
        {
            if (vengine == null)
            {
                throw new InvalidOperationException("Template Engine not yet initialized.");
            }
            this.context = CreateContext(context);
            return vengine.Evaluate(this.context, output, templateName, inputTemplate);
        }

        private IContext CreateContext(IDictionary context)
        {
            return new VelocityContext(new Hashtable(context));
        }

        public void PutContextData(string key, object obj)
        {
            context.Put(key, obj);
        }

        private VelocityEngine vengine;

        private IContext context;
    }
}
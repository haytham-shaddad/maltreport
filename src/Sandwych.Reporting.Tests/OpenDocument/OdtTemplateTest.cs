﻿using System;
using System.Collections.Generic;
using Sandwych.Reporting.OpenDocument;
using Sandwych.Reporting.Tests.Common;
using Xunit;

namespace Sandwych.Reporting.Tests.OpenDocument
{

    public class OdtTemplateTest
    {
        private const string Template1OdtName = "Sandwych.Reporting.Tests.OpenDocument.Templates.Template1.odt";

        [Fact]
        public void CanCompileOdtDocumentTemplate()
        {
            using (var stream = DocumentTestHelper.GetResource(Template1OdtName))
            {
                var odt = new OdfDocument();
                odt.Load(stream);
                var template = new OdtTemplate(odt);
            }
        }

        [Fact]
        public void CanRenderOdtTemplate()
        {
            OdfTemplate template;
            using (var stream = DocumentTestHelper.GetResource(Template1OdtName))
            {
                template = new OdtTemplate(stream);
            }

            var dataSet = new TestingDataSet();
            var values = new Dictionary<string, object>()
            {
                { "table1", dataSet.Table1 },
                { "so", dataSet.SimpleObject },
            };
            var context = new TemplateContext(values);

            var result = template.Render(context);

            result.Save(@"c:\tmp\out.odt");
        }

    }
}
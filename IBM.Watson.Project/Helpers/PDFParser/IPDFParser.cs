﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBM.Watson.Project.Helpers.PDFParser
{
    public interface IPDFParser
    {
        string ExtractTextFromPdf(string path);
    }
}

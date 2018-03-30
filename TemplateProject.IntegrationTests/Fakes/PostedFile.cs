using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DonorPortal.IntegrationTest.Fakes
{
    public class PostedFile : HttpPostedFileBase
    {
        Stream stream;
        string contentType;
        string fileName;

        public PostedFile(Stream stream, string fileName)
        {
            this.stream = stream;
            this.fileName = fileName;
        }

        public override int ContentLength => (int)stream.Length;

        public override string ContentType => contentType;

        public override string FileName => fileName;

        public override Stream InputStream => stream;

        public override void SaveAs(string filename)
        {
            using (var file = File.Open(filename, FileMode.CreateNew))
                stream.CopyTo(file);
        }
    }
}

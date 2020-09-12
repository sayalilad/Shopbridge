using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace attempt_api.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        /*public string Get(int id)
        {
            return "value";
        }*/
        [HttpGet]
        public HttpResponseMessage GetImage(string fileName)
        {
            //Create HTTP Response.
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            //Set the File Path.
            string filePath = "";
            if (!fileName.Contains("thumb"))
                filePath = HttpContext.Current.Server.MapPath("~/Images/") + fileName;
            else
                filePath = HttpContext.Current.Server.MapPath("~/Thumbnails/") + fileName;

            //Check whether File exists.
            if (!File.Exists(filePath))
            {
                //Throw 404 (Not Found) exception if File not found.
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", fileName);
                throw new HttpResponseException(response);
            }

            //Read the File into a Byte Array.
            byte[] bytes = File.ReadAllBytes(filePath);

            //Set the Response Content.
            response.Content = new ByteArrayContent(bytes);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(fileName));
            return response;
        }

        // POST api/values
       /* public void Post([FromBody]string value)
        {
        }*/

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        public HttpResponseMessage PostImage()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Images/")))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Images/"));
            }
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Thumbnails/")))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Thumbnails/"));
            }
            foreach (string item in httpRequest.Files)
            {
                var postedfile = httpRequest.Files[item];
                var filepath = HttpContext.Current.Server.MapPath("~/Images/" + postedfile.FileName);
                postedfile.SaveAs(filepath);
                try
                {
                    Image image = Image.FromFile(filepath);
                    Image thumb = image.GetThumbnailImage(90, 120, () => false, IntPtr.Zero);
                    string thumb_fname = Path.GetFileNameWithoutExtension(postedfile.FileName) + "_thumb.jpg";

                    var filepath_thumb = HttpContext.Current.Server.MapPath("~/Thumbnails/" + thumb_fname);
                    thumb.Save(filepath_thumb);
                }
                catch (Exception er)
                {
                    Console.WriteLine(er.ToString());
                   // throw;
                }
            }
            return Request.CreateResponse(HttpStatusCode.Created);
        }


    }
}

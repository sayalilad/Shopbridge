using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Web.Util;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Net.Http.Formatting;
using System.IO;

namespace WebApplication1.Controllers
{
    public class ShopBridge_InventoryController : Controller
    {
        // GET: ShopBridge_Inventory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _ListOfItems()
        {
            try
            {
                #region attempt 1
                IEnumerable<Shopbridge_inventoryModel> item_ = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44394/api/");
                    var responseTask = client.GetAsync("Shopbridge_inventory_1");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readjob = result.Content.ReadAsAsync<IEnumerable<Shopbridge_inventoryModel>>();
                        readjob.Wait();
                        item_ = readjob.Result;
                    }
                    else
                    {
                        item_ = Enumerable.Empty<Shopbridge_inventoryModel>();
                        ModelState.AddModelError(string.Empty, "Server Error");
                    }
                }
                #endregion
                return PartialView(item_);
            }
            catch (Exception ex)
            {

                return View("Index");
            }
        }

      

  

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public ActionResult GetDetails(string id)
        {
            #region attempt1
            Shopbridge_inventoryModel item_ = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");
                var responseTask = client.GetAsync("Shopbridge_inventory_1/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readjob = result.Content.ReadAsAsync<Shopbridge_inventoryModel>();
                    readjob.Wait();
                    item_ = readjob.Result;
                }
                else
                {
                    item_ = new Shopbridge_inventoryModel();
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }
            #endregion
            return View("Details",item_);
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(Shopbridge_inventoryModel obj)
        {
            HttpPostedFileBase file = Request.Files["image_name"];
            if (file!=null && !string.IsNullOrEmpty(file.FileName))
            {
                obj.image_name = file.FileName;
                obj.thumb_name = file.FileName.Split('.')[0] + "_thumb.jpg";
                UploadFiles(file);
            }
            else
            {
                obj.image_name = "";
                obj.thumb_name = "";
            }

            var json = new JavaScriptSerializer().Serialize(obj);


            using (var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json"))
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");

                var postJob = client.PostAsync(client.BaseAddress + "shopbridge_inventory_1", stringContent);
                postJob.Wait();

                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                   // ModelState.Clear();
                    return RedirectToAction("_ListOfItems");
                }
            }
            return View("Index");
        }




        public ActionResult DeleteItem(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");
                var deleteTask = client.DeleteAsync(client.BaseAddress + "shopbridge_inventory_1/" + id);
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("_ListOfItems", "ShopBridge_Inventory");
            }
            return View("Index");
        }

        /* public void UploadImage(HttpPostedFileBase file)
         {
             using (var client = new HttpClient())
             {
                 using (var content = new MultipartFormDataContent())
                 {
                     byte[] temp = ConvertToByte(file);
                     //  MemoryStream target = new MemoryStream();
                     //  file.InputStream.CopyTo(target);
                     // byte[] Bytes = target.ToArray();

                     file.InputStream.Read(temp, 0, temp.Length);
                     var fileContent = new ByteArrayContent(temp);

                     fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                     content.Add(fileContent);

                     var requesturi = "https://localhost:44394/api/Values";
                     var result = client.PostAsync(requesturi, content).Result;
                     if (result.StatusCode == System.Net.HttpStatusCode.Created)
                     {
                         Console.WriteLine("Success");
                     }
                     else
                     {
                         Console.WriteLine("Fail");
                     }

                 }
             }

             //return View();
         }*/

        [HttpPost]
        public string UploadFiles(HttpPostedFileBase file)
        {
            using (var client = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    // foreach (var file in files)
                    {
                        byte[] imgData;
                        using (var reader = new BinaryReader(file.InputStream))
                        {
                            imgData = reader.ReadBytes(file.ContentLength);
                        }

                        formData.Add(new StreamContent(new MemoryStream(imgData)), "image", file.FileName);

                    }
                    var response = client.PostAsync("https://localhost:44394/api/Values", formData).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        return "falied";
                    }
                    return "success";
                }
            }

        }

       /* public byte[] ConvertToByte(HttpPostedFileBase file)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file.InputStream);
            imageByte = rdr.ReadBytes((int)file.ContentLength);
            return imageByte;
        }*/



        [HttpGet]
        public async Task GetImageThumbnail(string filename) //didn't use 
        {
            string filePath = "";
            if (!filename.Contains(".thumb"))
                filePath = HttpContext.Server.MapPath("~/Images/") + filename;
            else
                filePath = HttpContext.Server.MapPath("~/Thumbnails/") + filename;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/Values");
                var response = await client.GetAsync(client.BaseAddress + "?fileName=" + filename);

                using (var stream = await response.Content.ReadAsStreamAsync())
                {

                    var fileInfo = new FileInfo(filePath);
                    using (var fileStream = fileInfo.OpenWrite())
                    {
                        await stream.CopyToAsync(fileStream);
                    }
                }
            }
        }
       
    }

    public static class test
    {
        public static bool checkFileDoesntExists(string fname)
        {
            if (!File.Exists(fname))
            {
                return true;
            }
            else
                return false;
        }
    }
}